using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook
{
    public class DeleteModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IRepo<AddressBookEntry> _repo;

        public DeleteModel(IRepo<AddressBookEntry> repo, IMediator mediator)
        {
            _repo = repo;
            _mediator = mediator;
        }

        [BindProperty]
        public DeleteAddressRequest DeleteAddressRequest { get; set; }

        public void OnGet(Guid id)
        {

            var specification = new EntryByIdSpecification(id);
            var entries = _repo.Find(specification);
            var entry = entries.FirstOrDefault();

            if (entry != null)
            {
                DeleteAddressRequest = new DeleteAddressRequest
                {
                    Id = entry.Id,
                    Line1 = entry.Line1,
                    Line2 = entry.Line2,
                    City = entry.City,
                    State = entry.State,
                    PostalCode = entry.PostalCode
                };
            }

            if (entry == null)
            {
                throw new InvalidOperationException("Entry not found for deletion.");
            }
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var deleteRequest = new DeleteAddressRequest
            {
                Id = DeleteAddressRequest.Id,
            };

            await _mediator.Send(deleteRequest);

            return RedirectToPage("Index");
        }
    }
}
