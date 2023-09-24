namespace Example.Repos
{
    public interface ICustomerRepository
    {

        bool SaveChanges();

        //GETs
        /// <summary>
        /// Returns all the customers in the database
        /// </summary>
        /// <param name="rangePerInterval">The number of entries per query</param>
        /// <returns>A list of all the customers in the data source</returns>
        Task<IEnumerable<ICustomer>> GetCustomers(int interval, int rangePerInterval);
        /// <summary>
        /// Gets all the customers that were registered for the given country, specifying the number of results to be returned per call.
        /// </summary>
        /// <param name="ctry">The country where all the customers will be looked up</param>
        /// <param name="rangePerInterval">The max number of entries to be returned</param>
        /// <returns>A list of all the customers in the given country</returns>
        Task<IEnumerable<ICustomer>> GetCustomers(ICountry ctry, int interval, int rangePerInterval);
        /// <summary>
        /// Gets a single customer with the given number or <c>null</c> if non exists with the specified number
        /// </summary>
        /// <param name="phone">The number of the customer to be returned</param>
        /// <returns>The owner of the given phone number or <c>null</c> if number does not exist</returns>
        Task<object> GetCustomer(string? phone);

        //POSTs
        Task<bool> AddCountry(ICountry @new);
        // Task<bool> AddCustomers(ICustomer[] customers);

        //UPDATEs
        // Task<bool> SetNumber(string old);

        //DELETEs
        // Task<bool> RemoveCustomer(string nmber);
    }
}