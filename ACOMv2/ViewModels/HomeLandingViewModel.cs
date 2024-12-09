using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System.Windows.Input;
using ACOM.Models;
using System.Diagnostics;
using System.Collections.ObjectModel;
using CommunityToolkit.WinUI.Collections;
using static ACOMPlug.RawDataMassage;
using Microsoft.UI.Dispatching;
using Windows.Devices.SerialCommunication;
using SerialDevice = ACOM.Models.SerialDevice;

namespace ACOMv2.ViewModels;

public class CannelDataView : ObservableObject
{
    private string _DataName;
    public string DataName
    {
        get => _DataName;
        set => SetProperty(ref _DataName, value);
    }
    public string ID
    {
        get; private set;
    }
    private string _Data;
    public string Data
    {
        get => _Data;
        set => SetProperty(ref _Data, value);
    }
    private SolidColorBrush _DataColor;
    public SolidColorBrush DataColor
    {
        get => _DataColor;
        set
        {
            SetProperty(ref _DataColor, value);

        }
    }
    public bool is_View
    {
        get; set;
    }
    public ICommand Command
    {
        get; set;
    }

    public CannelDataView(string dataName, double data, string id)
    {

        //DataColor = new SolidColorBrush(Colors.Salmon);
        DataColor = new SolidColorBrush(Microsoft.UI.Colors.RoyalBlue);
        DataName = dataName;
        ID = id;
        Data = data.ToString();
        var deleteCommand = new StandardUICommand(StandardUICommandKind.Stop);
        // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
        Command = deleteCommand;
        is_View = true;
    }
    public CannelDataView(string dataName, string data, string id)
    {

        //DataColor = new SolidColorBrush(Colors.Salmon);
        DataColor = new SolidColorBrush(Microsoft.UI.Colors.RoyalBlue);
        DataName = dataName;
        ID = id;
        Data = data;
        var deleteCommand = new StandardUICommand(StandardUICommandKind.Stop);
        // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
        Command = deleteCommand;
        is_View = true;
    }
    public CannelDataView(string dataName, double data, string id, Windows.UI.Color color)
    {
        //DataColor = new SolidColorBrush(Colors.Salmon);
        DataColor = new SolidColorBrush(color);
        DataName = dataName;
        ID = id;
        Data = data.ToString();
        var deleteCommand = new StandardUICommand(StandardUICommandKind.Stop);
        // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
        Command = deleteCommand;
        is_View = true;
    }
    public bool is_equal(string id)
    {
        return ID == id;
    }
}

public class LinkDeviceDates : ObservableObject
{
    private string _DeviceName;
    private string _DeviceDesc = "NO desc";
    private int _boundRate = 115200;
    private int _dateBit = 8;
    private string _checkBit = "N";
    private string _stopBit = "1";
    private string _streamCtrl = "XON/XOFF";
    private string _overView = "NONE";

    public string is_connect = "false";
    private IO_Manage ioManage = IO_Manage.Instance;

    public string ConnectState
    {
        get => is_connect;
        set
        {
            SetProperty(ref is_connect, value);
            if (is_connect == "true")
            {
                Connect();
            }
            else
            {
                DisConnect();
            }
        }
    }
    public void Connect()
    {
        Debug.WriteLine(_DeviceName + " " + "connecting...");
        if (ioManage.Connect(_DeviceName) != null)
        {
            Debug.WriteLine(_DeviceName + " " + "connected");
            is_connect = "true";
        }
    }
    public void DisConnect()
    {

        ioManage.DisConnect(_DeviceName);
        Debug.WriteLine(_DeviceName + "disconnect");
        is_connect = "false";
    }
    public string DeviceName
    {
        get => _DeviceName;
        set => SetProperty(ref _DeviceName, value);
    }

    public string DeviceDesc
    {
        get => _DeviceDesc;
        set => SetProperty(ref _DeviceDesc, value);
    }

    public int BoundRate
    {
        get => (int)_boundRate;
        set => SetProperty(ref _boundRate, value);
    }

    public int DateBit
    {
        get => (int)_dateBit;
        set { SetProperty(ref _dateBit, value); Update(); }
    }

    public string CheckBit
    {
        get => (string)_checkBit;
        set
        {
            SetProperty(ref _checkBit, value); Update();
        }
    }
    public string StopBit
    {
        get => (string)_stopBit;
        set
        {
            SetProperty(ref _stopBit, value); Update();
        }
    }
    public string StreamCtrl
    {
        get => (string)_streamCtrl;
        set
        {
            SetProperty(ref _streamCtrl, value); Update();
        }
    }

    public string OverView
    {
        get => (string)_overView;
        set => SetProperty(ref _overView, value);
    }
    public void Update()
    {
        OverView = _boundRate.ToString() + " " + _dateBit.ToString() + _checkBit.ToString() + _stopBit.ToString();
    }
    public LinkDeviceDates(string deviceName)
    {
        DeviceName = deviceName;
        Update();
    }

    public LinkDeviceDates(string deviceName, string deviceDesc)
    {
        DeviceName = deviceName;
        DeviceDesc = deviceDesc;
        //DeviceDesc.Replace(deviceName,"");
        Update();
    }


}

public partial class HomeLandingViewModel : ObservableObject
{
    public IJsonNavigationViewService JsonNavigationViewService;
    public CannelDataView dataListDatas;

    public ObservableCollection<CannelDataView> dateSource = new(); //数据颜色
    public ObservableCollection<LinkDeviceDates> linkDeviceSource = new(); //连接设备
    public ObservableCollection<string> SerialPortsSource = new(); //连接设备
    public AdvancedCollectionView advancedCollectionView ;
    public List<ACOM.Models.SerialDevice> Devices= new();// = serialDevices;


    public HomeLandingViewModel(IJsonNavigationViewService jsonNavigationViewService)
    {
        JsonNavigationViewService = jsonNavigationViewService;
        
        //for (int i = 0; i < 20; i++)
        //{
        //    dateSource.Add(new CannelDataView("data" + i.ToString(), -1.2198256, i.ToString()));
        //    dateSource[i].DataColor.Color = ACOMv2.Common.ColorHelper.GenerateDistinctColor((i * 78) % 128);
        //}
        advancedCollectionView = new AdvancedCollectionView(dateSource, true);

        // Let's filter out the integers
        int nul;
        advancedCollectionView.Filter = x => !int.TryParse(((CannelDataView)x).DataName, out nul);

        // And sort ascending by the property "Name"
        advancedCollectionView.SortDescriptions.Add(new SortDescription("DataName", SortDirection.Descending));
         //linkDeviceSource.Add(new LinkDeviceDates("COM1"));

    }



    [RelayCommand]
    private void OnItemClick(RoutedEventArgs e)
    {
        var args = (ItemClickEventArgs)e;
        var item = (DataItem)args.ClickedItem;

        JsonNavigationViewService.NavigateTo(item.UniqueId + item.Parameter?.ToString(), item);
        //HomeLandingViewService.TextAddLine("wd");

    }















}
