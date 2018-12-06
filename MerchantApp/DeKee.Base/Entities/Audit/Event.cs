using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.Base.Entities.Audit
{
    public class Event<TItem> : Event
    {
        public TItem Data { get; set; }
    }
    public class Event : BaseEntity, IRepositoryEntity<string>
    {
        public string Level { get; set; }
        public string Module { get; set; }
        public string SessonId { get; set; }
        public string UserLogin { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public object Item { get; set; }
    }
}
