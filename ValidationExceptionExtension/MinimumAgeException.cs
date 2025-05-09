using System;

namespace Validation.ExceptonExtension
{
    /// <summary>
    /// Represents an exception that is thrown when a user's birth date is invalid or the minimum required age is not met.
    /// </summary>
    public class MinimumAgeException : Exception
    {
        /// <summary>
        /// Gets or sets the minimum age required for validation.
        /// </summary>
        public int MinimumAge { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumAgeException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MinimumAgeException(string message)
            : base(message) { }

        /// <summary>
        /// Validates the provided birth date against the specified minimum age.
        /// If the date is null, in the future, or does not meet the minimum age requirement, a <see cref="MinimumAgeException"/> is thrown.
        /// </summary>
        /// <param name="birthDate">The birth date to validate.</param>
        /// <param name="minimumAge">The minimum required age (default is 0).</param>
        /// <exception cref="MinimumAgeException">
        /// Thrown when the birth date is null, in the future, or the user does not meet the minimum age requirement.
        /// </exception>
        public static void ValidationThrow(DateTime? birthDate, int minimumAge = 0)
        {
            // Check if birth date is null
            if (!birthDate.HasValue)
                throw new MinimumAgeException("Birth date cannot be null.");

            // Check if birth date is a future date
            if (birthDate.Value > DateTime.Now)
                throw new MinimumAgeException($"Birth date cannot be in the future. Provided birth date: {birthDate.Value.ToShortDateString()}");

            // Check if the user is younger than the minimum required age
            // birthDate + minimumAge in years > today → means user hasn't reached the required age yet
            if (birthDate.Value.AddYears(minimumAge) > DateTime.Today)
                throw new MinimumAgeException($"User must be at least {minimumAge} years old.");
        }
    }
}
