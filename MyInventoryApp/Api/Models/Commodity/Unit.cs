
using SQLite;

namespace MyInventoryApp.Api.Models
{
    [Table("Units")]
    public class Unit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public UnitType UnitType { get; set; }
    }
}
