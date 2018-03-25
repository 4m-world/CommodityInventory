using System;
using System.Collections.Generic;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace MyInventoryApp.Api.Models
{
    [Table("Commodities")]
    public class CommodityItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string AltName { get; set; }

        public string Barcode { get; set; }

        public double UnitValue { get; set; }

        [ForeignKey(typeof(Unit))]
        public int UnitId { get; set; }

        [ManyToOne]
        public Unit Unit { get; set; }

        public double Price { get; set; }

        public string Notes { get; set; }

        public string Image { get; set; }

        public DateTime CreateUtcDate { get; set; } = DateTime.UtcNow;

        public bool IsSyncronized { get; set; } = false;
    }
}
