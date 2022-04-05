#nullable disable

namespace Recipe.Models.Db
{
    using System;

    public partial class LatestCustomersView
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Address1 { get; set; }
        
        public string Postcode { get; set; }
        
        public string Town { get; set; }
        
        public string Phone { get; set; }
        
        public DateTime? LatestRecipeCard { get; set; }
    }
}
