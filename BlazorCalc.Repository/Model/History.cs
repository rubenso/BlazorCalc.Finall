using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorCalc.Repository.Model
{
    [Table("History")]
    public class History
    {
        [Key] public Guid HistoryId { get; set; }

        [Required] public string Expression { get; set; }

        public string Result { get; set; }

        [Required] public DateTime CreatedOn { get; set; }
    }
}