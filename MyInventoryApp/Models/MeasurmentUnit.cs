using ReactiveUI;

namespace MyInventoryApp.Models
{
    public class MeasurmentUnit : ReactiveObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Abbr { get; set; }
    }
}
