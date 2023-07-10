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

namespace studyApp.SubViews
{
    /// <summary>
    /// UserControl1_mondaiView.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl_QuestionView1 : UserControl
    {
        public static canswer answerArr = new canswer();
        public UserControl_QuestionView1()
        {
            InitializeComponent();
            answerArr = new canswer();  //初期化
        }
        public class canswer
        {
            public string answer { get; set; } = ""; //問題文
        }

        private void choiceRadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            answerArr.answer = choiceRadioButton.Content.ToString();   //押した選択肢の問題文を格納、上書き
        }

        public string RadioButtonCommand
        {
            get { return (string)GetValue(RadioButtonCommandProperty); }
            set { SetValue(RadioButtonCommandProperty, value); }
        }

        // DependencyPropertyをCommandのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty RadioButtonCommandProperty =
            DependencyProperty.Register("RadioButtonCommand", typeof(string), typeof(UserControl_QuestionView1), new PropertyMetadata(null));
    }
}