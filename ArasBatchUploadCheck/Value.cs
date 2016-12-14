using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArasBatchUploadCheck
{
    public class AttList
    {
        public string List { get; set; }
        public string id { get; set; }
        public List<Value> Value { get; set; }
        public List<FilterValue> FilterValue { get; set; }
    }

    public class IValue
    {
        public string value{get;set;}
        public string label { get; set; }
    }
    public class Value : IValue
    {

    }

    public class FilterValue : IValue
    {
        public string filter { get; set; }
    }
}
