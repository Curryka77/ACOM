using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOMPlug;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ACOMv2.ViewModels
{
    using CannelMassage = ValueChangedMessage<List<ChannelViewData>>;


    public class partsBase
    {
        CannelMassage massage;
        Queue<List<ChannelViewData>> frams;
        AutoResetEvent messageEvent;
        Thread processingThread;

        public partsBase()
        {
            massage = new CannelMassage(new List<ChannelViewData>());
            frams = new Queue<List<ChannelViewData>>();
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
