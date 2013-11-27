using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messages
{
    public class ArticleUpdateMessage
    {
        public ArticleUpdateMessage()
        {
            ThrowError = string.Empty;
        }

        public string ArticleId { get; set; }
        public DateTime CreationDate { get; set; }
        public string ThrowError
        { get; set; }
    }
}
