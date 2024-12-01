using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public class Order
{
    public string? Id
    {
        get; set;
    }
    public string? Title
    {
        get; set;
    }
    public string? ImageUrl
    {
        get; set;
    }
    public string? EstimatedTime
    {
        get; set;
    }
    public string? Description
    {
        get; set;
    }
    public string? Price
    {
        get; set;
    }
}

public sealed partial class CartCard : UserControl
{
    public Order Order
    {
        get => (Order)GetValue(OrderProperty);
        set => SetValue(OrderProperty, value);
    }

    public static readonly DependencyProperty OrderProperty =
        DependencyProperty.Register("Order", typeof(Order), typeof(CartCard), new PropertyMetadata(null));

    public CartPage ParentPage
    {
        get; set;
    }

    public CartCard()
    {
        this.InitializeComponent();
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        int orderId = int.Parse(Order.Id);
        Debug.WriteLine(orderId);
        App.GetService<DatabaseService>().delete_order(orderId);
        ParentPage?.LoadOrders();
    }
}
