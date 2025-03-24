using System.ComponentModel.DataAnnotations;

namespace Domain.Utils
{
    public class EntryValueCustomValidationAttribute : ValidationAttribute
    {
        public EntryValueCustomValidationAttribute() : base(DomainErrorMessage.ValueErrorMessage)
        {
        }

        public override bool IsValid(object? value)
        {
            if (value is not null && value is decimal numero)
                return numero > 0;

            return false;
        }
    }
}