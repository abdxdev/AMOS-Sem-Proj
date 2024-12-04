using Automated_Menu_Ordering_System.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class BranchesPage : Page
{
    public BranchesViewModel ViewModel
    {
        get;
    }

    public BranchesPage()
    {
        ViewModel = App.GetService<BranchesViewModel>();
        InitializeComponent();
    }
}
