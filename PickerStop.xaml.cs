using CommunityToolkit.Maui.Views;

namespace Rooster;

public partial class PickerStop : Popup
{
    public PublicTranssport transport;
    public PickerStop(PublicTranssport transport)
    {
        InitializeComponent();
        this.transport = transport;
        var stopList = new List<string>();
        foreach(PublicTransportStop stop in transport.listStop)
        {
            stopList.Add(stop.NameStop);
        }
        picker.Title = "Выберите остановку";
        picker.ItemsSource = stopList;
    }

    private void closePickerPopup(object sender, EventArgs e)
    {
        Close(picker.SelectedItem);
    }
}