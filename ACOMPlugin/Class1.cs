using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using DryIoc;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml;
using Serilog;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
// 示例代码
using ShadowPluginLoader.MetaAttributes;
using ShadowPluginLoader.WinUI;
using ShadowPluginLoader.WinUI.Models;
using Windows.Devices.SerialCommunication;

 namespace ACOMPlugin.Core;



[ExportMeta]
public class ACOMPluginMetaData : AbstractPluginMetaData
{
    [Meta(Required = true, PropertyGroupName = "Author")]
    public string Author { get; init; }
}

public class ChannelData
{
    public string CData;
    public string Name;
    public string ExpendData;//扩展数据，用于隐藏部分的显示
    public DateTime DateTime;
}
public abstract class ACOMPluginBase : AbstractPlugin
{

     public abstract string GetEmoji();
    public abstract FrameworkElement Create();

    public abstract void UpdateData(List<ChannelData> newData);
    /// <summary>
    /// cmd 为 cmd_name;cmd_arg1;cmd_arg2
    /// </summary>
    /// <param name="cmd"></param>
    public delegate void UpdateCommands(List<string[]> cmd);
    public  event UpdateCommands UpdateCommand;//当外部设备变化触发
    public ACOMPluginBase()
    {

    }

     ~ACOMPluginBase()
    {
        UpdateCommand = null;
    }
}
 
public class ACOMPluginLoader : AbstractPluginLoader<ACOMPluginMetaData, ACOMPluginBase>
{
    protected override string PluginFolder { get; } = "ACOMPlugin";

    public ACOMPluginLoader(ILogger logger) : base(logger)
    {
    }
    public ACOMPluginLoader() : base()
    {
    }

}


