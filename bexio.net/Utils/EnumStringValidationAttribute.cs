using System.ComponentModel.DataAnnotations;

namespace bexio.net.Utils;

public class EnumStringValidationAttribute : ValidationAttribute
{
    private readonly Type _enumType;

    public EnumStringValidationAttribute(Type enumType)
    {
        if (!enumType.IsEnum)
            throw new ArgumentException("Type must be an enum.");
        
        _enumType = enumType;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success; // Allow null if required
        }

        if (_enumType.IsEnumDefined(value))
        {
            return ValidationResult.Success; // Valid enum value
        }

        return new ValidationResult($"The value '{value}' is not valid for enum {_enumType.Name}.");
    }
}