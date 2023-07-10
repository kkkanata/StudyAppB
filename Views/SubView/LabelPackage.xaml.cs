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

namespace studyApp
{
    /// <summary>
    /// UserControl1.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public string answertext
        {
            get { return (string)GetValue(answertextProperty); }
            set { SetValue(answertextProperty, value); }
        }
        public string questiontext
        {
            get { return (string)GetValue(questiontextProperty); }
            set { SetValue(questiontextProperty, value); }
        }

        // DependencyPropertyをCommentのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty answertextProperty =
            DependencyProperty.Register("answertext", typeof(string), typeof(UserControl1), new PropertyMetadata(string.Empty));
        //                    プロパティ名を指定,  プロパティの方を指定,  プロパティを所有する型を指定,  デフォルト値の設定

        // DependencyPropertyをCommentのバッキングストアとして使用する。 これにより、アニメーション、スタイル設定、バインディングなどが可能になる。
        public static readonly DependencyProperty questiontextProperty =
            DependencyProperty.Register("questiontext", typeof(string), typeof(UserControl1), new PropertyMetadata(string.Empty));
        //                    プロパティ名を指定,  プロパティの方を指定,  プロパティを所有する型を指定,  デフォルト値の設定
    }
}
