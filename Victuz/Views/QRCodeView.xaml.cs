using Camera.MAUI;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
namespace Victuz.Views;

public partial class QRCodeView : ContentPage
{
    public QRCodeView(byte[] qrCodeImage)
    {
        InitializeComponent();
        QRCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeImage));
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}