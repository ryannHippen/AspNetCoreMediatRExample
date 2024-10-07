using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressHandler 
    : IRequestHandler<DeleteAddressRequest, Guid>
{
    private readonly IRepo<AddressBookEntry> _repo;

    public DeleteAddressHandler(IRepo<AddressBookEntry> repo)
    {
        _repo = repo;
    }

    public async Task<Guid> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        var specification = new EntryByIdSpecification(request.Id);
        var entries = _repo.Find(specification);
        var entry = entries.FirstOrDefault();
        
        if (entry == null)
        {
            throw new InvalidOperationException($"No address book entry found with ID {request.Id}");
        }
        _repo.Remove(entry);
        return await Task.FromResult(entry.Id);
    }
}
