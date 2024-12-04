
using System.Collections.Concurrent;
using System.Net.Sockets;
using STTech.BytesIO.Kcp;
using STTech.BytesIO.Serial;
using STTech.BytesIO.Udp;
using Windows.Media.Protection.PlayReady;
using BytesIO =STTech.BytesIO;
using System;
using System.Collections.Generic;
//using System.Windows.Forms;
using STTech.BytesIO.Core;
using System.IO.Ports;
using System.Diagnostics;
//using static SkiaSharp.HarfBuzz.SKShaper;
using ACOMv2.Views;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
//using CommunityToolkit.WinUI.UI.Controls.TextToolbarSymbols;
using Microsoft.Windows.Management.Deployment;
using System.Xml.Linq;
using Windows.Gaming.Preview.GamesEnumeration;
using System.Text;
using System.Runtime.CompilerServices;
using ACOMv2.Models.Processers;
/**
 * 这个类用于管理IO输入输出
 * 
 * 
 * 
 * 
 */
namespace ACOM.Models
{

    using System.Management;
    using System.Timers;
    using ACOMPlug;

    public class ChannelProcesser
    {
        //List<ChannelMassage> _data = new List<ChannelMassage>(8);
        Dictionary<string, ChannelMassage> dataMap = new Dictionary<string, ChannelMassage>();
        List<byte> tmpbytes = new List<byte>(128);
        bool receivedFrame = false;
        public ChannelProcesser()
        {
            
        }
         ~ChannelProcesser()
        {

        }
        string HelpMassage="'[Name],[Date1],[Data2],...\n'";


        //public bool Date_Search(string IsName)
        //{
        //    foreach (ChannelMassage item in _data)
        //    {
        //        if (item.Name.CompareTo(IsName)==0)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        /// <summary>
        /// 用于处理传入的数据,并且当数据完成一次分包后返回有效数据，否则为null
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ChannelMassage> Process(RawDataMassage data)
        {
            Debug.WriteLine("数据处理");
            List<ChannelMassage> _data = new List<ChannelMassage>(8);
            int cnt = 0;
            int lastcnt = 0;
            //Debug.WriteLine("1");
            int i = 0;
            while (cnt < data._data.Count())
            {
                
                for (i = lastcnt; i < data._data.Count(); ++i)
                {
                    if (data._data[i] == '\n')
                    {
                        receivedFrame = true;
                        break;
                    }
                }
                cnt = i;
                //Debug.WriteLine("cnt" + cnt.ToString());

                //将长度为cnt的数据添加到缓冲区
                tmpbytes.AddRange(data._data[lastcnt..cnt]);
                lastcnt = cnt+1;
                if (!receivedFrame)
                {
                    Debug.WriteLine("数据处理完毕");
                    return _data;
                }
                receivedFrame = false;
                List<byte[]> lists = ByteSplitter.Split(tmpbytes, new List<byte> { (byte)':' });
                if (lists.Count > 2)
                {
                    //TODO:错误的格式，只取最后一个:分割的前面一段作为名字
                }
                else if (lists.Count == 2)
                {
                    //对其进行再次分包
                    List<byte[]> NameLists = ByteSplitter.Split(lists[0], new List<byte> { (byte)',' });
                    List<byte[]> dateLists = ByteSplitter.Split(lists[1], new List<byte> { (byte)',' });
                    //获取两个List的较大值
                    int count = NameLists.Count > dateLists.Count ? NameLists.Count : dateLists.Count;
                    //创建count个ChannelMassage
                    for (int j = 0; j < count; ++j)
                    {
                        byte[] Name = NameLists.ElementAtOrDefault(j) ?? ("Data" + j.ToString()).GetBytes();
                        byte[] date = dateLists.ElementAtOrDefault(j) ?? ("No Data").GetBytes();
                        try
                        {
                            // 尝试添加一个重复的键
                            dataMap.Add(Name.EncodeToString(), new ChannelMassage { Name = Name.EncodeToString(), rawChannelData = date });
                        }
                        catch (ArgumentException e)
                        {
                            _ = e;
                            dataMap[Name.EncodeToString()].rawChannelData = date;

                        }
                        Debug.WriteLine(date.EncodeToString());

                    }
                    Debug.WriteLine("数据分包完成");
                    //清理数据
                    tmpbytes.Clear();
                    _data.AddRange(dataMap.Values.ToList());

                }
                else
                {
                    //count==1
                    //说明是没有:的数据

                }


            }

            return _data;
        }
    }


   


    class IO_Manage: Singleton<IO_Manage>
    {
        public Vector<ChannelMassage> ChannelDatas;
        public HomeLandingPage page;
        ChannelProcesser channelProcesser = new();
        private void Client_OnExceptionOccurs(object sender, STTech.BytesIO.Core.ExceptionOccursEventArgs e)
        {
            Print($"异常: {e.Exception.Message}");
        }

        private void Client_OnDataSent(object sender, STTech.BytesIO.Core.DataSentEventArgs e)
        {
            Print($"发送: {e.Data.ToHexCodeString()}({e.Data.EncodeToString()})");
        }

        private void Client_OnDataReceived(object sender, STTech.BytesIO.Core.DataReceivedEventArgs e)
        {
            BytesIO.Serial.SerialClient client = (BytesIO.Serial.SerialClient)sender;
            Print((DateTime.Now - client.LastMessageReceivedTime).Microseconds.ToString() );
            Print($"接收: {e.Data.ToHexCodeString()}({e.Data.EncodeToString()})");
            channelProcesser.Process(new RawDataMassage(e.Data, DateTime.Now, RawDataMassage.DateSource.Serial, client.PortName));
            //channelProcesserMap[sender].Process(new RawDataMassage(e.Data, DateTime.Now, RawDataMassage.DateSource.Serial, client.PortName));

            charRecQueue.Enqueue(new RawDataMassage(e.Data,DateTime.Now,RawDataMassage.DateSource.Serial, client.PortName));
            page.TextAddLine(e.Data.EncodeToString());
        }

        private void Client_OnDisconnected(object sender, STTech.BytesIO.Core.DisconnectedEventArgs e)
        {
            Print("断开连接");
            channelProcesserMap.Remove(sender);
        }

        private void Client_OnConnectionFailed(object sender, STTech.BytesIO.Core.ConnectionFailedEventArgs e)
        {
            Print("连接失败");
        }

        private void Client_OnConnectedSuccessfully(object sender, STTech.BytesIO.Core.ConnectedSuccessfullyEventArgs e)
        {
            Print("连接成功");
            IPlugProcessBase plug =  Plugs.CreateInstance("ProcesserPlugWaterFire");
            channelProcesserMap.Add(sender, plug);
        }

        List<BytesIO.Serial.SerialClient>  serialClients = new();
        List<BytesIO.Tcp.TcpClient> tcpClients= new();
        List<BytesIO.Kcp.KcpClient> kcpClients= new();
        List<BytesIO.Udp.UdpClient> udpClients = new();
        ConcurrentQueue<RawDataMassage> charRecQueue=new();
        Dictionary<object, IPlugProcessBase> channelProcesserMap = new();










        public SerialClient Connect(string portName,int baudRate=115200,int dataBits=8,
            Parity parity = Parity.None, StopBits stopBits = StopBits.One)
        {
            SerialClient __client;
            foreach (BytesIO.Serial.SerialClient item in serialClients)
            {
                if (item.PortName == portName)
                {
                    __client = item;
                    goto NOT_INIT;
                }
            }
             __client = new SerialClient();
            // 监听连接成功事件
            __client.OnConnectedSuccessfully += Client_OnConnectedSuccessfully;
            // 监听连接失败事件
            __client.OnConnectionFailed += Client_OnConnectionFailed;
            // 监听断开连接事件
            __client.OnDisconnected += Client_OnDisconnected;
            // 监听接收数据事件
            __client.OnDataReceived += Client_OnDataReceived;
            // 监听发送数据事件
            __client.OnDataSent += Client_OnDataSent;
            // 监听发生异常事件
            __client.OnExceptionOccurs += Client_OnExceptionOccurs;

NOT_INIT:
            __client.PortName = portName;
            __client.BaudRate = baudRate;
            __client.DataBits = dataBits;
            __client.Parity = parity;
            __client.StopBits = stopBits;

            ConnectResult result = __client.Connect();
            if (result.IsSuccess || (result.ErrorCode == ConnectErrorCode.IsConnected))
            {
                serialClients.Add(__client);
            }
            else
            {

                __client = null;
            }
            return __client;
        }

        public bool DisConnect(SerialClient client)
        {
            DisconnectResult result = client.Disconnect();
            return result.IsSuccess;
        }
        public bool DisConnect(string portName)
        {
            foreach (BytesIO.Serial.SerialClient item in serialClients)
            {
               if(item.PortName == portName)
                {
                    DisconnectResult result = item.Disconnect();
                    return result.IsSuccess;
                }
            }
            return false;

        }
        private void Print(string msg)
        {
            Debug.WriteLine(msg);
            //object value = Invoke(new EventHandler(delegate
            //{
            //    tbRecv.AppendText($"[{DateTime.Now.ToLongTimeString()}] {msg}\r\n");
            //}));
        }
        public int doing = 1;
        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            // 在这里执行您的逻辑
            IO_Manage.Instance.doing = 1;
        }
        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (doing == 1)
            {
                doing = 0;
                Print("update" + e.ToString() + sender.GetType().ToString());
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 100; // 设置时间间隔为1000毫秒（1秒）
                timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerElapsed);
                timer.AutoReset = false; // 设置定时器在触发一次后不自动重置
                timer.Start();
            }
// ...


        }
        IO_Manage() {


            ManagementEventWatcher watcher;

            //创建ManagmentEventWatcher 对象
            watcher = new ManagementEventWatcher("SELECT *FROM Win32_DeviceChangeEvent WHERE EventType = 2 or EventType = 3 ");
            //添加设备变化事件处理程序
            watcher.EventArrived += Watcher_EventArrived;
            //开始监听
            watcher.Start();

            Plugs.Init("C:\\Users\\80520\\Documents\\GitHub\\ACOM\\ProcesserPlug_WaterFire\\bin\\Debug\\net8.0-windows10.0.19041.0");







        }
        ~IO_Manage() { }
    }

    public static class ByteArrayExtensions
    {
        public static string EncodeToString(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }
    }
}
