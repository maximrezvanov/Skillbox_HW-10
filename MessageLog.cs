using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_10
{
    struct MessageLog
    {
        public string Time { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }

        public MessageLog(string Time, string message, int id, string firstName)
        {
            this.Time = Time;
            this.Message = message; 
            this.Id = id;   
            this.FirstName = firstName;
        }
    }
}
