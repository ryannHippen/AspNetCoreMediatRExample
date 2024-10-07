using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RazorPagesLab.Pages.AddressBook;

public class DeleteAddressRequest
    : IRequest<Guid>
{
    public Guid Id { get; set; }

    [DisplayName("Address Line 1")]
    public string Line1 { get; set; }

    [DisplayName("Address Line 2")]
    public string Line2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    [DisplayName("Postal Code")]

    public string PostalCode { get; set; }
}