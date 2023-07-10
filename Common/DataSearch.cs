using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using JsonDataPack;


namespace studyApp.Common
{
    class DataSearch
    {
        /// <summary>
        /// 救援依頼データの救援依頼情報の救援依頼にマッチする要素番号を返す
        /// </summary>
        /// <param name="rNum">救援依頼番号</param>
        /// <returns>rescueRequest(救援依頼データ)の(rescueRequestDataのrNumber)にマッチする要素番号マッチしなければ-1</returns>
        public int rescueRequestSearch(int rNum)
        {
            int eleNum = -1;
            IList<JsonDataClass.RescueRequestData> rescuRecest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];

            for (int i = 0; i < rescuRecest.Count; i++)
            {
                if (rescuRecest[i].rNumber == rNum)
                {
                    eleNum = i;
                }
            }
            return eleNum;
        }


        /// <summary>
        /// 成績データの救援依頼回答状況配列の救援依頼にマッチする要素番号を返す
        /// </summary>
        /// <param name="rNum">救援依頼番号</param>
        /// <returns>grade(成績データ)の(rescueRequestStateのrNumber)にマッチする要素番号マッチしなければ-1</returns>
        public int ResqueSearch(int rNum)
        {
            int eleNum = -1;
            JsonDataClass.Grade grade = (JsonDataClass.Grade)Application.Current.Properties["Grade"];
            if(grade.rescueRequestState != null)
            {
                for (int i = 0; i < grade.rescueRequestState.Length; i++)
                {
                    if (grade.rescueRequestState[i].rNumber == rNum)
                    {
                        eleNum = i;
                    }
                }
            }
            return eleNum;
        }

        /// <summary>
        /// 指定した問題番号の解答形式を返す
        /// </summary>
        /// <param name="rNum">rescueRequestDataesの要素番号</param>
        /// <param name="qNum">問題番号</param>
        /// <returns>単一選択=0,複数選択=1</returns>
        public int SelectSearch(int rNum, int qNum)
        {
            IList<JsonDataClass.RescueRequestData> rescuRecest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
            int select = 0;
            int num = QuestionSearch(rNum, qNum);
            if (rescuRecest[rNum].question[num].qType == "複数選択")
            {
                select = 1;
            }
            return select;
        }

        /// <summary>
        /// 問題番号にマッチする要素番号を返す
        /// </summary>
        /// <param name="Number">問題番号</param>
        /// <returns>rescueRequest(救援依頼データ)のQuestionのqNumberにマッチする要素番号</returns>
        public int QuestionSearch(int rNum, int Number)
        {
            int eleNum = -1;
            if (Number >= 0)
            {
                IList<JsonDataClass.RescueRequestData> rescueRequest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];

                for (int i = 0; i < rescueRequest[rNum].question.Length; i++)
                {
                    if (rescueRequest[rNum].question[i].qNumber == Number)
                    {
                        eleNum = i;
                    }
                }
            }
            return eleNum;
        }

        /// <summary>
        /// 成績データのmiss又はworkAccidentの選択肢番号にマッチする要素番号を返す
        /// </summary>
        /// <param name="Choices">選択肢番号</param>
        /// <returns>rescueRequest(救援依頼データ)のChoicesのcNumberにマッチする要素番号</returns>
        public int ChoicesSearch(int rNum, int Number, int Choices)
        {
            int eleCho = -1;
            if (Choices >= 0)
            {
                IList<JsonDataClass.RescueRequestData> rescueRequest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];

                for (int i = 0; i < rescueRequest[rNum].question[Number].choices.Length; i++)
                {
                    if (rescueRequest[rNum].question[Number].choices[i].cNumber == Choices)
                    {
                        eleCho = i;
                    }
                }
            }
            return eleCho;
        }
    }
}
