using System.Diagnostics;
using ACOMPlugin.Core;
using CustomExtensions.WinUI;
using DryIoc;
using DiFactory = ShadowPluginLoader.WinUI.DiFactory;

namespace ACOMv2;

public partial class App : Application
{
    public static Window MainWindow = Window.Current;
    public IServiceProvider Services { get; }
    public new static App Current => (App)Application.Current;
    public IJsonNavigationViewService GetJsonNavigationViewService => GetService<IJsonNavigationViewService>();
    public IThemeService GetThemeService => GetService<IThemeService>();

    public static T GetService<T>() where T : class
    {
        if ((App.Current as App)!.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }
    public async Task LoadMyExtensionAsync(string assemblyLoadPath)
    {
        // save off the handle so we can clean up our registration with the hosting process later if desired.
        Debug.WriteLine("start load "+ assemblyLoadPath);

        IExtensionAssembly asmHandle = await ApplicationExtensionHost.Current.LoadExtensionAsync(assemblyLoadPath);
        Debug.WriteLine("loaded plugin "+ asmHandle.GetType().Name + " "+ asmHandle.GetType().FullName);
    }
    
    public App()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NCaF5cXmZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWH5ceXRXRWhZUkN2W0I=");
        Services = ConfigureServices();
        this.InitializeComponent();
        ApplicationExtensionHost.Initialize(this);
        DiFactory.Services.Register<ACOMPluginLoader>(reuse: Reuse.Singleton);
 
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IJsonNavigationViewService>(factory =>
        {
            var json = new JsonNavigationViewService();
            json.ConfigDefaultPage(typeof(HomeLandingPage));
            json.ConfigSettingsPage(typeof(SettingsPage));
            return json;
        });

        services.AddTransient<MainViewModel>();
        services.AddSingleton<ContextMenuService>();
        services.AddTransient<GeneralSettingViewModel>();
        services.AddTransient<AppUpdateSettingViewModel>();
        services.AddTransient<AboutUsSettingViewModel>();
        services.AddTransient<BreadCrumbBarViewModel>();
        services.AddTransient<HomeLandingViewModel>();

        return services.BuildServiceProvider();
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new Window();

        if (MainWindow.Content is not Frame rootFrame)
        {
            MainWindow.Content = rootFrame = new Frame();
        }

        if (GetThemeService != null)
        {
            GetThemeService.AutoInitialize(MainWindow);
        }

        var menuService = GetService<ContextMenuService>();
        if (menuService != null)
        {
            ContextMenuItem menu = new ContextMenuItem
            {
                Title = "Open ACOMv2 Here",
                Param = @"""{path}""",
                AcceptFileFlag = (int)FileMatchFlagEnum.All,
                AcceptDirectoryFlag = (int)(DirectoryMatchFlagEnum.Directory | DirectoryMatchFlagEnum.Background | DirectoryMatchFlagEnum.Desktop),
                AcceptMultipleFilesFlag = (int)FilesMatchFlagEnum.Each,
                Index = 0,
                Enabled = true,
                Icon = ProcessInfoHelper.GetFileVersionInfo().FileName,
                Exe = "ACOMv2.exe"
            };

            await menuService.SaveAsync(menu);
        }

        rootFrame.Navigate(typeof(MainPage));

        MainWindow.Title = MainWindow.AppWindow.Title = ProcessInfoHelper.ProductNameAndVersion;
        MainWindow.AppWindow.SetIcon("Assets/icon.ico");

        if (Settings.UseDeveloperMode)
        {
            ConfigureLogger();
        }

        MainWindow.Activate();

        UnhandledException += (s, e) => Logger?.Error(e.Exception, "UnhandledException");
    }
}

