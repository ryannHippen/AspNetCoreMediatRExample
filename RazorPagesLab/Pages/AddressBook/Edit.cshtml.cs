using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		var specification = new EntryByIdSpecification(id);
		var entries = _repo.Find(specification);
		var entry = entries.FirstOrDefault();

		if (entry != null)
		{
			UpdateAddressRequest = new UpdateAddressRequest
			{
				Id = entry.Id,
				Line1 = entry.Line1,
				Line2 = entry.Line2,
				City = entry.City,
				State = entry.State,
				PostalCode = entry.PostalCode
			};
		}
	}

	public async Task<ActionResult> OnPost()
	{
		if (!ModelState.IsValid)
		{
			return Page();
		}

		var updateRequest = new UpdateAddressRequest
		{
			Id = UpdateAddressRequest.Id,
			Line1 = UpdateAddressRequest.Line1,
			Line2 = UpdateAddressRequest.Line2,
			City = UpdateAddressRequest.City,
			State = UpdateAddressRequest.State,
			PostalCode = UpdateAddressRequest.PostalCode
		};

		await _mediator.Send(updateRequest);

		return RedirectToPage("Index");
	}
}