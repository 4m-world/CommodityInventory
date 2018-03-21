using System.Collections.Generic;
using ReactiveUI;

namespace MyInventoryApp.Models
{
    public class Item : ReactiveObject
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public string Barcode { get; set; }

        public double Price { get; set; }

        public double MeasurmentValue { get; set; }

        public int MeasurmentUnitId { get; set; }

        public MeasurmentUnit MeasurmentUnit { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public IList<string> Images { get; set; }

        private bool _isSynced;
        public bool IsSynced
        {
            get => _isSynced;
            set => this.RaiseAndSetIfChanged(ref _isSynced, value);
        }

        public Item()
        {
            Images = new List<string>();
        }
    }
}