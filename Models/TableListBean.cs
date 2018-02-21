using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DianDianClient.Models
{
    public class TableInfoBean
    {
        public int? tableposkey { get; set; }
        public string qrCode { get; set; }
        public int? tableNo { get; set; }
        public string tableName { get; set; }
        public int? peopleNum { get; set; }
        public int? state { get; set; }
        public int? enable { get; set; }
        public int? isDel { get; set; }
        public int? isRoom { get; set; }
        public int? peoples { get; set; }
        public int? floorid { get; set; }
        public string opentime { get; set; }
        public int? shopuserid { get; set; }
        public string shopusername { get; set; }
        public decimal? tfuwu { get; set; }
        public string cfmainkey { get; set; }
        public string cfmealkey { get; set; }
        public double? amount { get; set; }
        public int? iscomplete { get; set; }
    }
    public class TableListBean
    {
        public int code { get; set; }
        public List<v_cfmainmeal> result { get; set; }
        public string msg { get; set; }
    }
}
