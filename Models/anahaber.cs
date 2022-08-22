using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCreate.Models
{
    public class anahaber
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Topic { get; set; }
        public string image { get; set; }
        [NotMapped]
        public IFormFile File { set; get; }

    }
}
