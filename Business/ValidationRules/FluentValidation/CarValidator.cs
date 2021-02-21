using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BrandId).NotEmpty().GreaterThan(0).WithMessage("Geçersiz değer girdiniz");
            RuleFor(c => c.ColorId).NotEmpty().GreaterThan(0).WithMessage("Geçersiz değer girdiniz");
            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(0).WithMessage("Geçersiz fiyat girdiniz");
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.ModelYear).GreaterThan(0).WithMessage("Geçersiz yıl girdiniz");
        }
    }
}
