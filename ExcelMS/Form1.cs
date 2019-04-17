using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelMS
{    
    public partial class Form1 : Form
    {
        private ExcelMSsupport ExcelM;
        public Form1()
        {
            InitializeComponent();
            ExcelM = new ExcelMSsupport();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ExcelM.ExcelMSsupportStart();
        }
    }
}
