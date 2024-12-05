using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CanvasMoveView;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Numerics;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Core;
using Microsoft.UI.Input;

public sealed class ZoomableContainer : Grid
    {
        private const double EdgeThreshold = 20; // 边缘阈值
        private double _scaleFactor = 1.0; // 缩放因子

        public ZoomableContainer()
        {
            this.PointerEntered += ZoomableContainer_PointerEntered;
            this.PointerExited += ZoomableContainer_PointerExited;
            this.PointerPressed += ZoomableContainer_PointerPressed;
            this.PointerMoved += ZoomableContainer_PointerMoved;
            this.PointerReleased += ZoomableContainer_PointerReleased;
           // this.PointerWheelChanged += ZoomableContainer_PointerWheelChanged;
        }

        private void ZoomableContainer_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            UpdateCursor(e);
        }

        private void ZoomableContainer_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            // 当鼠标离开控件时，重置鼠标指针
           // this.Cursor = null;
        }

        private void ZoomableContainer_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // 开始缩放操作
        }

        private void ZoomableContainer_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            UpdateCursor(e);
        }

        private void ZoomableContainer_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // 结束缩放操作
        }

        //private void ZoomableContainer_PointerWheelChanged(object sender, PointerWheelChangedEventArgs e)
        //{
        //    if (IsCursorNearEdge())
        //    {
        //        // 放大或缩小
        //        double zoomDelta = e.Delta / 120; // 滚轮每次转动的增量
        //        _scaleFactor += zoomDelta * 0.1; // 缩放步长，根据需要调整
        //        _scaleFactor = Math.Clamp(_scaleFactor, 0.5, 2.0); // 限制缩放级别

        //        // 应用缩放
        //        ScaleTransform scaleTransform = new ScaleTransform
        //        {
        //            ScaleX = _scaleFactor,
        //            ScaleY = _scaleFactor
        //        };
        //        this.RenderTransform = scaleTransform;
        //    }
        //}

        //private bool IsCursorNearEdge()
        //{
        //    PointerPoint pointerPoint = Windows.UI.Input.PointerPoint.GetCurrentPoint(this.Get);
        //    double x = pointerPoint.Position.X;
        //    double y = pointerPoint.Position.Y;

        //    return (x < EdgeThreshold || x > this.ActualWidth - EdgeThreshold ||
        //            y < EdgeThreshold || y > this.ActualHeight - EdgeThreshold);
        //}

        private void UpdateCursor(PointerRoutedEventArgs e)
        {
            if (IsCursorNearEdge())
            {
                // 设置鼠标指针为调整大小的形状
               // this.Cursor = new CoreCursor(CoreCursorType.SizeNorthwestSoutheast, 1);
            }
            else
            {
               // this.Cursor = null;
            }
        }
    
}
