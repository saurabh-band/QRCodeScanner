using BarcodeScanner.Mobile;

namespace ScannerApp
{
    public partial class MainPage : ContentPage 
    {
        int count = 0;

        public MainPage()
        {
            BarcodeScanner.Mobile.Methods.SetSupportBarcodeFormat(BarcodeScanner.Mobile.BarcodeFormats.All);
            InitializeComponent();
            BarcodeScanner.Mobile.Methods.AskForRequiredPermissions();
        }

        private void Camera_OnDetected(object sender, BarcodeScanner.Mobile.OnDetectedEventArg e)
        {
            List<BarcodeResult> results = e.BarcodeResults;

            string resultText = string.Empty;
            for (int i = 0; i < results.Count; i++)
            {
                resultText += $"Type : {results[i].BarcodeType}, Value: {results[i].DisplayValue}{Environment.NewLine}";
            }

            Dispatcher.Dispatch(async () =>
            {
                await DisplayAlertAsync("Barcode Detected", resultText, "OK");

                if (await DisplayAlertAsync("Do you want to continue scanning?", "Press OK to continue scanning or Cancel to stop.", "OK", "Cancel"))
                    Camera.IsScanning = true;
            });
        }
    }
}
