#nullable disable

namespace Recipe.Models.Db
{
    using System;
    using System.Collections.Generic;

    public partial class Customer
    {
        public Customer()
        {
            RecipeCards = new HashSet<RecipeCard>();
        }

        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Address1 { get; set; }
        
        public string Address2 { get; set; }
        
        public string Postcode { get; set; }
        
        public string Town { get; set; }
        
        public string Phone { get; set; }
        
        public string Mobile { get; set; }
        
        public string Email { get; set; }
        
        public string Notes { get; set; }
        
        public bool AllowGdprdataStoring { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public string Uniquekey { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedDate { get; set; }

        public virtual ICollection<RecipeCard> RecipeCards { get; set; }
    }
}
