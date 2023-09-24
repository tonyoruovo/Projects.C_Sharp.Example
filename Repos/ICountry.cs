namespace Example.Repos
{
    /// <summary>
    /// The country data which the <c><see cref="Example.Repos.ICustomer" /></c> is composed of.
    /// </summary>
    public interface ICountry
    {
        /// <summary>
        /// The country's generic name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The numeric code as a string. This is to preserve leading zeros in certain codes
        /// </summary>
        string CountryCode { get; }
        /// <summary>
        /// The ISO of the given country.
        /// </summary>
        string CountryIso { get; }
        /// <summary>
        /// The Telecom companies registered for this country.
        /// </summary>
        IEnumerable<ICountryDetails> CountryDetails { get; }
    }
}