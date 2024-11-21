using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class CartCard : UserControl
{
    public Order Order
    {
        get => (Order)GetValue(OrderProperty);
        set => SetValue(OrderProperty, value);
    }

    public static readonly DependencyProperty OrderProperty =
        DependencyProperty.Register("Order", typeof(Order), typeof(CartCard), new PropertyMetadata(null));

    public CartCard()
    {
        this.InitializeComponent();
    }
}
