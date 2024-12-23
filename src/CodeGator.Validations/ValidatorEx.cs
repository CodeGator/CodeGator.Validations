
namespace System.ComponentModel.DataAnnotations;

/// <summary>
/// This class provides methods we wish the <see cref="Validator"/> class
/// had provided.
/// </summary>
public static class ValidatorEx
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    ///  This method tests whether the given object instance is valid.
    /// </summary>
    /// <remarks>
    ///     This method evaluates all <see cref="ValidationAttribute" />s attached to the object instance's type.  It also
    ///     checks to ensure all properties marked with <see cref="RequiredAttribute" /> are set.  If
    ///     <paramref name="validateAllProperties" />
    ///     is <c>true</c>, this method will also evaluate the <see cref="ValidationAttribute" />s for all the immediate
    ///     properties
    ///     of this object.  This process is recursive if the <paramref name="recursive"/> property is set to <c>true</c>.
    ///     <para>
    ///         If <paramref name="validationResults" /> is null, then execution will abort upon the first validation
    ///         failure.  If <paramref name="validationResults" /> is non-null, then all validation attributes will be
    ///         evaluated.
    ///     </para>
    ///     <para>
    ///         For any given property, if it has a <see cref="RequiredAttribute" /> that fails validation, no other validators
    ///         will be evaluated for that property.
    ///     </para>
    /// </remarks>
    /// <param name="instance">The object instance to test.  It cannot be null.</param>
    /// <param name="validationContext">Describes the object to validate and provides services and context for the validators.</param>
    /// <param name="validationResults">Optional collection to receive <see cref="ValidationResult" />s for the failures.</param>
    /// <param name="validateAllProperties">
    ///     If <c>true</c>, also evaluates all properties of the object (this process is not
    ///     recursive over properties of the properties).
    /// </param>
    /// <param name="recursive">Used to validate decorated properties on child objects.</param>
    /// <returns><c>true</c> if the object is valid, <c>false</c> if any validation errors are encountered.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="instance" /> is null.</exception>
    /// <exception cref="ArgumentException">
    ///     When <paramref name="instance" /> doesn't match the
    ///     <see cref="ValidationContext.ObjectInstance" />on <paramref name="validationContext" />.
    /// </exception>
    public static bool TryValidateObject(
        object instance, 
        ValidationContext validationContext,
        ICollection<ValidationResult>? validationResults, 
        bool validateAllProperties,
        bool recursive
        )
    {
        var result = Validator.TryValidateObject( 
            instance, 
            validationContext, 
            validationResults, 
            validateAllProperties 
            );

        if (result)
        {
            if (recursive)
            {
                var props = instance.GetType().GetProperties(
                    BindingFlags.Instance | BindingFlags.Public
                    ).Where(x =>
                        x.CanRead &&
                        x.CanWrite &&
                        x.PropertyType.IsClass &&
                        x.PropertyType != typeof(string) &&
                        x.PropertyType != typeof(DateTime) &&
                        x.PropertyType != typeof(DateTimeOffset) &&
                        x.PropertyType != typeof(TimeSpan) &&
                        x.PropertyType != typeof(decimal) &&
                        x.PropertyType != typeof(Uri) &&
                        x.PropertyType != typeof(Guid) &&
                        x.PropertyType != typeof(Nullable)
                        );

                foreach (var prop in props)
                {
                    var tempValidationResults = new List<ValidationResult>();
                    var getGetMethod = prop.GetGetMethod();

                    if (getGetMethod is null)
                    {
                        continue;    
                    }

                    if (getGetMethod.GetParameters().Length == 0)
                    {
                        continue;
                    }

                    var propValue = getGetMethod.Invoke(instance, null);

                    if (propValue is not null) 
                    {
                        if (null != getGetMethod.ReturnType.GetInterface(
                            nameof(System.Collections.IEnumerable)
                            ))
                        {
                            foreach (var obj in ((propValue as System.Collections.IEnumerable) ?? 
                                Array.Empty<object>()))
                            {
                                var tempValidationContext = new ValidationContext(
                                    obj
                                    );

                                result &= ValidatorEx.TryValidateObject(
                                    obj,
                                    tempValidationContext,
                                    tempValidationResults,
                                    validateAllProperties,
                                    recursive
                                    );
                            }
                        }
                        else
                        {
                            var tempValidationContext = new ValidationContext(
                                propValue
                                );
                            
                            result = ValidatorEx.TryValidateObject(
                                propValue,
                                tempValidationContext,
                                tempValidationResults,
                                validateAllProperties,
                                recursive
                                );
                        }
                        
                        if (!result)
                        {
                            foreach (var res in tempValidationResults)
                            {
                                validationResults?.Add(
                                    new ValidationResult(
                                        $"'{prop.Name}' -> '{res.ErrorMessage}'",
                                        res.MemberNames
                                        )                                    
                                    );
                            }
                            break;
                        }
                    }
                    else
                    {
                        if (prop.CustomAttributes.Any(
                            x => typeof(RequiredAttribute).IsAssignableFrom(x.AttributeType)
                            ))
                        {
                            validationResults?.Add(
                                new ValidationResult(
                                    string.Format(
                                        CultureInfo.CurrentCulture,
                                        "'{0}' -> 'The property is null but is marked as required!'",
                                        prop.Name
                                        )
                                    )
                                );

                            result = false;
                            break;
                        }
                    }
                }
            }
        }
        return result;
    }

    #endregion
}
