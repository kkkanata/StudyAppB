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

using JsonDataPack;
using System.Windows.Media.Animation;

namespace studyApp.Views
{
    /// <summary>
    /// Mainpage.xaml の相互作用ロジック
    /// </summary>
    public partial class Mainpage : Page
    {
        public Mainpage()
        {
            InitializeComponent();
            JsonDataClass.Member member = (JsonDataClass.Member)Application.Current.Properties["Menber"];
            String UserName = member.mKanjiName;

            userNameText.Content = "ユーザー：" + UserName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var GradeViewTransition = new GradeView();
            NavigationService.Navigate(GradeViewTransition);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var RequestViewTransition = new RequestView();
            NavigationService.Navigate(RequestViewTransition);
        }


        // 参考URL「https://araramistudio.jimdo.com/2016/11/24/wpf%E3%81%A7waitingcircle%E3%82%B3%E3%83%B3%E3%83%88%E3%83%AD%E3%83%BC%E3%83%AB%E3%82%92%E4%BD%9C%E3%82%8B/」
        // Commonでの処理はx.nameの参照ができない？
        bool bgflg = false;
        // 円を書く
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            // cx,cy  ・・・円の中心座標
            double cx = 50.0;
            double cy = 50.0;
            // r        ・・・円の半径
            double r = 45.0;
            // cnt　  ・・・円の分割数（14分割）
            int cnt = 14;
            // deg　 ・・・分割した１つ分の角度
            double deg = 360.0 / (double)cnt;
            // degS　・・・円弧と円弧の隙間の角度
            double degS = deg * 0.8;

            // クリックで切り替え
            if (bgflg == false)
            {
                // stackpanelを画面全体に拡大
                BG.Width = 1950;
                BG.Height = 1050;
                //背景の色と透明度
                BG.Background = Brushes.Gray;
                BG.Opacity = 0.6;
                bgflg = true;

                // インジケータ―
                // MainCanvasに表示
                for (int i = 0; i < cnt; ++i)
                {
                    var si1 = Math.Sin((270.0 - (double)i * deg) / 180.0 * Math.PI);
                    var co1 = Math.Cos((270.0 - (double)i * deg) / 180.0 * Math.PI);
                    var si2 = Math.Sin((270.0 - (double)(i + 1) * deg + degS) / 180.0 * Math.PI);
                    var co2 = Math.Cos((270.0 - (double)(i + 1) * deg + degS) / 180.0 * Math.PI);
                    var x1 = r * co1 + cx;
                    var y1 = r * si1 + cy;
                    var x2 = r * co2 + cx;
                    var y2 = r * si2 + cy;

                    var path = new Path();
                    // マークアップ構文で円弧を書く場合↓
                    // M[始点X],[始点Y] A[円半径X],[円半径Y] [回転角] [180度以上の時1] [正の角の時1] [終点X],[終点Y]
                    path.Data = Geometry.Parse(string.Format("M {0},{1} A {2},{2} 0 0 0 {3},{4}", x1, y1, r, x2, y2));
                    path.Stroke = new SolidColorBrush(Color.FromArgb((byte)(255 - (i * 256 / cnt)), CircleColor.R, CircleColor.G, CircleColor.B));
                    // 回転している物の長さ
                    path.StrokeThickness = 40.0;
                    MainCanvas.Children.Add(path);
                }
                // アニメーション起動
                animetion(cnt, deg, 80);
            }
            else if (bgflg == true)
            {
                // stackpanelを表示しないようにする
                BG.Width = 0;
                BG.Height = 0;
                //背景の透明度
                BG.Opacity = 0;
                bgflg = false;
                // アニメーション停止？
                animetion(cnt, deg, 0);
            }
        }
        public void animetion(int cnt, double deg, int spin)
        {
            // アニメーションを作成する
            //Canvasを回転させるアニメーションを作る。
            //DiscreteDoubleKeyFrameを使うと補間せずに値が変わるので、フレーム毎の移動量を円弧の角度(deg)と合わせるといい感じになる。
            //DiscreteDoubleKeyFrameを使うと補間せずに値が変わるので、フレーム毎の移動量を円弧の角度(deg)と合わせるといい感じになる。
            var kf = new DoubleAnimationUsingKeyFrames();
            kf.RepeatBehavior = RepeatBehavior.Forever;
            for (int i = 0; i < cnt; ++i)
            {
                //回転速度
                kf.KeyFrames.Add(new DiscreteDoubleKeyFrame()
                {
                    KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(i * spin)),
                    Value = i * deg
                });
            }
            // RotateTransformのBeginAnimationメソッドを使って、Angleプロパティへのアニメーションを開始する。
            MainTrans.BeginAnimation(RotateTransform.AngleProperty, kf);
        }

        // 円の色を指定するCircleColorを依存関係プロパティとして作成。
        // DependencyProperty.Registerメソッドを使ってプロパティを作成
        // プロパティ名、プロパティの型、プロパティを所有するクラスの型に加えて、PropertyMetadataから初期値やプロパティ変更時のコールバックメソッドを指定
        public static readonly DependencyProperty CircleColorProperty =
            DependencyProperty.Register(
                "CircleColor", // プロパティ名を指定
                typeof(Color), // プロパティの型を指定
                typeof(Mainpage), // プロパティを所有する型を指定
                                  // (0, 0, 0)は回転している物の色
                new UIPropertyMetadata(Color.FromRgb(0, 0, 0),
                    (d, e) => { (d as Mainpage).OnCircleColorPropertyChanged(e); }));
        // 通常のプロパティを作成
        public Color CircleColor
        {
            get { return (Color)GetValue(CircleColorProperty); }
            set { SetValue(CircleColorProperty, value); }
        }


        // 円の色を変えられた時の処理
        //色が変更されたらCanvas内の要素（円弧）のstrokeプロパティのブラシを再作成するが、アルファ値だけは元の値を使ってR,G,Bのみを変更する。
        public void OnCircleColorPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (null == MainCanvas) return;
            if (null == MainCanvas.Children) return;

            foreach (var child in MainCanvas.Children)
            {
                var shp = child as Shape;
                var sb = shp.Stroke as SolidColorBrush;
                var a = sb.Color.A;
                shp.Stroke = new SolidColorBrush(Color.FromArgb(a, CircleColor.R, CircleColor.G, CircleColor.B));
            }
        }
    }
}