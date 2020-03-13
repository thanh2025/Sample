using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Process usbtenki = new Process();

        private void Button1_Click(object sender, EventArgs e)
        {
            usbtenki.StartInfo.FileName = "usbtenkiget";
            usbtenki.StartInfo.Arguments = "-i 0,1";
            usbtenki.StartInfo.UseShellExecute = false;
            usbtenki.StartInfo.RedirectStandardOutput = true;

            try
            {
                usbtenki.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("could not run usbtenkiget:  " + ex);
                return;
            }

            usbtenki.WaitForExit();

            String output = usbtenki.StandardOutput.ReadLine();

            if (output == null)
            {
                MessageBox.Show("usbtenkiget did not return data");
                return;
            }

            float[] fields = output.Split(',').Select(field => float.Parse(field.Trim())).ToArray();

            if (fields.Length != 2)
            {
                MessageBox.Show("usbtenkiget returned an incorrect number of fields");
                return;
            }

            richTextBox1.Clear();
            richTextBox1.AppendText("Temperature (C): " + fields[0] + "\r");
            richTextBox1.AppendText("RH......... (%): " + fields[1] + "\r");
            //MessageBox.Show("Temperature (C): " + fields[0]);
            //MessageBox.Show("RH......... (%): " + fields[1]);
            //Console.WriteLine("Pressure. (kPa): " + fields[2]);
            //Console.WriteLine("Temperature (F): " + (fields[0] * 9 / 5 + 32));
        }
    }
}
