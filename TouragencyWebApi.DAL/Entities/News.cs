using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouragencyWebApi.DAL.Entities
{
    public class News
    {
        public long Id { get; set; }
        public string Caption { get; set; }
        public string Text { get; set; }
        public DateTime PublishDateTime { get; set; }
        public string? PhotoUrl{ get; set; }
        public bool IsVisible { get; set; }
        public bool IsImportant { get; set; }
    }
}
