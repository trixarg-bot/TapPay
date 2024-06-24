using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace TapPay.Views
{
   public partial class NavigationHelper : ContentPage
    {

    
        public NavigationHelper()
        {
            InitializeComponent();
        }

         public static async Task NavigateWithLoadingIndicatorAsync(Page currentPage, Page nextPage, ActivityIndicator loadingIndicator)
        {
                await currentPage.Navigation.PushAsync(nextPage);
                loadingIndicator.IsRunning = false;
                loadingIndicator.IsVisible = false;
        }
    }
}