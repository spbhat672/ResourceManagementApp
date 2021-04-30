using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceManagement.Models
{
    public class ResourceRequestModel
    {
        public Header header { get; set; }
        public Body body { get; set; }

        public class Header
        {
            public int requestId { get { return 1; } }
            public string requestType { get { return "resourceState"; } }
            public Producer producer { get; set; }
            public string version { get { return "1.0"; } }
            public DateTime dateMsg { get { return (Convert.ToDateTime("20180203 10:00:00")); } }
        }

        public class Producer
        {
            public string id { get { return "1"; } }
        }

        public class Body
        {
            public DateTime dates { get { return (Convert.ToDateTime("2018-01-30T08:00:00Z")); } }
            public ItemSet itemSet { get; set; }

        }

        public class ItemSet
        {
            public Items items { get; set; }
        }

        public class Items
        {
            public string id { get { return "extid1"; } }
            public ResourceWithValue tags { get; set; }
        }

        public class ResourceWithValue
        {
            public long Id { get; set; }

            public int TypeId { get; set; }

            public string Type { get; set; }

            public int StatusId { get; set; }

            public string Status { get; set; }

            public long LocationId { get; set; }

            public Location LocationValue { get; set; }

            public string Name { get; set; }
        }


        public class Location
        {
            public long Id { get; set; }

            public decimal X { get; set; }

            public decimal Y { get; set; }

            public decimal Z { get; set; }

            public decimal Rotation { get; set; }
        }
    }
}