using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;

namespace Opendata
{
    public class JSON
    {

        public class Rootobject
        {
            public Paging paging { get; set; }
            public Datum[] data { get; set; }
        }

        public class Paging
        {
            public int[] next { get; set; }
            public int[] last { get; set; }
            public int records { get; set; }
            public int pages { get; set; }
            public int pageCurrent { get; set; }
            public int pageNext { get; set; }
            public object pagePrev { get; set; }
            public int pageSize { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string glascontainer_nr { get; set; }
            public string point_lat { get; set; }
            public string point_lng { get; set; }
            public string straatnaam { get; set; }
            public string huisnummer { get; set; }
            public string postcode { get; set; }
            public string type { get; set; }
            public string ondergrond_verhard { get; set; }
            public string opmerking { get; set; }
            public string begindatum { get; set; }
            public string objectid { get; set; }
            public string gisid { get; set; }
            public Location locatie { get; set; }
        }
    }
}
