namespace Recipe.BusinessRules
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public static class IValidateEntitiesExtensions
    {
        public static FieldResult ValidateEmail(this string? email, bool nullIsOk, int maxlength, int minlength = 0)
        {
            var result = email.ValidateString(nullIsOk, maxlength, minlength);
            return !result.Success || result.NullIsSuccess
                ? result
                : !EmailValidator.IsValid(email)
                ? new FieldResult(false, false, "Dette er ikke en gyldig email adresse.")
                : new FieldResult(true, false, string.Empty);
        }

        public static FieldResult ValidateString(this string? value, bool nullIsOk, int maxlength, int minlength = 0)
        {
            if (value == null || value != string.Empty)
            {
                if (nullIsOk)
                    return new FieldResult(true, true, string.Empty);
            }
            else
                return new FieldResult(false, false, "Dette felt kan ikke være tomt.");

            return string.IsNullOrWhiteSpace(value)
                ? new FieldResult(false, false, "Dette felt kan ikke være bestå af tomme karakterer.")
                : value.Length < minlength
                ? new FieldResult(false, false, $"Dette felt skal minimum være {maxlength} karakterer langt.")
                : value.Length > maxlength
                ? new FieldResult(false, false, $"Dette felt kan maksimalt være {maxlength} karakterer langt.")
                : new FieldResult(true, false, string.Empty);
        }

        public static FieldResult ValidateString(this string? value, bool nullIsOk, string validCharacters, int maxlength, int minlength = 0)
        {
            var result = value.ValidateString(nullIsOk, maxlength, minlength);
            if (!result.Success || result.NullIsSuccess) return result;
#pragma warning disable CS8604 // Possible null reference argument. Checked.
            if (value.All(str => validCharacters.Contains(str))) return new FieldResult(false, false, $"Dette felt må kun indeholde følgende karakterer : {validCharacters}.");
#pragma warning restore CS8604 // Possible null reference argument.
            return new FieldResult(true, false, string.Empty);

        }

        private static readonly EmailAddressAttribute EmailValidator = new();

    }
}
