using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_10
{
    struct MessageLog
    {
        public string time { get; set; }
        public string message { get; set; }
        public long id { get; set; }
        public string FirstName { get; set; }

        public MessageLog(string time, string message, long id, string firstName)
        {
            this.time = time;
            this.message = message; 
            this.id = id;   
            this.FirstName = firstName;
        }
    }
}
