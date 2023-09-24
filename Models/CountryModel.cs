
using System.ComponentModel.DataAnnotations;
using static Example.Repos.Utils;

namespace Example.Models
{
    /// <summary>
    /// A Model that was populated by the data source (db or file system) meant to be used in the server only for CRUD operations
    /// and represents the country against which a phone number is registered.
    /// </summary>
    public class CountryModel : IComparable<CountryModel>
    {
        /// <summary>
        /// The primary key
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// The country code
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// The country name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The 2 or 3 letter ISO code for the country.
        /// </summary>
        public string CountryIso { get; set; }
        /// <summary>
        /// The Telecom companies registered for this country.
        /// </summary>
        public IEnumerable<CountryDetailsModel> CountryDetails { get; set; }

        /// <summary>
        /// The uniquely generated integer which enables this object to be used in a <c><see cref="System.Collections.Hashtable" /></c>
        /// </summary>
        /// <returns>The hash code for this model</returns>
        public override int GetHashCode()
        {
            return (Id << 0xffff) | CountryCode.GetHashCode();
        }
        /// <summary>
        /// Enables predicatable comparison for this object by returning <c>true</c> if the argument equals this object.
        /// </summary>
        /// <param name="obj">The object to which this is being compared</param>
        /// <returns><c>true</c> if and only if this object equals the argument or <c>false</c> if otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var model = obj as CountryModel;
            return model != null && Id == model.Id && CountryCode == model.CountryCode &&
            Name.Equals(model.Name) && CountryIso.Equals(model.CountryIso);
        }
        /// <summary>
        /// Enables sorting and ordering by apply numerical comparison to this object.
        /// </summary>
        /// <param name="other">The right side of this comparison. A <c>null</c> value automatically means this parameter is less than this object</param>
        /// <returns><c>1</c>, <c>0</c> or <c>-1</c> as this object is <i>greater than</i>, <i>equal to</i> or <i>less than</i> the argument.</returns>
        public int CompareTo(CountryModel? other)
        {
            if(other == null) return 1;

            var byId = ForInt().Compare(Id, other.Id);
            if(byId != 0) return byId;

            var byc = ForString().Compare(CountryCode, other.CountryCode);
            if(byc != 0) return byc;

            var byn = ForString().Compare(Name, other.Name);
            if(byn != 0) return byn;

            return ForString().Compare(CountryIso, other.CountryIso);
        }
    }
}