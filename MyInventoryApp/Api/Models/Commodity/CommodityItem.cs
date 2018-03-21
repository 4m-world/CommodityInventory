using System;
using System.Collections.Generic;

namespace MyInventoryApp.Api.Models
{
    public class CommodityItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public string Barcode { get; set; }

        public double UnitValue { get; set; }

        public Unit Unit { get; set; }

        public double Price { get; set; }

        public string Notes { get; set; }

        public IList<string> Images { get; set; }

        public DateTime CreateUtcDate { get; set; }

        public bool IsSyncronized { get; set; }
    }
}
