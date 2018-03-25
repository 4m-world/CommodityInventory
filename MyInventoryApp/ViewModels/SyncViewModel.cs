using System.Threading.Tasks;
using System.Windows.Input;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.ViewModels.Base;

namespace MyInventoryApp.ViewModels
{
    public class SyncViewModel : BaseViewModel
    {
        int _totalRecords;
        readonly ICommodityService _commodityService;
        ICommand _shareItems;

        public int TotalRecords
        {
            get => _totalRecords;
            set => UpdateAndNotifyOnChange(ref _totalRecords, value);
        }

        public SyncViewModel(ICommodityService commodityService)
        {
            _commodityService = commodityService;
        }


        public ICommand ShareItems
        {
            get => _shareItems = _shareItems ?? new DelegateCommandAsync(ShareCommodities);
        }

        Task ShareCommodities()
        {
            return Task.FromResult(false);
        }

		public override Task Initalize(object navigationContext)
		{
            TotalRecords = 100;
            return base.Initalize(navigationContext);
		}
	}
}

