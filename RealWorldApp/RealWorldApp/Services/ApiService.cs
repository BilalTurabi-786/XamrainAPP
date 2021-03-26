using Newtonsoft.Json;
using RealWorldApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net.Http.Headers;
using UnixTimeStamp;
using Xamarin.Forms;
using SQLite;
using RealWorldApp.Data;
using System.Reflection;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Org.Json;
using System.Collections;

namespace RealWorldApp.Services
{
    public class ApiService
    {
        private SQLiteConnection database;
        public ApiService()
        {
            try
            {

                database = DependencyService.Get<ISQLiteInterface>().GetSQLiteConnection();
                database.CreateTable<Register>();
                database.CreateTable<AddToCart>();
                database.CreateTable<ShoppingCartItem>();
                database.CreateTable<Order>();
                database.CreateTable<OrderDetail>();


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

       

        public string RegisterUser(string name , string email , string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };

            
            var data = database.Table<Register>();
            var d1 = data.Where(x => x.Name == register.Name && x.Email == register.Email).FirstOrDefault();
            if (d1 == null)
            {
                database.Insert(register);
                return "Sucessfully Added";
            }
            else
            {
                return "Already Mail id Exist";
            }
           
        }

        public string Login(string email , string password)
        {
            var login = new Register()
            {
                Email = email,
                Password = password
            };

            var data = database.Table<Register>();

            var chk = data.Where(x => x.Email == login.Email && x.Password == login.Password).First();

            if(chk == null)
            {
                return "Invalid Creditentials Email or Password";
            }
            else
            {
                Preferences.Set("userName" , chk.Name);
                Preferences.Set("userId", chk.ID);
                return "Correct Creditentials";
                
            }
        }

        

        public  List<Category> GetCategories()
        {
            
            string jsonFileName = "Category.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            var reader = new System.IO.StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<List<Category>>(jsonString);
        }

        public List<PopularProduct> GetPopularProducts()
        {
            string jsonFileName = "Product.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            var reader = new System.IO.StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            
            return JsonConvert.DeserializeObject<List<PopularProduct>>(jsonString);



        }
        public  List<Product> GetProductById(int productId)
        {
            string jsonFileName = "Product.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            var reader = new System.IO.StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            var response = JsonConvert.DeserializeObject<List<Product>>(jsonString);

            return response;
            
            
        }

        public List<Product> GetProductByCategory(int categoryId)
        {
            string jsonFileName = "Product.json";
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{jsonFileName}");
            var reader = new System.IO.StreamReader(stream);
            var jsonString = reader.ReadToEnd();
            var response = JsonConvert.DeserializeObject<List<Product>>(jsonString);

            return response;
            
        }

        public string AddItemsInCart(AddToCart addToCart)
        {
            var cart = new AddToCart()
            {
                CustomerId = addToCart.CustomerId,
                Price = addToCart.Price,
                ProductId = addToCart.ProductId,
                Qty = addToCart.Qty,
                TotalAmount = addToCart.TotalAmount
            };

            var shoppingcartitem = new ShoppingCartItem()
            {
                Price = Convert.ToDouble(addToCart.Price),
                ProductId = addToCart.ProductId,
                CustomerId = addToCart.CustomerId,
                Qty = Convert.ToInt32(addToCart.Qty),
                TotalAmount = Convert.ToDouble(addToCart.TotalAmount)

            };

            database.Insert(cart);
            database.Insert(shoppingcartitem);
            return "added";
        }


        public ICollection<ShoppingCartItem> GetCartSubTotal(int userId)
        {
            var data = database.Table<ShoppingCartItem>();

            var response = data.Where(x=> x.CustomerId == userId).ToList();
            return response;
          
        }

        public ICollection<ShoppingCartItem> GetShoppingCartItems(int userId)
        {
            var item = database.Table<ShoppingCartItem>();
            var data = item.Where(x => x.CustomerId == userId).ToList();

            return data;
        }

       
       

        public string ClearShoppingCart(int userId)
        {
            
           var data =  database.Table<ShoppingCartItem>();
            var item = data.Where(x => x.CustomerId == userId).ToList();
            foreach(var d in item)
            {

                database.Delete(d);
            }
            
            return "1";
           
        }

        public string PlaceOrder(Order order)
        {
            var cartitem = database.Table<ShoppingCartItem>();
            
            order.isOrderCompleted = false;
            order.orderPlaced = DateTime.Now;

            database.Insert(order);

            var shoppingcartitem = cartitem.Where(x => x.CustomerId == order.userId);

            foreach(var item in shoppingcartitem)
            {
                var orderDetail = new OrderDetail()
                {
                    price = item.Price,
                    totalAmount = item.TotalAmount,
                    qty = item.Qty,
                    productId = item.ProductId,
                    orderId = item.id
                };
                database.Insert(orderDetail);
            }


            string response = order.id.ToString();

            return response;
        }
      

        public List<Order> GetOrdersByUser(int userId)
        {
            var data = database.Table<Order>();
            var response = data.Where(x => x.userId == userId).OrderByDescending(s => s.orderPlaced).ToList();
            return response;
           
        }

        public ICollection<OrderDetail> GetOrderDetails(int orderId)
        {
            var data = database.Table<OrderDetail>();
            var response = data.Where(x => x.id == orderId).ToList();
            return response;
            
        }
    }

    
}

