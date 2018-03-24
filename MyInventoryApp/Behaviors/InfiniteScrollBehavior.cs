using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyInventoryApp.Behaviors
{
    public class InfiniteScrollBehavior : BehaviorBase<ListView>
    {
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(
            nameof(LoadMoreCommand),
            typeof(ICommand),
            typeof(InfiniteScrollBehavior),
            null
        );

        public ICommand LoadMoreCommand
        {
            get => (ICommand)GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }


		protected override void OnAttachedTo(ListView bindable)
		{
            base.OnAttachedTo(bindable);
            bindable.ItemAppearing +=  Bindable_ItemAppearing;
		}

		protected override void OnDetachingFrom(ListView bindable)
		{
            base.OnDetachingFrom(bindable);
            bindable.ItemAppearing -= Bindable_ItemAppearing;
		}

		

        void Bindable_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (AssociatedObject.ItemsSource is IList items && e.Item == items[items.Count - 1])
            {
                if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                    LoadMoreCommand.Execute(null);
            }
        }

	}
}
