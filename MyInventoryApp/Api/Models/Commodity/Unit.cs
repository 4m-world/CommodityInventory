
namespace MyInventoryApp.Api.Models
{
    public class Unit
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }

        public UnitType UnitType { get; set; }
    }
}
