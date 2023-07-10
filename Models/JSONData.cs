using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studyApp.Models
{
    class JSONData
    {        
            public class Menber
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
                        public List<int> mChoices { get; set; }
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
                    public List<Miss> miss { get; set; }
                    public WorkAccident workAccident { get; set; }
                    public int rScore { get; set; }

            }

                public string gNumber { get; set; }
                public List<RescueRequestState> rescueRequestState { get; set; }

            }

            public class RescueRequestData
            {
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
                public int rNumber { get; set; }
                public string rVehicle { get; set; }
                
                public string rTitle { get; set; }
                public string rVNumber { get; set; }
                public string rCapacity { get; set; }
                public string rWorkPlace { get; set; }
                public string rMeetingPlace { get; set; }
                public string rCategory { get; set; }
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
            }
    }
}
