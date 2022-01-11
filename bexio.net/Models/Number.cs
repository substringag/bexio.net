using System;
using System.Globalization;

namespace bexio.net.Models
{
    /// <summary>
    /// Represents a number, or null, given from the Bexio API.
    /// </summary>
    public class Number
    {
        private readonly string? _number;

        public Number(string? number)
        {
            _number = number;
        }

        public string? GetNumber()
        {
            return _number;
        }

        public bool IsNull()
        {
            return _number == null;
        }

        public double? GetDouble()
        {
            return GetDouble(CultureInfo.InvariantCulture);
        }

        public double? GetDouble(IFormatProvider formatProvider)
        {
            if (_number == null) return null;
            return double.TryParse(_number, NumberStyles.Currency, formatProvider, out double d)
                ? d
                : null;
        }

        public decimal? GetDecimal()
        {
            return GetDecimal(CultureInfo.InvariantCulture);
        }

        public decimal? GetDecimal(IFormatProvider formatProvider)
        {
            if (_number == null) return null;
            return decimal.TryParse(_number, NumberStyles.Currency, formatProvider, out decimal d)
                ? d
                : null;
        }

        public static bool operator ==(Number? a, Number? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a._number == b._number;
        }

        public static bool operator !=(Number? a, Number? b)
        {
            return !(a == b);
        }

        public override bool Equals(object? obj)
        {
            return obj is Number number && this == number;
        }

        public override int GetHashCode()
        {
            return _number?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return _number ?? "";
        }
    }
}
