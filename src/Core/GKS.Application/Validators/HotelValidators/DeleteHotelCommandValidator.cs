using FluentValidation;
using GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Validators.HotelValidators
{
    public class DeleteHotelCommandValidator : AbstractValidator<DeleteHotelRequest>
    {
        public DeleteHotelCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Silinmek istenen kaydın bilgisini göndermeniz gerekmektedir.");
        }
    }
}
