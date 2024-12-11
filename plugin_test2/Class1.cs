using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ACOMPlugin.Core;
using ShadowPluginLoader.MetaAttributes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.




namespace plugin_test2.Helpers
{
}
namespace plugin_test2
{
[AutoPluginMeta]
public partial class Class1 : ACOMPluginBase
{
    public override string DisplayName => throw new NotImplementedException();

    public override string GetEmoji()
    {
        return "ASDFGHJKL";
    }
}
}
