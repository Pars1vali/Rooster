using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooster
{
   public class PublicTranssport
    {
        /*класс общесвтенного трансопрта*/

        public LinkedList<PublicTransportStop> listStop;
        public PublicTranssport(string numberPublicTransport)
        {
            if(numberPublicTransport == "4")
            {
                listStop = new LinkedList<PublicTransportStop>();
                init_tram4();
            }
        }
        private void init_tram4()
        {
            listStop.AddLast(new PublicTransportStop(new double[] { 45.03899, 39.093872 }, "Улица Тюляева"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.037880, 39.089990 }, "Уральская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.035971, 39.086601 }, "Симферопольская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.034834, 39.083038 }, "Монтажная"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.033448, 39.078823 }, "Просторная"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.031370, 39.072136 }, "ЖК на Магистральной"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.031026, 39.067472 }, "Онежская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.030805, 39.064328 }, "Магистральная"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.027609, 39.064260 }, "Нежинская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.024486, 39.065532 }, "Сормовская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.019079, 39.068218 }, "Детские Ясли"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.014794, 39.063049 }, "ТЭЦ"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.013462, 39.057532 }, "Парк Солнечный Остров"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.013245, 39.051552 }, "Супермаркет"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.015149, 39.047228 }, "Старокубанская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.016382, 39.042824 }, "Стасова"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.017439, 39.037854 }, "Кинотеатр Болгария"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.018429, 39.033433 }, "Айвазовского"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.019367, 39.029032 }, "Университет"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.020699, 39.023191 }, "Восточное Депо"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.021355, 39.019348 }, "Полины Осипенко"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.021355, 39.019348 }, "Таманская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.020865, 39.010479 }, "Шевченко"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.020340, 39.004524 }, "Павлова"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.019957, 39.000356 }, "Вишняковой"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.023440, 38.992069 }, "Стадион Кубань"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.022148, 38.988804 }, "Железнодорожная"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.024759, 38.984028 }, "Базовская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.025440, 38.979839 }, "Янковского"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.026114, 38.975818 }, "Кооперативный Рынок"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.023712, 38.974430 }, "Ленина "));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.020519, 38.973387 }, "Мира"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.017398, 38.972308 }, "Советская"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.014491, 38.971366 }, "Городской Сад"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.010470, 38.968770 }, "Водолечебница "));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.005923, 38.967069 }, "Завод Седина"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.002746, 38.964516 }, "Нефтезавод"));
            listStop.AddLast(new PublicTransportStop(new double[] { 45.000083, 38.962648 }, "Индустриальная "));


        }

    }
}
