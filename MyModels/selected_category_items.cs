using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DianDianClient.MyModels
{
    public static class selected_category_items
    {
        public static List<_selected_category_items> list = new List<_selected_category_items>();
        public class _selected_category_items
        {
            public int num { get; set; }
            public int itemKey { get; set; }
            public decimal sprice { get; set; }
            public int standardkey { get; set; }
            public string standardname { get; set; } 
            public string itemName { get; set; }
            public string itemImgs { get; set; }
            public int itemcategorykey { get; set; }
            public Bitmap bitmap { get; set; }

        }
        public static Temp temp = new Temp();
       public class Temp
        {
            public string str { get; set; }
            public decimal sprice { get; set; }
            public string standardname { get; set; }
            public int itemKey { get; set; }
            public int standardkey { get; set; }
        }
    }
}
