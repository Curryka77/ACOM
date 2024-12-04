using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOMPlug;
using PluginInfo = ACOMPlug.PluginInfo;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProcesserPlugWaterFire;
public class ByteSplitter
{
    public static List<byte[]> Split(List<byte> data, List<byte> delimiter)
    {
        var result = new List<byte[]>();
        var start = 0;
        int index;

        while ((index = IndexOf(data, delimiter, start)) != -1)
        {
            var length = index - start;
            var chunk = data.GetRange(start, length).ToArray();
            result.Add(chunk);
            start = index + delimiter.Count;
        }

        // Add the last chunk
        if (start < data.Count)
        {
            var chunk = data.GetRange(start, data.Count - start).ToArray();
            result.Add(chunk);
        }

        return result;
    }
    public static List<byte[]> Split(byte[] data, List<byte> delimiter)
    {
        var result = new List<byte[]>();
        var start = 0;
        int index;

        while ((index = IndexOf(data, delimiter, start)) != -1)
        {
            var length = index - start;
            var chunk = new byte[length];
            Array.Copy(data, start, chunk, 0, length);
            result.Add(chunk);
            start = index + delimiter.Count;
        }

        // Add the last chunk
        if (start < data.Length)
        {
            var chunk = new byte[data.Length - start];
            Array.Copy(data, start, chunk, 0, chunk.Length);
            result.Add(chunk);
        }

        return result;
    }

    private static int IndexOf(byte[] data, List<byte> delimiter, int start)
    {
        for (var i = start; i <= data.Length - delimiter.Count; i++)
        {
            var match = true;
            for (var j = 0; j < delimiter.Count; j++)
            {
                if (data[i + j] != delimiter[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return i;
            }
        }
        return -1;
    }
    private static int IndexOf(List<byte> data, List<byte> delimiter, int start)
    {
        for (var i = start; i <= data.Count - delimiter.Count; i++)
        {
            var match = true;
            for (var j = 0; j < delimiter.Count; j++)
            {
                if (data[i + j] != delimiter[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                return i;
            }
        }
        return -1;
    }
}
public class ProcesserPlugWaterFire : IPlugProcessBase
{


    Dictionary<string, ChannelMassage> dataMap = new Dictionary<string, ChannelMassage>();
    List<byte> tmpbytes = new List<byte>(128);
    bool receivedFrame = false;
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
        var _data = new List<ChannelMassage>(8);
        var cnt = 0;
        var lastcnt = 0;
        //Debug.WriteLine("1");
        var i = 0;
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
            lastcnt = cnt + 1;
            if (!receivedFrame)
            {
                Debug.WriteLine("数据处理完毕");
                return _data;
            }
            receivedFrame = false;
            var lists = ByteSplitter.Split(tmpbytes, new List<byte> { (byte)':' });
            if (lists.Count > 2)
            {
                //TODO:错误的格式，只取最后一个:分割的前面一段作为名字
            }
            else if (lists.Count == 2)
            {
                //对其进行再次分包
                var NameLists = ByteSplitter.Split(lists[0], new List<byte> { (byte)',' });
                var dateLists = ByteSplitter.Split(lists[1], new List<byte> { (byte)',' });
                //获取两个List的较大值
                var count = NameLists.Count > dateLists.Count ? NameLists.Count : dateLists.Count;
                //创建count个ChannelMassage
                for (var j = 0; j < count; ++j)
                {
                    var Name = NameLists.ElementAtOrDefault(j) ?? ("Data" + j.ToString()).GetBytes();
                    var date = dateLists.ElementAtOrDefault(j) ?? "No Data".GetBytes();
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
