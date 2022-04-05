#nullable disable

namespace Recipe.Models.Db
{
    using System.Collections.Generic;

    public partial class MedicinType
    {
        public MedicinType()
        {
            Medicins = new HashSet<Medicin>();
            RecipeLines = new HashSet<RecipeLine>();
        }

        public int Id { get; set; }
        
        public string MedicinTypeName { get; set; }
        
        public bool IsActive { get; set; }

        public virtual ICollection<Medicin> Medicins { get; set; }
        
        public virtual ICollection<RecipeLine> RecipeLines { get; set; }
    }
}
