using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c_Stm32Proje2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {

            if (textBoxUserName.Text == "saharobotik")
            {
                if (textBoxPass.Text == "saha123")
                {
                    new Form2().Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Error:  Please enter correct information");
                }
            }

            else
            {
                MessageBox.Show("Error:  Please enter correct information");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
