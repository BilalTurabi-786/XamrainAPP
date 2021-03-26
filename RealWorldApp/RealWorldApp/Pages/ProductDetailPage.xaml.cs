using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private int _productId;
        private ApiService ApiService = new ApiService();
        public ObservableCollection<PopularProduct> ProductsCollection;
        public ProductDetailPage(int productId)
        {
            InitializeComponent();
            GetProductDetails(productId);
            ProductsCollection = new ObservableCollection<PopularProduct>();
            _productId = productId;
        }

        public ProductDetailPage()
        {
        }

        private void GetProductDetails(int productId)
        {
            var product = ApiService.GetProductById(productId);
            
            foreach(var item in product.Where(s => s.id == productId))
            {
                LblName.Text = item.name;
                LblDetail.Text = item.detail;
                ImgProduct.Source = item.FullImageUrl;
                LblPrice.Text = item.price.ToString();
                LblTotalPrice.Text = LblPrice.Text;
            }

        }

        private void TapIncrement_Tapped(object sender, EventArgs e)
        {
            var i = Convert.ToInt16(LblQty.Text);
            i++;
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text)).ToString();

        }

        private void TapDecrement_Tapped(object sender, EventArgs e)
        {
            var i = Convert.ToInt16(LblQty.Text);
            i--;
            if (i < 1)
            {
                return;
            }
            LblQty.Text = i.ToString();
            LblTotalPrice.Text = (Convert.ToInt16(LblQty.Text) * Convert.ToInt16(LblPrice.Text)).ToString();
        }

        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void BtnAddToCart_Clicked(object sender, EventArgs e)
        {
            var addToCart = new AddToCart();
            addToCart.Qty = LblQty.Text;
            addToCart.Price = LblPrice.Text;
            addToCart.TotalAmount = LblTotalPrice.Text;
            addToCart.ProductId = _productId;
            addToCart.CustomerId = Preferences.Get("userId", 0);

            var response = ApiService.AddItemsInCart(addToCart);
            if (response == "added")
            {
                await DisplayAlert("", "Your items has been added to the cart", "Alright");
            }
            else
            {
                await DisplayAlert("Oops", "Something went wrong", "Cancel");
            }
        }
    }
}