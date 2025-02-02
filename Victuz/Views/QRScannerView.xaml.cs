using Camera.MAUI;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace Victuz.Views
{
    public partial class QRScannerView : ContentPage
    {
        public QRScannerView(int eventId)
        {
            //InitializeComponent();
            _eventId = eventId;
        }
        private readonly int _eventId;
        private async void CameraView_OnDetected(object sender, BarcodeDetectionEventArgs e)
        {
            var result = e.Results.FirstOrDefault()?.Value;
            if (!string.IsNullOrEmpty(result))
            {
                await Dispatcher.DispatchAsync(async () =>
                {
                    //ResultLabel.Text = $"Scanned QR Code: {result}";

                    // Controleer de QR-code in de database
                    var qrCode = await App.Database.GetQRCodeAsync(userId: 1, eventId: 1); 
                    if (qrCode != null && qrCode.QRCodeContent == result)
                    {
                        await DisplayAlert("Success", "QR Code is valid!", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Invalid QR Code!", "OK");
                    }
                });
            }
        }

        private void OnStartScannerClicked(object sender, EventArgs e)
        {
            //cameraView.IsTorchOn = !cameraView.IsTorchOn;
        }
    }
}