using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Source
{
    public class PersonTypeAttributes : Attribute
    {
        public string Name { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsClient { get; set; }
        public string Role { get; set; }
    }
}
