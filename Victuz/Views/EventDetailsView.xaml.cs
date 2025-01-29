using Victuz.Models;
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
        /*
        var writer = new ZXing.Common.BarcodeWriter
        {
            Format = ZXing.BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions
            {
                Width = 300,
                Height = 300
            }
        };

        // Genereer QR-code als bitmap
        var bitmap = writer.Write(content);

        using (var stream = new MemoryStream())
        {
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
        */
        return null;
    }

    // Back knop
    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    
   
}