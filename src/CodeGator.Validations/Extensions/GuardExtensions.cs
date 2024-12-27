
#pragma warning disable IDE0130
namespace System;
#pragma warning restore IDE0130

/// <summary>
/// This class utility contains extension methods related to the <see cref="Guard"/>
/// type.
/// </summary>
public static partial class GuardExtensions
{
    // *******************************************************************
    // Public methods.
    // *******************************************************************

    #region Public methods

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains an object that fails one or more property validations.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> argument.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an object that fails
    /// one or more property validations.</exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidObject(Guard, object, string, string, string, int)"/>
    /// method.
    /// <code>
    /// using System.ComponentModel.DataAnnotations;
    /// 
    /// class MyModel
    /// {
    ///    [Required]
    ///    public string A { get; set; }
    /// }
    /// 
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = new MyModel() { A = "" };
    /// 
    ///         // throws an exception, since the A property is missing.
    ///         Guard.Instance().ThrowIfInvalidObject(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfInvalidObject(
        this Guard guard,
        object argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        var validationResults = new List<ValidationResult>();

        if (!ValidatorEx.TryValidateObject(
            argValue,
            new ValidationContext(argValue),
            validationResults,
            true,
            true
            ))
        {
            var propertyNames = string.Join(
                ",",
                validationResults.Select(x => 
                    string.Join(",", x.MemberNames)
                    )
                );

            var errorMessages = string.Join(
                ",",
                validationResults.Select(x => x.ErrorMessage)
                );

            var exception = new ArgumentException(
                paramName: argName,
                message: "Validation error!"
                );

            exception.Data["errors"] = errorMessages;
            exception.Data["properties"] = propertyNames;
            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains a null reference.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> argument.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a null value.</exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNull(Guard, object, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         object arg = null;
    /// 
    ///         // throws an exception, since the argument is null.
    ///         Guard.Instance().ThrowIfNull(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNull(
        this Guard guard,
        object? argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == null)
        {
            var exception = new ArgumentNullException(
                paramName: argName,
                message: "The argument should not be null!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains a null reference or an empty string.
    /// </summary>
    /// <param name="guard">The guard instance to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> argument.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an empty or null 
    /// string value.</exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNullOrEmpty(Guard, string, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = "";
    /// 
    ///         // throws an exception, since the argument is empty.
    ///         Guard.Instance().ThrowIfNullOrEmpty(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNullOrEmpty(
        this Guard guard,
        string? argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (string.IsNullOrEmpty(argValue))
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument should not be null or empty!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains a value that is less than zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThanZero(Guard, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = -1;
    /// 
    ///         // throws an exception, since the argument is invalid.
    ///         Guard.Instance().ThrowIfLessThanZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThanZero(
        this Guard guard,
        int argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue < 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument should not contain a value < 0!"
            );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains a value that is less than zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThanZero(Guard, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = -1L;
    /// 
    ///         // throws an exception, since the argument is invalid.
    ///         Guard.Instance().ThrowIfLessThanZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThanZero(
        this Guard guard,
        long argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue < 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument should not contain a value < 0!"
            );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }

        return guard;
    }

    // *******************************************************************


    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains an empty time span value.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an empty time 
    /// span value.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfZero(Guard, TimeSpan, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = TimeSpan.Zero;
    /// 
    ///         // throws an exception, since the argument is invalid.
    ///         Guard.Instance().ThrowIfZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfZero(
        this Guard guard,
        TimeSpan argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == TimeSpan.Zero)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument should not contain a value < 0!"
            );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains an invalid DateTime value.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an invalid value.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfInvalidDateTime(Guard, DateTime, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = new DateTime();
    /// 
    ///         // throws an exception, since the argument is invalid.
    ///         Guard.Instance().ThrowIfInvalidDateTime(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfInvalidDateTime(
        this Guard guard,
        DateTime argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!DateTime.TryParse($"{argValue}", out _))
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument contains an invalid date-time value!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains a FALSE value.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a FALSE value.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfFalse(Guard, bool, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = false;
    /// 
    ///         // throws an exception, since the argument is false.
    ///         Guard.Instance().ThrowIfFalse(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfFalse(
        this Guard guard,
        bool argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!argValue)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument must not be FALSE!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains a TRUE value.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a FALSE value.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfTrue(Guard, bool, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = true;
    /// 
    ///         // throws an exception, since the argument is true.
    ///         Guard.Instance().ThrowIfTrue(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfTrue(
        this Guard guard,
        bool argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument must not be TRUE!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument contains a value that is less than or equal to zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than or equal to zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThanOrEqualZero(Guard, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is less than or equal to zero.
    ///         Guard.Instance().ThrowIfLessThanOrEqualZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThanOrEqualZero(
        this Guard guard,
        int argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue <= 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument must contain a value > 0!"                
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the '<paramref name="argValue"/>
    /// argument contains a value that is less than or equal to zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than or equal to zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThanOrEqualZero(Guard, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is less than or equal to zero.
    ///         Guard.Instance().ThrowIfLessThanOrEqualZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThanOrEqualZero(
        this Guard guard,
        long argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue <= 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument must contain a value > 0!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an <see cref="ArgumentException"/> if the value
    /// of the <paramref name="argValue"/> parameter has a value less than
    /// or equal to zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> argument.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than, or equal to zero.</exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThanOrEqualZero(Guard, TimeSpan, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = Timespan.Zero;
    /// 
    ///         // throws an exception, since the argument is less than or equal to zero.
    ///         Guard.Instance().ThrowIfLessThanOrEqualZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThanOrEqualZero(
        this Guard guard,
        TimeSpan argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue <= TimeSpan.Zero)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: "The argument must contain a value > 0!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method will throw an exception if the <paramref name="argValue"/>
    /// argument is less than the <paramref name="amount"/> argument.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to be validated.</param>
    /// <param name="amount">The amount to be used for validation.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than the <paramref name="amount"/> argument.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThan(Guard, int, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 1;
    /// 
    ///         // make the value to compare it against.
    ///         var amount = 2; 
    /// 
    ///         // throws an exception, since the argument is less than 2.
    ///         Guard.Instance().ThrowIfLessThan(arg, amount, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThan(
        this Guard guard,
        int argValue,
        int amount,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue < amount)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be less than {amount}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method will throw an exception if the <paramref name="argValue"/>
    /// argument is less than the <paramref name="amount"/> argument.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to be validated.</param>
    /// <param name="amount">The amount to be used for validation.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than the <paramref name="amount"/> argument.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThan(Guard, long, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 1;
    /// 
    ///         // make the value to compare it against.
    ///         var amount = 2; 
    /// 
    ///         // throws an exception, since the argument is less than 2.
    ///         Guard.Instance().ThrowIfLessThan(arg, amount, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThan(
        this Guard guard,
        long argValue,
        long amount,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue < amount)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be less than {amount}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method will throw an exception if the <paramref name="argValue"/>
    /// argument is less than the <paramref name="amount"/> argument.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to be validated.</param>
    /// <param name="amount">The amount to be used for validation.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than the <paramref name="amount"/> argument.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfLessThan(Guard, TimeSpan, TimeSpan, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = new timeSpan(1, 1, 1);
    /// 
    ///         // make the value to compare it against.
    ///         var amount = new TimeSpan(2, 2, 2); 
    /// 
    ///         // throws an exception, since the argument is less than 2:2:2.
    ///         Guard.Instance().ThrowIfLessThan(arg, amount, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfLessThan(
        this Guard guard,
        TimeSpan argValue,
        TimeSpan amount,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue < amount)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be less than {amount}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method will throw an exception if the <paramref name="argValue"/> 
    /// argument is greater than the <paramref name="amount"/> argument. 
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to be validated.</param>
    /// <param name="amount">The amount to be used for validation.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// greater than the <paramref name="amount"/> argument.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfGreaterThan(Guard, int, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 2;
    /// 
    ///         // make the value to compare it against.
    ///         var amount = 1; 
    /// 
    ///         // throws an exception, since the argument is greater than 1.
    ///         Guard.Instance().ThrowIfGreaterThan(arg, amount, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfGreaterThan(
        this Guard guard,
        int argValue,
        int amount,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue > amount)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be greater than {amount}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method will throw an exception if the <paramref name="argValue"/> 
    /// argument is greater than the <paramref name="amount"/> argument. 
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to be validated.</param>
    /// <param name="amount">The amount to be used for validation.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// greater than the <paramref name="amount"/> argument.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfGreaterThan(Guard, long, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 2;
    /// 
    ///         // make the value to compare it against.
    ///         var amount = 1; 
    /// 
    ///         // throws an exception, since the argument is greater than 1.
    ///         Guard.Instance().ThrowIfGreaterThan(arg, amount, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfGreaterThan(
        this Guard guard,
        long argValue,
        long amount,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue > amount)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be greater than {amount}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method will throw an exception if the <paramref name="argValue"/> 
    /// argument is greater than the <paramref name="amount"/> argument. 
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to be validated.</param>
    /// <param name="amount">The amount to be used for validation.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// greater than the <paramref name="amount"/> argument.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfGreaterThan(Guard, TimeSpan, TimeSpan, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = new TimeSpan(1, 1, 1);
    /// 
    ///         // make the value to compare it against.
    ///         var amount = new TimeSpan(1, 1, 1); 
    /// 
    ///         // throws an exception, since the argument is greater than 1.
    ///         Guard.Instance().ThrowIfGreaterThan(arg, amount, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfGreaterThan(
        this Guard guard,
        TimeSpan argValue,
        TimeSpan amount,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue > amount)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be greater than {amount}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains a zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfZero(Guard, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is zero.
    ///         Guard.Instance().ThrowIfZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfZero(
        this Guard guard,
        int argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be zero!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains a zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfZero(Guard, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is zero.
    ///         Guard.Instance().ThrowIfZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfZero(
        this Guard guard,
        long argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be zero!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains something other than zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// something other than zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfZero(Guard, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 1;
    /// 
    ///         // throws an exception, since the argument is not zero.
    ///         Guard.Instance().ThrowIfNotZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotZero(
        this Guard guard,
        long argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue != 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not be zero!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains something other than zero.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// something other than zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotZero(Guard, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 1;
    /// 
    ///         // throws an exception, since the argument is not zero.
    ///         Guard.Instance().ThrowIfNotZero(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotZero(
        this Guard guard,
        int argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue != 0)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must be zero!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/>
    /// argument does not contain a null reference or an empty string.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument does not contain an empty 
    /// or null string value.</exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotNullOrEmpty(Guard, string, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = "testing, 1, 2, 3";
    /// 
    ///         // throws an exception, since the string not null or empty.
    ///         Guard.Instance().ThrowIfNotNullOrEmpty(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotNullOrEmpty(
        this Guard guard,
        string? argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!string.IsNullOrEmpty(argValue))
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must be null or empty!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains an empty GUID instance.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an empty GUID.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfEmptyGuid(Guard, Guid, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = Guid.Empty;
    /// 
    ///         // throws an exception, since the GUID is empty.
    ///         Guard.Instance().ThrowIfEmptyGuid(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfEmptyGuid(
        this Guard guard,
        Guid argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == Guid.Empty)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument may not contain an empty GUID!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument contains an empty TimeSpan instance.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains an empty GUID.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfEmptyTimeSpan(Guard, TimeSpan, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = TimeSpan.Zero;
    /// 
    ///         // throws an exception, since the time span is empty.
    ///         Guard.Instance().ThrowIfEmptyTimeSpan(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfEmptyTimeSpan(
        this Guard guard,
        TimeSpan argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == TimeSpan.Zero)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument may not contain an empty time span!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument does not contain an empty GUID instance.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument does not contain an 
    /// empty GUID.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotEmptyGuid(Guard, Guid, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = Guid.Parse("some guid value");
    /// 
    ///         // throws an exception, since the GUID is not empty.
    ///         Guard.Instance().ThrowIfNotEmptyGuid(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotEmptyGuid(
        this Guard guard,
        Guid argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue != Guid.Empty)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument should contain an empty GUID!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // ******************************************************************

    /// <summary>
    /// This method throws an exception if the <paramref name="argValue"/> 
    /// argument does not contain an empty TimeSpan instance.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument does not contain an 
    /// empty GUID.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotEmptyTimeSpan(Guard, TimeSpan, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = TimeSpan.Parse("some time span value");
    /// 
    ///         // throws an exception, since the time span is not empty.
    ///         Guard.Instance().ThrowIfNotEmptyTimeSpan(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotEmptyTimeSpan(
        this Guard guard,
        TimeSpan argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue != TimeSpan.Zero)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument should contain an empty time span!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the "argValue" argument 
    /// contains a malformed URI.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a malformed uri.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfMalformedUri(Guard, string, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = "*";
    /// 
    ///         // throws an exception, since the URI is malformed.
    ///         Guard.Instance().ThrowIfMalformedUri(arg, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfMalformedUri(
        this Guard guard,
        string argValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (!Uri.IsWellFormedUriString(argValue, UriKind.RelativeOrAbsolute))
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument is not a well formed URI!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the '<paramref name="argValue"/>
    /// argument contains a value that is equal to the <paramref name="compareValue"/>
    /// parameter.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="compareValue">The comparison value.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than or equal to zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfEqual(Guard, long, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is equal.
    ///         Guard.Instance().ThrowIfEqual(arg, 0, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfEqual(
        this Guard guard,
        long argValue,
        long compareValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == compareValue)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument may not equal {compareValue}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the '<paramref name="argValue"/>
    /// argument contains a value that is equal to the <paramref name="compareValue"/>
    /// parameter.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="compareValue">The comparison value.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than or equal to zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfEqual(Guard, int, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is equal.
    ///         Guard.Instance().ThrowIfEqual(arg, 0, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfEqual(
        this Guard guard,
        int argValue,
        int compareValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue == compareValue)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must equal {compareValue}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the '<paramref name="argValue"/>
    /// argument contains a value that is not equal to the <paramref name="compareValue"/>
    /// parameter.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="compareValue">The comparison value.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than or equal to zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotEqual(Guard, long, long, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is not equal.
    ///         Guard.Instance().ThrowIfNotEqual(arg, 1, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotEqual(
        this Guard guard,
        long argValue,
        long compareValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue != compareValue)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must not equal {compareValue}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    // *******************************************************************

    /// <summary>
    /// This method throws an exception if the '<paramref name="argValue"/>
    /// argument contains a value that is not equal to the <paramref name="compareValue"/>
    /// parameter.
    /// </summary>
    /// <param name="guard">The guard to use for the operation.</param>
    /// <param name="argValue">The argument to test.</param>
    /// <param name="compareValue">The comparison value.</param>
    /// <param name="argName">The name of the argument.</param>
    /// <param name="memberName">Not used. Supplied by the compiler.</param>
    /// <param name="sourceFilePath">Not used. Supplied by the compiler.</param>
    /// <param name="sourceLineNumber">Not used. Supplied by the compiler.</param>
    /// <returns>The <paramref name="guard"/> value.</returns>
    /// <exception cref="ArgumentException">This exception is thrown when
    /// the <paramref name="argValue"/> argument contains a value that is
    /// less than or equal to zero.
    /// </exception>
    /// <example>
    /// This example shows how to call the <see cref="GuardExtensions.ThrowIfNotEqual(Guard, int, int, string, string, string, int)"/>
    /// method.
    /// <code>
    /// class TestClass
    /// {
    ///     static void Main()
    ///     {
    ///         // make an invalid argument.
    ///         var arg = 0;
    /// 
    ///         // throws an exception, since the argument is not equal.
    ///         Guard.Instance().ThrowIfNotEqual(arg, 1, nameof(arg));
    ///     }
    /// }
    /// </code>
    /// </example>
    public static Guard ThrowIfNotEqual(
        this Guard guard,
        int argValue,
        int compareValue,
        string argName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
        )
    {
        if (argValue != compareValue)
        {
            var exception = new ArgumentException(
                paramName: argName,
                message: $"The argument must equal {compareValue}!"
                );

            exception.Data["memberName"] = memberName;
            exception.Data["sourceLineNumber"] = sourceLineNumber;
            exception.Data["sourceFilePath"] = sourceFilePath;

            throw exception;
        }
        return guard;
    }

    #endregion
}
