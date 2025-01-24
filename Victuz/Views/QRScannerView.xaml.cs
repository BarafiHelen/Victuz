using ZXing.Net.Maui;

namespace Victuz.Views
{
    public partial class QRScannerView : ContentPage
    {
        public QRScannerView()
        {
            InitializeComponent();
        }

        private void CameraView_OnDetected(object sender, BarcodeDetectionEventArgs e)
        {
            var result = e.Results.FirstOrDefault()?.Value;
            if (result != null)
            {
                Dispatcher.Dispatch(() =>
                {
                    ResultLabel.Text = $"Scanned QR Code: {result}";
                });
            }
        }

        private void OnStartScannerClicked(object sender, EventArgs e)
        {
           
        }
    }
}