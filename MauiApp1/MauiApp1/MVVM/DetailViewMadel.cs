using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace MauiApp1.MVVM
{

    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel : ObservableObject
    {
        [ObservableProperty]
        string text;

        [ObservableProperty]
        string? productName;
        [ObservableProperty]
        string? description;
        [ObservableProperty]
        string? category;
        [ObservableProperty]
        string? amount;

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
