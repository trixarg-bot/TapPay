using Microsoft.Maui.Controls;
using Plugin.NFC;
using TapPay.Models;

namespace TapPay.Views
{
    public partial class NFCReaderPage : ContentPage
    {
        public NFCReaderPage()
        {
            InitializeComponent();
            CrossNFC.Current.OnMessageReceived += Current_OnMessageReceived;
        }

        private async void Current_OnMessageReceived(ITagInfo tagInfo)
        {
            if (tagInfo == null)
                return;

            string tagId = tagInfo.SerialNumber;
            var tarjeta = await App.Database.GetTarjetaByTagIdAsync(tagId);
            if (tarjeta != null)
            {
                BalanceLabel.Text = $"Saldo disponible: {tarjeta.Saldo}";
            }
        }

        private void OnReadNFCCicked(object sender, EventArgs e)
        {
            CrossNFC.Current.StartListening();
        }
    }
}

