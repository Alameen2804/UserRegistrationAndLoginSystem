using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserRegistrationNLogin;
using System.IO;

namespace RegistrationForm
{
    public partial class Form1 : Form
    {
        UserRegistrationNLogin.Class1 obj = new Class1();

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                obj.Email = textBox5.Text;
                obj.GenerateOTP(textBox5.Text);
                MessageBox.Show("Successfully OTP sended to " + textBox5.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text == obj.Otp.ToString())
                {
                    obj.Name = textBox1.Text;
                    obj.DateofBirth = dateTimePicker1.Value;
                    textBox3.Text = obj.Age.ToString();
                    obj.UserName = textBox6.Text;
                    obj.Email = textBox5.Text;
                    obj.Password = textBox4.Text;
                    obj.Address = textBox2.Text;
                    obj.ToTextFile();

                    MessageBox.Show("Successfully registered your details!");
                }
                else
                {
                    MessageBox.Show("Please check the one time password!");
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
    }
}