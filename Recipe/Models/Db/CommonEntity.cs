#nullable disable

namespace Recipe.Models.Db
{
    public partial class CommonEntity
    {
        public int Id { get; set; }
        
        public byte[] RftData { get; set; }
    }
}
