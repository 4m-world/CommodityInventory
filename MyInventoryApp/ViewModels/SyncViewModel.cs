using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyInventoryApp.Api.Services.Commodity;
using MyInventoryApp.Services.FileHelper;
using MyInventoryApp.Services.Share;
using MyInventoryApp.ViewModels.Base;
using Newtonsoft.Json;

namespace MyInventoryApp.ViewModels
{
    public class SyncViewModel : BaseViewModel
    {
        int _totalRecords;
        string _progress;

        readonly ICommodityService _commodityService;
        readonly IFileHelper _fileHelper;
        readonly IShareService _shareService;

        ICommand _shareItems;

        public int TotalRecords
        {
            get => _totalRecords;
            set => UpdateAndNotifyOnChange(ref _totalRecords, value);
        }

        public string Progress
        {
            get => _progress;
            set => UpdateAndNotifyOnChange(ref _progress, value);
        }

        public SyncViewModel(
            ICommodityService commodityService, 
            IFileHelper fileHelper,
            IShareService shareService)
        {
            _commodityService = commodityService;
            _fileHelper = fileHelper;
            _shareService = shareService;
        }


        public ICommand ShareItems
        {
            get => _shareItems = _shareItems ?? new DelegateCommandAsync(ShareCommodities);
        }

        async Task ShareCommodities()
        {
            if (IsBusy) return;

            Progress = "0% initating";

            string fileName = $"CM_{DateTime.UtcNow.Ticks}.json";
            var content = new StringBuilder();
            var totalPages = Math.Floor((decimal)TotalRecords / 30);
            var index = 0;
            Progress = "1% preparing content";

            while(index<=totalPages)
            {
                var result = await _commodityService.GetCommoditiesAsync(string.Empty, index, 30);

                foreach (var item in result)
                {
                    //if (item.IsSyncronized) continue;
                    content.AppendLine($"{JsonConvert.SerializeObject(item)},");
                    item.IsSyncronized = true;
                    await _commodityService.UpdateCommodityItemAsync(item);
                }

                index++;
                Progress = $"{1 + totalPages / index * 100 - 5}% preparing content";
            }

            Progress = "Almost there saving...";
            var filePath =await _fileHelper.SaveText(fileName, content.ToString());
            IsBusy = false;
            Progress = "Complete";
            await Task.Delay(500);
            await _shareService.Show("Share Commodities", "Select an email client or cloud service in order to share collectd commodities", filePath);
            Progress = "Save to File";
        }

		public override async Task Initalize(object navigationContext)
		{
            if (IsBusy) return;
            IsBusy = true;
            Progress = "Save to File";
            TotalRecords = await _commodityService.GetCommoditiesCountAsync();
            IsBusy = false;
		}
	}
}

