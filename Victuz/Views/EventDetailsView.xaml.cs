using Victuz.Models;
using ZXing.Net.Maui;
using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;
using System.Drawing;
using System.Drawing.Imaging;
namespace Victuz.Views;

public partial class EventDetailsView : ContentPage
{
    private Event _event;

    public EventDetailsView(Event selectedEvent)
    {
        InitializeComponent();
        BindingContext = selectedEvent;

    }

    private async void OnJoinEventClicked(object sender, EventArgs e)
    {
        try
        {
            // Voeg de gebruiker toe aan de deelnemerslijst
            var participation = new Participation
            {
                EventID = _event.ID,
                UserID = 1, 
                JoinDate = DateTime.Now
            };

            await App.Database.SaveItemAsync(participation);

            // QR-Code inhoud: combinatie van gebruiker en event
            var qrContent = $"Event: {_event.Title}, User: {1}, Date: {_event.Date}";

            // QR-Code genereren
            var qrCode = GenerateQRCode(qrContent);

            // QR-Code opslaan in de database
            var qrCodeModel = new QRCode
            {
                EventID = _event.ID,
                UserID = 1, // TODO: Vervang met de ingelogde gebruiker-ID
                QRCodeContent = qrContent,
                QRCodeImage = qrCode // Opslaan als byte-array
            };
            // Stuur gebruiker naar de QR-codepagina
            await Navigation.PushAsync(new QRCodeView(qrCode));
            await App.Database.SaveItemAsync(qrCodeModel);

            // QR-Code succesvol gegenereerd en opgeslagen
            await DisplayAlert("Success", "You have joined the event! QR-Code generated.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to join event: {ex.Message}", "OK");
        }
    }
    private byte[] GenerateQRCode(string content)
    {
        var writer = new ZXing.BarcodeWriterPixelData
        {
            Format = ZXing.BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions
            {
                Width = 300,
                Height = 300,
                Margin = 1
            }
        };

        var pixelData = writer.Write(content);

        using (var bitmap = new SKBitmap(pixelData.Width, pixelData.Height, SKColorType.Rgba8888, SKAlphaType.Premul))
        {
            bitmap.InstallPixels(pixelData.Pixels);

            using (var stream = new MemoryStream())
            {
                bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
                return stream.ToArray(); // Converteer naar een byte-array
            }
        }
    }
    private SKBitmap ByteArrayToBitmap(byte[] imageData)
    {
        using (var stream = new MemoryStream(imageData))
        {
            return SKBitmap.Decode(stream);
        }
    }
    

    // Back knop
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    
   
}