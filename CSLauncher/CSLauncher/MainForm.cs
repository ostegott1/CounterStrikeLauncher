using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApplication3
{
    public partial class MainForm : Form
    {
        private List<String> serverList;
        private String selectedServer;
        private String selectedHLexe;
        private FunctionalityClass fnClass = new FunctionalityClass();
        //private Thread oThread = new Thread(new ThreadStart(LoadDatabase()));

        public MainForm()
        {
           
            InitializeComponent();
            pictureBox1.BackColor = Color.Transparent;
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            serverList = fnClass.LoadDatabase();
            for(int i = 0; i < serverList.Count; i++)
            {
                if (fnClass.isIPValid(serverList[i]))
                    comboBox1.Items.Add(serverList[i]);
                else
                    button3.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();

            if (radioButton2.Checked == true)
            {
                foreach (String line in serverList)
                {

                    if (line.Contains("Steam.exe") || line.Contains("steam.exe"))
                        selectedHLexe = line;
                    else
                    {
                        button3.Enabled = true;
                        selectedHLexe = "";
                    }


                    if (!selectedHLexe.Equals("")) 
                    {
                        textBox1.Text = selectedHLexe;
                        proc.EnableRaisingEvents = true;
                        proc.StartInfo.FileName = selectedHLexe;
                        proc.StartInfo.Arguments = "-applaunch 10 -fullscreen +connect " + selectedServer;
                        proc.Start();
                        proc.WaitForExit();
                        MessageBox.Show("You launched Counter Strike 1.6 through Non-Steam!");
                    }
                }
            }

            if (radioButton1.Checked == true)
            {
                foreach (String line in serverList)
                {

                    if (line.Contains("hl.exe"))
                        selectedHLexe = line;
                    else
                    {
                        button3.Enabled = true;
                        selectedHLexe = "";
                    }

                    if (selectedHLexe.Equals(""))
                    {
                        MessageBox.Show("You should point out hl.exe");
                    }
                    else
                    {
                        textBox1.Text = selectedHLexe;
                        proc.EnableRaisingEvents = true;
                        proc.StartInfo.FileName = selectedHLexe;
                        proc.StartInfo.Arguments = "-game cstrike -fullscreen +connect " + selectedServer;
                        proc.Start();
                        proc.WaitForExit();
                        MessageBox.Show("You launched Counter Strike 1.6 through Non-Steam!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Boolean isValid = fnClass.isIPValid(textBox1.Text);
            if (isValid == false)
                MessageBox.Show("You MUST enter a valid IP address + port");
            else
            {
                comboBox1.Items.Add(textBox1.Text);
                fnClass.AddServerToTheFile(textBox1.Text);
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

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }


       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedServer = (String)comboBox1.SelectedItem;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                fnClass.AddServerToTheFile(openFileDialog1.FileName);
                selectedHLexe = openFileDialog1.FileName;
                button3.Enabled = false;
            }
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DemoFilesForm f2 = new DemoFilesForm(selectedHLexe);
            f2.FormClosed += new FormClosedEventHandler(childFormClosed);
            f2.Show();
            this.Hide();
            MessageBox.Show("In Progress", "Page Info", MessageBoxButtons.OK);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("It might be completed after several weeks");
        }

        public void childFormClosed(object sender, EventArgs e)
        {
            this.Show();
        }

        
    }
}
