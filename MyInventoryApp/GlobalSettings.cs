using System;
namespace MyInventoryApp
{
    public class GlobalSettings
    {
        public static GlobalSettings Instance { get; private set; }

        public string UnitEndpoint { get; set; }

        public string CommodityEndpoint { get; set; }
    }
}
