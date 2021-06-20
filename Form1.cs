using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace SpreadsheetApp
{
    public partial class Form1 : Form
    {
        public String func;
        public bool flag = false;
        public ShareableSpreadSheet SH;

        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SH = new ShareableSpreadSheet(1, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            func = "Load";
            textBox1.Text = "please write here your path and then click ok";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            func = "SearchString";
            if (flag == false)
            {
                textBox1.Text = "please first load the spread sheet";
            }
            else { textBox1.Text = "please write here your text and then click ok"; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            func = "GetCell";
            if (flag == false)
            {
                textBox1.Text = "please first load the spread sheet";
            }
            else
            {
                textBox1.Text = "please write here your indexes in format ROW,COL and then click ok";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            func = "SetCell";
            if (flag == false)
            {
                textBox1.Text = "please first load the spread sheet";
            }
            else
            {
                textBox1.Text = "please write here your indexes and text in format ROW,COL,TXT and then click ok";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_MouseEnte(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (func == "Load") {
                try { 
                string title = "Load";
                if (SH.load(textBox1.Text))
                {
                    flag = true;
                    string message = "Load Successed";
                    MessageBox.Show(message, title);
                }
                else {
                    string message = "Load Fialed";
                    MessageBox.Show(message, title); 
                }
            }
                catch
            {
                string title = "ERROR";
                string message = "Invalid format";
                MessageBox.Show(message, title);
            }
        }
            else if (func == "SearchString" && flag == true)
            {
                try { 
                int row = 0;
                int col = 0;
                string title = "SearchString";
                if (SH.searchString(textBox1.Text, ref row, ref col))
                {
                    string message = textBox1.Text + " found in cell (" + row.ToString() + "," + col.ToString() + ")";
                    MessageBox.Show(message, title);
                }
                else
                {
                    string message = textBox1.Text + " not found";
                    MessageBox.Show(message, title);
                }
            }
                catch
            {
                string title = "ERROR";
                string message = "Invalid format";
                MessageBox.Show(message, title);
            }
        }
            else if (func == "GetCell" && flag == true) 
            {
                try
                {
                    string title = "GetCell";
                    string[] rc = textBox1.Text.Split(",");
                    int row = 0;
                    int col = 0;
                    SH.getSize(ref row, ref col);
                    if (Convert.ToInt32(rc[0]) > row || Convert.ToInt32(rc[1]) > col || Convert.ToInt32(rc[0]) <= 0 || Convert.ToInt32(rc[1]) <= 0)
                    {
                        string message = textBox1.Text + "Invalid indexes";
                        MessageBox.Show(message, title);
                    }
                    else
                    {
                        string message = "The value is: " + SH.getCell(Convert.ToInt32(rc[0]), Convert.ToInt32(rc[1]));
                        MessageBox.Show(message, title);
                    }
                }
                catch
                {
                    string title = "ERROR";
                    string message = "Invalid format";
                    MessageBox.Show(message, title);
                }
            }
            else if (func == "SetCell" && flag == true) 
            {
                string title = "SetCell";
                try { 
                {
                    string[] rc = textBox1.Text.Split(",");
                    if (SH.setCell(Convert.ToInt32(rc[0]), Convert.ToInt32(rc[1]),rc[2]))
                    {
                        string message = "Set cell successed";
                        MessageBox.Show(message, title);
                    }
                    else
                    {
                        string message = "Set cell faild";
                        MessageBox.Show(message, title);
                    }
                }
            }
                catch
            {
                title = "ERROR";
                string message = "Invalid format";
                MessageBox.Show(message, title);
            }
        }
            else
            {
                string title = "Load";
                string message = "Please first load the spread sheet";
                MessageBox.Show(message, title);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(SH);
            f.ShowDialog();
        }
    }
}
