namespace Recipe.BusinessRules
{
    using Recipe.Models.Db;
    using System;
    using System.Collections.Generic;

    public class CustomerModelValidation : IValidateEntities<Customer>
    {
        private static readonly IDictionary<string, Func<string?, FieldResult>> fieldValidations = new Dictionary<string, Func<string?, FieldResult>>();
        private const string phoneCharacters = "0123456789 +-()#";

        static CustomerModelValidation()
        {
            fieldValidations.Add("Name", NameValidation);
            fieldValidations.Add("Address1", Address1Validation);
            fieldValidations.Add("Address2", Address2Validation);
            fieldValidations.Add("Postcode", PostcodeValidation);
            fieldValidations.Add("Town", TownValidation);
            fieldValidations.Add("Phone", PhoneValidation);
            fieldValidations.Add("Mobile", MobileValidation);
            fieldValidations.Add("Email", EmailValidation);
        }
        public DomainResult Validate(IValidatable<Customer> entity)
        {
            IList<ArgumentException> exceptions = new List<ArgumentException>();
            bool result = true;
            foreach (var item in entity.GetType().GetProperties())
            {
                var field = ValidateField(item.Name, item.PropertyType, item.GetValue(entity));
                result &= field.Success;
                if (!field.Success) exceptions.Add(new ArgumentException(field.Message, item.Name));
            }

            return new(result, exceptions);
        }

        private static FieldResult ValidateField(string propertyName, Type propertyType, object? value) => !fieldValidations.TryGetValue(propertyName, out Func<string?, FieldResult>? result)
                ? new FieldResult(true, false, string.Empty)
                : value == null
                ? result(null)
                : value.GetType() != propertyType
                ? throw new ArgumentException("Mismatch in object and type", nameof(value))
                : value.GetType() != typeof(string)
                ? throw new ArgumentException("Only strings are supported", nameof(propertyType))
                : result(value.ToString());

        private static FieldResult NameValidation(string? value) => value.ValidateString(false, 50, 2);

        private static FieldResult Address1Validation(string? value) => value.ValidateString(true, 50);

        private static FieldResult Address2Validation(string? value) => value.ValidateString(true, 50);

        private static FieldResult PostcodeValidation(string? value) => value.ValidateString(true, 7, 4);

        private static FieldResult TownValidation(string? value) => value.ValidateString(true, 40);

        private static FieldResult PhoneValidation(string? value) => value.ValidateString(true, phoneCharacters, 20, 8);

        private static FieldResult MobileValidation(string? value) => value.ValidateString(true, phoneCharacters, 20, 8);

        private static FieldResult EmailValidation(string? value) => value.ValidateEmail(true, 100);

    }
}
