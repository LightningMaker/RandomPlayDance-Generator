﻿using FFMpegCore;
using RandomPlayDance_Generator_3.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RandomPlayDance_Generator_3
{
    internal class DashDownloader
    {
        public static void DownloadAudio(string songName, string songID, long cid, string songPath, string videoPath)
        {
            string dashURL = BiliDashVideoStream.GetVideoStreamURL(songID, cid);
            Form1.Instance.UpdateLog("下载Dash源: " + dashURL, Form1.LogLevel.Detail);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                client.DefaultRequestHeaders.Referrer = new Uri("http://www.bilibili.com");

                HttpResponseMessage response = client.GetAsync(dashURL).Result;

                if (response.IsSuccessStatusCode)
                {
                    string dashFile = videoPath + "\\" + songID + ".m4a";
                    using (FileStream fs = new FileStream(dashFile, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        response.Content.CopyToAsync(fs).Wait();
                    }
                    Form1.Instance.UpdateLog(songName + " 下载完成", Form1.LogLevel.Message);

                    long length = new FileInfo(dashFile).Length;
                    //Form1.Instance.UpdateLog($"视频文件大小：{(length / 1024f / 1024).ToString("0.0")} MB", Form1.LogLevel.Detail);

                    string saveFile = songPath + "\\" + songID + ".mp3";

                    if (FFMpeg.ExtractAudio(dashFile, saveFile))
                    {
                        Form1.Instance.UpdateLog(songName + " 音频转换完成", Form1.LogLevel.Message);
                    }
                    else
                    {
                        Form1.Instance.UpdateLog(songName + " 音频转换失败", Form1.LogLevel.Error);
                    }
                }
                else
                {
                    Form1.Instance.UpdateLog(songName + " 下载失败：" + response.StatusCode, Form1.LogLevel.Error);
                    Form1.Instance.UpdateLog("已跳过当前歌曲", Form1.LogLevel.Warning);
                }
            }

        }


       

    }
}
