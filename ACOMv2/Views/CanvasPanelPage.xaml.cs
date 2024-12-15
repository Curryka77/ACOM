using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Diagnostics;
using CommunityToolkit.Labs.WinUI;
using Windows.UI.Core;
using Microsoft.UI.Input;
using System.Reflection;
using System.Collections.ObjectModel;
using CommunityToolkit.WinUI.Controls;
using ShadowPluginLoader.WinUI;
using Windows.Devices.Enumeration;
using ACOMPlugin.Core;
using DryIoc;
using ACOM.Models;
using ACOMPlug;
using ACOMv2.Models.Processers;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ACOMv2.Views
{
    public static class UIElementExtensions
    {
        private static CoreCursor defaultCursor;

        public static void ChangeCursor(this UIElement uiElement, InputCursor cursor)
        {
            Type type = typeof(UIElement);
            type.InvokeMember("ProtectedCursor", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance, null, uiElement, new object[] { cursor });
        }
    }

    //public static class ContentSizerExtensions
    //{
    //    //private static CoreCursor defaultCursor;
    //    protected override void OnManipulationDelta(ManipulationDeltaRoutedEventArgs e)
    //    {

    //    }

    //}

    //public class Part : Widget
    //{
    //    Grid elementGrid;//记录元素的Grid
    //    public Part(ref Grid elementGrid)
    //    {
    //        this.elementGrid = elementGrid;
    //    }
    //}

        /// <summary>
        /// An empty page that can be used on its own or navigated to within a Frame.
        /// </summary>
        public sealed partial class CanvasPanelPage : Page
    {


        public FrameworkElement InitializeElementGrid(FrameworkElement element)
        {

            // 创建Grid
            Grid elementGrid = new Grid()
            {
                Name = element.Name + "Grid",
                MinHeight = 32,
                MinWidth = 32,
                Style = (Style)Application.Current.Resources["GridCardPanel"]
            };
            //创建组件
            Widget part = new(ref element);

            // 设置Canvas位置（在C#中，我们通常不设置Canvas.Left和Canvas.Top，因为它们是Canvas特有的属性，这里假设你想要将Grid放置在Canvas中）
            // 你需要一个Canvas作为父容器，然后添加Grid到其中，并设置位置
            Canvas.SetLeft(elementGrid, 90);
            Canvas.SetTop(elementGrid, 90);

            // 定义列
            ColumnDefinition columnDef1 = new ColumnDefinition();
            ColumnDefinition columnDef2 = new ColumnDefinition() { Width = GridLength.Auto };
            elementGrid.ColumnDefinitions.Add(columnDef1);
            elementGrid.ColumnDefinitions.Add(columnDef2);

            // 定义行
            RowDefinition rowDef1 = new RowDefinition();
            RowDefinition rowDef2 = new RowDefinition() { Height = GridLength.Auto };
            elementGrid.RowDefinitions.Add(rowDef1);
            elementGrid.RowDefinitions.Add(rowDef2);

            Grid.SetColumn(element, 0);
            Grid.SetRow(element, 0);
            elementGrid.Children.Add(element);

            // 创建水平ContentSizer
            ContentSizer horizontalContentSizer = new ContentSizer()
            {
                Height = 2,
                VerticalAlignment = VerticalAlignment.Bottom,
                Orientation = Orientation.Horizontal,
                TargetControl = elementGrid,
                Visibility = Visibility.Visible
            };
            horizontalContentSizer.ManipulationDelta += ContentSizer_ManipulationDelta;

            Grid.SetRow(horizontalContentSizer, 1);

            elementGrid.Children.Add(horizontalContentSizer);

            // 创建垂直ContentSizer
            ContentSizer verticalContentSizer = new ContentSizer()
            {
                Width = 2,
                HorizontalAlignment = HorizontalAlignment.Right,
                TargetControl = elementGrid,
                Visibility = Visibility.Visible
            };

            Grid.SetColumn(verticalContentSizer, 1);

            verticalContentSizer.ManipulationDelta += ContentSizer_ManipulationDelta;

            elementGrid.Children.Add(verticalContentSizer);

            // 将Grid添加到页面或其他父容器中
            return elementGrid; // 假设你是在Page中，并且将Grid设置为页面的内容
        }
        public void CreateWidget(ACOMPluginBase WidgetPlug)
        {
            if (WidgetPlug == null) return;
            WidgetPlug.UpdateCommand += UpdateWidgetCommend;
            IO_Manage.updateCannelViewMsg += WidgetPlug.UpdateData;
            CanvasView1.Items.Add(InitializeElementGrid(WidgetPlug.Create()));
        }

        private void UpdateWidgetCommend(object sender, List<string[]> cmd)
        {
            throw new NotImplementedException();
        }

        public CanvasPanelPage()
        {
            strings = new ObservableCollection<Symbol>
            {
                Symbol.AddFriend,
                Symbol.Forward,
                Symbol.Share
            };
            this.InitializeComponent();
            CreateWidget(Plugs.WidgetPlugins["ACOMPlug.Widget.Slide"]);


             //FrameworkElement element = InitializeElementGrid( (Activator.CreateInstance(Models.Processers.Plugs.WidgetsPlugins[0]) as IPlugWidgetBase).Create());
             //CanvasView1.Items.Add( new Button() { Content = "Button1" });
        }

        private void Border_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            //Debug.WriteLine("cesfec");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Button button1 = sender as Button;
            //button1.ChangeCursor(InputSystemCursor.Create(InputSystemCursorShape.Wait));

            Button button = new Button();
            button.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;
            //CanvasView1.Items.Add(button);
        }

        private void ContentSizer_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            e.Handled = true;
        }



        private readonly Random rnd = new();
        private ObservableCollection<Symbol> strings { get; }




       

        private void rangeSelector_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void OptionsAllCheckBox_Indeterminate(object sender, RoutedEventArgs e)
        {

        }
    }
}
