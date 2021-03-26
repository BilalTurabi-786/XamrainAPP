using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        Register register = new Register();
        ApiService apiService = new ApiService();
        public SignupPage()
        {
            InitializeComponent();
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            if (!EntPassword.Text.Equals(EntConfirmPassword.Text))
            {
                await DisplayAlert("Password mismatch", "Please check your confirm password", "Cancel");
            }
            else
            {
                
                var returnvalue = apiService.RegisterUser(EntName.Text , EntEmail.Text , EntPassword.Text);
                if(returnvalue == "Sucessfully Added")
                {
                    await DisplayAlert("Hi", "Your account has been created", "Alright");
                    await Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert("Oops", "Something went wrong", "Cancel");
                }
            }
            //if (!EntPassword.Text.Equals(EntConfirmPassword.Text))
            //{
            //    await DisplayAlert("Password mismatch", "Please check your confirm password", "Cancel");
            //}
            //else
            //{
            //    var register = (Register)BindingContext;
            //    if (!string.IsNullOrEmpty(register.Name) && !string.IsNullOrEmpty(register.Email) && !string.IsNullOrEmpty(register.Password))
            //    {
            //        await App.Database.RegisterUser(register);
            //        await DisplayAlert("Hi", "Your account has been created", "Alright");
            //        await Navigation.PushModalAsync(new LoginPage());
            //    }
            //    else
            //    {
            //        await DisplayAlert("Oops", "Something went wrong", "Cancel");
            //    }
            //}

            //var response = await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
            //if (response)
            //{
            //    await DisplayAlert("Hi", "Your account has been created", "Alright");
            //    await Navigation.PushModalAsync(new LoginPage());
            //}
            //else
            //{
            //    await DisplayAlert("Oops", "Something went wrong", "Cancel");
            //}

        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new LoginPage());
        }
    }
}