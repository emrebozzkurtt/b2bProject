﻿using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.Title.Length)
                .NotEmpty()
                .GreaterThanOrEqualTo(2);
            RuleFor(category => category.SubCategories[0]).Empty();
        }
    }
}
