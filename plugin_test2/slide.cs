using System;
using System.Collections.Generic;
using ACOMCommmon;



//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ACOMPlugin.Core;
using Microsoft.UI.Xaml;
using ShadowPluginLoader.MetaAttributes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.




namespace WidgetPlug.Slide.Helpers
{
}
namespace WidgetPlug.Slide
{
[AutoPluginMeta]
public partial class Slide : ACOMPluginBase
{
        WidgetSlide widget;
        public override string DisplayName => throw new NotImplementedException();

        public override IEnumerable<string> ResourceDictionaries => base.ResourceDictionaries;

        public override FrameworkElement Create()
        {
            widget = new WidgetSlide();
            return widget;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string GetEmoji()
        {
            return "Happy";
        }
        //public void UpdateData(List<CannelData> newData)
        //{

        //}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void UpdateData(List<CannelData> newData)
        {
             
        }

        protected override void Disable()
        {
            base.Disable();
        }

        protected override void Enable()
        {
            base.Enable();
        }

    }
}
