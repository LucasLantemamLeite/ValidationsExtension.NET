using System;
using System.ComponentModel.DataAnnotations;

namespace Validation.ViewModel
{
    /// <summary>
    /// A custom validation attribute used to validate that a user's birth date is not in the future
    /// and ensures the user is at least the specified minimum age.
    /// </summary>
    public class BirthDateLessThanToday : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the minimum age for the user. Default value is 0, which means no minimum age requirement.
        /// </summary>
        public int MinimumAge { get; set; } = 0;

        /// <summary>
        /// Validates if the provided birth date is less than today's date and if the user is at least the specified minimum age.
        /// </summary>
        /// <param name="value">The value to validate. It should be a <see cref="DateTime"/> representing the user's birth date.</param>
        /// <param name="validationContext">The context in which the validation is being performed. This parameter can be used to access metadata about the property being validated.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the validation succeeded or failed. 
        /// If the validation fails, the result will contain an error message.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Check if the value is a valid DateTime
            if (value is DateTime BirthDate)
            {
                // Check if the user is old enough based on the specified minimum age
                if (BirthDate.AddYears(MinimumAge) > DateTime.Today)
                    // If the user is not old enough, return a validation error
                    return new ValidationResult(ErrorMessage ?? $"User must be at least {MinimumAge} years old.");

                // Check if the birth date is in the future
                if (BirthDate > DateTime.Today)
                    // If the birth date is in the future, return a validation error
                    return new ValidationResult(ErrorMessage ?? $"The BirthDate: {BirthDate} cannot be greater than the current date");

                // If everything is valid, return success
                return ValidationResult.Success;
            }

            // If the value is not a valid DateTime, return a validation error
            return new ValidationResult(ErrorMessage ?? "BirthDate is not a valid DateTime");
        }
    }
}
