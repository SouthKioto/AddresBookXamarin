using AddresBook.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AddresBook.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAdressPage : ContentPage
    {
        public AddAdressPage()
        {
            InitializeComponent();
        }

        private void CancelAdd(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

        private void CleanAllEntry(object sender, EventArgs e)
        {
            nazwisko.Text = string.Empty;
            imie.Text = string.Empty;
            telefon.Text = string.Empty;
            email.Text = string.Empty;
        }

        private void AddToDatabase(object sender, EventArgs e)
        {
            AddresData adres = new AddresData();
            adres.nazwisko = nazwisko.Text;
            adres.imie = imie.Text;
            adres.telefon = telefon.Text;
            adres.email = email.Text;

            App.Database.AddAddressData(adres);

            Navigation.PushAsync(new MainPage());

        }
    }
}