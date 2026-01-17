using RandomPlayDance_Generator_3.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore;
using Newtonsoft.Json;
using System.Diagnostics;
using FFMpegCore.Enums;

namespace RandomPlayDance_Generator_3
{
    internal class MediaThread
    {
        public static bool EnableLoudnorm { get; set; } = false;

        public void StartProcessing()
        {
            var thread = new Thread(ProcessingThread);
            thread.IsBackground = true;
            thread.Start();
        }

        public void ProcessingThread()
        {
            try
            {

                Form1.Instance.SetProgressBarValue(0);

                // 测试WBI签名
                //WBISignature.GetWBIQuery(new Dictionary<string, string>());

                // 读取歌单文件
                Form1.Instance.UpdateLog("准备读取歌单文件...", Form1.LogLevel.Message);
                DataTable dtTable = ExcelReader.ReadExcel();

                #region 基本路径设置

                // Clear and recreate raw folder
                string videoPath = "temp/videos";
                if (Directory.Exists(videoPath))
                {
                    Directory.Delete(videoPath, true);
                }
                Directory.CreateDirectory(videoPath);

                // Create song folder if not exists
                string songPath = "temp/songs";
                if (!Directory.Exists(songPath))
                {
                    Directory.CreateDirectory(songPath);
                }

                // Clear and recrate cut folder
                string cutPath = "temp/cuts";
                if (Directory.Exists(cutPath))
                {
                    Directory.Delete(cutPath, true);
                }
                Directory.CreateDirectory(cutPath);

                #endregion

                // 初始化 ffmpeg
                GlobalFFOptions.Configure(new FFOptions { BinaryFolder = "./assets", TemporaryFilesFolder = "/tmp" });

                #region 下载歌曲


                int currentCount = 0;
                foreach (DataRow row in dtTable.Rows)
                {
                    currentCount++;

                    string songName = row[0].ToString();
                    string songID = row[1].ToString();


                    Form1.Instance.UpdateLog($"【{currentCount}/{dtTable.Rows.Count}】正在下载 " + songName + "...", Form1.LogLevel.Message);

                    try
                    {
                        // detect if the song file exists in songs folder
                        string songFile = songPath + "\\" + songID + ".mp3";

                        // Songs from Bilibili
                        if (songID.ToUpper().StartsWith("BV"))
                        {
                            // Detect if the file exist and greater than 100kb
                            if (File.Exists(songFile) && new FileInfo(songFile).Length > 100 * 1024)
                            {
                                Form1.Instance.UpdateLog(songName + " 已存在，跳过下载", Form1.LogLevel.Message);
                            }
                            else
                            {
                                Form1.Instance.UpdateLog("获取视频信息中...", Form1.LogLevel.Detail);
                                long cid = BiliVideoInfo.GetVideoCid(songID);
                                Form1.Instance.UpdateLog("视频cid: " + cid, Form1.LogLevel.Detail);

                                if (!Form1.IsUsingDash)
                                {
                                    MP4Downloader.DownloadAudio(songName, songID, cid, songPath, videoPath);
                                }
                                else
                                {
                                    DashDownloader.DownloadAudio(songName, songID, cid, songPath, videoPath);
                                }

                            }
                        }
                        // other platforms
                        else
                        {
                            // Not supported
                            Form1.Instance.UpdateLog(songName + " 歌曲来源暂不支持", Form1.LogLevel.Error);
                            Form1.Instance.UpdateLog("已跳过当前歌曲", Form1.LogLevel.Warning);
                        }
                        /*{
                            string baseURL = "http://music.163.com/song/media/outer/url?id=";
                            string songURL = baseURL + songID + ".mp3";
                            try
                            {
                                using (WebClient client = new WebClient())
                                {
                                    client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                                    client.Headers.Add("Referer", "music.163.com");
                                    var uri = new Uri(songURL);
                                    string saveFile = songPath + "\\" + songID + ".mp3";
                                    client.DownloadFileTaskAsync(uri, saveFile).Wait();

                                    Form1.Instance.UpdateLog(songName + " 下载完成", Form1.LogLevel.Message);
                                }
                            }
                            catch (Exception ex)
                            {
                                Form1.Instance.UpdateLog(songName + " 下载失败: " + ex.Message, Form1.LogLevel.Error);
                            }



                            Form1.Instance.UpdateLog("等待5秒...", Form1.LogLevel.Detail);
                            Thread.Sleep(5000);
                        }**/

                        Form1.Instance.SetProgressBarValue(90 * currentCount / dtTable.Rows.Count);

                    }
                    catch (Exception ex)
                    {
                        Form1.Instance.UpdateLog(songName + " 处理出现错误: " + ex.Message, Form1.LogLevel.Error);
                        Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);
                    }
                }
                #endregion

                int SongSuccessCount = 0;
                int SongFailCount = 0;

                #region 裁剪乐曲
                foreach (DataRow row in dtTable.Rows)
                {
                    string songName = row[0].ToString();
                    string songID = row[1].ToString();
                    string loadFile = songPath + "\\" + songID + ".mp3";
                    string saveFile = cutPath + "\\" + songID + ".mp3";
                    string cutTime = row[2].ToString();
                    cutTime = cutTime.Replace("：", ":");
                    cutTime = cutTime.Replace("~", "-");
                    string[] cutTimeArray = cutTime.Split('-');
                    try
                    {
                        TimeSpan begin = TimeSpan.Parse("00:" + cutTimeArray[0]);
                        TimeSpan end = TimeSpan.Parse("00:" + cutTimeArray[1]);

                        Form1.Instance.UpdateLog("裁剪 " + songName + "..." + begin + "-" + end, Form1.LogLevel.Message);

                        //FFMpeg.SubVideo(loadFile, saveFile, begin, end);
                        FFMpegArguments.FromFileInput(loadFile)
                            .OutputToFile(saveFile, true, options =>
                            {
                                options.WithDuration(end - begin);

                                if (EnableLoudnorm)
                                {
                                    options.WithCustomArgument("-af loudnorm=I=-16:TP=-1.5:LRA=11");
                                }

                                options.WithAudioCodec(AudioCodec.LibMp3Lame);
                                options.WithAudioBitrate(320);
                            })
                            .ProcessSynchronously();

                        //TrimMp3(loadFile, saveFile, begin, end);
                        Form1.Instance.UpdateLog("裁剪完成", Form1.LogLevel.Message);

                        // 检测裁剪后的文件是否 < 10 kb
                        long length = new FileInfo(saveFile).Length;
                        if (length < 10 * 1024)
                        {
                            Form1.Instance.UpdateLog("歌曲 " + songName + " 裁剪后文件已损坏，请检查剪辑时间是否填写正确", Form1.LogLevel.Warning);
                        }
                        else
                        {
                            SongSuccessCount++;// 成功计数
                        }
                    }
                    catch (Exception ex)
                    {
                        // Copy file
                        try
                        {
                            File.Copy(loadFile, saveFile, true);
                            Form1.Instance.UpdateLog("未填写 " + songName + " 的剪辑时间，默认使用全曲", Form1.LogLevel.Warning);

                            SongSuccessCount++;// 成功计数
                        }
                        catch (Exception exc)
                        {
                            Form1.Instance.UpdateLog(songName + " 文件不存在。错误：" + exc.Message, Form1.LogLevel.Error);
                            Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);
                            Form1.Instance.UpdateLog("已跳过当前歌曲", Form1.LogLevel.Warning);

                            SongFailCount++;// 失败计数
                        }

                    }
                }

                #endregion


                #region 准备合并的歌曲

                Form1.Instance.UpdateLog("正在合并歌曲...", Form1.LogLevel.Message);
                List<string> songPaths = new List<string>();
                foreach (DataRow row in dtTable.Rows)
                {
                    string songName = row[0].ToString();
                    string songID = row[1].ToString();
                    // append the env path
                    string file = "temp/cuts/" + songID + ".mp3";

                    if (!File.Exists(file))
                    {
                        Form1.Instance.UpdateLog("歌曲 " + songName + " 不存在，已跳过", Form1.LogLevel.Warning);
                    }
                    else
                    {
                        // 检测文件是否 < 10 kb
                        long length = new FileInfo(file).Length;
                        if (length < 10 * 1024)
                        {
                            Form1.Instance.UpdateLog("歌曲 " + songName + " 文件已损坏，已跳过", Form1.LogLevel.Warning);
                        }
                        else
                        {
                            songPaths.Add(file);
                            Form1.Instance.UpdateLog("已添加歌曲 " + songName, Form1.LogLevel.Message);
                        }

                    }
                }

                // 决定是否打乱
                if (Form1.IsShuffled)
                {
                    Form1.Instance.UpdateLog("正在打乱歌曲顺序...", Form1.LogLevel.Message);
                    songPaths = songPaths.OrderBy(x => Guid.NewGuid()).ToList();

                    try
                    {
                        // 在日志中显示打乱后的歌曲顺序
                        for (int i = 0; i < songPaths.Count; i++)
                        {
                            // 重新读取excel查找歌曲名
                            foreach (DataRow row in dtTable.Rows)
                            {
                                if (row[1].ToString() == Path.GetFileNameWithoutExtension(songPaths[i]))
                                {
                                    Form1.Instance.UpdateLog($"【{i + 1}】" + row[0].ToString(), Form1.LogLevel.Detail);

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Form1.Instance.UpdateLog("获取歌曲名称失败：" + ex.Message, Form1.LogLevel.Error);
                        Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);
                    }
                }

                try
                {
                    // 插入间隔音频
                    if (Form1.IntervalMode == 1)
                    {
                        int time = Form1.IntervalTime;
                        if (time > 0)
                        {
                            string tempDir = "temp";
                            if (!Directory.Exists(tempDir))
                            {
                                Directory.CreateDirectory(tempDir);
                            }
                            string intervalFile = Path.Combine(tempDir, $"BlankAudio_{time}s.mp3");
                            string blankSource = "assets/BlankAudio.mp3";
                            if (!File.Exists(intervalFile))
                            {
                                // 构造 N 个输入
                                var argument = FFMpegArguments.FromFileInput(blankSource);
                                for (int i = 1; i < time; i++)
                                {
                                    argument.AddFileInput(blankSource);
                                }

                                // 构造 filter_complex
                                // [0:0][1:0]...[N-1:0]concat=n=N:v=0:a=1[out]
                                StringBuilder filter = new StringBuilder();
                                for (int i = 0; i < time; i++)
                                {
                                    filter.Append($"[{i}:0]");
                                }
                                filter.Append($"concat=n={time}:v=0:a=1[out]");

                                argument.OutputToFile(intervalFile, true, builder =>
                                {
                                    builder.WithCustomArgument($"-filter_complex \"{filter}\" -map \"[out]\"");
                                }).ProcessSynchronously();
                            }
                            Form1.Instance.UpdateLog($"正在插入空白间隔，时间为{time}秒", Form1.LogLevel.Message);
                            for (int i = songPaths.Count - 1; i >= 0; i--)
                            {
                                songPaths.Insert(i, intervalFile);
                            }
                        }
                    }
                    else if (Form1.IntervalMode == 2)
                    {
                        Form1.Instance.UpdateLog($"正在插入5秒倒计时", Form1.LogLevel.Message);
                        string countdownFile = "assets/Countdown.mp3";
                        for (int i = songPaths.Count - 1; i >= 0; i--)
                        {
                            songPaths.Insert(i, countdownFile);
                        }
                    }
                    else
                    {
                        if (Form1.CustomIntervalPath != null && File.Exists(Form1.CustomIntervalPath))
                        {
                            Form1.Instance.UpdateLog($"正在插入自定义过渡音频", Form1.LogLevel.Message);
                            string customIntervalFile = Form1.CustomIntervalPath;
                            for (int i = songPaths.Count - 1; i >= 0; i--)
                            {
                                songPaths.Insert(i, customIntervalFile);
                            }
                        }
                        else
                        {
                            Form1.Instance.UpdateLog("自定义过渡音频文件不存在，已跳过", Form1.LogLevel.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Form1.Instance.UpdateLog("插入过渡音频失败：" + ex.Message, Form1.LogLevel.Error);
                    Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);
                }

                Form1.Instance.SetProgressBarValue(95);

                // 生成导出文件夹
                string exportPath = "exports";
                if (!Directory.Exists(exportPath))
                {
                    Directory.CreateDirectory(exportPath);
                }

                Form1.Instance.UpdateLog("正在生成音频，请耐心等待", Form1.LogLevel.Message);

                #endregion

                #region 调用ffmpeg合并音频
                try
                {
                    // 1. 生成 audiolist.txt 文件
                    string listFilePath = Path.Combine("temp", "AudioList.txt");
                    StringBuilder sbList = new StringBuilder();

                    foreach (var pathUrl in songPaths)
                    {
                        // 获取绝对路径，并替换反斜杠为正斜杠（FFmpeg concat文件格式要求）
                        string absPath = Path.GetFullPath(pathUrl).Replace("\\", "/");
                        // 写入格式: file 'C:/path/to/file.mp3'
                        sbList.AppendLine($"file '{absPath}'");
                    }

                    File.WriteAllText(listFilePath, sbList.ToString());
                    //Form1.Instance.UpdateLog($"已生成合并列表文件: {listFilePath}", Form1.LogLevel.Detail);

                    // 2. 准备导出路径
                    string path = Form1.ExcelPath;
                    if (path == null)
                    {
                        path = "歌单.xlsx";
                    }
                    string exportFile = Path.Combine(exportPath, Path.GetFileNameWithoutExtension(path)
                        + "-" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".mp3");

                    // 3. 配置 FFmpeg 参数
                    // 使用 -f concat -safe 0 -i list.txt
                    var argument = FFMpegArguments.FromFileInput(listFilePath, true, options => options.WithCustomArgument("-f concat -safe 0"));

                    argument.OutputToFile(exportFile, true, options =>
                    {
                        // 如果开启了响度均衡
                        /*if (EnableLoudnorm)
                        {
                            // 使用 -af (audio filter) 而不是 complex filter，因为现在只有一个输入流(concat后的流)
                            options.WithCustomArgument("-af loudnorm=I=-16:TP=-1.5:LRA=11");
                        }**/

                        // 强制重新编码为 MP3 (libmp3lame)，确保合并后的时间戳和格式正确
                        // 如果不重新编码直接 copy，可能会因为采样率不一致导致合并失败或播放异常
                        options.WithAudioCodec(AudioCodec.LibMp3Lame);

                        // 可选：设置比特率，保证音质
                        options.WithAudioBitrate(320);
                    }).ProcessSynchronously();

                    // 输出计数
                    Form1.Instance.UpdateLog($"剪辑成功 {SongSuccessCount} 首，失败 {SongFailCount} 首", Form1.LogLevel.Message);
                    Form1.Instance.UpdateLog("已生成随舞音频 " + Path.Combine("\\", exportFile), Form1.LogLevel.Message);

                    try
                    {
                        // 打开文件夹
                        Process.Start("explorer.exe", Path.Combine(Environment.CurrentDirectory, exportPath));
                    }
                    catch (Exception ex)
                    {
                        Form1.Instance.UpdateLog("打开文件夹失败，请手动打开exports文件夹获取生成的音频文件" + ex.Message, Form1.LogLevel.Warning);
                    }
                }
                catch (Exception ex)
                {
                    Form1.Instance.UpdateLog("歌曲合并失败，错误：" + ex.Message, Form1.LogLevel.Error);
                    Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);
                }

                Form1.Instance.SetProgressBarValue(100);

                // Set buttonStart to enabled
                Form1.Instance.SetButtonStartEnabled(true);

                #endregion

            }
            catch (Exception ex)
            {
                Form1.Instance.UpdateLog("处理出现错误: " + ex.Message, Form1.LogLevel.Error);
                Form1.Instance.UpdateLog(ex.StackTrace, Form1.LogLevel.Error);

                Form1.Instance.SetButtonStartEnabled(true);
            }
        }
    }
}
