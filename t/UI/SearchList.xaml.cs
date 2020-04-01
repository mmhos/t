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
    public partial class SearchList : ContentPage
    {
        public SearchList()
        {
            InitializeComponent();
        }
		private void OnValueChanged(object sender, TextChangedEventArgs e)
		{
			//viewModel.Search();
		}

		private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
		//	var personInfo = e.SelectedItem as Person;
		//	var employeeView = new EmployeeXaml
		//	{
		//		BindingContext = new PersonViewModel(personInfo, favoritesRepository)
		//	};

		//	Navigation.PushAsync(employeeView);
		//
		}
	}
}