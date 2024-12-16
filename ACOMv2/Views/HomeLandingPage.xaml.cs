namespace ACOMv2.Views;

using System.Diagnostics;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ICommand = System.Windows.Input.ICommand;
using ACOM.Models;
using Microsoft.UI.Xaml.Controls;
using TextControlBoxNS;
using Windows.System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinUICommunity;
using System.Management;
using static ACOMv2.ViewModels.HomeLandingViewModel;
using CommunityToolkit.WinUI.Collections;
using ACOMPlug;
using static ACOM.Models.IO_Manage;
using ACOMv2.Common;







public class ConsolString : INotifyPropertyChanged
{
    private string _totalString;

    public event PropertyChangedEventHandler PropertyChanged = delegate { };

    public ConsolString(string init_date)
    {
        this._totalString = init_date;
    }

    public string totalString
    {
        get { return this._totalString; }
        set
        {
            this._totalString = value;
            this.OnPropertyChanged();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        // Raise the PropertyChanged event, passing the name of the property whose value has changed.
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
public sealed partial class HomeLandingPage : Page
{
    public string AppInfo { get; set; }
    //public HomeLandingViewModel ViewModel { get; }

    ConsolString total_str = new("none");

    public  string ContentRequestItemName = "string.Empty";
    public bool Is_up = false;//appbar


    // public MainPage_Singleton mainPage_Singleton = MainPage_Singleton.GetInstance();

    string[] PortsDesc = { "NULL" };


    // 当文件系统发生变化时调用的事件处理器
    private static void USBOnChanged(object source, FileSystemEventArgs e)
    {
        Debug.WriteLine($"Port {e.FullPath} has changed.");
    }

    // 当文件被删除时调用的事件处理器
    private static void USBOnDeleted(object source, FileSystemEventArgs e)
    {
        Debug.WriteLine($"Port {e.FullPath} has been deleted.");
    }


    public void TextAddLine(string str)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            total_str.totalString += str;
            //dialogTextBox.AddLine(dialogTextBox.CurrentLineIndex, str);
            //dialogTextBox.SetLineText(dialogTextBox.CurrentLineIndex, str);
            //dialogTextBox.ScrollBottomIntoView();

            dialogTextBox.Text = total_str.totalString;
            //dialogTextBox.LoadText(total_str.totalString);

            str = null;
            //scrollview1.ScrollTo(scrollview1.ActualHeight + scrollview1.VerticalOffset,0);

            dialogTextBox.ScrollLineIntoView(dialogTextBox.NumberOfLines);
        });
        
        //GC.Collect();
    }

    public HomeLandingViewModel ViewModel { get; }


    private void Instance_update(List<SerialDevice> serialDevices)
    {
        try
        {
            DispatcherQueue.TryEnqueue(() =>
            {
                Debug.WriteLine("update serial1");
                Debug.WriteLine("update serial2");

                foreach (var device in serialDevices)
                {
                    if (!ViewModel.SerialPortsSource.Contains(device.PortName))
                    {
                        ViewModel.SerialPortsSource.Add(device.PortName);
                        ViewModel.serialDevices.Add(new SerialDevices(device.PortName, device.FriendlyName));
                    }
                }

                // Remove ports that are no longer available
                for (int i = ViewModel.SerialPortsSource.Count - 1; i >= 0; i--)
                {
                    if (!serialDevices.Any(d => d.PortName == ViewModel.SerialPortsSource[i]))
                    {
                        ViewModel.SerialPortsSource.RemoveAt(i);
                        ViewModel.serialDevices.RemoveAt(i);
                    }
                }
            });

        }
        catch (Exception e)
        {
            Debug.WriteLine("Instance_update DispatcherQueue error " + e.Message);
        }
        
    }

    private void UpdateCannelViewMsg(List<CannelData> globChannelViewData)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            foreach (CannelData it in globChannelViewData)
            {
                var existingItem = ViewModel.dateSource.FirstOrDefault(itt => itt.DataName == it.DataName);
                if (existingItem != null)
                {
                    existingItem.Data = it.Data;
                     // Update other properties as needed
                }
                else
                {
                    ViewModel.dateSource.Add(new CannelData(it.DataName, it.Data));
                    ViewModel.dateSource.Last().DataColor = ACOMCommmon.ColorHelper.AutoGetColor(ViewModel.dateSource.Count);
                }
            }
        });
    }

    public HomeLandingPage()
    {
        IO_Manage.Instance.page = this;
        List<SerialDevice> serialDevices = new();
        ViewModel = App.GetService<HomeLandingViewModel>();
 
        IO_Manage.updateLoadStats += IO_Manage_updateLoadStats;


        this.InitializeComponent();







        ViewModel.advancedCollectionView.SortDescriptions.Add(new SortDescription("DataName", SortDirection.Descending));

        //注册回调函数
        IO_Manage.updateDevices += Instance_update;
        IO_Manage.updateCannelViewMsg += UpdateCannelViewMsg;
        IO_Manage.Instance.updateSerialDevce();

        //ListView_LinkDevice.ItemsSource = ViewModel.advancedCollectionView;

        //ViewModel.advancedCollectionView.Add(new CannelData("data", -0, "9"));
        //ViewModel.dateSource.Add(new CannelData("data1", -0, "9"));
        //AppInfo = $"{App.Current.AppName} v{App.Current.AppVersion}";


        //Use a builtin language -> see list a bit higher
        dialogTextBox.CodeLanguage = TextControlBox.GetCodeLanguageFromId(CodeLanguageId.CSharp);

        //Use a custom language:
        // 假设你的JSON文件名为"data.json"，位于与你的程序相同的目录下
        //string filePath = "C:\\Users\\80520\\source\\repos\\ACOM\\ACOM\\Properties\\HighLightSettinng.json";
        //string jsonContent = null;
        //// 检查文件是否存在
        //if (File.Exists(filePath))
        //{
        //    // 读取文件内容到字符串
        //    jsonContent = File.ReadAllText(filePath);

        //    // 打印读取的内容
        //    Console.WriteLine(jsonContent);
        //}
        //else
        //{
        //    while (true) ;
        //}



        //dialogTextBox.Text = File.ReadAllText("C:\\Users\\80520\\Downloads\\ttt.txt");
        //var result = TextControlBox.GetCodeLanguageFromJson(jsonContent);
        //if (result.Succeed)
        //    dialogTextBox.CodeLanguage = result.CodeLanguage;
        //else {
        //    while (true) ;
        //}

    }

    private const int MaxLoadStatSourceSize = 100;
    private  int cnt = 0;
    private void IO_Manage_updateLoadStats(object sender, double load_s)
    {
        DispatcherQueue.TryEnqueue(() =>
        {
            if (ViewModel.loadStatSource.Count >= MaxLoadStatSourceSize)
            {
                ViewModel.loadStatSource.RemoveAt(0);
            }
            cnt+=1;
            ViewModel.loadStatSource.Add(new LoadStat() { Load = load_s, Time = DateTime.Now, StrTime = cnt.ToString() });
        });
    }

    private void TabView_Loaded(object sender, RoutedEventArgs e)
    {
        for (int i = 0; i < 1; i++)
        {
            if (sender != null)
            {
                (sender as TabView).TabItems.Add(CreateNewTab(i));
            }
        }
    }

    private void TabView_AddButtonClick(TabView sender, object args)
    {
        sender.TabItems.Add(CreateNewTab(sender.TabItems.Count));
    }

    private void TabView_TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
    {
        sender.TabItems.Remove(args.Tab);
    }

    private TabViewItem CreateNewTab(int index)
    {
        TabViewItem newItem = new TabViewItem();

        newItem.Header = $"Document {index}";
        newItem.IconSource = new SymbolIconSource() { Symbol = Symbol.Document };

        // The content of the tab is often a frame that contains a page, though it could be any UIElement.
        Frame frame = new Frame();

        switch (index % 3)
        {
            case 0:
                frame.Navigate(typeof(CanvasPanelPage));
                break;
            case 1:
                frame.Navigate(typeof(CanvasPanelPage));
                break;
            case 2:
                frame.Navigate(typeof(CanvasPanelPage));
                break;
        }

        newItem.Content = frame;

        return newItem;
    }

    private void HoverButton_Click(object sender, RoutedEventArgs e)
    {
        AppBarButton? button = sender as AppBarButton;
        Grid parent = button.FindParent<Grid>();
        foreach (var item in ViewModel.dateSource)
        {
            if (item.DataName == parent.Tag as string)
            {
                if (button != null)
                {
                    if (item.is_View == true)
                    {
                        FontIcon icon = new FontIcon();
                        icon.Glyph = "\uED1A";

                        button.Icon = icon;
                        item.is_View = false;
                    }
                    else
                    {
                        FontIcon icon = new FontIcon();
                        icon.Glyph = "\uE890";

                        button.Icon = icon;
                        item.is_View = true;
                    }

                }
            }
        }
    }

    private void ShowMenu(UIElement sender, bool isTransient, object Tag)
    {
        FlyoutShowOptions myOption = new FlyoutShowOptions();
        myOption.ShowMode = isTransient ? FlyoutShowMode.Transient : FlyoutShowMode.Standard;
        CommandBarFlyout1.ShowAt(sender, myOption);
    }

    private void DataList_ContextRequested(UIElement sender, ContextRequestedEventArgs args)
    {
        Grid grid = sender as Grid;
        if (grid != null)
        {
            //mainPage_Singleton.ContentRequestItemName = grid.Tag as string;
            ContentRequestItemName = grid.Tag as string;
            Debug.WriteLine(ContentRequestItemName);
            ShowMenu(sender, true,grid.Tag);
        }
    }

    private void DataList_ContextCanceled(UIElement sender, RoutedEventArgs e)
    {
        // Show a context menu in transient mode
        // Focus will not move to the menu
        // ShowMenu(sender, true);
    }

    //private void AppBarButton_Click_Play(object sender, RoutedEventArgs e)
    //{
    //    var barbutton = sender as AppBarToggleButton;

    //    if (barbutton != null)
    //    {
    //        if (barbutton.IsChecked == true)
    //        {
    //            FontIcon icon = new FontIcon();
    //            icon.Glyph = "\uE769";
    //            barbutton.Icon = icon;
    //        }
    //        else
    //        {
    //            FontIcon icon = new FontIcon();
    //            icon.Glyph = "\uE768";
    //            barbutton.Icon = icon;
    //        }
    //    }
    //}
    private void AppBarButton_Click(object sender, RoutedEventArgs e)
    {
    }
    //private void AppBarButton_Click_Down(object sender, RoutedEventArgs e)
    //{
    //    var barbutton = sender as AppBarButton;

    //    if (barbutton != null)
    //    {

    //        if (Is_up == true)
    //        {
    //            FontIcon icon = new FontIcon();
    //            icon.Glyph = "\uE896";
    //            barbutton.Icon = icon;
    //            Is_up = false;
    //        }
    //        else
    //        {
    //            FontIcon icon = new FontIcon();
    //            icon.Glyph = "\uE898";
    //            barbutton.Icon = icon;
    //            Is_up = true;
    //        }
    //    }
    //}
    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        //textBox.Background = new SolidColorBrush(Colors.RoyalBlue);
        // string str = mainPage_Singleton.ContentRequestItemName.ToString();
        if (textBox != null)
        {
            //var str = textBox.Tag as string;
            ViewModel.dateSource.FindByProperty(ContentRequestItemName, item => item.DataName).DataName = textBox.Text;

            //ViewModel.dateSource[Convert.ToInt32(str)].DataName = textBox.Text;
        }
    }

    private void myColorPicker_ColorChanged(ColorPicker sender, ColorChangedEventArgs args)
    {
        ViewModel.dateSource.FindByProperty(ContentRequestItemName, item => item.DataName).DataColor = ColorHelper.GetHexFromColor(sender.Color);
    }

    private void AppBarButton_Click_ADD(object sender, RoutedEventArgs e)
    {
    }

    private void ComboBox_DropDownOpened(object sender, object e)
    {
        Debug.WriteLine("start to scanf serial");
        //string[] _SerialPortsSource = System.IO.Ports.SerialPort.GetPortNames();
        //ViewModel.SerialPortsSource.Clear();
        //foreach (string port in _SerialPortsSource)
        //{
        //    Debug.WriteLine("serial com" + port);
        //    ViewModel.SerialPortsSource.Add(port);
        //}
    }

    private void combox_COM_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // 获取发送事件的 ComboBox
        var comboBox = sender as Microsoft.UI.Xaml.Controls.ComboBox;

        if (comboBox != null)
        {
            if (comboBox.SelectedItem != null)
            {
                foreach (SerialDevices dev in ViewModel.serialDevices)
                {
                    if (dev.DeviceName.Equals(comboBox.SelectedItem.ToString()))
                    {
                        LinkSerial_Boundrate.SelectedValue = dev.BoundRate.ToString();
                        LinkSerial_DataLength.SelectedValue = dev.DateBit.ToString();
                        LinkSerial_StopBit.SelectedValue = dev.StopBit;
                        LinkSerial_StreamCtrl.SelectedValue = dev.StreamCtrl;
                        if (dev.ConnectState)
                        {
                            ConnectButton.IsChecked = true;

                        }
                        else
                        {
                            ConnectButton.IsChecked = false;

                        }
                    }
                }
            }
            // 获取当前选中项
            var selectedItem = comboBox.SelectedItem;
            foreach (var port in PortsDesc)
            {
                Debug.WriteLine(port);
                if (selectedItem != null)
                {
                    if (port.Contains(selectedItem.ToString()))
                    {
                        TextBlockCOM_Desc.Text = port;
                        Console.WriteLine($"Changed selection to: {selectedItem}");
                        return;
                    }
                }
            }
        }
    }

    private void combox_COM_TextSubmitted(ComboBox sender, ComboBoxTextSubmittedEventArgs args)
    {
        // 获取发送事件的 ComboBox
        var comboBox = sender as Microsoft.UI.Xaml.Controls.ComboBox;

        if (comboBox != null)
        {
            // 获取当前选中项
            var selectedItem = comboBox.SelectedItem;
            TextBlockCOM_Desc.Text = IO_Manage.Instance.GetFriendlyName(selectedItem.ToString());
        }
    }

    private void ConfingSerialDeviceChanged(object sender, SelectionChangedEventArgs e)
    {

        if (LinkSerial_Boundrate != null && LinkSerial_DataLength != null &&
            LinkSerial_StopBit != null && LinkSerial_StreamCtrl != null &&
            LinkSerial_Boundrate.SelectedValue != null && LinkSerial_DataLength.SelectedValue != null &&
            LinkSerial_StopBit.SelectedValue != null && LinkSerial_StreamCtrl.SelectedValue != null && combox_COM.SelectedItem != null
            )
        {
            var portName = combox_COM.SelectedItem.ToString();
            //if()
            foreach (SerialDevices dev in ViewModel.serialDevices)
            {
                if (dev.DeviceName.Equals(portName))
                {
                    Debug.WriteLine(LinkSerial_Boundrate.SelectedValue.ToString());

                    TextBlockCOM_Desc.Text = dev.DeviceDesc;

                    dev.BoundRate = Convert.ToInt32(LinkSerial_Boundrate.SelectedValue.ToString());
                    dev.DateBit = Convert.ToInt32(LinkSerial_DataLength.SelectedValue);
                    dev.StopBit = (string)LinkSerial_StopBit.SelectedValue;
                    dev.StreamCtrl = (string)LinkSerial_StreamCtrl.SelectedValue;
                    if (LinkSerial_CheckBit.SelectedIndex == 0)
                    {
                        dev.CheckBit = "N";
                    }
                    else if (LinkSerial_CheckBit.SelectedIndex == 1)
                    {
                        dev.CheckBit = "O";
                    }
                    else if (LinkSerial_CheckBit.SelectedIndex == 2)
                    {
                        dev.CheckBit = "D";
                    }
                    else
                    {
                        dev.CheckBit = " ";
                    }
                }
            }
        }

    }
    private void LinkButton_Click(object sender, RoutedEventArgs e)
    {
        AppBarButton button = sender as AppBarButton;
        var barbutton = sender as AppBarButton;

        if (barbutton != null)
        {
            string DeviceName = barbutton.Tag as string;
            foreach (SerialDevices dev in ViewModel.serialDevices)
            {
                if (dev.DeviceName.Equals(DeviceName))
                {
                    if (dev.ConnectState)
                    {
                        dev.DisConnect();
                        ConnectButton.IsChecked = false;

                        return;
                    }
                    else
                    {
                        //TODO BUG 应该有重复的项目导致会有异常
                        dev.Connect();
                        ConnectButton.IsChecked = true; 
                        return;
                    }
                }
            }
        }
    }
    private void ConnectButton_Click(object sender, RoutedEventArgs e)
    {
        if (LinkSerial_Boundrate != null && LinkSerial_DataLength != null &&
            LinkSerial_StopBit != null && LinkSerial_StreamCtrl != null &&
            LinkSerial_Boundrate.SelectedValue != null && LinkSerial_DataLength.SelectedValue != null &&
            LinkSerial_StopBit.SelectedValue != null && LinkSerial_StreamCtrl.SelectedValue != null)
        {
            if (combox_COM.SelectedItem != null)
            {
                foreach (SerialDevices dev in ViewModel.serialDevices)
                {
                    if (dev.DeviceName.Equals(combox_COM.SelectedItem.ToString()))
                    {
                        if (dev.ConnectState)
                        {
                            dev.DisConnect();
                            ConnectButton.IsChecked = false;
                            return;
                        }
                        else
                        {
                            //TODO BUG 应该有重复的项目导致会有异常
                            dev.Connect();
                            ConnectButton.IsChecked = true;
                            return;
                        }
                    }
                }
            }
            else
            {
                // ConnectButton. = false;  
            }

        }
        else
        {
            // ConnectButton.Checked = false;
        }

    }

    
    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        if (LineEnding.Equals("\\r\\n"))
        {
 
            dialogTextBox.SetText(dialogTextBox.Text + SendTextBox.Text+"\r\n");


        }
        else if (LineEnding.Equals("\\n"))
        {
            dialogTextBox.SetText(dialogTextBox.Text + SendTextBox.Text + "\n");
        }
        else
        {
            dialogTextBox.SetText(dialogTextBox.Text + SendTextBox.Text);

        }
        dialogTextBox.ScrollPageDown();
    }

    string LineEnding = "\\r\\n";

    private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
    {
        dialogTextBox.Text = string.Empty;
        dialogTextBox.DeleteLine(1);
    }

    private void LineEndingButton_Click(object sender, RoutedEventArgs e)
    {
        if (LineEnding.Equals("无"))
        {
            LineEnding = "\\r\\n";
        }
        else if (LineEnding.Equals("\\r\\n"))
        {
            LineEnding = "\\n";

        }
        else if (LineEnding.Equals("\\n"))
        {
            LineEnding = "无";
        }

        LineEndingTextBlock.Text = LineEnding;

    }
}

