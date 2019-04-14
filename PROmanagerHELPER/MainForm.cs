using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using PROmanagerHELPER.CoreRussProfil;
using PROmanagerHELPER.CoreRussProfil.RussProfil;
using ScrapySharp.Network;
using PROmanagerHELPER.CoreRussProfil.KompanyType;

namespace PROmanagerHELPER
{
    public partial class MainForm : Form
    {
       // private static string StrRequestINN = null;

        ParserWorkerRUS<List<IKOMPANY>> parser;

        public MainForm()
        {
            InitializeComponent();

            parser = new ParserWorkerRUS<List<IKOMPANY>>(new RussProfilParser());

            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, List<IKOMPANY> arg2)
        {
            if (arg2 != null)
            {
                int i = 0;
                foreach (var item in arg2)
                {
                    listBox1.Items.Add(arg2[i].Name);
                    listBox1.Items.Add(arg2[i].ID.ToString());
                    listBox1.Items.Add(arg2[i].Adress.NotFuulAdress);
                    listBox1.Items.Add(arg2[i].INN);
                    listBox1.Items.Add(arg2[i].OGRN);
                    listBox1.Items.Add(arg2[i].TypeOfOwner);
                    if (arg2[i].TypeOfOwner != "No Data")
                    {                        
                        listBox1.Items.Add(arg2[i].Owner.Surname);
                        listBox1.Items.Add(arg2[i].Owner.Name);
                        listBox1.Items.Add(arg2[i].Owner.MiddleName);
                    }
                    listBox1.Items.Add("---------------------------------");


                    i++;
                }        
            }
            
            
        }

        private void Parser_OnCompleted(object obj, List<IKOMPANY> arg2)
        {
            if (arg2 != null)
                MessageBox.Show($"Все организации загружены!\n{arg2.Count} организаций.");

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int IsActive = 0;
            if (radioButton1.Checked)
            {
                IsActive = 0;
            }
            if (radioButton2.Checked)
            {
                IsActive = 1;
            }
            if (radioButton3.Checked)
            {
                IsActive = 3;
            }

            parser.Settings = new RussProfilSettings();
            parser.Start(textStreet.Text, IsActive);
        }

    }
}
