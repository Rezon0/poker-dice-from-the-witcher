using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form1 : Form
    {
        bool PVE_mod = true;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            PVE_mod = true;
            Form2 form2 = new Form2(PVE_mod);
            form2.Show();            
        }

        private void PVP_button_Click(object sender, EventArgs e)
        {
            Visible = false;
            PVE_mod = false;
            Form2 form2 = new Form2(PVE_mod);
            form2.Show();
        }
    }
}
