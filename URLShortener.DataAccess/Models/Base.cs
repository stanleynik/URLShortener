using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccess.Models
{
    // To add common fields to other models.
    public class Base
    {
            public Base()
            {
                CreationDate = DateTime.Now;
            }
            public DateTime CreationDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
    }
}
