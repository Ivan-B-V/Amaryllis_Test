using System.Collections.Generic;

namespace MetaweatherTests.DataTransferObjects
{
    class ResponceLocation : TownDescExtended
    {
        public List<WheatherDescription> consolidated_weather { get; set; }
        public TownDescBase parent { get; set; }
        public List<Source> sources { get; set; }
    }
}
