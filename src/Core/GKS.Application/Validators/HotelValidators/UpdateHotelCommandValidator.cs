using FluentValidation;
using GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Validators.HotelValidators
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelRequest>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Cnnot Be Null");
        }
    }
}
