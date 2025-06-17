using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectManager_Server.Attribute;

/// <summary>
///     Custom Attribute ensure that a defined number of property cannot be null
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class NumberOfNullAuthorizedAttribute : ValidationAttribute
{
    private readonly string[] _otherProperties;
    private readonly int _minNumberOfProps;
    private readonly int _maxNumberOfProps;
    
    /// <summary>
    ///     ctor also ensure that provided range of number of null is valid 
    /// </summary>
    /// <param name="otherProperties">Name of property to check (in addition to the one where this property is set)</param>
    /// <param name="minNumberOfProps">Minimum number of null authorized</param>
    /// <param name="maxNumberOfProps">Maximum number of null authorized</param>
    public NumberOfNullAuthorizedAttribute(string[] otherProperties, int minNumberOfProps, int maxNumberOfProps)
    {
        _otherProperties = otherProperties;
        _minNumberOfProps = minNumberOfProps;
        _maxNumberOfProps = maxNumberOfProps;
        if ( _otherProperties.Length + 1 < _maxNumberOfProps )
            throw new InvalidOperationException("Number of provided property is above the maxNumber of authorized property");
        if ( _otherProperties.Length + 1 < _minNumberOfProps )
            throw new InvalidOperationException("Number of provided property is below the minNumber of authorized property");
    }
    
    /// <summary>
    ///     Validator function:
    ///     <ul>
    ///         <li>Retrieve every property of tested object</li>
    ///         <li>Ensure that not all property are null</li>
    ///     </ul>
    /// </summary>
    /// <param name="value">The object to test</param>
    /// <param name="validationContext">The context used to validation (hold object to validate)</param>
    /// <returns>True if object match condition or false</returns>
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var firstProp = RetrieveValueFromProp(validationContext, validationContext.DisplayName) == null ? 1 : 0 ;
        var numberOfNull = firstProp + _otherProperties.Select(prop => RetrieveValueFromProp(validationContext, prop)).Count(propValue => propValue == null);
        
        if(_minNumberOfProps <= numberOfNull && numberOfNull <= _maxNumberOfProps)
            return ValidationResult.Success!;
        
        return new ValidationResult("Number of null is outside the authorized range of values");
    }

    /// <summary>
    ///     Retrieve a Value of a Property 
    /// </summary>
    /// <param name="validationContext">Context Validation (used to retrieve object to test)</param>
    /// <param name="propName">Name of the property to retrieve the values for</param>
    /// <returns>Value of the property</returns>
    /// <exception cref="InvalidOperationException">If property with this name is not found</exception>
    private static object? RetrieveValueFromProp(ValidationContext validationContext, string propName)
    {
        var propertyInfo = validationContext.ObjectType.GetProperty(propName) ?? throw new InvalidOperationException($"Property {propName} not found");
        return propertyInfo.GetValue(validationContext.ObjectInstance);
    }
}