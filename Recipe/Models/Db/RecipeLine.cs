#nullable disable

namespace Recipe.Models.Db
{
    public partial class RecipeLine
    {
        public int Id { get; set; }
        
        public int RecipeCardId { get; set; }
        
        public string MedicinName { get; set; }
        
        public string Morning { get; set; }
        
        public string Noon { get; set; }
        
        public string Evening { get; set; }
        
        public string Midnight { get; set; }
        
        public int Position { get; set; }
        
        public int MedicinTypeId { get; set; }
        
        public int? UnitsId { get; set; }

        public virtual MedicinType MedicinType { get; set; }
        
        public virtual RecipeCard RecipeCard { get; set; }
        
        public virtual Unit Units { get; set; }
    }
}
