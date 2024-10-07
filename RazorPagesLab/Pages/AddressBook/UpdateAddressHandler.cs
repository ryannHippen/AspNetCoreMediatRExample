using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class UpdateAddressHandler
	: IRequestHandler<UpdateAddressRequest, Guid>
{
	private readonly IRepo<AddressBookEntry> _repo;

	public UpdateAddressHandler(IRepo<AddressBookEntry> repo)
	{
		_repo = repo;
	}

	public async Task<Guid> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
	{
        var specification = new EntryByIdSpecification(request.Id);
        var entries = _repo.Find(specification);
        var entry = entries.FirstOrDefault();

        if (entry == null)
        {
            throw new InvalidOperationException($"No address book entry found with ID {request.Id}");
        }

        entry.Update(request.Line1, request.Line2, request.City, request.State, request.PostalCode);
        _repo.Update(entry);

        return await Task.FromResult(entry.Id);
    }
}