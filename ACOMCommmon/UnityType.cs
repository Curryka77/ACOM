using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ACOMCommmon;
public class unityType
{
}

//public class CannelData
//{
//    public string ChannelData;
//    public string Name;
//    public string ExpendData;//扩展数据，用于隐藏部分的显示
//    public DateTime DateTime;
//}
public class ChannelMassage
{

    public byte[] rawChannelData;
    public string Name;
    public string Type;//可选建议项目，由前级分包器创建提交到后级解释器进行高级运算或者直接显示
    public DateTime DateTime;

}
public class RawDataMassage
{
    public enum DateSource
    {
        Serial,
        TCP,
        UDP,
        KCP,
        Others,
        Unknowns,
    }
    public readonly byte[] _data;
    public RawDataMassage(byte[] data, DateTime dateTime, DateSource dateSource, string userdata = "")
    {
        _data = data;
        _dateTime = dateTime;
        _dateSource = dateSource;
        this.userdata = userdata;
    }
    public DateTime _dateTime;
    public DateSource _dateSource;
    public string userdata;
};


public class CannelData : ObservableObject
{
    private string _DataName;
    public string DataName
    {
        get => _DataName;
        set => SetProperty(ref _DataName, value);
    }
  
    private string _Data;
    public string Data
    {
        get => _Data;
        set => SetProperty(ref _Data, value);
    }

    private DateTime _DateTime;
    public DateTime DateTime
    {
        get => _DateTime;
        set => SetProperty(ref _DateTime, value);
    }
    //private SolidColorBrush _DataColor;
    //public SolidColorBrush DataColor
    //{
    //    get => _DataColor;
    //    set =>SetProperty(ref _DataColor, value);
    //}

    private string _DataColor = "#ffebee";
    public string DataColor
    {
        get => _DataColor;
        set => SetProperty(ref _DataColor, value);
    }    //public SolidColorBrush DataColor
    //{
    //    get => ColorHelper.ConvertHexToSolidColorBrush(_DataColor);
    //    set => SetProperty<string>(ref _DataColor, ColorHelper.ConvertSolidColorBrushToHex(value));
    //}
    public bool is_View
    {
        get; set;
    }
   
    public CannelData(string dataName, double data)
    {

        //DataColor = new SolidColorBrush(Colors.Salmon);
        //DataColor = ColorHelper.AutoGetColor();
        DataColor = "#ff9e80";

        DataName = dataName;
        Data = data.ToString();
         // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
         is_View = true;
    }
    public CannelData(string dataName, string data)
    {

        //DataColor = new SolidColorBrush(Colors.Salmon);
        //DataColor = ColorHelper.AutoGetColor();

        DataColor = "#ff9e80";
        //Debug.WriteLine("DataColor:" + DataColor);
        DataName = dataName;
        Data = data;
         // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
         is_View = true;
    }
    public CannelData(string dataName, string data, string color)
    {
        //DataColor = new SolidColorBrush(Colors.Salmon);
        DataColor = color;
        //Debug.WriteLine("DataColor:" + DataColor); DataName = dataName;
        Data = data;
         // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
         is_View = true;
    }
 
}
