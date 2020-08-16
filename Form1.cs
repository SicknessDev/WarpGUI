using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace warpGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            string sFileName = "";

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                sFileName = choofdlog.FileName;
            }

            textBox1.Text = Path.GetFullPath(sFileName);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "output.exe";
            // set filters - this can be done in properties as well
            savefile.Filter = "All Files (*.*)|*.*";
            string savePath = "";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                savePath = Path.GetFullPath(savefile.FileName);
            }

            textBox2.Text = savePath;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" || textBox2.Text != "")
            {
                string inputPath = textBox1.Text;
                string outputPath = textBox2.Text;

                mergeTheFiles(inputPath, outputPath);

                Process.Start("explorer.exe", Path.GetDirectoryName(outputPath));

            }
        }



        private void mergeTheFiles(string inputPath, string outputPath)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.FileName = "cmd.exe";

            startInfo.Arguments = $"/C warp-packer --arch windows-x64 --input_dir {Path.GetDirectoryName(inputPath)} --exec {Path.GetFileName(inputPath)} --output {outputPath}";

            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
