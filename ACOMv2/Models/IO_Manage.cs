
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
    using Windows.System;
    using WinUICommunity;

    public class ChannelProcesser
    {
        //List<ChannelMassage> _data = new List<ChannelMassage>(8);
 
        List<byte> tmpbytes = new List<byte>(4096);
        List<byte[]> frameDatas = new();
        static int unNameIndex = 0;
        Queue<RawDataMassage> noPloicyRawDataMassages = new Queue<RawDataMassage>(20);
        public RawDataMassage GetNopolicyData()
        {
            if (noPloicyRawDataMassages.Count > 0)
            {
                return noPloicyRawDataMassages.Dequeue();
            }
            else
            {
                return null;
            }
        }


        public ChannelProcesser()
        {
            
        }
         ~ChannelProcesser()
        {

        }


        public static  void PrintProcessedData(List<ChannelMassage> data)
        {
            if (data == null)
            {
                Debug.WriteLine("No data\n");
                return;
            }
            foreach (var item in data)
            {
                Debug.WriteLine($"Name: {item.Name}, Type: {item.Type}, DateTime: {item.DateTime}");
                if (item.rawChannelData != null)
                {
                    Debug.WriteLine("Raw Data:");
                    Debug.WriteLine("Char\tHex");
                    for (int i = 0; i < item.rawChannelData.Length; i++)
                    {
                        char character = item.rawChannelData[i] >= 32 && item.rawChannelData[i] <= 126 ? (char)item.rawChannelData[i] : ' ';
                        string hexValue = item.rawChannelData[i].ToString("X2");
                        Debug.WriteLine($"{character}\t{hexValue}");
                    }
                }
                else
                {
                    Debug.WriteLine("Raw Data: null");
                }
                Debug.WriteLine(" ");
            }
        }

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
            foreach (byte b in data._data)
            {
                tmpbytes.Add(b);
                if (b == '\n')
                {
                    frameDatas.Add(tmpbytes.ToArray());
                    tmpbytes.Clear();
                }
            }
            if (frameDatas.Count != 0)
            {
                List<ChannelMassage> frames = new List<ChannelMassage>(frameDatas.Count);
                foreach (var frame in frameDatas)
                {
                    string frameString = Encoding.UTF8.GetString(frame);
                    if (frameString.Contains(":"))
                    {
                        var parts = frameString.Split(':');
                        var leftPart = string.Join(":", parts.Take(parts.Length - 1)).Split(',').ToList();
                        var rightPart = parts.Last().Split(',').ToList();

                        for (int i = 0; i < Math.Max(leftPart.Count, rightPart.Count); i++)
                        {
                            ChannelMassage channelMassage = new ChannelMassage
                            {
                                rawChannelData = i < rightPart.Count ? Encoding.UTF8.GetBytes(rightPart[i]) : null,
                                Name = i < leftPart.Count ? leftPart[i] : "AutoName" + unNameIndex++,
                                Type = "Processed",
                                DateTime = data._dateTime
                            };
                            frames.Add(channelMassage);
                        }
                    }
                    else
                    {
                        noPloicyRawDataMassages.Enqueue(new RawDataMassage(frame, data._dateTime, data._dateSource));
                    }
                    unNameIndex = 0;
                }
                frameDatas.Clear();
                return frames;
            }
            else return null;
        }

        protected static string IdentifyDataType(byte[] data, string Type = "Auto")
        {
            if (data == null || data.Length == 0)
            {
                Debug.WriteLine("Empty or null data");
                return "Empty or null data";
            }

            // 尝试将byte数组转换为String，使用默认的ANSI编码
            try
            {
                string ansiString = Encoding.Default.GetString(data);
                // 如果转换成功，且没有抛出DecoderFallbackException异常，则可能是ANSI编码的字符串
                Debug.WriteLine("ANSI encoded string");
                return "str (ANSI encoding)";
            }
            catch (DecoderFallbackException)
            {
                // 如果转换失败，说明不是有效的ANSI编码
                try
                {
                    string strGbk = Encoding.GetEncoding("GBK").GetString(data);
                    return "GBK: " + strGbk;
                }
                catch (Exception)
                {
                    // 如果GBK转换也失败，返回错误信息
                    //return "无法识别的编码";
                }
            }

            // 根据字节长度判断基本数据类型
            switch (data.Length)
            {
                case 1:
                    Debug.WriteLine("byte or sbyte");
                    return "byte or sbyte";
                case 2:
                    Debug.WriteLine("short or ushort");
                    return "short or ushort";
                case 4:
                    Debug.WriteLine("int, uint, float, or ANSI encoded string (if length is 1)");
                    return "int, uint, float, or ANSI encoded string (if length is 1)";
                case 8:
                    Debug.WriteLine("long, ulong, double");
                    return "long, ulong, double";
                default:
                    break;
            }

            // 如果无法确定，返回其他
            Debug.WriteLine("unknown");
            return "unknown";
        }
        /// <summary>
        /// 用于处理传入的数据,并且将数据解析为需要显示的内容
        /// </summary>
        /// <param name="massages"></param>
        /// <returns></returns>
        public List<ChannelViewData> Process(List<ChannelMassage> massages)
        {
            Debug.WriteLine("get msg cnt is"+massages.Count());
            List < ChannelViewData > result = new List<ChannelViewData>();
            for (int i = 0; i < massages.Count; i++)
            {
                var data = massages[i].rawChannelData;
                var type = IdentifyDataType(data);
                result.Add(new ChannelViewData { Name = massages[i].Name, ChannelData = System.Text.Encoding.UTF8.GetString(data) });

            }
            return result;

        }
    }


    public class SerialDevice
    {
        public string PortName;
        public string FriendlyName;
        public bool IsConnect;

    }


    class IO_Manage: Singleton<IO_Manage>
    {
        public Vector<ChannelMassage> ChannelDatas;
        public HomeLandingPage page;
        static ChannelProcesser channelProcesser = new();
        static Dictionary<string, ChannelMassage> GlobChannelMassage = new Dictionary<string, ChannelMassage>();
        static List<ChannelViewData> GlobChannelViewData = new();
        public delegate void ReceivedCannelMsg( Dictionary<string, ChannelMassage> dataMap);
        public delegate void UpdateCannelViewMsg(List<ChannelViewData> globChannelViewData);
        public delegate void UpdateDevices(List<SerialDevice> serialDevices);
        // 基于上面的委托定义事件
        public static event ReceivedCannelMsg receivedCannelMsg ;
        public static event UpdateCannelViewMsg updateCannelViewMsg;
        public static event UpdateDevices updateDevices ;

        List<BytesIO.Serial.SerialClient> serialClients = new();
        List<BytesIO.Tcp.TcpClient> tcpClients = new();
        List<BytesIO.Kcp.KcpClient> kcpClients = new();
        List<BytesIO.Udp.UdpClient> udpClients = new();
        ConcurrentQueue<RawDataMassage> charRecQueue = new();
        Dictionary<object, IPlugProcessBase> channelProcesserMap = new();

        List<SerialDevice> serialDevices = new();

        public static void UnionGlobChannelMassage(List<ChannelMassage> Data)
        {
            if (Data == null) return;
            
            foreach (var pair in Data)
            {
                if (GlobChannelMassage.ContainsKey(pair.Name)) GlobChannelMassage[pair.Name] = pair;
                else GlobChannelMassage.Add(pair.Name, pair);

                // result.Add(pair.Key, pair.Value);
            }
            //触发钩子函数
            if(receivedCannelMsg != null)
                receivedCannelMsg( GlobChannelMassage);
            ChannelProcesser.PrintProcessedData(GlobChannelMassage.Values.ToList());

            UnionGlobChannelViewMassage(channelProcesser.Process(Data));//更新成通道数据
        }

        public static void UnionGlobChannelViewMassage(List<ChannelViewData> Data)
        {
            if (Data == null) return;   
            for (int i = 0; i < GlobChannelViewData.Count; i++)
            {
                for (int j = 0; j < Data.Count; j++)
                {
                    if (GlobChannelViewData[i].Name == Data[j].Name)
                    {
                        GlobChannelViewData[i] = Data[j];
                    }
                    else
                    {
                        GlobChannelViewData.Add(Data[j]);
                    }
                }
                //触发钩子函数
            }
            if (updateCannelViewMsg != null)
                updateCannelViewMsg(GlobChannelViewData);
            Debug.WriteLine("更新通道显示数据 "+ Data.Count +" ");
        }


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
            UnionGlobChannelMassage(channelProcesser.Process(new RawDataMassage(e.Data, DateTime.Now, RawDataMassage.DateSource.Serial, client.PortName)));
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
            IO_Manage.Instance.updateSerialDevce();
        }

        public void updateSerialDevce()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // 更新串口设备列表和友好名称
            serialDevices.Clear();
            foreach (string portName in SerialPort.GetPortNames())
            {
                SerialDevice device = new SerialDevice
                {
                    PortName = portName,
                    FriendlyName = GetFriendlyName(portName),
                    IsConnect = false
                };
                serialDevices.Add(device);
            }

            stopwatch.Stop();
            Debug.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
            if (updateDevices != null)
                updateDevices(serialDevices);
        }
        private void Watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            if (doing == 1)
            {
                doing = 0;
                //Print("update" + e.ToString() + sender.GetType().ToString());
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 30; // 设置时间间隔为100毫秒（0.1秒）
                timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerElapsed);
                timer.AutoReset = false; // 设置定时器在触发一次后不自动重置
                timer.Start();
            }
            else
            {
                return;
            }
            
        }

        private Dictionary<string, string> portFriendlyNameCache = new Dictionary<string, string>();

        private string GetFriendlyName(string portName)
        {


            if (portFriendlyNameCache.ContainsKey(portName))
            {
                return portFriendlyNameCache[portName];
            }
            try
            {
                using (var searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%({portName})%'"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        string friendlyName = obj["Caption"].ToString();
                        portFriendlyNameCache[portName] = friendlyName;
                        return friendlyName;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting friendly name for {portName}: {ex.Message}");
            }


            return portName;

        }

        IO_Manage() {


            ManagementEventWatcher watcher;

            //创建ManagmentEventWatcher 对象
            watcher = new ManagementEventWatcher("SELECT *FROM Win32_DeviceChangeEvent WHERE EventType = 2 or EventType = 3 ");
            //添加设备变化事件处理程序
            watcher.EventArrived += Watcher_EventArrived;
            //开始监听
            watcher.Start();
            Plugs.Init("C:\\Users\\80520\\source\\repos\\ACOM\\ACOMv2\\Assets\\Plugs\\");







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
