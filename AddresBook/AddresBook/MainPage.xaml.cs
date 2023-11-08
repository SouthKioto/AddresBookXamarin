using AddresBook.Classes;
using AddresBook.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace AddresBook
{
    public partial class MainPage : ContentPage
    {
        protected ObservableCollection<AddresData> addresData;
        private int currentPage = 1;
        private int itemsPerPage = 5;

        public MainPage()
        {
            InitializeComponent();
            OnAppearing();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            addresData = new ObservableCollection<AddresData>(await App.Database.GetAll());
            var pageData = addresData.Take(itemsPerPage).ToList();
            listaAdresow.ItemsSource = pageData;
            UpdateProgressBar();
        }

        private void AddToDatabase(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddAdressPage());
        }

        private void EditDataPage(object sender, EventArgs e)
        {
            AddresData selected = listaAdresow.SelectedItem as AddresData;
            if (selected != null)
            {
                Navigation.PushAsync(new EditAdressPage(selected.id, selected.nazwisko, selected.imie, selected.telefon, selected.email));
            }
            else
            {
                DisplayAlert("Info", "Proszę wybrać wartość", "OK");
            }

        }

        private async void DeleteData(object sender, EventArgs e)
        {
            AddresData selected = listaAdresow.SelectedItem as AddresData;
            if (selected != null)
            {
                bool answer = await DisplayAlert("Question?", "Czy na pewno chcesz usunąć ten adres?", "Yes", "No");
                if (answer == true)
                {
                    AddresData adress = new AddresData();
                    adress.id = selected.id;
                    adress.nazwisko = selected.nazwisko;
                    adress.imie = selected.imie;
                    adress.telefon = selected.telefon;
                    adress.email = selected.email;

                    await App.Database.DeleteAddressData(adress);

                    addresData = new ObservableCollection<AddresData>(await App.Database.GetAll());
                    listaAdresow.ItemsSource = addresData;
                }
                else
                {
                    await DisplayAlert("Info", "Akcja została anulowana", "OK");
                }
            }
            else
            {
                await DisplayAlert("Info", "Proszę wybrać wartość", "OK");
            }
        }

        private void SearchFromList(object sender, TextChangedEventArgs e)
        {
            string search = searchBar.Text.ToLower();

            var filtered = addresData.Where(p =>
                p.imie.ToLower().Contains(search) ||
                p.nazwisko.ToLower().Contains(search) ||
                p.telefon.ToLower().Contains(search) ||
                p.email.ToLower().Contains(search)
            ).ToList();

            listaAdresow.ItemsSource = filtered;
        }

        //______________

        private void ListBeforeClick(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateListView();
            }
        }

        private void ListAfterClick(object sender, EventArgs e)
        {
            if (currentPage < (int)Math.Ceiling((double)addresData.Count / itemsPerPage))
            {
                currentPage++;
                UpdateListView();
            }
        }

        private void UpdateProgressBar()
        {
            int totalPages = (int)Math.Ceiling((double)addresData.Count / itemsPerPage);
            progressBar.Progress = (double)currentPage / totalPages;
        }

        private void UpdateListView()
        {
            int startIndex = (currentPage - 1) * itemsPerPage;
            var pageData = addresData.Skip(startIndex).Take(itemsPerPage).ToList();
            listaAdresow.ItemsSource = pageData;
            UpdateProgressBar();
        }
    }
}
