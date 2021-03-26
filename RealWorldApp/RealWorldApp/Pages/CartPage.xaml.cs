using Newtonsoft.Json;
using RealWorldApp.Models;
using RealWorldApp.Services;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<ShowCartItem> ShoppingCartCollection;
        ApiService ApiService = new ApiService();
        public CartPage()
        {
            InitializeComponent();
            ShoppingCartCollection = new ObservableCollection<ShowCartItem>();
            GetShoppingCartItems();
            GetTotalPrice();
        }

        private void GetTotalPrice()
        {
            var totalPrice = ApiService.GetCartSubTotal(Preferences.Get("userId", 0));

            var data = totalPrice.Select(x => x.TotalAmount).Sum();
            
            LblTotalPrice.Text = data.ToString();
        }

        private void GetShoppingCartItems()
        {
            var shoppingCartItems = ApiService.GetShoppingCartItems(Preferences.Get("userId", 0));

            string jsonFileName = "Product.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            var reader = new System.IO.StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            var response = JsonConvert.DeserializeObject<List<Product>>(jsonString);

            var data = (from r in response
                        join i in shoppingCartItems on r.id equals i.ProductId
                        select new
                        {
                            i.id,
                            i.Price,
                            i.Qty,
                            r.name,
                            i.TotalAmount
                        }).ToList();

            List<ShowCartItem> ob = new List<ShowCartItem>();
            foreach(var item in data)
            {

                ShowCartItem clr = new ShowCartItem();
                clr.id = item.id;
                clr.Price = item.Price;
                clr.Qty = item.Qty;
                clr.TotalAmount = item.TotalAmount;
                clr.productName = item.name;

                ShoppingCartCollection.Add(clr);

            }
            
            LvShoppingCart.ItemsSource = ShoppingCartCollection;
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void TapClearCart_Tapped(object sender, EventArgs e)
        {
            var response = ApiService.ClearShoppingCart(Preferences.Get("userId", 0));
            
            if (response == "1")
            {
                DisplayAlert("", "Your cart has been cleared", "Alright");
                LvShoppingCart.ItemsSource = null;
                LblTotalPrice.Text = "0";
            }
            else
            {
                DisplayAlert("", "Something went wrong", "Cancel");
            }
        }

        private void BtnProceed_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(LblTotalPrice.Text)));
        }
    }
}