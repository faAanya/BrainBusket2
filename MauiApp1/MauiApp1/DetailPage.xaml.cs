using MauiApp1.DB;
using MauiApp1.MVVM;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
    private readonly LocalDBService localDBService;
	private int editProductId;



 //   public DetailPage(DetailViewModel detailViewModel, LocalDBService db)
	//{
	//	InitializeComponent();
	//	BindingContext = detailViewModel;
 //       localDBService = db;

	//	Task.Run(async()=> listView.ItemsSource = await db.GetProduct());
 //   }

	//private async void saveButton_Clicked(object sender, EventArgs e)
	//{
	//	if(editProductId == 0) 
	//	{
	//		await localDBService.Create(new Product
	//		{
	//			Description = descriptionEntryField.Text,
	//			Category = categoryEntryField.Text,
	//			amount = int.Parse(amountEntryField.Text)
	//		});
	//	}
	//	else
	//	{
	//		await localDBService.Update(new Product
	//		{
	//			Description = descriptionEntryField.Text,
	//			Category = categoryEntryField.Text,
 //               amount = int.Parse(amountEntryField.Text)
 //           });
 //       }
	//	//descriptionEntryField.Text = string.Empty;
	//	//categoryEntryField.Text = string.Empty;
	//	//amountEntryField.Text = 0.ToString();

	//	listView.ItemsSource = await localDBService.GetProduct();
	//}

	//private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
	//{
	//	var product = (Product)e.Item;
		
	//	var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

	//	switch (action)
	//	{
	//		case "Edit":
	//			editProductId = product.Id;
	//			descriptionEntryField.Text = product.Description;
	//			categoryEntryField.Text = product.Category;
	//			amountEntryField.Text = product.amount.ToString();
	//			break;
	//		case "Delete":
	//			await localDBService.Delete(product);
	//			listView.ItemsSource = await localDBService.GetProduct();
	//			break;

	//	}
	//}
}