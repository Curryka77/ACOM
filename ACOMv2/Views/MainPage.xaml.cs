using ACOMv2.Views.DeviceConnect;
using Microsoft.UI.Xaml.Media.Animation;

namespace ACOMv2.Views;

public sealed partial class MainPage : Page
{
    private int previousSelectedIndex;

    public MainViewModel ViewModel { get; }
    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        this.InitializeComponent();
        App.MainWindow.ExtendsContentIntoTitleBar = true;
        App.MainWindow.SetTitleBar(AppTitleBar);

        var jsonNavigationViewService = App.GetService<IJsonNavigationViewService>() as JsonNavigationViewService;
        if (jsonNavigationViewService != null)
        {
            jsonNavigationViewService.Initialize(NavView, NavFrame, NavigationPageMappings.PageDictionary);
            jsonNavigationViewService.ConfigJson("Assets/NavViewMenu/AppData.json");
            jsonNavigationViewService.ConfigBreadcrumbBar(JsonBreadCrumbNavigator, BreadcrumbPageMappings.PageDictionary);
        }




        MainFrame.Navigate(typeof(HomeLandingPage), null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });

    }

    private void AppTitleBar_BackRequested(TitleBar sender, object args)
    {
        if (NavFrame.CanGoBack)
        {
            NavFrame.GoBack();
        }
    }

    private void AppTitleBar_PaneToggleRequested(TitleBar sender, object args)
    {
        NavView.IsPaneOpen = !NavView.IsPaneOpen;
    }

    private void NavFrame_Navigated(object sender, NavigationEventArgs e)
    {
        AppTitleBar.IsBackButtonVisible = NavFrame.CanGoBack;
    }

    private void ThemeButton_Click(object sender, RoutedEventArgs e)
    {
        ThemeService.ChangeThemeWithoutSave(App.MainWindow);
    }

    private void OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        AutoSuggestBoxHelper.OnITitleBarAutoSuggestBoxTextChangedEvent(sender, args, NavFrame);
    }

    private void OnQuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        AutoSuggestBoxHelper.OnITitleBarAutoSuggestBoxQuerySubmittedEvent(sender, args, NavFrame);
    }

    private void SelectorBar_SelectionChanged(SelectorBar sender, SelectorBarSelectionChangedEventArgs args)
    {
        SelectorBarItem selectedItem = sender.SelectedItem;
        int currentSelectedIndex = sender.Items.IndexOf(selectedItem);
        System.Type pageType;

        switch (currentSelectedIndex)
        {
            case 0:
                pageType = typeof(SerialConnect);
                break;
            case 1:
                pageType = typeof(TCPConnect);
                break;
            case 2:
                pageType = typeof(UDPConnect);
                break;

            default:
                pageType = typeof(SerialConnect);
                break;
        }

        var slideNavigationTransitionEffect = currentSelectedIndex - previousSelectedIndex > 0 ? SlideNavigationTransitionEffect.FromRight : SlideNavigationTransitionEffect.FromLeft;

        //DeviceConnectFrame.Navigate(pageType, null, new SlideNavigationTransitionInfo() { Effect = slideNavigationTransitionEffect });

        previousSelectedIndex = currentSelectedIndex;

    }






}

