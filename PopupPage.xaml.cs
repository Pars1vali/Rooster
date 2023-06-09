using CommunityToolkit.Maui.Views;
using Rooster;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiToolkitPopupSample;

public partial class PopupPage : Popup
{

    double valueRadius = 250;   // значения радиуса по умолчанию
    public PopupPage()
    {
        InitializeComponent();
    }

   /* метод slider'а обеспечивающий выбор значения радуиса из 250, 500, 750, 1000 и показывающий визуальнео представление радуиса на карте*/
    public void set_ValueRadius(object sender, ValueChangedEventArgs e) 
    {
        double value = e.NewValue; // значение slider
        if (value >= 250 & value <= 375) // тогда устанавливаем в 250
        {
            Slider.Value = 250;
        } 
        else if (value > 375 & value<=625)// тогда устанавливаем в 500
        {
            Slider.Value = 500;
        }
        else if (value > 625 & value <= 875)// тогда устанавливаем в 750
        {
            Slider.Value = 750;
        }
        else // иначе  устанавливаем в 1000
        {
            Slider.Value = 1000;
        }

        valueRadius = e.NewValue; // записываеем значения радиуса 

        MainPage.splach_circleRadius(valueRadius); // передаем в метод MainPage для визуального отображения радуса, где должен прозвенеь будильник

        valueLabel.Text = value.ToString("f0");  // отображаем в метке значения радиуса
    }

    /*если нажата кнопка - "check_mark2", то закрываем popup и передаем значения радиуса, где должен прозвнеть будильник в MainPage*/
    private async void check_markBtn(object sender, EventArgs e)
    {
        Close(valueRadius);
    }

    /*при первом появлении popup вызвать метод из MainPage, который  отобразит радуис  на карте, где должен прозвенеть будильник с значением 250*/
    private void set_startRadius(object sender, EventArgs e)
    {
        MainPage.splach_circleRadius(valueRadius); 
    }
}