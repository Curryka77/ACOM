using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ACOMPlug;
public class ChannelViewData
{
    public string ChannelData;
    public string Name;
    public string ExpendData;//扩展数据，用于隐藏部分的显示
    public DateTime DateTime;
}
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
public class PluginInfo
{
    public string Name { set; get; }

    public string Version { set; get; }

    public string Author { set; get; }

    public DateTime LastTime { set; get; }
}

public interface IPlugProcessBase : IDisposable
{
    PluginInfo GetPluginInformation();

    public List<ChannelMassage> Process(RawDataMassage data);
    public RawDataMassage GetNopolicyData();
}


public interface IPlugDataMuxBase : IDisposable
{
    PluginInfo GetPluginInformation();

    public List<ChannelViewData> Process(List<ChannelMassage> massages);

}

public interface IPlugWidgetBase : IDisposable
{
    PluginInfo GetPluginInformation();

    void Parts_ContextRequested(UIElement sender, ContextRequestedEventArgs args);//用于呼出右键菜单
    FrameworkElement Create();
}
