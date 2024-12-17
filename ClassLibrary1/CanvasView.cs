// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Labs.WinUI.CanvasViewInternal;
using CommunityToolkit.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System.Collections.Specialized;
using Windows.Foundation.Collections;
using System.Diagnostics;
using Microsoft.UI.Windowing;
using Windows.Foundation;
using System.Windows.Input;
using Windows.Devices.Input;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;
using Microsoft.UI.Content;
using CommunityToolkit;

namespace CommunityToolkit.Labs.WinUI;

/// <summary>
/// <see cref="CanvasView"/> is an <see cref="ItemsControl"/> which uses a <see cref="Canvas"/> for the layout of its items.
/// It which provides built-in support for presenting a collection of items bound to specific coordinates 
/// and drag-and-drop support of those items.
/// </summary>
public partial class CanvasView : ItemsControl
{
    private (DependencyProperty, string)[] LiftedProperties = new (DependencyProperty, string)[] {
        (Canvas.LeftProperty, "(Canvas.Left)"),
        (Canvas.TopProperty, "(Canvas.Top)"),
        (Canvas.ZIndexProperty, "(Canvas.ZIndex)"),
        (ManipulationModeProperty, "ManipulationMode")
    };

    public SolidColorBrush DefaultBorderBrush { get; set; }
    public Thickness DefaultBorderThickness { get; set; }
    public Thickness DefaultPadding { get; set; } // 控件内部的边距

    public CanvasView()
    {

        // 设置默认的边框样式
        DefaultBorderBrush = new SolidColorBrush(Colors.Black);
        DefaultBorderThickness = new Thickness(1);
        DefaultPadding = new Thickness(10); // 设置边框比控件大一圈的内边距

        // TODO: Need to use XamlReader because of https://github.com/microsoft/microsoft-ui-xaml/issues/2898
        ItemsPanel = XamlReader.Load("<ItemsPanelTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Canvas/></ItemsPanelTemplate>") as ItemsPanelTemplate;
    }

   
    // 假设ContentSizer是你的自定义控件，你需要确保它已经被定义
    // 并且ManipulationDelta事件处理器也已经定义
    private void ContentSizer_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
        // 你的事件处理逻辑
    }

    public void Add(UIElement element)
    {

    }
    //protected override void OnApplyTemplate()
    //{
    //    base.OnApplyTemplate();

    //    // 为Canvas中的每个子控件添加边框
    //    ItemCollection itemCollection = this.Items;
    //    int cnt = itemCollection.Count;
    //    for(int i = 0; i < cnt; i++)

    //    {
    //        Border border = new Border
    //        {
    //            BorderBrush = new SolidColorBrush(Colors.Black), // 边框颜色
    //            BorderThickness = new Thickness(1), // 边框厚度
    //            Padding = new Thickness(10), // 边框与内容之间的间距
    //            Child = (UIElement)itemCollection[0] // 子控件作为Border的子元素
    //        };
    //        double left = Canvas.GetLeft((UIElement)itemCollection[0]);
    //        double top = Canvas.GetTop((UIElement)itemCollection[0]);
    //        // 替换原有的子控件为Border控件
    //        this.Items.Remove(itemCollection[0]);
            
    //        Canvas.SetLeft(border, left);
    //        Canvas.SetTop(border, top);
    //        this.Items.Add(border);

    //    }
    //    Debug.WriteLine("w");
    //}

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
{
        base.PrepareContainerForItemOverride(element, item);

        // ContentPresenter is the default container for Canvas.
        if (element is FrameworkElement cp)
        {
            //TODO:
            //_ = using CommunityToolkit.Helpers.(() =>
            //{
            //    SetupChildBinding(cp);
            //});

            // Loaded is not firing when dynamically loading an element to the collection. Relay on CompositionTargetHelper above.
            // Seems like a bug in Loaded event?
            cp.Loaded += ContentPresenter_Loaded;
            cp.ManipulationDelta += ContentPresenter_ManipulationDelta;
        }

        // TODO: Do we want to support something else in a custom template?? else if (item is FrameworkElement fe && fe.FindDescendant/GetContentControl?)
    }

    protected override void ClearContainerForItemOverride(DependencyObject element, object item)
    {
        base.ClearContainerForItemOverride(element, item);

        if (element is FrameworkElement cp)
        {
            cp.Loaded -= ContentPresenter_Loaded;
            cp.ManipulationDelta -= ContentPresenter_ManipulationDelta;
        }
    }

    private void ContentPresenter_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
        // Move the rectangle.
        if (sender is FrameworkElement cp)
        {
            // TODO: Seeing some drift, not sure if due to DPI or just general drift
            // or probably we need to do the start/from delta approach we did with SizerBase to resolve.

            // We know that most likely these values have been bound to a data model object of some sort
            // Therefore, we need to use this helper to update the underlying model value of our bound property.
            //cp.WidtControl.MousePositionh = 128;
            cp.Focus(FocusState.Pointer);
            cp.SetBindingExpressionValue(Canvas.LeftProperty, Canvas.GetLeft(cp) + e.Delta.Translation.X);
            cp.SetBindingExpressionValue(Canvas.TopProperty, Canvas.GetTop(cp) + e.Delta.Translation.Y);
 
        }
    }

    private void ContentPresenter_Loaded(object sender, RoutedEventArgs args)
    {
        if (sender is ContentPresenter cp)
        {
            cp.Loaded -= ContentPresenter_Loaded;

            SetupChildBinding(cp);
        }
    }

    private void SetupChildBinding(ContentPresenter cp)
    {
        // Get direct visual descendant for ContentPresenter to look for Canvas properties within Template.
        var child = VisualTreeHelper.GetChild(cp, 0);

        if (child != null)
        {
            // TODO: Should we avoid doing this twice?

            // Hook up any properties we care about from the templated children to it's parent ContentPresenter.
            foreach ((var prop, var path) in LiftedProperties)
            {
                var binding = new Binding();
                binding.Source = child;
                ////binding.Mode = BindingMode.TwoWay; // TODO: Should this be exposed as a general property?
                binding.Path = new PropertyPath(path);

                cp.SetBinding(prop, binding);
            }
        }
    }
}
