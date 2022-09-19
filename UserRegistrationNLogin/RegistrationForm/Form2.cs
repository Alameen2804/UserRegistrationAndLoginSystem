using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationForm
{
    public partial class Form2 : Form
    {
        UserRegistrationNLogin.Class1 obj = new UserRegistrationNLogin.Class1();

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (obj.IsAlreadyRegistered(textBox1.Text, obj.getHashSha256(textBox2.Text)) == true)
                {
                    obj.sendAlertMail();
                    MessageBox.Show("Successfully Login!");
                }
                else
                {
                    MessageBox.Show("Please register first!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }
    }
}
