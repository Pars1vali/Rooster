using CommunityToolkit.Maui.Views;

namespace Rooster;

public partial class AlarmPopup : Popup
{
	public AlarmPopup()
	{
		InitializeComponent();
	}
    /*�������� popup � �������� �������� ��� ���������� ����������*/
    private void turnOffAlarm(object sender, EventArgs e)
    {
        Close(true);
    }
}