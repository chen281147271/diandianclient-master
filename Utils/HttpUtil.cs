using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DianDian.util
{
    class HttpUtil
    {
        private static string server = "http://app.diandiancaidan.com/";
        public static string doPost(List<KeyValuePair<String, String>> paramList, string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
          
                //开始登录
              
              
            
                 HttpResponseMessage response = httpClient.PostAsync(new Uri(server+url), new FormUrlEncodedContent(paramList)).Result;
                String result = response.Content.ReadAsStringAsync().Result;
              
           

               Console.WriteLine("登录成功！"+ result);

               //用完要记得释放
               httpClient.Dispose();
            return result;

        }
        public static string doPost(string pdata)
        {
           

            //开始登录
            string url = server + "shop/api.do?m=offLineUpdate";//server+http://127.0.0.1:8080/DdService/
            try
            {
                string re = PostXmlResponse(url, pdata);
                Console.WriteLine("登录成功！" + re);
                return re;
            }
            catch (Exception e) {
                Console.Write(e);
                return null;
            }
           
        }

        //body是要传递的参数,格式"roleId=1&uid=2" //post的cotentType填写:  //"application/x-www-form-urlencoded" //soap填写:"text/xml; charset=utf-8"     
        public static string PostHttp(string url, string body, string contentType)     {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = "POST";         httpWebRequest.Timeout = 20000;
            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();
            return responseContent;
        }

        public static string PostXmlResponse(string url, string xmlString)

        {
            HttpContent httpContent = new StringContent(xmlString);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();



            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                return t.Result;
            }
            return string.Empty;
        }

    }
}
