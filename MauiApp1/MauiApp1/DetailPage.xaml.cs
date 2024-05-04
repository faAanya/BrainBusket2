using MauiApp1.DB;
using MauiApp1.MVVM;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    private readonly MainViewModel viewModel;

    public DetailPage(MainViewModel mainViewModel)
    {
        InitializeComponent();

        BindingContext = mainViewModel;
        viewModel = mainViewModel;

    }

    private async void Back_Button_Clicked(object sender, EventArgs e)
    {
        viewModel.SaveProductAsync();
        await Shell.Current.GoToAsync("..");
    }
    
}