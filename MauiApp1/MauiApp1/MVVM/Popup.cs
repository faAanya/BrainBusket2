using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.DB;
using MauiApp1.MVVM;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1
{
    public class PickerViewModel : ObservableObject, INotifyPropertyChanged
    {
        public List<Category> categories {  get; set; }
        public MainViewModel viewModel;

        public PickerViewModel(MainViewModel mainViewModel)
        {
            viewModel = mainViewModel;
            
            categories = viewModel.Categories.ToList();
        }

        public Category _category;
        public Category SelectedCategory
        {
            get { return _category; }
            set { _category = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if(PropertyChanged == null) {
                return;
            }
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
