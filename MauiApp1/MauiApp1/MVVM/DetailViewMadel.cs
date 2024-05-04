using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace MauiApp1.MVVM
{

    [QueryProperty("Text", "Text")]
    public partial class DetailViewModel : ObservableObject
    {
        //[RelayCommand]
        //async Task GoBack()
        //{
        //    await Shell.Current.GoToAsync("..");
        //}
       
    }
}
