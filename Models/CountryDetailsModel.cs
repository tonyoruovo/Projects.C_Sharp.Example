
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Example.Repos.Utils;

namespace Example.Models
{
    /// <summary>
    /// A Model that was populated by the data source (db or file system) meant to be use in the server only for CRUD operations
    /// and represents a model that contains data compacted for the details of the country for which a customer's phone number exists.
    /// </summary>
    public class CountryDetailsModel : IComparable<CountryDetailsModel>
    {
        /// <summary>
        /// The primary key
        /// </summary>
        [Key]
        public int Id {get; set;}
        /// <summary>
        /// The primary of the owner of this details
        /// </summary>
        [ForeignKey(nameof(CountryModel.Id))]
        public int CountryId {get; set;}
        /// <summary>
        /// The operator's name.
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// The operator's code
        /// </summary>
        public string OperatorCode { get; set; }

        /// <summary>
        /// The uniquely generated integer which enables this object to be used in a <c><see cref="System.Collections.Hashtable" /></c>
        /// </summary>
        /// <returns>The hash code for this model</returns>
        public override int GetHashCode()
        {
            return (Id << 0xffff) | CountryId;
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
            var model = obj as CountryDetailsModel;
            return model != null && Id == model.Id && CountryId == model.CountryId &&
            Operator.Equals(model.Operator) && OperatorCode.Equals(model.OperatorCode);
        }
        /// <summary>
        /// Enables sorting and ordering by apply numerical comparison to this object.
        /// </summary>
        /// <param name="other">The right side of this comparison. A <c>null</c> value automatically means this parameter is less than this object</param>
        /// <returns><c>1</c>, <c>0</c> or <c>-1</c> as this object is <i>greater than</i>, <i>equal to</i> or <i>less than</i> the argument.</returns>
        public int CompareTo(CountryDetailsModel? other)
        {
            if(other == null) return 1;

            var byId = ForInt().Compare(Id, other.Id);
            if(byId != 0) return byId;

            var byOp = ForString().Compare(Operator, other.Operator);
            if(byOp != 0) return byOp;

            var byOpc = ForString().Compare(OperatorCode, other.OperatorCode);
            if(byOpc != 0) return byOpc;

            return ForInt().Compare(CountryId, other.CountryId);
        }
    }
}