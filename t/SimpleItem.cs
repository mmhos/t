using System;
using System.Collections.Generic;
using System.Text;

namespace t
{
    class SimpleItem
    {
        public string Name { get; set; }
        public string Status { get; set; }

        //Constructor
        public SimpleItem(String name ,String status)
        {
            Name = name;
            Status = status;
        }

    }
}
