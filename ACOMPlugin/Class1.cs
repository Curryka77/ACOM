using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using Serilog;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
// 示例代码
using ShadowPluginLoader.MetaAttributes;
using ShadowPluginLoader.WinUI;
using ShadowPluginLoader.WinUI.Models;

 namespace ACOMPlugin.Core;

[ExportMeta]
public class ACOMPluginMetaData : AbstractPluginMetaData
{
    [Meta(Required = true, PropertyGroupName = "Author")]
    public string Author { get; init; }
}

public abstract class ACOMPluginBase : AbstractPlugin
{
     public abstract string GetEmoji();
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

