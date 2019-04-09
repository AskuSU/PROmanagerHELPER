
using HabraHabr.Core;
using HabraHabr.Core.Habra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace HabraHabr
{
    public partial class HabraForm : Form
    {
        ParserWorker<string[]> parser;

        public HabraForm()
        {
            InitializeComponent();

            parser = new ParserWorker<string[]>(new HabraParser());

            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1, string[] arg2)
        {
            listTitles.Items.AddRange(arg2);
        }

        private void Parser_OnCompleted(object obj)
        {
            MessageBox.Show("All works done!");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            parser.Settings = new HabraSettings((int)numericStart.Value, (int)numericEnd.Value);
            parser.Start();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            parser.Abort();
        }

       
    }
}
