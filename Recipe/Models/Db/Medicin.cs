#nullable disable

namespace Recipe.Models.Db
{
    public partial class Medicin
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int MedicinTypeId { get; set; }
        
        public int UnitId { get; set; }

        public virtual MedicinType MedicinType { get; set; }
        
        public virtual Unit Unit { get; set; }
    }
}
