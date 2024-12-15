
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

namespace DataMuxerPlugAutoOne;
public class DataMuxerPlugAutoOne : IPlugDataMuxBase
{
    /// <summary>
    /// 获取插件信息
    /// </summary>
    /// <returns></returns>
    public PluginInfo GetPluginInformation()
    {
        return new PluginInfo()
        {
            Author = "Dyyt587",
            Name = "DataMuxerPlugAutoOne",
            Version = "V0.0.2",
            LastTime = DateTime.Now
        };
    }

    void IDisposable.Dispose()
    {
        Console.WriteLine("释放内存");
    }
    protected static string IdentifyDataType(byte[] data,string Type = "Auto")
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
    public List<CannelData> Process(List<ChannelMassage> massages)
    {
        for(int i = 0; i < massages.Count; i++)
        {
            var data = massages[i].rawChannelData;
            var type = IdentifyDataType(data);
         }
        return null;

    }

    //public List<CannelData> Process(List<ChannelMassage> massages)
    //{
    //    throw new NotImplementedException();
    //}
}
