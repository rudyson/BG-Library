using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.NET.Library.Models.Dto.Library;
using FluentValidation;

namespace BG.NET.Library.BusinessLogicLayer.Validators.Dto
{
    public class AuthorDtoUpdateValidator : AbstractValidator<AuthorDtoUpdate>
    {
        public AuthorDtoUpdateValidator() {
            RuleFor(x => x.Birthday).GreaterThan(DateOnly.MinValue);
        }
    }
}
