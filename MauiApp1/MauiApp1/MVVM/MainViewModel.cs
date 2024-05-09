
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.DB;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace MauiApp1.MVVM
{

    public partial class MainViewModel : ObservableObject, INotifyPropertyChanged
    {

        public MainViewModel viewModel;

       
        private readonly LocalDBService DBService;
        public MainViewModel(LocalDBService db)
        {
            DBService = db;

            myBorderBackgroundColor = Color.FromArgb("#FFFF");

            IsLoaded = true;
        }

        [ObservableProperty]
        public Color myBorderBackgroundColor;


        [ObservableProperty]
        private ObservableCollection<Product> items = new();

      

        [ObservableProperty]
        private ObservableCollection<Product> boughtItems = new();

        [ObservableProperty]
        private ObservableCollection<Category> categories = new()
        {
            
            new Category()
            {
                CategoryId = 0,
                CategoryName = "Овощи",
                CategoryColor = Color.FromRgba("#8BB98E")
            },new Category()
            {
            CategoryId = 1,
                CategoryName = "Фрукты",
                CategoryColor = Color.FromRgba("#fffff")
            },new Category()
            {
            CategoryId = 2,
                CategoryName = "Выпечка",
                CategoryColor = Color.FromRgba("#FFCF88")
            },new Category()
            { 
             CategoryId = 3,
                CategoryName = "Мясо и рыба",
                CategoryColor = Color.FromRgba("#FF7979")
            },new Category()
            {
             CategoryId = 4,
                CategoryName = "Молочные изделия",
                CategoryColor = Color.FromRgba("#FF7979")
            },new Category()
            {
             CategoryId = 5,
                CategoryName = "Быт и гигиена",
                CategoryColor = Color.FromRgba("#FF8BCA")
            },new Category()
            {
             CategoryId = 6,
                CategoryName = "Сладости",
                CategoryColor = Color.FromRgba("#B88BB9")
            }
            ,new Category()
            {
             CategoryId = 7,
                CategoryName = "Другое",
                CategoryColor = Color.FromRgba("#cfcdc8")
            }
        };



     
        [ObservableProperty]
        private Product operationProduct = new();

        [ObservableProperty]
        private Category operationCategory = new();

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string busyText;

        [ObservableProperty]
        private bool isLoaded = true;


        [RelayCommand]
        private void SortItems()
        {
            var items2 = Items.OrderBy(x => x.ProductCategoryId);
            Items = new ObservableCollection<Product>();

            foreach (var item in items2)
            {
                Items.Add(item);
            }
        }


        [RelayCommand]
        public async Task SetProductState(Product product)
        {
            product.IsBought = !product.IsBought;
            if (product.IsBought == true)
            {

                Items.Remove(product);
                BoughtItems.Add(product);
            }
            else
            {
                Items.Add(product);
                BoughtItems.Remove(product);
            }
            await ExecuteAsync(async () =>
            {

                await DBService.UpdateItemAsunc<Product>(product);
                var productCopy = product.Clone();
                product = new();
                SetOperatingProductCommand.Execute(new());

            }, busyText);


          
        }

        public async Task LoadProductsAsync()
        {
           
            await ExecuteAsync(async() =>
            {
                var products = await DBService.GetAllAsync<Product>();

                if (products is not null && products.Any())
                {
                    Items ??= new ObservableCollection<Product>();
                    BoughtItems ??= new ObservableCollection<Product>();
                }
                  

                foreach (var product in products)
                {   
                   
                    if(product.IsBought == true  && IsLoaded/*&& BoughtItems.Contains(product)*/)
                    
                    {
                        Items.Remove(product);
                        BoughtItems.Add(product);
                    }
                    else if (!product.IsBought == true && IsLoaded/*&& Items.Contains(product)*/)
                    {
                        Items.Add(product);
                    }
                    else
                    {
                        continue;
                    }
                   
                }
                if (IsLoaded)
                {
                    SortItems();
                }
                IsLoaded = false;
            }, "Fetchnig products");
        }

        [RelayCommand]
        private void SetOperatingProduct(Product? product) => OperationProduct = product ?? new();



        [RelayCommand]
        
        public async Task SaveProductAsync()
        {
            if (OperationProduct is null)
                return;

            var (isValid, errorMessage) = OperationProduct.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Ошибка", errorMessage, "Ок");
                return;
            }

            var busyText = OperationProduct.Id == 0 ? "Creating Product" : "Updating product";
            await ExecuteAsync(async () =>
            {
                if (OperationProduct.Id == 0)
                {
                    await DBService.AddItemAsunc<Product>(OperationProduct);
                    Items.Add(OperationProduct);
                }
                else
                {
                    await DBService.UpdateItemAsunc<Product>(OperationProduct);


                    var productCopy = OperationProduct.Clone();
                    var index = Items.IndexOf(OperationProduct);
                    Items.RemoveAt(index);

                    Items.Insert(index, productCopy);
                }
                OperationProduct = new();
                SetOperatingProductCommand.Execute(new());
            }, busyText);

        }



        [RelayCommand]
        private async Task DeleteProductAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await DBService.DeleteByKeyItemAsunc<Product>(id))
                {
                    var unBoughtProduct = Items.FirstOrDefault(x => x.Id == id);
                    var BoughtProduct = BoughtItems.FirstOrDefault(x => x.Id == id);


                    if (Items.Contains(unBoughtProduct) && !BoughtItems.Contains(unBoughtProduct))
                    {
                        Items.Remove(unBoughtProduct);
                    }
                    else if(!Items.Contains(BoughtProduct) && BoughtItems.Contains(BoughtProduct))
                    {
                        BoughtItems.Remove(BoughtProduct);
                    }  
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Product was not deleted", "Ok");
                }
            }, "Deleting product");

          
            
        }

        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing";
            try
            {
                 await operation?.Invoke();
            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing";
            }
        }

        //public async Task LoadCategoriesAsync()
        //{
        //    await ExecuteAsync(async () =>
        //    {
        //        var categories = await DBService.GetAllAsync<Category>();

        //        if (categories is not null && categories.Any())
        //        {
        //            Categories ??= new ObservableCollection<Category>();
                 
        //        }


        //        foreach (var category in categories)
        //        {
        //            Categories.Add(category);
        //        }


        //    }, "Fetchnig products");
        //}

        //[RelayCommand]
        //private void SetOperatingCategory(Category? category) => OperationCategory = category ?? new();



        //[RelayCommand]
        //public async Task SaveCategoryAsync()
        //{
        //    if (OperationCategory is null)
        //        return;




        //    var busyText = OperationCategory.CategoryId == 0 ? "Creating Product" : "Updating product";
        //    await ExecuteAsync(async () =>
        //    {
        //        if (OperationCategory.CategoryId == 0)
        //        {
        //            await DBService.AddItemAsunc<Category>(OperationCategory);
        //            Categories.Add(OperationCategory);
        //        }
        //        else
        //        {
        //            await DBService.UpdateItemAsunc<Category>(OperationCategory);


        //            var categoryCopy = OperationCategory.Clone();
        //            var index = Categories.IndexOf(OperationCategory);
        //            Categories.RemoveAt(index);

        //            Categories.Insert(index, categoryCopy);
        //        }
        //        OperationCategory = new();
        //        SetOperatingCategoryCommand.Execute(new());
        //    }, busyText);

        //}


        //[RelayCommand]
        //private async Task DeleteCategoryAsync(int id)
        //{
        //    await ExecuteAsync(async () =>
        //    {
        //        if (await DBService.DeleteByKeyItemAsunc<Category>(id))
        //        {
        //            var category = Categories.FirstOrDefault(x => x.CategoryId == id);
        //            Categories.Remove(category);
        //        }
        //        else
        //        {
        //            await Shell.Current.DisplayAlert("Delete Error", "Product was not deleted", "Ok");
        //        }
        //    }, "Deleting product");



        //}
        public async void TapCommand()
        {
   
            OperationProduct.Description = await Shell.Current.DisplayPromptAsync(OperationProduct.Name, "Описание", "Ok", null, null);
            SaveProductAsync();
            
        }
    }


}
