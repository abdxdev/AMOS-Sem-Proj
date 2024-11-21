using Automated_Menu_Ordering_System.ViewModels;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Npgsql;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class PizzasPage : Page
{
    public PizzasViewModel ViewModel
    {
        get;
    }

    public PizzasPage()
    {
        ViewModel = App.GetService<PizzasViewModel>();
        InitializeComponent();
        DataContext = this;
        FetchItemData();
    }

    private Item MakeItemFromReader(NpgsqlDataReader reader)
    {
        return new Item
        {
            Id = reader["id"].ToString(),
            Title = reader["name"].ToString(),
            ImageUrl = reader["image_url"].ToString(),
            Description = reader["description"].ToString(),
            AvgRating = reader["avg_rating"].ToString(),
            EstimatedTime = $"{reader["estimated_time"]} mins",
            Price = $"Rs. {reader["price"]}",
        };
    }

    private void FetchItemData()
    {
        try
        {
            var reader = App.GetService<DatabaseService>().get_product_by_category_and_subcategory("pizza");
            while (reader.Read())
            {
                var subcategory = reader["subcategory"].ToString();
                if (subcategory == "specialty_pizza")
                {
                    SpecialtyPizzasPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "classic_pizza")
                {
                    ClassicPizzasPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "meat_lover_pizza")
                {
                    MeatLoversPizzasPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
                else if (subcategory == "vegetarian_pizza")
                {
                    VegetarianPizzasPanel.Children.Add(new CardStyle1
                    {
                        Item = MakeItemFromReader(reader)
                    });
                }
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
