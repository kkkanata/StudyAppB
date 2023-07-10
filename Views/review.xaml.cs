using System;
using System.Collections.Generic;
using System.Drawing;
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

using JsonDataPack;
using studyApp.Common;

namespace studyApp.Views
{
    public class ViewModelData
    {
        // requestNameTextに入れる文字列
        public String requestNameText { get; set; }

        // 問題文
        public String questiontext { get; set; }

        // 作業事故の答え
        public String accidenttext { get; set; }

        // ミスの答え
        public String misstext { get; set; }

        // ミスの数
        public String misscount { get; set; }
    }

    /// <summary>
    /// review.xaml の相互作用ロジック
    /// </summary>
    public partial class review : Page
    {
        public review()
        {
            InitializeComponent();

            //ループに使える
            this.DataContext = new ViewModel();
        }

        public class ViewModel
        {
            // requestNameTextに入れる文字列
            public List<ViewModelData> requestNameLists { get; set; }

            // 作業事故
            public List<ViewModelData> accidentLists { get; set; }

            // ミス
            public List<ViewModelData> missLists { get; set; }

            // ミスの数
            public List<ViewModelData> misstexts { get; set; }

            public ViewModel()
            {
                // おそらくrescueとgradesの配列の要素は同じ

                //成績情報データ出力方法
                JsonDataClass.Grade Grade = (JsonDataClass.Grade)Application.Current.Properties["Grade"];
                var dataSearch = new DataSearch();
                // rNumberを受け取る
                int rNum = (int)Application.Current.Properties["rNumber"];
                var elegNum = dataSearch.ResqueSearch(rNum);
                // データが見つかったか
                if (elegNum != -1)
                {
                    // rescueRequestState[0]の配列でrescueRequestState01～04を決めている
                    var rescueRequestState = Grade.rescueRequestState[elegNum];
                    // ↑のelegNumは成績情報画面からもらったrNumberとrescueRequestStateのrNumberが一致する要素数


                    //救援依頼データ出力方法
                    IList<JsonDataClass.RescueRequestData> rescueRequest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
                    //rescue[0]の配列でrescueRequestData001～004を決めている
                    var elerNum = dataSearch.rescueRequestSearch(rNum);
                    // データが見つかったか
                    if (elerNum != -1)
                    {
                        var rescue = rescueRequest[elerNum];
                        // ↑のelerNumは成績情報画面からもらうったrNumberとrescueRequestStateのrNumberが一致する要素数


                        //UIコンポーネントの（エンジン不始動１）がjson仕様書の救援依頼データrCategoryから持ってくる場合
                        this.requestNameLists = new List<ViewModelData>
                        {
                            //作業事故の表示内容
                            new ViewModelData{
                                requestNameText = "（" +rescue.rCategory + "）"
                            }
                        };

                        //questionの配列の要素
                        var qAccident = rescueRequestState.workAccident.sNumber;
                        //choicesの配列の要素
                        var Accident = rescueRequestState.workAccident.sChoices;
                        // qAccidentとAccidentが見つからなかったら-1を返す
                        var elesNum = dataSearch.QuestionSearch(elerNum, qAccident);
                        if(elesNum != -1)
                        {
                            var elesCho = dataSearch.ChoicesSearch(elerNum, elesNum, Accident);
                            // 返された値が-1なら飛ばす
                            if (elesNum >= 0 && elesCho >= 0)
                            {
                                this.accidentLists = new List<ViewModelData>
                                {
                                    // 作業事故の表示内容
                                    new ViewModelData{
                                        questiontext ="Q." + rescue.question[elesNum].choices[elesCho].cAnswer,
                                        accidenttext ="A." + rescue.question[elesNum].choices[elesCho].cExplanation
                                    }
                                };
                            }
                        }


                        //ミスの数
                        var missCount = 0;
                        this.missLists = new List<ViewModelData>() { };
                        // Missの配列の要素数ループ
                        for (int i = 0; i < rescueRequestState.miss.Length; i++)
                        {
                            // questionの配列の要素
                            var qMiss = rescueRequestState.miss[i].mNumber;
                            // qMissが見つからなかったら-1を返す
                            var elemNum = dataSearch.QuestionSearch(elerNum, qMiss);
                            // 返された値が-1なら飛ばす
                            if (elemNum >= 0)
                            {
                                // mChoicesの配列の要素ループ
                                for (int j = 0; j < rescueRequestState.miss[i].mChoices.Length; j++)
                                {
                                    // choicesの配列の要素
                                    var ins = rescueRequestState.miss[i].mChoices[j];
                                    // insが見つからなかったら-1を返す
                                    var elemCho = dataSearch.ChoicesSearch(elerNum, elemNum, ins);
                                    // 返された値が-1なら飛ばす
                                    if (elemCho >= 0)
                                    {
                                        missCount++;
                                        //ミスの表示内容
                                        this.missLists.Add(new ViewModelData
                                        {
                                            questiontext ="Q." + rescue.question[elemNum].choices[elemCho].cAnswer,
                                            misstext ="A." + rescue.question[elemNum].choices[elemCho].cExplanation
                                        });
                                    }
                                }
                            }
                        }
                        //ミスの数を表示
                        this.misstexts = new List<ViewModelData>
                        {
                            //missTextの表示内容
                            new ViewModelData{
                                misscount = "ミス（" + missCount + "）"
                            }
                        };
                    }
                }
            }
        }

        private void returnToGradeAndInfomationButton_Click(object sender, RoutedEventArgs e)
        {
            // GradeViewへ移動
            this.NavigationService.Navigate(new GradeView());
        }
    }
}
