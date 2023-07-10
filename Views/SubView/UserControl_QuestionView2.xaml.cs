using studyApp.Common;
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

namespace studyApp.Views.SubView
{
    /// <summary>
    /// UserControl2_mondaiView.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl_QuestionView2 : UserControl
    {
        public List<string> test = new List<string>();
        public static canswer[] answerArr = new canswer[4];
        public UserControl_QuestionView2()
        {
            InitializeComponent();
            n = 0;
            for (int i = 0; i < answerArr.Length; i++)
            {
                answerArr[i] = new canswer();   //初期化
            }
        }
        static int n = 0;   //55～73行目で使うanswerArrの添え字

        public class canswer
        {
            public string answer { get; set; } = ""; //問題文
            public bool flag { get; set; } = false; //選ばれているかのフラグ
            public int answer_num { get; set; } = -1;

        }


        public string RadioButtonCommand
        {
            get { return (string)GetValue(RadioButtonCommandProperty); }
            set { SetValue(RadioButtonCommandProperty, value); }
        }

        // DependencyPropertyをCommandのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty RadioButtonCommandProperty =
            DependencyProperty.Register("RadioButtonCommand", typeof(string), typeof(UserControl_QuestionView2), new PropertyMetadata(null));

        private void checkbox_Checked(object sender, RoutedEventArgs e)     //選択肢にチェックマークを付けたときの処理
        {
            bool flg = false;
            for (int i = 0; i < answerArr.Length; i++)
            {
                if (answerArr[i].answer.Equals(checkbox.Content.ToString()))    //選択したものがanswerArrの中にある時の処理
                {
                    answerArr[i].flag = true;
                    flg = true;
                    break;
                }
            }
            if (flg == false)       //選択した問題のanswerArrのflagがfalseの時の処理
            {
                answerArr[n].answer = checkbox.Content.ToString();
                answerArr[n].flag = true;
                n++;
            }
        }

        private void checkbox_Unchecked(object sender, RoutedEventArgs e)     //選択肢からチェックマークを外したときの処理
        {
            for (int i = 0; i < answerArr.Length; i++)
            {
                if (answerArr[i].answer.Equals(checkbox.Content.ToString()))    //選択したものがanswerArrの中にある時の処理
                {
                    answerArr[i].flag = false;
                    break;
                }
            }
        }
    }
}