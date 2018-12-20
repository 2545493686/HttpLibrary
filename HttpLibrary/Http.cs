using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LanQ.HttpLibrary
{
    public class Http
    {
        public static string Post(string formData, string header, string url, string encoding = "UTF-8")
        {
            //ServicePointManager.Expect100Continue = false;  

            byte[] postData = Encoding.GetEncoding(encoding).GetBytes(formData);

            WebClient webClient = new WebClient();

            string[] headerData = header.Split('\n'); //直接复制过来的header
            AddHeaders(webClient, headerData); //添加头

            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.GetEncoding(encoding).GetString(responseData);//解码  
            return srcString;
        }

        private static void AddHeaders(WebClient webClient, string[] headerData)
        {
            foreach (string item in headerData)
            {
                string name = item.Substring(0, item.IndexOf(':'));

                string data = item.Substring(item.IndexOf(':') + 2);
                //Console.WriteLine(string.Format("name:{0} data:{1}", name, data));
                try
                {
                    webClient.Headers.Add(name, data);
                }
                catch (Exception)
                {
                    Console.WriteLine("error {0} can not be add", name);
                }
            }
        }

        public static string Get(string url, int sleep = 0, string encoding = "UTF-8")
        {
            Thread.Sleep(sleep);
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36");
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  

            byte[] responseData;

            try
            {
                responseData = webClient.DownloadData(url);//得到返回字符流  
            }
            catch (Exception)
            {
                return string.Empty;
            }

            string srcString = Encoding.GetEncoding(encoding).GetString(responseData);//解码  
            return srcString;
        }
    }
}
