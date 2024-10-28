using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RandomPlayDance_Generator_3.Tools
{
    internal class BiliMP4VideoStream
    {
        public static string GetVideoStreamURL(string bvid, long cid)
        {
            try
            {
                Form1 form1 = Form1.Instance;

                //string url = "https://api.bilibili.com/x/player/wbi/playurl?bvid=" + bvid + "&cid=" + cid + "&platform=html5";
                string url = "https://api.bilibili.com/x/player/playurl?";
                Dictionary<string, string> queryParameters = new Dictionary<string, string>
                {
                    { "bvid", bvid },
                    { "cid", cid.ToString() },
                    { "platform", "html5" }
                };
                string signedParameters = WBISignature.GetWBIQuery(queryParameters);

                url += signedParameters;
                // form1.UpdateLog($"拼接url参数：{url}", Form1.LogLevel.Message);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.DefaultRequestHeaders.Add("referer", "https://www.bilibili.com");
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("视频流：" + result);
                Rootobject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(result);
                string stream = root?.data?.durl?.FirstOrDefault()?.url ?? "";
                if (stream == "")
                {
                    form1.UpdateLog($"获取{bvid}视频流失败，视频不存在。", Form1.LogLevel.Error);
                }

                return stream;

            }
            catch (Exception ex)
            {
                Form1.Instance.UpdateLog($"获取{bvid}视频流失败，错误：" + ex.Message, Form1.LogLevel.Error);
                Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);

                return "";
            }
        }



        public class Rootobject
        {
            public int code { get; set; }
            public string message { get; set; }
            public int ttl { get; set; }
            public Data data { get; set; }
        }

        public class Data
        {
            public string from { get; set; }
            public string result { get; set; }
            public string message { get; set; }
            public int quality { get; set; }
            public string format { get; set; }
            public int timelength { get; set; }
            public string accept_format { get; set; }
            public string[] accept_description { get; set; }
            public int[] accept_quality { get; set; }
            public int video_codecid { get; set; }
            public string seek_param { get; set; }
            public string seek_type { get; set; }
            public Durl[] durl { get; set; }
            public Support_Formats[] support_formats { get; set; }
            public object high_format { get; set; }
            public int last_play_time { get; set; }
            public int last_play_cid { get; set; }
            public object view_info { get; set; }
        }

        public class Durl
        {
            public int order { get; set; }
            public int length { get; set; }
            public int size { get; set; }
            public string ahead { get; set; }
            public string vhead { get; set; }
            public string url { get; set; }
            public string[] backup_url { get; set; }
        }

        public class Support_Formats
        {
            public int quality { get; set; }
            public string format { get; set; }
            public string new_description { get; set; }
            public string display_desc { get; set; }
            public string superscript { get; set; }
            public object codecs { get; set; }
        }
    }
}
