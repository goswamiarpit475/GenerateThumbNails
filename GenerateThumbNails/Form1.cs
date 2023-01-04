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
            GetThumbnail("", "");
        }
        public static void GetThumbnail(string video, string thumbnail)
        {
            var cmd = "C:\\ffmpeg\\bin\\ffmpeg  -i C:\\Users\\ahc\\Downloads\\SoundID\\sample_video.mp4 -ss 00:10:00.000 -vf scale=320:320:force_original_aspect_ratio=decrease -vframes 1 C:\\Users\\ahc\\Downloads\\SoundID\\output.png";

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
    }
}
