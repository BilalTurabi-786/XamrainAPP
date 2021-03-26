using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage
    {
        private double _totalPrice;
        public ApiService ApiService = new ApiService();
        public PlaceOrderPage(double totalPrice)
        {
            InitializeComponent();
            _totalPrice = totalPrice;
        }

        private void BtnPlaceOrder_Clicked(object sender, EventArgs e)
        {
            var order = new Order();
            order.fullName = EntName.Text;
            order.phone = EntPhone.Text;
            order.address = EntAddress.Text;
            order.isOrderCompleted = false;
            order.orderPlaced = DateTime.Now;
            order.userId = Preferences.Get("userId", 0);
            order.orderTotal = _totalPrice;

           
            var response = ApiService.PlaceOrder(order);
            if (response!=null)
            {
                DisplayAlert("", "Your Order Id is " + response, "Alright");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}