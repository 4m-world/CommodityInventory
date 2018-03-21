using Android.Widget;

namespace MyInventoryApp.Droid.Controls.Fab.Interfaces
{
    public interface IOnScrollChangedListener
    {
        void OnScrollChanged(ScrollView who, int l, int t, int oldl, int oldt);
    }
}
