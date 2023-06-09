using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rooster
{
    /*класс остановок общественного транспорта*/
    public class PublicTransportStop
    {
       public double[] cordinates { get; set; }
       public string NameStop { get; set; }

        public PublicTransportStop(double[] cordinates, string NameStop)
        {
            this.cordinates = cordinates;
            this.NameStop = NameStop;
        }

        public static explicit operator PublicTransportStop(LinkedListNode<PublicTransportStop> v)
        {
            throw new NotImplementedException();
        }
    }
}
