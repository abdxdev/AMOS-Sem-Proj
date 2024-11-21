using Automated_Menu_Ordering_System.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class DessertsPage : Page
{
    public DessertsViewModel ViewModel
    {
        get;
    }

    public DessertsPage()
    {
        ViewModel = App.GetService<DessertsViewModel>();
        InitializeComponent();
    }
}
