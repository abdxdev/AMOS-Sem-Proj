using Automated_Menu_Ordering_System.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class DrinksPage : Page
{
    public DrinksViewModel ViewModel
    {
        get;
    }

    public DrinksPage()
    {
        ViewModel = App.GetService<DrinksViewModel>();
        InitializeComponent();
    }
}
