using System.IO;
using System.Collections.Generic;


namespace Util
{
    public class DirectoryUtils
    {
        /// <summary>
        /// 指定したパスにディレクトリが存在しない場合
        /// すべてのディレクトリとサブディレクトリを作成する
        /// </summary>
        public static DirectoryInfo SafeCreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                return null;
            }
            return Directory.CreateDirectory(path);
        }

        /// <summary>
        /// 指定したパスにあるファイル名をすべて取得
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFileNamesInPath(string path)
        {
            //"C:\test"以下のファイルをすべて取得する
            //ワイルドカード"*"は、すべてのファイルを意味する
            string[] files = System.IO.Directory.GetFiles(
                path, "*", System.IO.SearchOption.AllDirectories);

            return files;
        }
    }
}