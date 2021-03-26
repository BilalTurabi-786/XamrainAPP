using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        public ObservableCollection<OrderDetail> OrderDetailCollection;
        public ApiService ApiService = new ApiService();
        public OrderDetailPage(int orderId)
        {
            InitializeComponent();
            OrderDetailCollection = new ObservableCollection<OrderDetail>();
            GetOrderDetail(orderId);
        }
        private void GetOrderDetail(int orderId)
        {
            var orders = ApiService.GetOrderDetails(orderId);
            var orderDetails = orders.ToList();
            foreach (var item in orderDetails)
            {
                OrderDetailCollection.Add(item);
            }

            LvOrderDetail.ItemsSource = OrderDetailCollection;

            LblTotalPrice.Text = OrderDetailCollection.Select(x => x.totalAmount).Sum() + " $ ";
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}