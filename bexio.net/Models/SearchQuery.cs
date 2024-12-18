using System.ComponentModel.DataAnnotations;
using bexio.net.Utils;

namespace bexio.net.Models
{
    public record SearchQuery
    {
        /// <summary>
        /// Field which should be search over
        /// </summary>
        [MaxLength(255)]
        public string Field { get; set; } = "";

        /// <summary>
        /// Value to search for
        /// </summary>
        [MaxLength(255)]
        public string Value { get; set; } = "";

        /// <summary>
        /// Possible values: See <see cref="bexio.net.Helpers.Enums.SearchCriteria"/>
        /// </summary>
        [EnumStringValidation(typeof(Helpers.Enums.SearchCriteria))]
        public string Criteria { get; set; } = "=";

        public SearchQuery()
        {
        }

        public SearchQuery(string field, string value, string criteria = "=")
        {
            Field    = field;
            Value    = value;
            Criteria = criteria;
        }
    }
}
