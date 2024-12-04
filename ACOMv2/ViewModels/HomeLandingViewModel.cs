using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System.Windows.Input;

namespace ACOMv2.ViewModels;

public class CannelDataView : ObservableObject
{
    private string _DataName;
    public string DataName
    {
        get => _DataName;
        set => SetProperty(ref _DataName, value);
    }
    public string ID
    {
        get; private set;
    }
    private double _Data;
    public double Data
    {
        get => _Data;
        set => SetProperty(ref _Data, value);
    }
    private SolidColorBrush _DataColor;
    public SolidColorBrush DataColor
    {
        get => _DataColor;
        set
        {
            SetProperty(ref _DataColor, value);

        }
    }
    public bool is_View
    {
        get; set;
    }
    public ICommand Command
    {
        get; set;
    }

    public CannelDataView(string dataName, double data, string id)
    {

        //DataColor = new SolidColorBrush(Colors.Salmon);
        DataColor = new SolidColorBrush(Microsoft.UI.Colors.RoyalBlue);
        DataName = dataName;
        ID = id;
        Data = data;
        var deleteCommand = new StandardUICommand(StandardUICommandKind.Stop);
        // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
        Command = deleteCommand;
        is_View = true;
    }
    public CannelDataView(string dataName, double data, string id, Windows.UI.Color color)
    {
        //DataColor = new SolidColorBrush(Colors.Salmon);
        DataColor = new SolidColorBrush(color);
        DataName = dataName;
        ID = id;
        Data = data;
        var deleteCommand = new StandardUICommand(StandardUICommandKind.Stop);
        // deleteCommand.ExecuteRequested += DeleteCommand_ExecuteRequested;
        Command = deleteCommand;
        is_View = true;
    }
    public bool is_equal(string id)
    {
        return ID == id;
    }
}


public partial class HomeLandingViewModel : ObservableObject
{
    public IJsonNavigationViewService JsonNavigationViewService;
    public HomeLandingViewModel(IJsonNavigationViewService jsonNavigationViewService)
    {
        JsonNavigationViewService = jsonNavigationViewService;
    }

    [RelayCommand]
    private void OnItemClick(RoutedEventArgs e)
    {
        var args = (ItemClickEventArgs)e;
        var item = (DataItem)args.ClickedItem;

        JsonNavigationViewService.NavigateTo(item.UniqueId + item.Parameter?.ToString(), item);
        //HomeLandingViewService.TextAddLine("wd");

    }

    public CannelDataView dataListDatas;

}
