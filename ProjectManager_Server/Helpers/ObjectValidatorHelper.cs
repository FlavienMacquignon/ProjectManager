using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager_Server.Helpers;

/// <summary>
///     Validator Helper
///     A static class to quickly validate objects
/// </summary>
public static class ObjectValidatorHelper
{
    /// <summary>
    ///     Validate an the provided object or throw an exception
    /// </summary>
    /// <param name="toValidate">The object to validate</param>
    /// <exception cref="ValidationException">If validation of the object fail</exception>
    public static void Validate(object? toValidate)
    {
        var validationResults = new List<ValidationResult>();
        if (toValidate != null)
        {
            var validationContext = new ValidationContext(toValidate);

            bool isValid = Validator.TryValidateObject(
                toValidate,
                validationContext,
                validationResults,
                validateAllProperties: true
            );
            if ( !isValid )
                throw new ValidationException(); // TODO Aggregate ValidationResults
        }
    }
}