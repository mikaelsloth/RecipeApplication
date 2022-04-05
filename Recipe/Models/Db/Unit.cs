#nullable disable

namespace Recipe.Models.Db
{
    using System.Collections.Generic;

    public partial class Unit
    {
        public Unit()
        {
            Medicins = new HashSet<Medicin>();
            RecipeLines = new HashSet<RecipeLine>();
        }

        public int Id { get; set; }
        public string UnitName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Medicin> Medicins { get; set; }
        public virtual ICollection<RecipeLine> RecipeLines { get; set; }
    }
}
