using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOMPlugin.Core;
using DryIoc;
using Serilog;
using ShadowPluginLoader.MetaAttributes;
using ShadowPluginLoader.WinUI;
using ShadowPluginLoader.WinUI.Models;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

// 示例的插件主类
namespace ACOMPlugin.WidgetPluginSlide;

[AutoPluginMeta]
public partial class WidgetPluginSlide : ACOMPluginBase
{
    public override string DisplayName => throw new NotImplementedException();

    public override string Id => throw new NotImplementedException();

    public override string GetEmoji()
    {
        throw new NotImplementedException();
    }
}
