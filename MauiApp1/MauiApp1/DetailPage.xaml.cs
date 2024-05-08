using MauiApp1.DB;
using MauiApp1.MVVM;
using Microsoft.Maui.Graphics.Converters;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    private readonly MainViewModel viewModel;

    public DetailPage(MainViewModel mainViewModel)
    {
        InitializeComponent();

        BindingContext = mainViewModel;
        viewModel = mainViewModel;

        picker.SelectedItem = viewModel.Categories[viewModel.OperationProduct.ProductCategoryId];

    }

    private async void Back_Button_Clicked(object sender, EventArgs e)
    {
        viewModel.SaveProductAsync();
        await Shell.Current.GoToAsync("..");
    }
    void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {

        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {

            ColorTypeConverter converter = new ColorTypeConverter();

            viewModel.OperationProduct.ProductCategoryColor = (converter.ConvertToString(viewModel.Categories[selectedIndex].CategoryColor));
            viewModel.OperationProduct.ProductCategoryId = selectedIndex;
        }
    }
}