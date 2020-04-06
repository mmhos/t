using System;
using System.Collections.Generic;
using System.Text;

namespace t
{
    public class UniqueItemIdentifier
    {
        public int solnum;
        public string location;
        public string agency;
        public string customer;

        //Constructor
        public UniqueItemIdentifier()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.solnum = 8057057;
            this.location = "NY PENN";
            this.agency = "MTA";
            this.customer = "LIRR";
        }
    }
}
