using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecureChat.Core.Interfaces
{
    public interface IMessage
    {
        [Key]
        int MessageID { get; set; }
        string From { get; set; }
        string To { get; set; }
        string Body { get; set; }
        bool? Status { get; set; }
        DateTime? SendDate { get; set; }
        bool? IsDeleted { get; set; }
    }
}
