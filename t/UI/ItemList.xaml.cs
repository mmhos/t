using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace t.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemList : ContentPage
    {
        public ItemList()
        {
            InitializeComponent();
        }
        protected  override void OnAppearing()
        {
            //base.OnAppearing();

            //if (LoginViewModel.ShouldShowLogin(App.LastUseTime))
            //    await Navigation.PushModalAsync(new LoginXaml());

            //favoritesRepository = await XmlFavoritesRepository.OpenIsolatedStorage("XamarinFavorites.xml");

            //if (favoritesRepository.GetAll().Count() == 0)
            //    favoritesRepository = await XmlFavoritesRepository.OpenFile("XamarinFavorites.xml");

            //viewModel = new FavoritesViewModel(favoritesRepository, true);

            //listView.ItemsSource = viewModel.Groups;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var person = e.SelectedItem as Person;
            //var employeeView = new EmployeeXaml
            //{
            //    BindingContext = new PersonViewModel(person, favoritesRepository)
            //};

            //Navigation.PushAsync(employeeView);
        }
    }
}