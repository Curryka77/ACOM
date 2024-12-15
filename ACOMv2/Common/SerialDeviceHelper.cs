using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACOMv2.Common;
internal class SerialDeviceHelper
{
    public static string ConvertCheckBitToFriendlyName(string str)
    {
        return null;
    }
    readonly private static List<string> checkBit = new()
    {
        "无",
        "奇校验",
        "偶校验",
    };
    readonly private static List<string> streamCtrl = new()
    {
        "无",
        "CTS/RTS",
        "XON/XOFF",
    };
    readonly private static List<string> stopBit = new()
    {
        "1",
        "1.5",
        "2",
    };
    readonly private static List<string> dataLength = new()
    {
        "5",
        "6",
        "7",
        "8",
        "9",
    };
    public static List<string> CheckBit { get => checkBit; }
    public static List<string> StreamCtrl { get => streamCtrl; }
    public static List<string> StopBit { get => stopBit; }
    public static List<string> DataLength { get => dataLength; }
 }
