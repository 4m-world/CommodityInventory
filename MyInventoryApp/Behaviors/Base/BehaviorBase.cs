using System;
using Xamarin.Forms;

namespace MyInventoryApp.Behaviors
{
    public class BehaviorBase<T> : Behavior<T>
        where T :BindableObject
    {
        public T AssociatedObject { get; private set; }

		protected override void OnAttachedTo(T bindable)
		{
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;

            if (bindable.BindingContext != null)
                BindingContext = bindable.BindingContext;
            bindable.BindingContextChanged += Bindable_BindingContextChanged;
		}

		protected override void OnDetachingFrom(T bindable)
		{
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            AssociatedObject = null;
		}

		protected override void OnBindingContextChanged()
		{
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject?.BindingContext;
		}

		void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

	}
}
