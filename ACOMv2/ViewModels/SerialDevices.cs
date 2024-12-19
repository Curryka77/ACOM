using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOM.Models;

namespace ACOMv2.ViewModels;
public class SerialDevices : ObservableObject
{
    private string _DeviceName;
    private string _DeviceDesc = "NO desc";
    private int _boundRate = 115200;
    private int _dateBit = 8;
    private string _checkBit = "N";
    private string _stopBit = "1";
    private string _streamCtrl = "XON/XOFF";
    private string _overView = "NONE";

    private bool is_connect = false;

    private IconElement icon = new SymbolIcon(Symbol.Play);
    private IO_Manage ioManage = IO_Manage.Instance;

    public bool ConnectState
    {
        get => is_connect;
        set
        {
            SetProperty(ref is_connect, value);
        }
    }
    public bool Connect()
    {
        Debug.WriteLine(_DeviceName + " connecting...");
        if (ioManage.Connect(_DeviceName, _boundRate, _dateBit,
            SerialDeviceHelper.ConvertToParity(_checkBit), SerialDeviceHelper.ConvertToStopBit(_stopBit)) != null)
        {
            Debug.WriteLine(_DeviceName + " connected");
            is_connect = true;
            Icon = new SymbolIcon(Symbol.Pause);
            return true;

        }
        else
        {
            //TODO : 完成错误日志输出，和其他提示
            return false;

        }
    }
    public bool DisConnect()
    {
        Debug.WriteLine(_DeviceName + " disconnect...");
        if (ioManage.DisConnect(_DeviceName) == true)
        {
            is_connect = false;
            Icon = new SymbolIcon(Symbol.Play);
            return true;
        }
        else
        {
            return false;
        }


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
    public IconElement Icon
    {
        get => icon;
        set
        {
            SetProperty(ref icon, value); Update();

        }
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
    public SerialDevices(string deviceName)
    {
        DeviceName = deviceName;
        Update();
    }

    public SerialDevices(string deviceName, string deviceDesc)
    {
        DeviceName = deviceName;
        DeviceDesc = deviceDesc;
        //DeviceDesc.Replace(deviceName,"");
        Update();
    }

    public string getDeviceName(SerialDevices device)
    {
        return device.DeviceName;
    }

}

