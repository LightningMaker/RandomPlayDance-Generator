using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RandomPlayDance_Generator_3.Tools
{
    internal class BiliVideoInfo
    {
        public static long GetVideoCid(string bvid)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://api.bilibili.com/x/web-interface/view?bvid=" + bvid);
                HttpResponseMessage response = client.GetAsync(client.BaseAddress).Result;
                string result = response.Content.ReadAsStringAsync().Result;
                Rootobject root = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(result);

                if (root.data != null)
                {
                    return root.data.cid;
                }
                else
                {
                    Form1.Instance.UpdateLog("获取视频cid失败，原因：" + root.message, Form1.LogLevel.Error);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Form1.Instance.UpdateLog("获取视频cid失败：" + ex.Message, Form1.LogLevel.Error);
                Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);

                return 0;
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
            public string bvid { get; set; }
            public long aid { get; set; }
            public string pic { get; set; }
            public string title { get; set; }
            public long cid { get; set; }

        }
    }
}
