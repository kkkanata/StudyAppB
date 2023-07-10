using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JsonDataPack;

namespace studyApp.Common
{
    class OtherClass
    {
        // 月の差分を返す関数
        public int CalcMonthInterval(DateTime day)
        {
            DateTime dateFrom = day;
            DateTime dateTo = DateTime.Now;
            // 大きいDateTime - 小さいDateTimeにする
            if (day > dateTo)
            {
                dateFrom = dateTo;
                dateTo = day;
            }
            // 差分を計算
            var interval = (dateTo.Month + (dateTo.Year - dateFrom.Year) * 12) - dateFrom.Month;
            return interval;
        }
        //rCategoryを重複しない配列を返す関数
        public List<string> ReturnCategoryArray()
        {
            List<string> categoryArray = new List<string>();
            IList<JsonDataClass.RescueRequestData> rescuRecest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
            for(int i=0; i< rescuRecest.Count; i++)
            {
                if (!categoryArray.Contains(rescuRecest[i].rCategory))
                {
                    categoryArray.Add(rescuRecest[i].rCategory);
                }
            }
            return categoryArray;
        }

        //rCreateDateを重複しない配列を返す関数
        public List<string> ReturnCreateDateArray()
        {
            List<string> createDateArray = new List<string>();
            IList<JsonDataClass.RescueRequestData> rescuRecest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
            for (int i = 0; i < rescuRecest.Count; i++)
            {
                if (!createDateArray.Contains(rescuRecest[i].rCreateDate))
                {
                    createDateArray.Add(rescuRecest[i].rCreateDate);
                }
            }
            return createDateArray;
        }
    }
}
