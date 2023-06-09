using CommunityToolkit.Maui.Views;
using MauiToolkitPopupSample;
using MetroLog;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Plugin.Maui.Audio;
using System;
using System.Linq;

namespace Rooster;

public partial class MainPage : ContentPage
{
    static WebView webView; // переменная, обеспечивающая доступ к WebView карты

    static ILogger<MainPage> _logger;   // объект для вывода инфорамции в консоль - logger
    private IAudioManager audioManager; // объект аудипроигрывателя
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    double radius = 250;    // раудиус в котором должен прозвенеть будильник

    double latitudeX0, longitudeY0; // координаты точки , где находятся пользователь
    double latitudeX1, longitudeY1; // координаты метки, в радиусе которой должен прозвенеть будильник

    bool isClicked = true;  // переменная хранит значение была ли нажата кнопка, по умолчанию - нажата
    bool inArea = false;    // переменная хранит значения находится ли пользователь в радиусе метки, по умолчанию - нет
    bool isCancel = false;  // переменная хранит значения, нужно ли выключить будильник, по умолчанию - нет


    PublicTranssport tram_4;


    public MainPage(ILogger<MainPage> logger, IAudioManager audioManager)
    {
        InitializeComponent();
        _logger = logger;   // инициализация объекта logger
        this.audioManager = audioManager;   // инициализация аудипроигрывателя
        webView = map;  // присваивание значение статической переменной карты
    }

    /*Метод вызывает задачу для опредления  гео-локацию пользователя и отображения точки гео - локации на карте*/
    private async void GeoLocationTap(object sender, TappedEventArgs e)
    {
        await GetCurrentLocation(true); // вызов метода для опредления гео-локации
    }


    /*задача определяет гео-локацию пользователя и взывает js метод из файла index.html, передав ему кординаты пользователя для отображения гео-позиции на карте*/
    async Task GetCurrentLocation(bool isSetCenter)
    {
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(1));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token); // опредления гео-локации пользователя

            if (location != null)
            {
                latitudeX0 = location.Latitude; // широта
                longitudeY0 = location.Longitude;  // долгота 

                /*следующие действия обеспечивавют корректную передачу парметров в js метод  - широты, долготы, логического значения - нужно ли приближать точку гео-локации*/
                double latitude = latitudeX0 * 1000000000000;
                double longitude = longitudeY0 * 1000000000000;
                string longt = longitude + "";
                if (isSetCenter == true)
                {
                    longt = longitude + ", true";
                }
                else if (isSetCenter == false)
                {
                    longt = longitude + ", false";
                }

                await map.EvaluateJavaScriptAsync($"locat({latitude},{longt})");    // вызов js метода для отображения гео-локации пользователя и вывод данных в консоль
            }

        }
        catch (Exception ex)
        {
            await DisplayAlert("Проблема с GPS", "Не удается получить сигнал", "OK"); // если gps не включен
        }
        finally
        {
            _isCheckingLocation = false;
        }
    }

    void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }

    /*метод кнопки splach_pointerBtn, при первом нажатии на кнопку появляется метка и кнопка set_pointerBtn - "для утверждения метки"*/
    private async void splach_pointer(object sender, EventArgs e)
    {
        if (isClicked)
        {
            await map.EvaluateJavaScriptAsync($"splach_pointer()"); // вызов js метода, для отображения предпросмотра метки
            set_pointerBtn.IsVisible = true;    // показываем кнопку set_pointerBtn
            isClicked = false;
        }
        else
        {
            isClicked = true;
            await map.EvaluateJavaScriptAsync($"hide_splach()"); // вызов js метода для скрытия метки предпросмотра
            set_pointerBtn.IsVisible = false; // скрываем кнопку set_pointerBtn
        }
    }

    /*метод устанавливающий метку на карте и определяющие гео-кординаты метки*/
    private async void set_pointer(object sender, EventArgs e)
    {
        string line = await map.EvaluateJavaScriptAsync($"set_pointer()");  // вызов js метода, внедреющего метку на карту и возвращающего её коррдинаты 
        line = line.Trim(new char[] { '[', ']', ' ' });
        string[] lines = line.Split(',');
        lines[0] = lines[0].Replace('.', ',');
        lines[1] = lines[1].Replace('.', ',');
        double[] coordinates = new double[] { Convert.ToDouble(lines[1]), Convert.ToDouble(lines[0]) };

        latitudeX1 = coordinates[0]; // широта метки
        longitudeY1 = coordinates[1];   // долготы метки

        set_pointerBtn.IsVisible = false; // скрываем кнопку set_pointerBtn
        set_circleRadius(); // вызываем метод для отображения popup для выбора радиуса, где должен прозвенеть будильник
    }

    /*метод для показа popup для выбора радуиса, где прозвенит будильник*/
    public async void set_circleRadius()
    {
        try
        {
            var popup = new PopupPage(); //создания объекта popup
            var result = await this.ShowPopupAsync(popup); // показ его на экране
            radius = (double)result; // выбранный радиус пользоватлем записываем
            await map.EvaluateJavaScriptAsync($"set_circleRadius({radius})"); // вызываем js метод для отображения на карте радиуса, где прозвенит будильник
            distanceCalc(); // вызваем метод для подсета растояния от положения пользователя до радиуса метки
            cancel.IsVisible = true; // показываем кнопку cancel
        }
        catch (System.NullReferenceException)// если пользователь вышел из popup
        {
            await DisplayAlert("Данные не введены", "Укажите радиус местности, в котором должен прозвенеть будильник", "OK");   // показываем предупреждение
            set_circleRadius(); // вызываем этот же метод
        }
    }

    /*определяем растояние между меткой и положением пользователя*/
    public async void distanceCalc()
    {
        if (isCancel)  // если нажата кнопка отмены прекращаем работу метода 
        {
            isCancel = false;
            return;
        }
        await GetCurrentLocation(false); // определяем гео-положение пользователя, не приблиая карту к точке где он находится

        double theta = longitudeY0 - longitudeY1;
        double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
            Math.Sin(latitudeX0 * (Math.PI / 180)) * Math.Sin(latitudeX1 * (Math.PI / 180)) +
            Math.Cos(latitudeX0 * (Math.PI / 180)) * Math.Cos(latitudeX1 * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180))
        ); // высчитываем растояние между точками

        inArea = (Math.Round(distance * 1.609344, 2)) <= radius / 1000; // входит ли положение пользователя в радиус метки
        _logger.LogInformation("distance: " + Math.Round(distance * 1.609344, 2) + " inArea: " + inArea); // выводим данные в коносль

        if (inArea) // если входит, вызываем будильник и прекращаем работу метода
        {
            setAlarm();
            return;
        }
        else //если нет, то делаем задержку этого метода  на 5 секунд и вызываем его снова
        {
            await Task.Delay(5000);
            distanceCalc();
        }
    }

    /*метод включащий будильник и играющий  его жо того пока пользователя не нажмет на появивщимся popup кнопку - ""*/
    public async void setAlarm()
    {
        var player = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("rooster_sound.mp3")); // инциализируем объект с выбранной песней
        player.Loop = true; // играть постоянно
        player.Play(); //включить аудипроигрыватель
        var alarm = new AlarmPopup(); // создание объекта popup
        var result = await this.ShowPopupAsync(alarm); // показ popup
        if (result is bool == true) //когда popup вернет значение выключить аудипроигрыватель
        {
            player.Stop();
        }
    }

    public static async void splach_circleRadius(double value)
    {
        await webView.EvaluateJavaScriptAsync($"splach_circleRadius({value})");
    }

    /*метод кнопки - "cancel", выключающий будильник*/
    private async void cancelBtn(object sender, EventArgs e)
    {
        isCancel = true; // прекратить будильник
        cancel.IsVisible = false; // сделать кнопку cancek не видимой
        await map.EvaluateJavaScriptAsync($"cancel()"); // вызов js метода убирающего метку и радиус с карты

    }

    // метод для создания будуильника, который прозвенит по приезде к остановке
    private async void setStopAlarm(object sender, EventArgs e)
    {
        tram_4 = new PublicTranssport("4"); // создания объекта общественного транспорта  трамвая №4
        PublicTransportStop userStop; // остановка на которой пользователь
        PublicTransportStop stop_chousing = null;   // остановка выбранная пользователем
        PublicTransportStop stop_alarm;

        bool direction = true; // навпрление в котром двигается пользователь по умолчанию  - сверху вниз списка остановок

        // показ popup для выбора остановки
        var picker_popup = new PickerStop(tram_4);
        string result_picker_popup = (string)(await this.ShowPopupAsync(picker_popup));

        stop_chousing = tram_4.listStop.Where(x => x.NameStop == result_picker_popup).First();
        
        double lat = stop_chousing.cordinates[0] * 1000000000000;
        double longt = stop_chousing.cordinates[1] * 1000000000000;
        await map.EvaluateJavaScriptAsync($"draw_marker({lat},{longt})"); // вызов js метода для отображения метки остановки

        var slider_popup = new CountStop();
        int result_slider_popup = (int)await this.ShowPopupAsync(slider_popup);

        await GetCurrentLocation(false); // опредлеяем текущее расположение пользователя не приближая его на карте
        userStop = defineStopUser(); // опредлеяем остановку на которой находится пользователь
        _logger.LogInformation(""+userStop.NameStop);
        // определяем направление движение
        foreach (PublicTransportStop stop in tram_4.listStop)
        {
            if (stop.NameStop == userStop.NameStop)
            {
                direction = false;
                break;
            }
            if (stop.NameStop == stop_chousing.NameStop)
            {
                direction = true;
                break;
            }
        }

        // определить остановку за result_slider_popup до выбранной
        stop_alarm = stop_chousing; // остановка на которой должен прозвенеть будильник
        if (direction)
        {
            _logger.LogInformation("" + direction);
            for (int i = 0; i < result_slider_popup; i++)
            {
                PublicTransportStop stop = tram_4.listStop.Find(stop_alarm).Next.Value;
                stop_alarm = stop;
                _logger.LogInformation("" + stop_alarm.NameStop + "- stop before");
            }
        }
        else if(!direction)
        {
            _logger.LogInformation(""+direction);
            for (int i = 0; i < result_slider_popup; i++)
            {
                PublicTransportStop stop = tram_4.listStop.Find(stop_alarm).Previous.Value;
                stop_alarm = stop;
                _logger.LogInformation("" + stop_alarm.NameStop + "- stop after");
            }

        }

        lat = stop_alarm.cordinates[0] * 1000000000000;
        longt = stop_alarm.cordinates[1] * 1000000000000;
        await map.EvaluateJavaScriptAsync($"set_poinetStop({lat},{longt})");

        cancel.IsVisible = true;
        distanceFromUserToStop(stop_alarm);
        
        _logger.LogInformation(userStop.NameStop + " " + stop_chousing.NameStop  + " " + stop_alarm.NameStop);

    }

    private PublicTransportStop defineStopUser()
    {
        PublicTransportStop nerlyStop = null;
        double min_distance = 1000;

        foreach (PublicTransportStop stop in tram_4.listStop)
        {
            double theta = longitudeY0 - stop.cordinates[1];
            double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
                Math.Sin(latitudeX0 * (Math.PI / 180)) * Math.Sin(stop.cordinates[0] * (Math.PI / 180)) +
                Math.Cos(latitudeX0 * (Math.PI / 180)) * Math.Cos(stop.cordinates[0] * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180))
            ) * 1.609344; // высчитываем растояние между точками

            if (distance < min_distance)
            {
                min_distance = distance;
                nerlyStop = stop;
            }
        }
        return nerlyStop;
    }

    private async void distanceFromUserToStop(PublicTransportStop stop_alarm)
    {
        if (isCancel)
        {
            isCancel = false;
            return;
        }
        await GetCurrentLocation(false);
        double theta = longitudeY0 - stop_alarm.cordinates[1];
        double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
            Math.Sin(latitudeX0 * (Math.PI / 180)) * Math.Sin(stop_alarm.cordinates[0] * (Math.PI / 180)) +
            Math.Cos(latitudeX0 * (Math.PI / 180)) * Math.Cos(stop_alarm.cordinates[0] * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180))
        ) * 1.609344;

        _logger.LogInformation("distance: " + distance);
        if (distance < 0.1)
        {
            setAlarm();
        }
        else
        {
            await Task.Delay(5000);
            distanceFromUserToStop(stop_alarm);
        }
    } 


}



