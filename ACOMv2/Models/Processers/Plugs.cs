using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ACOMPlug;

namespace ACOMv2.Models.Processers;

public class Plugs
{

    /// <summary>
    /// 递归读取文件夹中名字开头为Plug_的后缀为DLL的文件
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <returns>符合条件的文件列表</returns>
    public static List<string> GetPlugDllFiles(string path)
    {
        List<string> dllFiles = new List<string>();
        GetPlugDllFilesRecursive(path, dllFiles);
        return dllFiles;
    }

    private static void GetPlugDllFilesRecursive(string path, List<string> dllFiles)
    {
        try
        {
            foreach (var file in Directory.GetFiles(path, "ProcesserPlug*.dll"))
            {
                dllFiles.Add(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                GetPlugDllFilesRecursive(directory, dllFiles);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error accessing {path}: {ex.Message}");
        }
    }


    /// <summary>
    /// 当前拥有的插件
    /// </summary>
    static public Dictionary<string, Type> IPlugins = new Dictionary<string, Type>();
    /// <summary>
    /// 当前拥有的插件信息
    /// </summary>
    static public Dictionary<string, PluginInfo> IPluginInfos = new Dictionary<string, PluginInfo>();
    /// <summary>
    /// 文件监听
    /// </summary>
    //static FileListenerServer _fileListener = null;

    /// <summary>
    /// 初始化插件
    /// </summary>
    static public void Init(string path)
    {
        Debug.WriteLine(string.Format("==========【{0}】==========", "开始加载插件"+ path));
        // 1.获取文件夹下所有dll文件
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        //var dlls = directoryInfo.GetFiles();
        List<string> dllFiles = new();
        GetPlugDllFilesRecursive(path, dllFiles);
        Debug.WriteLine("find accept files:");

        foreach (var item in dllFiles)
        {
            Debug.WriteLine(item);

        }
        // 2.启动每个dll文件
        for (int i = 0; i < dllFiles.Count(); i++)
        {
            // 2.1 获取程序集
            var fileData = File.ReadAllBytes(dllFiles[i]);
            Assembly asm = Assembly.Load(fileData);
            var manifestModuleName = asm.ManifestModule.ScopeName;
            // 2.2 dll名称
            var classLibrayName = manifestModuleName.Remove(manifestModuleName.LastIndexOf("."), manifestModuleName.Length - manifestModuleName.LastIndexOf("."));
            Type type = asm.GetType("ProcesserPlugWaterFire.ProcesserPlugWaterFire");
            Debug.WriteLine("已加载" + dllFiles[i]);
            if (!typeof(IPlugProcessBase).IsAssignableFrom(type))
            {
                Debug.WriteLine("未继承插件接口");
                continue;
            }
            //dll实例化
            var instance = Activator.CreateInstance(type) as IPlugProcessBase;
            var protocolInfo = instance.GetPluginInformation();
            Debug.WriteLine($"插件名称：{protocolInfo.Name}");
            Debug.WriteLine($"插件版本：{protocolInfo.Version}");
            Debug.WriteLine($"插件作者：{protocolInfo.Author}");
            Debug.WriteLine($"插件时间：{protocolInfo.LastTime}");

            IPlugins.Add(protocolInfo.Name, type);
            IPluginInfos.Add(protocolInfo.Name, protocolInfo);
            //释放插件资源
            instance.Dispose();
            instance = null;
        }

        Debug.WriteLine(string.Format("==========【{0}】==========", "插件加载完成"));
        Debug.WriteLine(string.Format("==========【{0}】==========", "共加载插件{0}个"), IPlugins.Count);
    }

    static public IPlugProcessBase CreateInstance(string PlugName)
    {
        if (IPlugins.ContainsKey(PlugName))
        {
            return Activator.CreateInstance(IPlugins[PlugName]) as IPlugProcessBase;
        }
        else
        {
            Debug.WriteLine("error plug name:"+PlugName);
            return null;
        }
    }



}
