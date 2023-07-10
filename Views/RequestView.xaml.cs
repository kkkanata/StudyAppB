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
    public class ControlData
    {
        public String Status { get; set; } // 解答状況
        public String Title { get; set; } // タイトル
        public int rNumber { get; set; } // 救援依頼番号
        public String fbButton { get; set; } // rStartQuestionのページのpath
        public String cButton { get; set; } // rSavePointのページのpass
    }

    public partial class RequestView : Page
    {
        public RequestView()
        {
            InitializeComponent();
            DataContext = new Control("-", "全依頼区分");
            var other = new OtherClass();
            // comboboxにカテゴリーを登録
            var category = other.ReturnCategoryArray();
            foreach (var val in category)
            {
                //依頼区分をPullDownに追加
                requestPullDown.Items.Add(val);
            }
        }

        public class Control
        {
            public List<ControlData> Controls { get; set; }
            /// <summary>
            /// QuestionCellの初期値を格納する
            /// </summary>
            /// <param name="States">解答状況</param>
            /// <param name="category">カテゴリー名</param>
            public Control(String States, String category)
            {
                String cpath = ""; // セーブポイントのページ遷移先を格納する変数
                String fbpath;     // 最初のページ遷移先を格納する変数
                IList<JsonDataClass.RescueRequestData> Rescue = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
                JsonDataClass.Grade grade = (JsonDataClass.Grade)Application.Current.Properties["Grade"];
                DataSearch dataSearch = new DataSearch();
                List<ControlData> controlDatas = new List<ControlData>();
                // 各大問の解答状況などの初期値を格納
                for(int i = 0; i < Rescue.Count(); i++)
                {
                    // 成績データの救援依頼にマッチする要素番号
                    int num = dataSearch.ResqueSearch(Rescue[i].rNumber);
                    if (num == -1)
                    {
                        if((States == "-" || States == "未解答") && (Rescue[i].rCategory == category || category == "全依頼区分"))
                        {
                            // 解答方法の判定(rStartQuestion)
                            if (dataSearch.SelectSearch(i, Rescue[i].rStartQuestion) == 0)
                            {
                                fbpath = "../QuestionView1.xaml";
                            }
                            else
                            {
                                fbpath = "../QuestionView2.xaml";
                            }
                            ControlData controlData = new ControlData { Status = "未解答", Title = Rescue[i].rTitle, rNumber = Rescue[i].rNumber, fbButton = fbpath, cButton = cpath };
                            controlDatas.Add(controlData);
                        }
                    }
                    else
                    {
                        if ((grade.rescueRequestState[num].rAnswered == States || States == "-") && (Rescue[i].rCategory == category || category == "全依頼区分"))
                        {
                            // 解答方法の判定(rStartQuestion)
                            if (dataSearch.SelectSearch(i, Rescue[i].rStartQuestion) == 0)
                            {
                                fbpath = "../QuestionView1.xaml";
                            }
                            else
                            {
                                fbpath = "../QuestionView2.xaml";
                            }
                            // 解答方法の判定(rSavePoint)
                            if (grade.rescueRequestState[num].rAnswered == "解答中" && dataSearch.SelectSearch(i,grade.rescueRequestState[num].rSavePoint) == 0)
                            {
                                cpath = "../QuestionView1.xaml";
                            }
                            else
                            {
                                cpath = "../QuestionView2.xaml";
                            }
                            ControlData controlData = new ControlData { Status = grade.rescueRequestState[num].rAnswered, Title = Rescue[i].rTitle, rNumber = Rescue[i].rNumber, fbButton = fbpath, cButton = cpath };
                            controlDatas.Add(controlData);
                        }
                    }
                }
                //表示
                Controls = controlDatas;
            }
        }

        private void backToMainButton_Click(object sender, RoutedEventArgs e)
        {
            Mainpage mainpage = new Mainpage();
            NavigationService.Navigate(mainpage);
        }

        private void answerStatusPullDown_DropDownClosed(object sender, EventArgs e)
        {
            // 絞り込み後の再生成
            DataContext = new Control(answerStatusPullDown.Text,requestPullDown.Text);
        }

        private void requestPullDown_DropDownClosed(object sender, EventArgs e)
        {
            // 絞り込み後の再生成
            DataContext = new Control(answerStatusPullDown.Text, requestPullDown.Text);
        }
    }
}
