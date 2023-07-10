using studyApp.Views;
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


namespace studyApp
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class QuestionCell : UserControl
    {

        public QuestionCell()
        {
            InitializeComponent();
        }

        public string stateText
        {
            get { return (string)GetValue(stateTextProperty); }
            set { SetValue(stateTextProperty, value); }
        }

        // DependencyPropertyをCommentのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty stateTextProperty =
            DependencyProperty.Register("stateText", typeof(string), typeof(QuestionCell), new PropertyMetadata(string.Empty));

        public string requestText
        {
            get { return (string)GetValue(requestTextProperty); }
            set { SetValue(requestTextProperty, value); }
        }

        // DependencyPropertyをCommentのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty requestTextProperty =
            DependencyProperty.Register("requestText", typeof(string), typeof(QuestionCell), new PropertyMetadata(string.Empty));

        public int rNumber
        {
            get { return int.Parse((string)GetValue(rNumberProperty)); }
            set { SetValue(rNumberProperty, value); }
        }

        // DependencyPropertyをCommentのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty rNumberProperty =
            DependencyProperty.Register("rNumber", typeof(string), typeof(QuestionCell), new PropertyMetadata(string.Empty));

        public string cButton
        {
            get { return (string)GetValue(cButtonProperty); }
            set { SetValue(cButtonProperty, value); }
        }

        // DependencyPropertyをCommentのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty cButtonProperty =
            DependencyProperty.Register("cButton", typeof(string), typeof(QuestionCell), new PropertyMetadata(string.Empty));

        public string fbButton
        {
            get { return (string)GetValue(fbButtonProperty); }
            set { SetValue(fbButtonProperty, value); }
        }

        // DependencyPropertyをCommentのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty fbButtonProperty =
            DependencyProperty.Register("fbButton", typeof(string), typeof(QuestionCell), new PropertyMetadata(string.Empty));

        
        //続きからボタン
        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            JsonDataClass.Grade grade = (JsonDataClass.Grade)Application.Current.Properties["Grade"];
            JsonDataClass.Grade.RescueRequestState rescue = new JsonDataClass.Grade.RescueRequestState();
            DataSearch dataSearch = new DataSearch();
            int gNum = dataSearch.ResqueSearch(rNumber);
            int rNum = dataSearch.rescueRequestSearch(rNumber);
            // 問題画面へ渡す変数を成績データから格納
            Application.Current.Properties["count"] = 0;
            rescue = grade.rescueRequestState[gNum];
            Application.Current.Properties["Rescue"] = rescue;
            Application.Current.Properties["next"] = rNum;
            int nowNum = grade.rescueRequestState[gNum].rSavePoint;
            int questionNum = dataSearch.QuestionSearch(rNum, nowNum);
            Application.Current.Properties["mondai"] = questionNum;
        }

        // 最初からボタン
        private void fromTheBeginingButton_Click(object sender, RoutedEventArgs e)
        {
            DataSearch dataSearch = new DataSearch();
            IList<JsonDataClass.RescueRequestData> rescuRecest = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
            JsonDataClass.Grade.RescueRequestState rescue = new JsonDataClass.Grade.RescueRequestState();
            JsonDataClass.Grade.RescueRequestState.WorkAccident work = new JsonDataClass.Grade.RescueRequestState.WorkAccident();
            JsonDataClass.Grade.RescueRequestState.Miss[] miss = new JsonDataClass.Grade.RescueRequestState.Miss[0];
            // 救援依頼データの救援依頼番号にマッチする要素番号
            int rNum = dataSearch.rescueRequestSearch(rNumber);
            // 問題番号にマッチする要素番号
            int mNum = dataSearch.QuestionSearch(rNum, rescuRecest[rNum].rStartQuestion);

            // 問題画面へ渡す変数の格納
            Application.Current.Properties["count"] = 0;
            rescue.rNumber = rNumber;
            rescue.rAnswered = "解答中";
            rescue.rMissCount = 0;
            rescue.rScore = 100;
            rescue.workAccident = work;
            rescue.miss = miss;
            // rescueは解答中のデータの一時格納用
            Application.Current.Properties["Rescue"] = rescue;
            Application.Current.Properties["next"] = rNum;
            Application.Current.Properties["mondai"] = mNum;
        }

        private void Load_Loaded(object sender, RoutedEventArgs e)
        {
            // 解答状況によって色を変える
            // 解答中のみ続きからボタンを機能させる
            switch (stateText)
            {
                case "解答済":
                    Load.Background = Brushes.LightGreen;
                    break;
                case "解答ミス":
                    Load.Background = Brushes.Salmon;
                    break;

                case "解答中":
                    Load.Background = Brushes.LightGoldenrodYellow;
                    continueButton.IsEnabled = true;
                    break;

                case "未解答":
                    Load.Background = Brushes.Gray;
                    break;
            }
        }
    }
}