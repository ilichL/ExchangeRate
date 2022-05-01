using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.DTOs
{
    public class CommentsDTO
    {//нужен для ProductDTO
        public string Text { get; set; }
        public string UserName { get; set; }
        public int Rate { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
