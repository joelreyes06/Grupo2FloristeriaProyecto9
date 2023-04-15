using FloristeriaProyecto.Modelo;
using FloristeriaProyecto.Service;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FloristeriaProyecto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCompras : ContentPage
    {
        public Compra Site;
        bool editando = false;

        public List<Compra> oListaCompra { get; set; }
        public PageCompras()
        {
            InitializeComponent();
            ObtenerCompra();
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (editando)
            {
                

                editando = false;

                Site = null;
            }
        }

        private async void ObtenerCompra()
        {
            List<Compra> oObjecto = await ApiServiceFirebase.ObtenerCompra();

            if (oObjecto != null)
            {
                oListaCompra = oObjecto;
                ListViewCompra.ItemsSource = oObjecto;
            }

        }


        private async void listSites_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Site = e.Item as Compra;


            }
            catch (Exception ex)
            {
                Message("Error:", ex.Message);
            }

        }

        private async void Message(string title, string message)
        {
            await DisplayAlert(title, message, "OK");
        }

        private void ListViewCompra_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (!Application.Current.Properties.ContainsKey("contador"))
            {
                Application.Current.Properties.Add("contador", 1);

                CrossLocalNotifications.Current.Show("Floristeria Margaritas", "Tu pedido ha llegado, Que lo Disfrutes!!.", 4, DateTime.Now.AddSeconds(1));
            }
        }
    }
}