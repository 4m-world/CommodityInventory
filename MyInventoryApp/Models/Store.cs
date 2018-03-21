using System;
using ReactiveUI;

namespace MyInventoryApp.Models
{
    public class Store : ReactiveObject
    {
        public string Name { get; set; }

        public string AltName { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }

        public string Notes { get; set; }

        public DateTime CreateUtcDate { get; set; }
    }
}
