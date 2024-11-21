using Automated_Menu_Ordering_System.ViewModels;

using Microsoft.UI.Xaml.Controls;

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
    }
}
