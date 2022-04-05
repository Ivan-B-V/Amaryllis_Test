using System;

namespace MetaweatherTests.DataTransferObjects
{
    public class TownDescExtended : TownDecsLattLong
    {
        public DateTime time { get; set; }
        public DateTime sun_rice { get; set; }
        public DateTime sun_set { get; set; }
        public string timezone_name { get; set; }
        public string timezone { get; set; }
    }
}
