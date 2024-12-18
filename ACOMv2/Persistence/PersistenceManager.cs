using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACOMv2.Common;
using CommunityToolkit.HighPerformance;
using DryIoc.ImTools;
using Windows.ApplicationModel.AppService;
using Windows.Storage;

namespace ACOMv2.Persistence;


/// <summary>
/// 持久化组件，完成接收数据，发送数据，日志等的数据持久化
/// </summary>
public class PersistenceManager : Singleton<PersistenceManager>
{
    public string receivedDirectoryPath = Constants.LogDirectoryPath;
    public string StartTime = DateTime.Now.ToString("yyyyMMddHHmmss");
    public string ReceiveFile;
    public string SendFile;
    public string AllFile;
    private Encoding encoding;

    PersistenceManager()
    {
        // 获取当前日期
        string currentDate = DateTime.Now.ToString("yyyyMMdd");

        // 判断日期文件夹是否存在，不存在则创建
        string dateDirectoryPath = Path.Combine(receivedDirectoryPath, currentDate);
        if (!Directory.Exists(dateDirectoryPath))
        {
            Directory.CreateDirectory(dateDirectoryPath);
        }

        // 设置存储文件路径
        ReceiveFile = Path.Combine(dateDirectoryPath, $"{StartTime}_Receive.txt");
        SendFile = Path.Combine(dateDirectoryPath, $"{StartTime}_Send.txt");
        AllFile = Path.Combine(dateDirectoryPath, $"{StartTime}_All.txt");

        // 判断文件是否存在，不存在则创建
        if (!File.Exists(ReceiveFile))
        {
            File.Create(ReceiveFile).Dispose();
        }
        if (!File.Exists(SendFile))
        {
            File.Create(SendFile).Dispose();
        }
        if (!File.Exists(AllFile))
        {
            File.Create(AllFile).Dispose();
        }
    }

    public async Task SaveReceivedDataToFileAsync(object sender, byte[] data)
    {
        // 将数据写入接收文件
        using (var fileStream = new FileStream(ReceiveFile, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
        {
            await fileStream.WriteAsync(data, 0, data.Length);
        }
        // 将数据写入所有文件
        using (var fileStream = new FileStream(AllFile, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
        {
            string rec = "[receive<--]:\r\n";
            encoding ??= Encoding.UTF8; // 默认使用 UTF-8 编码
            byte[] a = encoding.GetBytes(rec);
            await fileStream.WriteAsync(a, 0, a.Length);
            await fileStream.WriteAsync(data, 0, data.Length);
        }
    }

    public async Task SaveSendDataToFileAsync(object sender, byte[] data)
    {
        // 将数据写入发送文件
        using (var fileStream = new FileStream(SendFile, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
        {

            await fileStream.WriteAsync(data, 0, data.Length);
        }
        // 将数据写入所有文件
        using (var fileStream = new FileStream(AllFile, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
        {
            string send = "[send-->]:\r\n";
            encoding ??= Encoding.UTF8; // 默认使用 UTF-8 编码
            byte[] a = encoding.GetBytes(send);
            await fileStream.WriteAsync(a, 0, a.Length);
            await fileStream.WriteAsync(data, 0, data.Length);
        }
    }
 
}
