using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MyInventoryApp.Api.Models;
using MyInventoryApp.Services.FileHelper;
using MyInventoryApp.ViewModels.Base;
using SQLite;
using Xamarin.Forms;

namespace MyInventoryApp.Api.Database
{
    public class CommodityDatabase
    {
        static CommodityDatabase _instance;
        const string DatabaseFileName = "CMDatabase.db3";

        public static CommodityDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CommodityDatabase(DependencyService.Get<IFileHelper>()
                                                      .GetLocalFilePath(DatabaseFileName));
                    Initalize();
                }

                return _instance;
            }
        }

        static async void Initalize()
        {
            var units = new List<Unit>
            {
                new Unit{ Name = "Kilogram", Abbr = "Kg", UnitType = UnitType.Mass },
                new Unit{ Name = "Gram", Abbr = "gm", UnitType = UnitType.Mass },
                new Unit{ Name = "Liter", Abbr = "Lt", UnitType = UnitType.Volume },
                new Unit{ Name = "Mililiter", Abbr = "ml", UnitType = UnitType.Volume }
            };

            foreach (var unit in units)
            {
                var dbItem = await Instance.GetUnitAsync(unit.Name);
                if (dbItem == null)
                    await Instance.SaveUnitAsync(unit);
            }
        }

        readonly SQLiteAsyncConnection _database;

        public CommodityDatabase(string dbPath)
        {
            try
            {
                _database = new SQLiteAsyncConnection(dbPath);

                _database.CreateTableAsync<CommodityItem>().Wait();
                _database.CreateTableAsync<Unit>().Wait();
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("CommodityDatabase creation exception: " + ex);
            }
        }

        #region Commodity Item

        public Task<int> TotalCommoditiesAsync()
        {
            return _database.Table<CommodityItem>().CountAsync();
        }

        public Task<CommodityItem> GetCommodityAsync(int id)
        {
            return _database.Table<CommodityItem>()
                            .Where(c => c.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<CommodityItem>> GetCommoditiesAsync(int pageIndex, int pageSize = 20)
        {
            return _database.Table<CommodityItem>()
                     .Skip(pageSize * pageIndex)
                     .Take(pageSize)
                     .ToListAsync();
        }

        public Task<int> SaveCommodityAsync(CommodityItem commodity)
        {
            if (commodity.Id != 0)
            {
                return _database.UpdateAsync(commodity);
            }

            return _database.InsertAsync(commodity);
        }

        public Task<int> DeleteCommodityAsync(CommodityItem commodity)
        {
            return _database.DeleteAsync(commodity);
        }

        #endregion

        #region Unit

        public Task<int> TotalUnitsAsync()
        {
            return _database.Table<Unit>()
                            .CountAsync();
        }

        public Task<Unit> GetUnitAsync(string name)
        {
            return _database.Table<Unit>()
                            .Where(u => u.Name.Equals(name))
                            .FirstOrDefaultAsync();
        }

        public Task<Unit> GetUnitAsync(int id)
        {
            return _database.Table<Unit>()
                            .Where(u => u.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<List<Unit>> GetUnitsAsync(int pageIndex, int pageSize = 20)
        {
            return _database.Table<Unit>()
                            .Skip(pageSize * pageIndex)
                            .Take(pageSize)
                            .ToListAsync();
        }

        public Task<int> SaveUnitAsync(Unit unit)
        {
            if (unit.Id != 0)
                return _database.UpdateAsync(unit);
            return _database.InsertAsync(unit);
        }

        public Task<int> DeleteUnitAsync(Unit unit)
        {
            return _database.DeleteAsync(unit);
        }

        #endregion
    }
}
