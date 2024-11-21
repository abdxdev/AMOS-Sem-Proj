using Automated_Menu_Ordering_System.ViewModels;

using Microsoft.UI.Xaml.Controls;

namespace Automated_Menu_Ordering_System.Views;

public sealed partial class SigninPage : Page
{
    public SigninViewModel ViewModel
    {
        get;
    }

    public SigninPage()
    {
        ViewModel = App.GetService<SigninViewModel>();
        InitializeComponent();
    }
}
