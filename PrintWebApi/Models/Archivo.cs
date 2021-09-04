using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrintWebApi.Models
{
    public class Archivo
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public double Size { get; set; }
        public string Location { get; set; }
    }
}
