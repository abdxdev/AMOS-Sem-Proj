using Automated_Menu_Ordering_System.Activation;
using Automated_Menu_Ordering_System.Contracts.Services;
using Automated_Menu_Ordering_System.Core.Contracts.Services;
using Automated_Menu_Ordering_System.Core.Services;
using Automated_Menu_Ordering_System.Helpers;
using Automated_Menu_Ordering_System.Models;
using Automated_Menu_Ordering_System.Notifications;
using Automated_Menu_Ordering_System.Services;
using Automated_Menu_Ordering_System.ViewModels;
using Automated_Menu_Ordering_System.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;

namespace Automated_Menu_Ordering_System;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static UIElement? AppTitlebar { get; set; }

    public static DatabaseService DatabaseService => GetService<DatabaseService>();
    //public static DatabaseService? DatabaseService
    //{
    //    get; private set;
    //}

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Other Activation Handlers
            services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

            // Services
            services.AddSingleton<IAppNotificationService, AppNotificationService>();
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // DatabaseService Registration
            services.AddSingleton<DatabaseService>(provider =>
            {
                // Load the .env file
                string envFilePath = Path.Combine(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\..\..\..\")), ".env");
                if (File.Exists(envFilePath))
                {
                    DotNetEnv.Env.Load(envFilePath);
                }
                else
                {
                    throw new FileNotFoundException($"Environment file not found at: {envFilePath}");
                }

                // Get the connection string from environment variables
                string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION")
                    ?? throw new ArgumentNullException("DATABASE_CONNECTION", "Database connection string is not set in environment variables.");

                // Initialize DatabaseService with the connection string
                return new DatabaseService(connectionString);
            });

            // Core Services
            services.AddSingleton<IFileService, FileService>();

            // Views and ViewModels
            services.AddTransient<OrdersViewModel>();
            services.AddTransient<OrdersPage>();
            services.AddTransient<CartViewModel>();
            services.AddTransient<CartPage>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<DrinksViewModel>();
            services.AddTransient<DrinksPage>();
            services.AddTransient<DessertsViewModel>();
            services.AddTransient<DessertsPage>();
            services.AddTransient<BurgersViewModel>();
            services.AddTransient<BurgersPage>();
            services.AddTransient<PizzasViewModel>();
            services.AddTransient<PizzasPage>();
            services.AddTransient<HomeViewModel>();
            services.AddTransient<HomePage>();
            services.AddTransient<SigninViewModel>();
            services.AddTransient<SigninPage>();
            services.AddTransient<ShellPage>();
            services.AddTransient<ShellViewModel>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
        }).
        Build();

        App.GetService<IAppNotificationService>().Initialize();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        App.GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));

        await App.GetService<IActivationService>().ActivateAsync(args);
    }
}
