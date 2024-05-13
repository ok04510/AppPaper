using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC研修5.Models
{
    public class ApplicationRowModel
    {
        public string ユーザーID { get; set; }
        public string 状態 { get; set; }
        public int 申請ID { get; set; }
        public string 申請書類 { get; set; }
        public string タイトル { get; set; }
        public string 申請日 { get; set; }
        public string 承認日 { get; set; }
        public string 連絡事項 { get; set; }
    }
}