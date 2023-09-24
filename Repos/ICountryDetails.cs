namespace Example.Repos
{
    /// <summary>
    /// An immutable object of which the <c><see cref="Example.Repos.ICountry" /></c> interface is composed.
    /// </summary>
    public interface ICountryDetails
    {
        /// <summary>
        /// The operator identifier
        /// </summary>
        string Operator {get;}
        /// <summary>
        /// The code for the identifier of the operator
        /// </summary>
        string OperatorCode {get;}
    }
}