using CommunityToolkit.Maui.Views;
using Rooster;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiToolkitPopupSample;

public partial class PopupPage : Popup
{

    double valueRadius = 250;   // �������� ������� �� ���������
    public PopupPage()
    {
        InitializeComponent();
    }

   /* ����� slider'� �������������� ����� �������� ������� �� 250, 500, 750, 1000 � ������������ ���������� ������������� ������� �� �����*/
    public void set_ValueRadius(object sender, ValueChangedEventArgs e) 
    {
        double value = e.NewValue; // �������� slider
        if (value >= 250 & value <= 375) // ����� ������������� � 250
        {
            Slider.Value = 250;
        } 
        else if (value > 375 & value<=625)// ����� ������������� � 500
        {
            Slider.Value = 500;
        }
        else if (value > 625 & value <= 875)// ����� ������������� � 750
        {
            Slider.Value = 750;
        }
        else // �����  ������������� � 1000
        {
            Slider.Value = 1000;
        }

        valueRadius = e.NewValue; // ����������� �������� ������� 

        MainPage.splach_circleRadius(valueRadius); // �������� � ����� MainPage ��� ����������� ����������� ������, ��� ������ ��������� ���������

        valueLabel.Text = value.ToString("f0");  // ���������� � ����� �������� �������
    }

    /*���� ������ ������ - "check_mark2", �� ��������� popup � �������� �������� �������, ��� ������ ��������� ��������� � MainPage*/
    private async void check_markBtn(object sender, EventArgs e)
    {
        Close(valueRadius);
    }

    /*��� ������ ��������� popup ������� ����� �� MainPage, �������  ��������� ������  �� �����, ��� ������ ���������� ��������� � ��������� 250*/
    private void set_startRadius(object sender, EventArgs e)
    {
        MainPage.splach_circleRadius(valueRadius); 
    }
}