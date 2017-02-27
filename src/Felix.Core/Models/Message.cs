using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Core.Models
{
    public class Message
    {
        public Message(string text) : this(text, DateTimeOffset.Now)
        {
            Id = Guid.NewGuid();
            Text = text;
        }

        public Message(string text, DateTimeOffset? dateTime)
        {
            Text = text;
            RecieveDate = dateTime ?? DateTimeOffset.Now;
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTimeOffset RecieveDate { get; set; }
    }
}
