using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Models
{
    public class BaseEntity
    {
        [Column(TypeName = "timestamp without time zone"), DisplayFormat(DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        [Column(TypeName = "timestamp without time zone"), DisplayFormat(DataFormatString = "{yyyy-MM-dd HH:mm:ss}")]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}