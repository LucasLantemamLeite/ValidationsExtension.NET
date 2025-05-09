using System;
using System.ComponentModel.DataAnnotations;

namespace Validation.ViewModel
{
    /// <summary>
    /// A custom validation attribute used to ensure a user's birth date results in a minimum required age.
    /// It validates that the birth date is not in the future and that the user meets the specified minimum age.
    /// </summary>
    public class MinimumAgeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the minimum required age for the user.
        /// Default is 0, meaning no minimum age restriction.
        /// </summary>
        public int MinimumAge { get; set; } = 0;

        /// <summary>
        /// Validates the provided birth date to ensure it meets the specified minimum age requirement,
        /// and that the birth date is not in the future.
        /// </summary>
        /// <param name="value">The value to validate. Should be a <see cref="DateTime"/> representing the user's birth date.</param>
        /// <param name="validationContext">The validation context that contains metadata about the validation operation.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the value passed the validation.
        /// If validation fails, returns an error message; otherwise, returns <see cref="ValidationResult.Success"/>.
        /// </returns>
        /// <remarks>
        /// This method checks if the birth date is in the future (which is invalid),
        /// and verifies that the user is at least the specified minimum age.
        /// </remarks>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // If the value is null or empty, it is considered valid (not applying any validation in that case)
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            // Ensure the MinimumAge is not less than zero
            if (MinimumAge < 0)
                return new ValidationResult(ErrorMessage ?? $"The MinimumAge cannot be less than zero. Actual value is: {MinimumAge}.");

            // Check if the provided value is a valid DateTime object
            if (value is DateTime BirthDate)
            {
                // Validate that the user is old enough and the birth date is not in the future
                if (BirthDate.AddYears(MinimumAge) > DateTime.Today)
                    return new ValidationResult(ErrorMessage ?? $"User must be at least {MinimumAge} years old.");

                // Return success if the birth date is valid and the user meets the minimum age requirement
                return ValidationResult.Success;
            }

            // If the value is not a valid DateTime, return an error
            return new ValidationResult(ErrorMessage ?? "BirthDate is not a valid DateTime.");
        }
    }
}