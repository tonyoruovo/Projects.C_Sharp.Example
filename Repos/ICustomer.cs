namespace Example.Repos
{
    /// <summary>
    /// A Customer is an immutable object that was populated by the controller or that bears data submitted to the db from a POST call.
    /// </summary>
    public interface ICustomer
    {
        /// <summary>
        /// The phone number of the customer. This is nullable because this is the primary key in the associated model
        /// </summary>
        string? Number { get; }
        /// <summary>
        /// The object containing details for the customer's country
        /// </summary>
        ICountry Country { get; }
    }
}