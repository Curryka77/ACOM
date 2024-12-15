using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml;
using ACOMCommmon;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ACOMPlug;

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

    public List<CannelData> Process(List<ChannelMassage> massages);

}

public interface IPlugWidgetBase : IDisposable
{
    PluginInfo GetPluginInformation();

    void Parts_ContextRequested(UIElement sender, ContextRequestedEventArgs args);//用于呼出右键菜单
    FrameworkElement Create();
}
