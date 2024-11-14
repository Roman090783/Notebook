using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF
{
    public class Dummy
    {
        public string Property { get; set; } = "Eigenschaft";
        public string GetterOnlyProperty => "konstante Eigenschaft";
    }
}
