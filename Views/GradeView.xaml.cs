using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using studyApp.Common;
using JsonDataPack;


namespace studyApp.Views
{
    public class Contents //格納用のクラス
    {
        public String Title { get; set; }
        public String Status { get; set; }
        public string DegreeOfProgress { get; set; }
        public string Miss { get; set; }
        public int progress { get; set; }
        public string Text { get; set; }
        public string Visibility { get; set; }
        public string Color { get; set; }
        public int rNumber { set; get; }
    }

    /// <summary>
    /// GradeView.xaml の相互作用ロジック
    /// </summary>
    public partial class GradeView : Page
    {
        public GradeView()
        {
            InitializeComponent();
            this.DataContext = new Control("全依頼区分","全期間");

            OtherClass otherClass = new OtherClass();
            List<string> categoryArray =otherClass.ReturnCategoryArray();
            IList<JsonDataClass.RescueRequestData> Rescue = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
            
            List<string> createDateArray =  new List<string>() {};
            List<string> comboBoxArray = new List<string>() { };
            
            foreach (string val in categoryArray)
            {
                //依頼区分をPullDownに追加
                gradeCategoryPullDown.Items.Add(val);
            }
            for(int cnt = 0; cnt < Rescue.Count(); cnt++)
            {
                if (!createDateArray.Contains(Rescue[cnt].rCreateDate))
                {
                    createDateArray.Add(Rescue[cnt].rCreateDate);
                }
                
            }
            for (int i = 1; i < createDateArray.Count()+1; i++)
            {
                comboBoxArray.Add("第" + i + "回");
            }
            
            foreach(string val2 in comboBoxArray)
            {
                gradePeriodPullDown.Items.Add(val2);
            }

        }

        public class Control
        {
            public List<Contents> Controls { get; set; }
            public Control(String category, String date )
            {
                IList<JsonDataClass.RescueRequestData> Rescue = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
                JsonDataClass.Grade Grade = (JsonDataClass.Grade)Application.Current.Properties["Grade"];
                DataSearch dataSearch = new DataSearch();
                List<Contents> controlDatabutton = new List<Contents>();
                //DateTime createTime = DateTime.Parse(Rescue[0].rCreateDate);

                List<string> createDateArray = new List<string>() { "null" };
                for (int cnt = 0; cnt < Rescue.Count(); cnt++)
                {
                    createDateArray.Add(Rescue[cnt].rCreateDate);
                }
                createDateArray.Distinct();

                int flagTime = 0;
                if (date != "全期間")
                {
                    flagTime = int.Parse(date.Substring(1, 1));
                }
                else
                {
                    date = "全期間";
                }

                //背景色を交互に出すためのカウント
                int colorCnt = 0;

                //依頼区分の個数分繰り返し
                for (int i = 0; i < Rescue.Count(); i++)
                {
                    /// <returns>grade(成績データ)の(rescueRequestStateのrNumber)にマッチする要素番号マッチした回数分データを格納</returns>
                    int num = dataSearch.ResqueSearch(Rescue[i].rNumber);
                    //問題の作成日を格納
                    string createTime = Rescue[i].rCreateDate;
                    //未解答の場合通る
                    if (num == -1)
                    {
                        if ((Rescue[i].rCategory == category || category == "全依頼区分") && (createDateArray[flagTime]== createTime|| (date == "全期間")))
                        {
                            //データをクラスに格納
                            Contents controlData = new Contents
                            {
                                Status = "未解答",
                                Title = Rescue[i].rTitle,
                                DegreeOfProgress = "0%",
                                Miss = "ミス：0",
                                progress = 0,
                                Text = "学習を進めましょう",
                                Visibility = "Hidden"   //ボタンを非表示にしテキストを表示するためのHidden

                            };
                            if(colorCnt % 2 == 0)
                            {
                                controlData.Color = "White";
                                colorCnt++;

                            }
                            else
                            {
                                controlData.Color = "#FFF0F0F0";
                                colorCnt++;
                            }
                            //controlDataを追加
                            controlDatabutton.Add(controlData);
                        }
                    }
                    else
                    {
                            if ((Rescue[i].rCategory == category || category == "全依頼区分") && (createDateArray[flagTime] == createTime || (date == "全期間")))
                            {
                            //解答中のデータの場合通る
                            if (Grade.rescueRequestState[num].rAnswered == "解答中")
                            {
                                //データをクラスに格納
                                Contents controlData = new Contents
                                {
                                    Status = Grade.rescueRequestState[num].rAnswered,
                                    Title = Rescue[i].rTitle,
                                    DegreeOfProgress = Grade.rescueRequestState[num].rProgress + "%",
                                    Miss = "ミス：" + Grade.rescueRequestState[num].rMissCount,
                                    progress = Grade.rescueRequestState[num].rProgress,
                                    rNumber = Rescue[i].rNumber
                                };
                                if (colorCnt % 2 == 0)
                                {
                                    controlData.Color = "White";
                                    colorCnt++;
                                }
                                else
                                {
                                    controlData.Color = "#FFF0F0F0";
                                    colorCnt++;
                                }
                                //controlDataを追加
                                controlDatabutton.Add(controlData);
                            }
                            //解答済みかつミスが0の場合通る
                            else if(Grade.rescueRequestState[num].rAnswered == "解答済" && Grade.rescueRequestState[num].rMissCount == 0)
                            {
                                //データをクラスに格納
                                Contents controlData = new Contents
                                {
                                    Status = Grade.rescueRequestState[num].rAnswered,
                                    Title = Rescue[i].rTitle,
                                    DegreeOfProgress = Grade.rescueRequestState[num].rProgress + "%",
                                    Miss = "ミス：" + Grade.rescueRequestState[num].rMissCount,
                                    progress = Grade.rescueRequestState[num].rProgress,
                                    Text = "good!",
                                    Visibility = "Hidden"   //ボタンを非表示にしテキストを表示するためのHidden
                                };
                                if (colorCnt % 2 == 0)
                                {
                                    controlData.Color = "White";
                                    colorCnt++;
                                }
                                else
                                {
                                    controlData.Color = "#FFF0F0F0";
                                    colorCnt++;
                                }
                                //controlDataを追加
                                controlDatabutton.Add(controlData);
                            }
                            //上記の条件に外れたものを通る
                            else
                            {
                                //データをクラスに格納
                                Contents controlData = new Contents
                                {
                                    Status = Grade.rescueRequestState[num].rAnswered,
                                    Title = Rescue[i].rTitle,
                                    DegreeOfProgress = Grade.rescueRequestState[num].rProgress + "%",
                                    Miss = "ミス：" + Grade.rescueRequestState[num].rMissCount,
                                    progress = Grade.rescueRequestState[num].rProgress,
                                    rNumber = Rescue[i].rNumber
                                };
                                if (colorCnt % 2 == 0)
                                {
                                    controlData.Color = "White";
                                    colorCnt++;
                                }
                                else
                                {
                                    controlData.Color = "#FFF0F0F0";
                                    colorCnt++;
                                }
                                //controlDataを追加
                                controlDatabutton.Add(controlData);
                            }
                        }
                    }
                }
                //追加したデータを表示
                Controls = controlDatabutton;
            }
            
        }
        private void returnToMainButton_Click(object sender, RoutedEventArgs e)
        {
            //クリック時にメイン画面遷移
            var MainpageTransition = new Mainpage();
            NavigationService.Navigate(MainpageTransition);
        }
        private void gradeCategoryPullDown_DropDownClosed(object sender, EventArgs e)
        {
            DataContext = new Control(gradeCategoryPullDown.Text, gradePeriodPullDown.Text);
        }
        private void gradePeriodPullDown_DropDownClosed(object sender, EventArgs e)
        {
            DataContext = new Control(gradeCategoryPullDown.Text,gradePeriodPullDown.Text);
        }
        private void gradeCategoryPullDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataContext = gradeCategoryPullDown.SelectedItem.ToString();
        }
    }
}
