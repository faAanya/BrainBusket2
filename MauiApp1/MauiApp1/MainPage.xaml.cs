using MauiApp1.MVVM;
using Microsoft.Maui.Graphics.Converters;
using System;

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

            picker.SelectedItem = viewModel.Categories[viewModel.Categories.Count - 1];

         
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
            
         
            int selectedIndex = picker.SelectedIndex;
            picker.SelectedItem = viewModel.Categories[selectedIndex];
            if (selectedIndex != -1)
            {
              
                ColorTypeConverter converter = new ColorTypeConverter();

                viewModel.OperationProduct.ProductCategoryColor = (converter.ConvertToString(viewModel.Categories[selectedIndex].CategoryColor));
                viewModel.OperationProduct.ProductCategoryId = selectedIndex;
                
            }
          
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ColorTypeConverter converter = new ColorTypeConverter();

            picker.SelectedItem = viewModel.Categories[picker.SelectedIndex];
            viewModel.OperationProduct.ProductCategoryColor = (converter.ConvertToString(viewModel.Categories[picker.SelectedIndex].CategoryColor));
            viewModel.OperationProduct.ProductCategoryId = picker.SelectedIndex;
        }
    }

}
