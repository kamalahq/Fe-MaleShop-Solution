using System.ComponentModel.DataAnnotations.Schema;

namespace Riode.WebUI.AppCode.Infrastructure
{
    public class BaseEntity : HistoryWatch
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
