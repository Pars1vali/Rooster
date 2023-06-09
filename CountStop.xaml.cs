using CommunityToolkit.Maui.Views;

namespace Rooster;

public partial class CountStop : Popup
{
	int countStop = 2;
	public CountStop()
	{
		InitializeComponent();
	}
	public void set_CountStop(object sender, ValueChangedEventArgs e)
	{
		double  value =  e.NewValue;
        if (value >= 2 & value <= 2.5) // ����� ������������� � 2
        {
            Slider.Value = 2;
        }
        else if (value > 2.5 & value <= 3.5)// ����� ������������� � 3
        {
            Slider.Value = 3;
        }
        else // �����  ������������� � 4
        {
            Slider.Value = 4;
        }

        countStop = (int) e.NewValue;

        valueLabel.Text = countStop + " ���������";
    }

    private void hide_popup(object sender, EventArgs e)
    {
        Close(countStop);
    }
}