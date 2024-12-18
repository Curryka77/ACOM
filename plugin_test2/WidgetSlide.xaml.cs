using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using CustomExtensions.WinUI;
using Microsoft.UI;
using Windows.Devices.Input;
using ACOMPlugin.Core;
using Windows.Globalization.NumberFormatting;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WidgetPlug.Slide;
public sealed partial class WidgetSlide : UserControl
{
    public Slide slide;

    IncrementNumberRounder rounder = new IncrementNumberRounder();


    public DecimalFormatter formatter = new DecimalFormatter();

    private void ShowMenu(UIElement sender, bool isTransient)
    {
        FlyoutShowOptions myOption = new FlyoutShowOptions();
        //myOption.ShowMode = isTransient ? FlyoutShowMode.Transient : FlyoutShowMode.Standard;
        PartsCommandBarFlyout.ShowAt(sender, myOption);
        

    }
    private void Parts_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
    {
        //Debug.WriteLine("test Parts_ContextRequested");
        ShowMenu(sender, true);

    }
    public WidgetSlide()
    {

        rounder.Increment = 0.000001;
        rounder.RoundingAlgorithm = RoundingAlgorithm.RoundDown;

        formatter.IntegerDigits = 1;
        formatter.FractionDigits = 7;
        formatter.NumberRounder = rounder;

        this.LoadComponent(ref _contentLoaded);
    }

    private void Slider_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
        e.Handled = true;
    }

    private void OptionsAllCheckBox1_Indeterminate(object sender, RoutedEventArgs e)
    {

    }

    private void OptionsAllCheckBox_Indeterminate(object sender, RoutedEventArgs e)
    {

    }

    private void WidgetDeleteButton_Click(object sender, RoutedEventArgs e)
    {
        List<SystemCommandEnum> cmd = new List<SystemCommandEnum>();
        cmd.Add(SystemCommandEnum.Delete);
        slide.OnSystemCommand(this, cmd);
    }

}
