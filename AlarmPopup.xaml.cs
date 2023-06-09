using CommunityToolkit.Maui.Views;

namespace Rooster;

public partial class AlarmPopup : Popup
{
	public AlarmPopup()
	{
		InitializeComponent();
	}
    /*закрытие popup и передача значения для выключения будельника*/
    private void turnOffAlarm(object sender, EventArgs e)
    {
        Close(true);
    }
}