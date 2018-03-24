using System;
using Xamarin.Forms;

namespace MyInventoryApp.Controls
{
    public class BarcodeLabel : Label
    {
        public static readonly BindableProperty BarcodeTypeProperty = BindableProperty.Create(
            nameof(BarcodeType),
            typeof(BarcodeType),
            typeof(BarcodeLabel),
            BarcodeType.Barcode39,
            propertyChanged: OnBarcodeTypeChanged
        );

        public BarcodeType BarcodeType 
        {
            get => (BarcodeType)GetValue(BarcodeTypeProperty);
            set => SetValue(BarcodeTypeProperty, value);
        }

        static void OnBarcodeTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var fontName = "free3of9";
            if(newValue is BarcodeType barcodeType){
                switch(barcodeType){
                    case BarcodeType.Barcode39:
                    default:
                        fontName = "free3of9";
                        break;
                }
            }

            ((Label)bindable).FontFamily = fontName;
        }

        public BarcodeLabel()
            :base()
        {
            //BarcodeType = BarcodeType.Barcode39;
          FontFamily = "free3of9";
        }
    }

    public enum BarcodeType 
    {
        Barcode39
    }
}
