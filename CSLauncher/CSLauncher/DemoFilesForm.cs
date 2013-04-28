using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class DemoFilesForm : Form
    {
        private String selectedCSExe;
        private String selectedDemo = "";

        public DemoFilesForm(String selectedExe)
        {
            selectedCSExe = selectedExe;
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String tempString = textBox1.Text;
            int startIndex = tempString.LastIndexOf("\\") + 1;
            textBox1.Text = startIndex.ToString();
            for (int i = startIndex; i < tempString.Length; i++)
            {
                selectedDemo += tempString[i];
            }
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            if (radioButton2.Checked == true)
            {
                proc.EnableRaisingEvents = true;
                proc.StartInfo.FileName = selectedCSExe;
                proc.StartInfo.Arguments = "-applaunch 10 -console +playdemo " + selectedDemo;
                proc.Start();
                proc.WaitForExit();
            }
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }
    }
}
