namespace Recipe.Models.Db
{
    public static partial class CustomerMethods
    {
        public static void MergeWith(this Customer existing, Customer customer)
        {
            existing.Name = customer.Name;
            existing.Address1 = customer.Address1;
            existing.Address2 = customer.Address2;
            existing.Postcode = customer.Postcode;
            existing.Town = customer.Town;
            existing.Phone = customer.Phone;
            existing.Mobile = customer.Mobile;
            existing.Email = customer.Email;
            existing.Notes += customer.Notes;
            existing.AllowGdprdataStoring = customer.AllowGdprdataStoring;
        }

        public static Customer Clone(this Customer existing)
        {
            Customer newCustomer = new();
            newCustomer.Name = existing.Name;
            newCustomer.Address1 = existing.Address1;
            newCustomer.Address2 = existing.Address2;
            newCustomer.Postcode = existing.Postcode;
            newCustomer.Town = existing.Town;
            newCustomer.Phone = existing.Phone;
            newCustomer.Mobile = existing.Mobile;
            newCustomer.Email = existing.Email;
            newCustomer.Notes = existing.Notes;
            newCustomer.AllowGdprdataStoring = existing.AllowGdprdataStoring;
            return newCustomer;
        }

        public static bool ValueEquals(this Customer existing, Customer customer)
        {
            bool result = true &&
                existing.Name == customer.Name &&
                existing.Address1 == customer.Address1 &&
                existing.Address2 == customer.Address2 &&
                existing.Postcode == customer.Postcode &&
                existing.Town == customer.Town &&
                existing.Phone == customer.Phone &&
                existing.Mobile == customer.Mobile &&
                existing.Email == customer.Email &&
                existing.Notes == customer.Notes &&
                existing.AllowGdprdataStoring == customer.AllowGdprdataStoring;
            return result;
        }
    }
}
