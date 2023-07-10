using System.IO;

namespace JsonDataPack
{
    public class initialize
    {
        public initialize()
        {
            //フォルダが存在するか確認する処理。
            if (Directory.Exists(@"temp"))
            {
                //指定したフォルダが存在する時の処理
            }
            else
            {
                //仕様書、ディレクトリ構造図参照
                Directory.CreateDirectory(@"temp");
                //Directory.CreateDirectory(@"testFolder");
                Directory.CreateDirectory(@"JsonResource/Personal");
                Directory.CreateDirectory(@"JsonResource/Rescue");
                Directory.CreateDirectory(@"ImgResouse/Rphoto");
                Directory.CreateDirectory(@"ImgResouse/Qphoto");
            }
        }
    }
}