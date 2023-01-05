using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFMpegSharp;
using FFMpegSharp.FFMPEG;

namespace GenerateThumbNails
{
    public partial class Form1 : Form
    {
         string ffmpegPath = "";
         string thumbPath = "";
         string inputfilePath = "";
        string vidMainDir = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //VideoInfo v= FFMpegSharp.VideoInfo.FromPath("");
            /* string inputFile = @"C:\\Users\\ahc\\Downloads\\SoundID\\sample_video.mp4";
             FileInfo output = new FileInfo("C:\\Users\\ahc\\Downloads\\SoundID\\t.png");

             var video = VideoInfo.FromPath(inputFile);

             new FFMpeg()
                 .Snapshot(
                     video,
                     output,
                     new Size(200, 400),
                     TimeSpan.FromSeconds(600)
                 );*/
            List<string> vidPaths = loadInputFile();
            int i = 0;
            foreach (string s in vidPaths)
            {
                i++;
                string vidPath = "";
                string thumbTime = "";
                string thumbNail = "";
                string[] x = s.Split('_');
                vidPath = vidMainDir + "\\" + x[0]+".mp4";
                thumbTime = x[1].Replace("\r", "");
                thumbNail = thumbPath+"\\"+x[0].Replace('/', '_').Replace("\r","")+".png";
                GetThumbnail(vidPath, thumbNail, thumbTime);
                lblStatus.Text = i.ToString() + " of " + vidPaths.Count().ToString()+"...";
                if (i >3 )
                {
                    break;
                }
            }
            lblStatus.Text = "Complete";
        }
        public void GetThumbnail(string video, string thumbnail,string thumbtime)
        {
            string cmd = ffmpegPath+" -i \""+video+"\" -ss "+thumbtime+" -vf scale=320:320:force_original_aspect_ratio=decrease -vframes 1 \""+thumbnail+"\"";

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C " + cmd
            };

            var process = new Process
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit();
            // process.WaitForExit(5*60*1000);

            //return LoadImage(thumbnail);
        }

        static Bitmap LoadImage(string path)
        {
            var ms = new MemoryStream(File.ReadAllBytes(path));
            return (Bitmap)Image.FromStream(ms);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string currDirPath = Directory.GetCurrentDirectory();
            ffmpegPath = currDirPath + "\\FFMPEG\\bin\\x64\\ffmpeg";
            inputfilePath = currDirPath + "\\video_data\\input.txt";
            thumbPath = currDirPath + "\\video_data\\thumbs";
            vidMainDir = currDirPath + "\\video_data";


        }
        private List<string> loadInputFile()
        {
            string inputTxt = File.ReadAllText(inputfilePath);
            List<string> videoPath = inputTxt.Split('\n').ToList();
            return videoPath;
        }
    }
}
