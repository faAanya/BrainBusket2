using MauiApp1.DB;
using MauiApp1.MVVM;

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
    
    }

}
