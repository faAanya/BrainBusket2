
using MauiApp1.DB;
using MauiApp1.MVVM;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics.Converters;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel viewModel;
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
           
            BindingContext = mainViewModel;
            viewModel = mainViewModel;
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
           
            await viewModel.LoadProductsAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DetailPage));
            //viewModel.TapCommand();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            
           var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
              
                ColorTypeConverter converter = new ColorTypeConverter();

                viewModel.OperationProduct.ProductCategoryColor = (converter.ConvertToString(viewModel.Categories[selectedIndex].CategoryColor));
                
            }
        }
    }

}
