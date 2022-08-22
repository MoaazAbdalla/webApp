using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyCreate.model
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string img { get; set; }
        public string Topic { get; set; }
        [ForeignKey("categoty")]
        public int categoeyId { get; set; }
        public Categoty categoty { get; set; }

        [NotMapped]
        public IFormFile File { set; get; }
    }
}
