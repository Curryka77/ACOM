﻿// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace ACOMv2.Views;

//using LiveChartsCore.SkiaSharpView;
//using LiveChartsCore;
using System.Collections.ObjectModel;
//using LiveChartsCore.Defaults;
using Newtonsoft.Json.Linq;
using System;
using System;
using System.Collections.Generic;
//using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
//using LiveChartsCore;
//using LiveChartsCore.Defaults;
//using LiveChartsCore.SkiaSharpView;
//using LiveChartsCore.SkiaSharpView.Painting;
//using SkiaSharp;



/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>

public class Point
{
    public double X { get; set; }
    public double Y { get; set; }
}

public sealed partial class MainCanvasPage : Page
{
    public MainCanvasPage()
    {
        //this.InitializeComponent();
    }
}
//public sealed partial class MainCanvasPage : Page
//{
//    private readonly Random _random = new();
//    //private readonly List<DateTimePoint> _values = new();
//    //private static TimeSpan span = new TimeSpan(0, 0, 0, 0, 10);
//    //private readonly DateTimeAxis _customAxis;// = new DateTimeAxis(span, Formatter);
//    //public ObservableCollection<ISeries> SeriesDates { get; set; } = new ObservableCollection<ISeries>();
//    //public Axis[] XAxesDates { get; set; }



//    public List<Point> Data { get; set; }

//    public object SyncDates { get; } = new object();
//    public bool IsReading { get; set; } = true;

//    private async Task ReadData()
//    {
//        // to keep this sample simple, we run the next infinite loop 
//        // in a real application you should stop the loop/task when the view is disposed 
//        while (IsReading)
//        {
//            await Task.Delay(20);

//            // Because we are updating the chart from a different thread 
//            // we need to use a lock to access the chart data. 
//            // this is not necessary if your changes are made in the UI thread. 
//            lock (SyncDates)
//            {
//                //_values.Add(new DateTimePoint(DateTime.Now, _random.Next(0, 10)));
//                //if (_values.Count > 1000) _values.RemoveAt(0);

//                //// we need to update the separators every time we add a new point 
//                //_customAxis.CustomSeparators = GetSeparators();
//            }
//        }
//    }
//    private double[] GetSeparators()
//    {
//        var now = DateTime.Now;

//        return new double[]
//        {
//        now.AddSeconds(-25).Ticks,
//        now.AddSeconds(-20).Ticks,
//        now.AddSeconds(-15).Ticks,
//        now.AddSeconds(-10).Ticks,
//        now.AddSeconds(-5).Ticks,
//        now.Ticks
//        };
//    }
//    private static string Formatter(DateTime date)
//    {
//        var secsAgo = (DateTime.Now - date).TotalSeconds;

//        return secsAgo < 1
//            ? "now"
//            : $"{secsAgo:N0}s ago";
//    }
//    public MainCanvasPage()
//    {

//        Data = new List<Point>()
//    {
//        new Point { X=1,Y=2 },
//        new Point { X=2,Y=2 },
//        new Point { X=3,Y=2 },
//        new Point { X=4,Y=2 },
//        new Point { X=5,Y=2}
//    };


//        this.InitializeComponent();


//        //chart1.XAxes = XAxesDates;
//        //chart2.XAxes = XAxesDates;


//    }
//}
