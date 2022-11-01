using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownApp.Shared.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Markdown { get; set; }
    }
}
