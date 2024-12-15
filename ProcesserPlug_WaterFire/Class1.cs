using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOMCommmon;
using ACOMPlug;
using PluginInfo = ACOMPlug.PluginInfo;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProcesserPlugWaterFire;
public class ProcesserPlugWaterFire : IPlugProcessBase
{


    List<byte> tmpbytes = new List<byte>(4096);
    List<byte[]> frameData = new();
    static int unNameIndex = 0;
    Queue<RawDataMassage> noPloicyRawDataMassages = new Queue<RawDataMassage>(20);
    /// <summary>
    /// 获取插件信息
    /// </summary>
    /// <returns></returns>
    public PluginInfo GetPluginInformation()
    {
        return new PluginInfo()
        {
            Author = "Dyyt587",
            Name = "ProcesserPlugWaterFire",
            Version = "V0.0.1",
            LastTime = DateTime.Now
        };
    }
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

    void IDisposable.Dispose()
    {
        Console.WriteLine("释放内存");
    }

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
                frameData.Add(tmpbytes.ToArray());
                tmpbytes.Clear();
            }
        }
        if (frameData.Count != 0)
        {
            List<ChannelMassage> frames = new List<ChannelMassage>(frameData.Count);
            foreach (var frame in frameData)
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
                            Name = i < leftPart.Count ? leftPart[i] : "Channel" + unNameIndex++,
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
            }
            frameData.Clear();
            unNameIndex = 0;
            return frames;
        }
        else return null;
    }

}


public static class StringExtensions
{
    /// <summary>
    /// 将字符串转换为字节数组
    /// </summary>
    /// <param name="str">要转换的字符串</param>
    /// <returns>转换后的字节数组</returns>
    public static byte[] GetBytes(this string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }
}
public static class ByteArrayExtensions
{
    public static string EncodeToString(this byte[] data)
    {
        return Encoding.UTF8.GetString(data);
    }
}
