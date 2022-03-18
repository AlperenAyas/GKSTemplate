using FluentValidation;
using GKS.Application.Features.CQRS.Requests.CommandRequests;
using GKS.Application.Features.CQRS.Requests.CommandRequests.HotelCommandRequests;
using GKS.Application.Features.CQRS.Response.CommandResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Application.Validators.HotelValidators
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelRequest>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Cnnot Be Null");
        }
    }
}
