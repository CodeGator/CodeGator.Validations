
namespace System;

/// <summary>
/// This class is a singleton implementation of the <see cref="Guard"/>
/// interface.
/// </summary>
public sealed class Guard 
{
    // *******************************************************************
    // Fields.
    // *******************************************************************

    #region Fields

    /// <summary>
    /// This field contains the singleton instance.
    /// </summary>
    private static Guard? _instance;

    #endregion

    // *******************************************************************
    // Properties.
    // *******************************************************************

    #region Properties

    /// <summary>
    /// This method creates a new singleton instance of the 
    /// </summary>
    /// <returns>The singleton instance.</returns>
    [DebuggerStepThrough]
    public static Guard Instance()
    {
        _instance ??= new Guard();
        return _instance;
    }

    #endregion

    // *******************************************************************
    // Constructors.
    // *******************************************************************

    #region Constructors

    /// <summary>
    /// This constructor creates a new instance of the <see cref="Guard"/>
    /// class.
    /// </summary>
    [DebuggerStepThrough]
    private Guard() { }

    #endregion
}
