
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MyInventoryApp.Droid
{
    [Activity(Label = "MyInventoryApp.Droid", 
              Icon = "@drawable/icon",
              Theme = "@style/MyTheme", 
              MainLauncher = false, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
                                                        Permission[] grantResults)
        {
            global::ZXing.Net.Mobile
                             .Android
                             .PermissionsHandler
                             .OnRequestPermissionsResult(requestCode, permissions, grantResults);


            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
	}
}
