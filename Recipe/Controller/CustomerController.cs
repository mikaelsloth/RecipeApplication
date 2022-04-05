namespace Recipe.Controller
{
    using Microsoft.EntityFrameworkCore;
    using Recipe.Models.Db;
    using System;
    using System.Threading.Tasks;

    public class CustomerController : BaseController, ICustomerController
    {
        public CustomerController(RecipeDbContext context) : base(context)
        {
        }

        public async Task<Customer?> GetFromNameSearch(string autocompletetext)
        {
            Tuple<string, string?, string?, string?> processedsearch = ProcessAutoCompleteNameText(autocompletetext);
            Customer? customer;
            return customer = await Database.Customers.FirstOrDefaultAsync(c => c.Name == processedsearch.Item1 && c.Address1 == processedsearch.Item2
                && c.Postcode == processedsearch.Item3 && c.Phone == processedsearch.Item4 && !c.IsDeleted);
        }

        public async Task<Customer?> GetFromPhoneSearch(string autocompletetext)
        {
            Tuple<string, string, string?, string?> processedsearch = ProcessAutoCompletePhoneText(autocompletetext);
            Customer? customer;
            return customer = await Database.Customers.FirstOrDefaultAsync(c => c.Name == processedsearch.Item2 && c.Address1 == processedsearch.Item3
                && c.Postcode == processedsearch.Item4 && (c.Phone == processedsearch.Item1 || c.Mobile == processedsearch.Item1) && !c.IsDeleted);
        }

        public async Task<Customer?> Get(int id)
        {
            Customer? customer = await Database.Customers
            .FindAsync(id);
            if (customer != null && customer.IsDeleted)
            {
                customer = null;
            }

            return customer;
        }

        public async Task<int> Add(Customer customer)
        {
            try
            {
                _ = await Database.Customers.AddAsync(customer);
                _ = await Database.SaveChangesAsync();
                return customer.Id;
            }
            catch { throw; }
        }

        public async Task Modify(Customer customer)
        {
            try
            {
                Customer? original = await Database.Customers.FindAsync(customer.Id);
                if (original != null && !original.IsDeleted)
                {
                    original.MergeWith(customer);
                    _ = await Database.SaveChangesAsync();
                    return;
                }

                throw new InvalidOperationException("Cannot modify a non-existing or deleted customer");
            }
            catch { throw; }
        }

        public async Task Delete(int id)
        {
            try
            {
                Customer? original = await Database.Customers.FindAsync(id);
                if (original != null)
                {
                    bool already_deleted = original.IsDeleted;
                    if (!already_deleted)
                    {
                        original.IsDeleted = true;
                        original.DeletedDate = DateTime.Today;
                        _ = await Database.SaveChangesAsync();
                    }

                    return;
                }

                throw new InvalidOperationException("Cannot delete a non-existing customer");
            }
            catch { throw; }
        }

        public async Task PermanentDelete(int id)
        {
            try
            {
                Customer? original = await Database.Customers.FindAsync(id);
                if (original != null)
                {
                    if (original.IsDeleted)
                    {
                        _ = Database.Customers.Remove(original);
                        _ = await Database.SaveChangesAsync();
                        return;
                    }

                    throw new InvalidOperationException("Cannot permanently delete a non-deleted customer");
                }

                throw new InvalidOperationException("Cannot permanently delete a non-existing customer");
            }
            catch { throw; }
        }

        private static Tuple<string, string?, string?, string?> ProcessAutoCompleteNameText(string autocompletetext)
        {
            string[] split = autocompletetext.Split(';', StringSplitOptions.TrimEntries);
            return Tuple.Create(split[0], string.IsNullOrWhiteSpace(split[1]) ? null : split[1], string.IsNullOrWhiteSpace(split[2]) ? null : split[2], string.IsNullOrWhiteSpace(split[3]) ? null : split[3]);
        }

        private static Tuple<string, string, string?, string?> ProcessAutoCompletePhoneText(string autocompletetext)
        {
            string[] split = autocompletetext.Split(';', StringSplitOptions.TrimEntries);
            return Tuple.Create(split[0], split[1], string.IsNullOrWhiteSpace(split[2]) ? null : split[2], string.IsNullOrWhiteSpace(split[3]) ? null : split[3]);
        }
    }
}
