
using System.ComponentModel.DataAnnotations;
using static Example.Repos.Utils;

namespace Example.Models
{
    /// <summary>
    /// A Model that was populated by the data source (db or file system) meant to be use in the server only for CRUD operations
    /// and represents a customer with a valid phone number.
    /// </summary>
    public class CustomerModel : IComparable<CustomerModel>
    {
        /// <summary>
        /// The phone number of this customer which is also their primary key
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// The model containing the data for the country against which this model was registered in the data source
        /// </summary>
        public CountryModel Country { get; set; }

        /// <summary>
        /// The uniquely generated integer which enables this object to be used in a <c><see cref="System.Collections.Hashtable" /></c>
        /// </summary>
        /// <returns>The hash code for this model</returns>
        public int GetHashCode()
        {
            return (Id.GetHashCode() << 0xffff) | Country.GetHashCode() ^ 0xf7f7f7;
        }
        /// <summary>
        /// Enables predicatable comparison for this object by returning <c>true</c> if the argument equals this object.
        /// </summary>
        /// <param name="obj">The object to which this is being compared</param>
        /// <returns><c>true</c> if and only if this object equals the argument or <c>false</c> if otherwise.</returns>
        public bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var model = obj as CustomerModel;
            return model != null && Id == model.Id && Country.Equals(model.Country);
        }
        /// <summary>
        /// Enables sorting and ordering by apply numerical comparison to this object.
        /// </summary>
        /// <param name="other">The right side of this comparison. A <c>null</c> value automatically means this parameter is less than this object</param>
        /// <returns><c>1</c>, <c>0</c> or <c>-1</c> as this object is <i>greater than</i>, <i>equal to</i> or <i>less than</i> the argument.</returns>
        public int CompareTo(CustomerModel? other)
        {
            if(other == null) return 1;

            var byn = ForInt().Compare(Id, other.Id);
            if(byn != 0) return byn;

            return Country.CompareTo(other.Country);
        }
    }
}