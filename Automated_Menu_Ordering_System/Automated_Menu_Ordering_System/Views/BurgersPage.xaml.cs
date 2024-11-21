using Automated_Menu_Ordering_System.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class BurgersPage : Page
{
    public BurgersViewModel ViewModel
    {
        get;
    }

    public BurgersPage()
    {
        ViewModel = App.GetService<BurgersViewModel>();
        InitializeComponent();
    }
}
