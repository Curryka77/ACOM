using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOMPlug;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DataMuxBasePlugAutoOne;
public class DataMuxBasePlugAutoOne : IPlugDataMuxBase
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
            Name = "DataMuxBasePlugAutoOne",
            Version = "V0.0.1",
            LastTime = DateTime.Now
        };
    }

    void IDisposable.Dispose()
    {
        Console.WriteLine("释放内存");
    }

    /// <summary>
    /// 用于处理传入的数据,并且将数据解析为需要显示的内容
    /// </summary>
    /// <param name="massages"></param>
    /// <returns></returns>
    public List<ChannelViewData> Process(List<ChannelMassage> massages)
    {

        return null;

    }
}
