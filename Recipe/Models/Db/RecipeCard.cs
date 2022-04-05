#nullable disable

namespace Recipe.Models.Db
{
    using System;
    using System.Collections.Generic;

    public partial class RecipeCard
    {
        public RecipeCard()
        {
            RecipeLines = new HashSet<RecipeLine>();
        }

        public int Id { get; set; }
        
        public int CustomerId { get; set; }
        
        public DateTime Date { get; set; }
        
        public string Remarks { get; set; }

        public virtual Customer Customer { get; set; }
        
        public virtual ICollection<RecipeLine> RecipeLines { get; set; }
    }
}
