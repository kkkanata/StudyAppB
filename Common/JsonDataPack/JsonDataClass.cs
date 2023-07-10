using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDataPack
{
    public class JsonDataClass
    {
        public class Member
        {
            public string mNumber { get; set; }
            public string mShibu { get; set; }
            public string mKichi { get; set; }
            public string mFuriganaName { get; set; }
            public string mKanjiName { get; set; }
            public string mPassword { get; set; }
            public Boolean mResetFlag { get; set; }
            public string mChangeDate { get; set; }
            public Boolean mKanriFlag { get; set; }
            public string mLoginDate { get; set; }
        }

        public class Grade
        {
            public class RescueRequestState
            {

                public class Miss
                {
                    public int mNumber { get; set; }
                    public int[] mChoices { get; set; }
                }
                public class WorkAccident
                {
                    public int sNumber { get; set; }
                    public int sChoices { get; set; }
                }

                public int rNumber { get; set; }
                public int rProgress { get; set; }
                public string rAnswered { get; set; }
                public int rSavePoint { get; set; }
                public int rMissCount { get; set; }
                public Miss[] miss { get; set; }
                public WorkAccident workAccident { get; set; }
                public int rScore { get; set; }

            }

            public string gNumber { get; set; }
            public RescueRequestState[] rescueRequestState { get; set; }

        }

        public class Shibu
        {
            public int sNumber { get; set; }
            public int sName { get; set; }
            public string sControlFlag { get; set; }
        }

        public class kichi
        {
            public string kNumber { get; set; }
            public string kName { get; set; }
        }

        public class RescueRequestData
        {

            public int rNumber { get; set; }
            public string rCapacity { get; set; }
                                                    //2022.10.11 rCapacityを追加
                                                    //rCapacity...最大積載量
                                                    //TODO:仕様書にrCapacityについて追加
            public string rVNumber { get; set; }
            public string rWorkPlace { get; set; }
            public string rMeetingPlace { get; set; }
            public string rVehicle { get; set; }
            public string rCategory { get; set; }   //rCategoryは絞り込み機能用
            public string rTitle { get; set; }      //rTitleは問題のタイトルとして表示する為に用意。
            public string rStatus { get; set; }
            public string rPlace { get; set; }
            public string rDetail { get; set; }
            public string rCarColor { get; set; }
            public string rPhoto { get; set; }
            public int rSuccessScore { get; set; }
            public string rCreateDate { get; set; }
            public Boolean rPublic { get; set; }
            public int rStartQuestion { get; set; }
            public Question[] question { get; set; }
            public class Question
            {
                public class Choices
                {
                    public int cNumber { get; set; }
                    public string cAnswer { get; set; }
                    public string cRight { get; set; }
                    public int cDecrement { get; set; }
                    public int cNext { get; set; }
                    public string cExplanation { get; set; }

                }

                public int qNumber { get; set; }
                public Boolean qCheckPoint { get; set; }
                public string qType { get; set; }
                public string qText { get; set; }
                public string[] qPhotos { get; set; }
                public int qNext { get; set; }
                public int qCoordinateX { get; set; }
                public int qCoordinateY { get; set; }
                public Choices[] choices { get; set; }
            }
        }
    }



}