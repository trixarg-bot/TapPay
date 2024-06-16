// using System;
// using Microsoft.Maui.Controls;
// using TapPay.Models;

// namespace TapPay.Views
// {
//     public partial class SalePage : ContentPage
//     {
//         private TarjetaNFC _currentTarjeta;

//         public SalePage(TarjetaNFC tarjeta)
//         {
//             InitializeComponent();
//             _currentTarjeta = tarjeta;
//             LoadProducts();
//         }

//         private async void LoadProducts()
//         {
//             var products = await App.Database.GetProductosConExistenciaAsync();
//             ProductsListView.ItemsSource = products;
//         }

//         private async void OnSaleClicked(object sender, EventArgs e)
//         {
//             var selectedProduct = ProductsListView.SelectedItem as Producto;
//             if (selectedProduct == null)
//             {
//                 await DisplayAlert("Error", "Por favor seleccione un producto.", "OK");
//                 return;
//             }

//             if (_currentTarjeta.Saldo < selectedProduct.Precio)
//             {
//                 await DisplayAlert("Error", "Saldo insuficiente en la tarjeta.", "OK");
//                 return;
//             }

//             // Realizar la transacción
//             _currentTarjeta.Saldo -= selectedProduct.Precio;
//             selectedProduct.Existencia--;

//             var transaccion = new Transaccion
//             {
//                 TarjetaId = _currentTarjeta.TarjetaId,
//                 ProductoId = selectedProduct.ProductoId,
//                 FechaHora = DateTime.Now,
//                 Monto = selectedProduct.Precio,
//                 Estado = "Completada"
//             };

//             await App.Database.SaveTransaccionAsync(transaccion);
//             await App.Database.UpdateTarjetaAsync(_currentTarjeta);
//             await App.Database.UpdateProductoAsync(selectedProduct);

//             await DisplayAlert("Éxito", "La transacción se ha completado.", "OK");

//             // Actualiza la UI
//             BalanceLabel.Text = $"Saldo disponible: {_currentTarjeta.Saldo}";
//             LoadProducts();
//         }
//     }
// }
