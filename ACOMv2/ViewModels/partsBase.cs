using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOMCommmon;
using ACOMPlug;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ACOMv2.ViewModels
{
    using CannelMassage = ValueChangedMessage<List<CannelData>>;

  
    public class Widget
    {
         //这个类实现对于每个控件的右键菜单，数据绑定，命令绑定，并且自带一个线程
        CannelMassage massage;
        Queue<List<CannelData>> frams;
        AutoResetEvent messageEvent;
        Thread processingThread;
        public FrameworkElement widget_element;//存储小部件对象

        public Widget(ref FrameworkElement element)
        {
            widget_element = element;
            massage = new CannelMassage(new List<CannelData>());
            frams = new Queue<List<CannelData>>();
            messageEvent = new AutoResetEvent(false);

            WeakReferenceMessenger.Default.Register<CannelMassage>(
            this,
            (_, m) =>
            {
                Debug.WriteLine("收到消息：" + m.Value.Count);
                frams.Enqueue(m.Value);
                messageEvent.Set();
            });

            processingThread = new Thread(new ThreadStart(ProcessMessages));
            processingThread.Start();
        }
         private void ProcessMessages()
        {
            Debug.WriteLine($"线程ID: {Thread.CurrentThread.ManagedThreadId} - 开始准备处理消息");
            while (true)
            {
                messageEvent.WaitOne();
                Debug.WriteLine($"线程ID: {Thread.CurrentThread.ManagedThreadId} - 处理消息");
                Handler();
                //bool result = messageEvent.WaitOne(1000);
            }
        }

        /// <summary>
        /// 用于处理IO Manager传递过来的数据,同时处理发送数据
        /// </summary>
        virtual public void Handler()
        {
            int received = frams.Count;
            while (received > 0)
            {
                var frame = frams.Dequeue();
                // 处理frame数据
                received--;
            }
        }
    }
}
