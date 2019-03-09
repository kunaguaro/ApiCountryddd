
using FluentValidation;
using System;
using System.Text.RegularExpressions;
using WebApi.Domain.Entities;

namespace WebApi.Service.Validators
{
    public class CountryValidator : AbstractValidator<Country>
    {
        public CountryValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .OnAnyFailure(x =>
                {
                    throw new ArgumentNullException("Can't found the object.");
                });

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Is necessary to inform the Name.")
                .MaximumLength(100).WithMessage("Name must be no longer than 100 characters.")
                .NotNull().WithMessage("Is necessary to inform the Name.");

            RuleFor(c => c.Population)
                .NotEmpty().WithMessage("Is necessary to inform the Population.")
                .Must(ValidatePopulation).WithMessage("Population must be greater than 0");

            RuleFor(c => c.Area)
              .NotEmpty().WithMessage("Is necessary to inform the Area.")
              .Must(ValidateArea).WithMessage("Area must be greater than 0");

            RuleFor(c => c.ISO3166)
                .NotEmpty().WithMessage("Is necessary to inform the ISO3166.")
                .Must(ValidateISO3166length).WithMessage("Population must have two or three characters")
                .Must(ValidateISO3166onlyCharacter).WithMessage("Population must have only characters");
        }


        private bool ValidatePopulation(int population)
        {

            if (population < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateArea(decimal area)
        {

            if (area < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateISO3166length(string ISO3166)
        {

            if (ISO3166.Length > 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateISO3166onlyCharacter(string ISO3166)
        {

            return Regex.IsMatch(ISO3166, @"^[a-zA-Z]+$");
           
        }
    }
}
