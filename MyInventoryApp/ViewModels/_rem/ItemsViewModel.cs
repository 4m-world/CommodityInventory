using System;
using System.Linq;
using ReactiveUI;
using MyInventoryApp.Models;
using MyInventoryApp.Services;
using System.Reactive.Linq;

namespace MyInventoryApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly IItemService _itemService;

        private ReactiveList<Item> _items;
        public ReactiveList<Item> Items{
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        private ObservableAsPropertyHelper<bool> _canAdd;
        public bool CanAdd => _canAdd?.Value ?? true;

        public ReactiveCommand AddCommand { get; private set; }

        public ReactiveCommand DeleteCommand { get; private set; }

        public ItemsViewModel(IItemService itemService, IScreen screen = null)
            : base(screen)
        {
            _itemService = itemService;

            Items.Add(new Item { Id = Guid.NewGuid().ToString(), Barcode = "11111111", Name = "Item one", IsSynced = false });
            Items.Add(new Item { Id = Guid.NewGuid().ToString(), Barcode = "11111111", Name = "Item one", IsSynced = false });
            Items.Add(new Item { Id = Guid.NewGuid().ToString(), Barcode = "11111111", Name = "Item one", IsSynced = false });
            Items.Add(new Item { Id = Guid.NewGuid().ToString(), Barcode = "11111111", Name = "Item one", IsSynced = false });
            Items.Add(new Item { Id = Guid.NewGuid().ToString(), Barcode = "11111111", Name = "Item one", IsSynced = false });

            Items.ItemChanged
                 .Where(x => x.PropertyName == "IsSynced" && x.Sender.IsSynced)
                 .Select(x => x.Sender)
                 .Subscribe(x =>
                 {
                    if(x.IsSynced){
                         Items.Remove(x);
                         Items.Add(x);
                    }
                 });
        }
    }
}
