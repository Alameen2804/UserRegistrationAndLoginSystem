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
    public partial class Form3 : Form
    {
        UserRegistrationNLogin.Class1 obj = new UserRegistrationNLogin.Class1();

        public Form3()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            obj.GenerateOTP(textBox1.Text);
            MessageBox.Show("Successfully OTP sended to " + textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == obj.Otp.ToString())
            {
                MessageBox.Show("Successfully reset your details!");
                obj.resetPassword(textBox1.Text, textBox2.Text);
            }
            else
            {
                MessageBox.Show("Please check the one time password!");
            }
        }
    }
}
