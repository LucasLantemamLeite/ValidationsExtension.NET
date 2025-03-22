using System;
using System.ComponentModel.DataAnnotations;

namespace Validation.ViewModel;

/// <summary>
/// A class used to validate if the birth date is less than today's date.
/// </summary>
public class BirthDateLessThanToday : ValidationAttribute
{

    /// <summary>
    /// Validates if the provided birth date is less than today's date.
    /// </summary>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

        if (value is DateTime BirthDate)
        {

            if (BirthDate < DateTime.Today)
                return ValidationResult.Success;

            return new ValidationResult(ErrorMessage ?? $"The BirthDate: {BirthDate} cannot be greater than the current date");

        }
        return new ValidationResult(ErrorMessage ?? "BirthDate is not a valid DateTime");

    }

}
