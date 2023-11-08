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
    public partial class EditAdressPage : ContentPage
    {
        protected int ID;
        protected string Nazwisko, Imie, Telefon, Email;

        public EditAdressPage(int id, string surname, string name, string phone, string mail)
        {
            InitializeComponent();
            ID = id;
            nazwisko.Text = Nazwisko = surname;
            imie.Text = Imie = name;
            telefon.Text = Telefon = phone;
            email.Text = Email = mail;
        }

        private void EditData(object sender, EventArgs e)
        {
            AddresData addres = new AddresData();
            addres.id = ID;
            addres.nazwisko = nazwisko.Text;
            addres.imie = imie.Text;
            addres.telefon = telefon.Text;
            addres.email = email.Text;

            App.Database.UpdateAddressData(addres);

            Navigation.PushAsync(new MainPage());
        }


        private void CancelEdit(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainPage());
        }

    }
}