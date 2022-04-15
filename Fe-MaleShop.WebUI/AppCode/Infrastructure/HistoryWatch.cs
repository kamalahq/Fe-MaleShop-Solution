using System;

namespace Riode.WebUI.AppCode.Infrastructure
{
    public class HistoryWatch
    {
        public int? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);

        public int? DeletedByUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
