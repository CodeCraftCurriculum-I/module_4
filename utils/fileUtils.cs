namespace Utils
{

    using System.IO;
    using System.Text;

    public class FileUtils
    {

        public static void WriteToFile(string path, string content)
        {
            FileStream fs = File.Create(path);
            byte[] info = new UTF8Encoding(true).GetBytes(content);
            fs.Write(info, 0, info.Length);

        }

        public static string ReadFromFile(string path)
        {
            string content = "";
            using (StreamReader sr = File.OpenText(path))
            {
                string? s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    content += s + "\n";
                }
            }
            return content;
        }
    }




}