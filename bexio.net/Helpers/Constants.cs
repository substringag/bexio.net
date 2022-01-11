namespace bexio.net.Helpers
{
    internal static class Constants
    {
        /// <summary>
        /// Possible values:<br/>
        /// "=" "equal" "!=" "not_equal"
        /// ">" "greater_than" ">=" "greater_equal"
        /// "&lt;" "less_than" "&lt;=" "less_equal"
        /// "like" "not_like"
        /// "is_null" "not_null"
        /// "in" "not_in"
        /// </summary>
        public static class SearchCriteria
        {
            public static string like         = "like";
            public static string notLike      = "not_like";
            public static string isNull       = "is_null";
            public static string notNull      = "not_null";
            public static string equal        = "equal";
            public static string notEqual     = "not_equal";
            public static string greaterThan  = "greater_than";
            public static string greaterEqual = "greater_equal";
            public static string lessThan     = "less_than";
            public static string lessEqual    = "less_equal";
        }
    }
}
