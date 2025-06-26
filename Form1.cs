using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomPlayDance_Generator_3
{
    public partial class Form1 : Form
    {
        public static Form1 Instance { get; private set; }
        public static string ExcelPath { get; private set; } = null;

        public static bool IsShuffled { get; private set; } = false;

        public static int IntervalMode { get; private set; } = 2;

        public static int IntervalTime { get; private set; } = 0;

        public static string CustomIntervalPath { get; private set; } = null;

        public static bool IsUsingDash { get; private set; } = true;

        public Form1()
        {
            InitializeComponent();
            Instance = this;
        }



        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            buttonClearCache.Enabled = false;
            // Start the media thread
            MediaThread mediaThread = new MediaThread();
            mediaThread.StartProcessing();
        }

        #region Log

        private delegate void UpdateLogDelegate(string message, LogLevel logLevel);

        public enum LogLevel
        {
            Message,
            Detail,
            Warning,
            Error
        }

        public void UpdateLog(string message, LogLevel logLevel)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateLogDelegate(UpdateLog), message, logLevel);
            }
            else
            {
                UpdateLogInMain(message, logLevel);
            }
        }

        private void UpdateLogInMain(string message, LogLevel logLevel)
        {
            textLogBox.SelectionStart = textLogBox.Text.Length;
            textLogBox.SelectionLength = 0;

            // Set the color based on the log level
            switch (logLevel)
            {
                case LogLevel.Message:
                    textLogBox.SelectionColor = Color.White;
                    break;
                case LogLevel.Detail:
                    textLogBox.SelectionColor = Color.Gray;
                    break;
                case LogLevel.Warning:
                    textLogBox.SelectionColor = Color.Orange;
                    break;
                case LogLevel.Error:
                    textLogBox.SelectionColor = Color.Red;
                    break;
            }

            // Append the message
            textLogBox.AppendText(message + Environment.NewLine);

            // Scroll to the end
            textLogBox.ScrollToCaret();
        }






        #endregion

        private void buttonClearCache_Click(object sender, EventArgs e)
        {
            // Show a confirmation dialog
            DialogResult result = MessageBox.Show("是否需要清空暂存？", "清除暂存", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // Delete the temp folder
                    if (Directory.Exists("temp"))
                    {
                        Directory.Delete("temp", true);
                    }
                }
                catch (Exception ex)
                {
                    UpdateLog("清除暂存歌曲失败：" + ex.Message, LogLevel.Error);
                }
            }


        }

        internal void SetButtonStartEnabled(bool isEnabled)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<bool>(SetButtonStartEnabled), isEnabled);
            }
            else
            {
                buttonStart.Enabled = isEnabled;
                buttonClearCache.Enabled = isEnabled;
            }
        }

        internal void SetProgressBarValue(int value)// 0-100
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(SetProgressBarValue), value);
            }
            else
            {
                progressBar.Value = value;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            ExcelPath = openFileDialog1.FileName;
            labelExcelPath.Text = Path.GetFileName(ExcelPath);
        }

        private void buttonImportExcel_Click(object sender, EventArgs e)
        {
            // Show the file dialog
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
            openFileDialog1.ShowDialog();
        }

        private void orderChoice1_CheckedChanged(object sender, EventArgs e)
        {
            IsShuffled = false;
        }

        private void orderChoice2_CheckedChanged(object sender, EventArgs e)
        {
            IsShuffled = true;
        }

        private void intervalChoice1_CheckedChanged(object sender, EventArgs e)
        {
            IntervalMode = 1;
        }

        private void intervalChoice2_CheckedChanged(object sender, EventArgs e)
        {
            IntervalMode = 2;
        }

        private void intervalChoice3_CheckedChanged(object sender, EventArgs e)
        {
            IntervalMode = 3;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            IntervalTime = (int)numericUpDown1.Value;
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            CustomIntervalPath = openFileDialog2.FileName;
            labelIntervalFile.Text = Path.GetFileName(CustomIntervalPath);
        }

        private void buttonImportAudio_Click(object sender, EventArgs e)
        {
            // Show the file dialog
            openFileDialog2.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog2.Filter = "Audio files (*.mp3, *.wav)|*.mp3;*.wav";
            openFileDialog2.ShowDialog();
        }


        private void textLogBox_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && textLogBox.SelectedText != "")
            {
                contextMenuStrip1.Show(textLogBox, new Point(e.X, e.Y));
            }
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            textLogBox.Copy();
        }

        private void MP4Download_CheckedChanged(object sender, EventArgs e)
        {
            IsUsingDash = false;
        }

        private void DashDownload_CheckedChanged(object sender, EventArgs e)
        {
            IsUsingDash = true;
        }

        private void checkBoxVol_CheckedChanged(object sender, EventArgs e)
        {
            MediaThread.EnableLoudnorm = checkBoxVol.Checked;
        }
    }
}
