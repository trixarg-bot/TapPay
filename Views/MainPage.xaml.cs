using Microsoft.Maui.Controls;
using TapPay.Models;
 
namespace TapPay
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public int usuario_id;
        public MainPage(int usuario_id)
        {
            InitializeComponent();
            this.usuario_id = usuario_id;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void OnViewDataClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.DataPage(usuario_id));
        }

         
    

    }
}
