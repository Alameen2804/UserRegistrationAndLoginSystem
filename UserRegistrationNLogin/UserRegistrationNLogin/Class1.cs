using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Net; //For host name and ip address

namespace UserRegistrationNLogin
{
    public class Class1
    {
        string filepath = @"C:\Users\ALAMEEN\Documents\DestinationFile\UserRegistrationDetails.txt";

        string _name;
        DateTime _dateofBirth;
        int _age;
        string _userName;
        string _password;
        string _email;
        string _address;
        int _otp;

        public int Otp
        {
            get { return _otp; }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (value.Length >= 5)
                {
                    _address = value;
                }
                else
                {
                    throw new Exception("Please check the address!");
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (CheckMailAddress(value) && value.Contains("@gmail.com"))
                {
                    _email = value;
                }
                else
                {
                    throw new Exception("Please check the mail address!");
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value.Length >= 7)
                {
                    if (PasswordChecker(value) == "Very Strong")
                    {
                        _password = value;
                    }
                    else
                    {
                        throw new Exception("Please enter the strong password!");
                    }
                }
                else
                {
                    throw new Exception("Please enter the valid password(min-7 chars)!");
                }
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (checkUserName(value))
                {
                    if (value.Length >= 5 && value.Length <= 15)
                    {
                        _userName = value;
                    }
                    else
                    {
                        throw new Exception("Please enter valid username(min 3 chars, max 15 chars)!");
                    }
                }
                else
                {
                    throw new Exception("Please enter the valid usernme!");
                }
            }
        }

        public int Age
        {
            get { return _age; }
        }

        public DateTime DateofBirth
        {
            get { return _dateofBirth; }
            set
            {
                int age = DateTime.Now.Year - value.Year;
                if (age >= 18 && age <= 60)
                {
                    this._age = age;
                    _dateofBirth = value;
                }
                else
                {
                    throw new Exception("Please enter valid date of birth!");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length >= 3 && value.Length <= 15)
                {
                    _name = value;
                }
                else
                {
                    throw new Exception("Enter the correct name!");
                }
            }
        }

        public void GenerateOTP(string toMailAddress)
        {
            Random r = new Random();
            _otp = r.Next(100000, 999999);
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("examsignalameen@gmail.com");
                msg.To.Add(toMailAddress);
                msg.Subject = "OTP";
                msg.Body = Otp.ToString();

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com"; 
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "examsignalameen@gmail.com";
                ntcd.Password = "qhnrugzejxvxwgob";
                smt.Credentials = ntcd;
                smt.EnableSsl = true;
                smt.Port = 587;
                smt.Send(msg);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ToTextFile()
        {
            string writetofile = _name + "," + _dateofBirth.ToString("dd/MM/yyyy") + "," + _age + "," + _userName + "," + _email + "," + getHashSha256(_password) + "," + _address + "," + _password;
            File.AppendAllText(filepath, writetofile + "\n");
        }

        string mail = "";
        public bool IsAlreadyRegistered(string username, string password)
        {
            bool check = false;
            string[] checkfile = File.ReadAllLines(filepath);
            foreach (var item in checkfile)
            {
                string[] data = item.Split(',');
                if (username == data[3] && password == data[5])
                {
                    check = true;
                    mail = data[4];
                }
                //else if (mail == data[3] && password != data[5])
                //{
                //    throw new Exception("Please enter correct password!");
                //}
            }
            return check;
        }

        public string PasswordChecker(string password)
        {
            bool foundLower = false;
            bool foundUpper = false;
            bool foundDigit = false;
            bool foundSpecialChar = false;

            string SpecialChar = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] SpecialCh = SpecialChar.ToCharArray();
            int n = password.Length;
            string Result = "";

            foreach (char item in password.ToCharArray())
            {
                if (char.IsLower(item))
                {
                    foundLower = true;
                }
                if (char.IsUpper(item))
                {
                    foundUpper = true;
                }
                if (char.IsDigit(item))
                {
                    foundDigit = true;
                }
                if (SpecialCh.Contains(item))
                {
                    foundSpecialChar = true;
                }
            }

            if (foundDigit && foundLower && foundSpecialChar && foundUpper && (n >= 12))
            {
                Result = "Very Strong";
            }
            else if (foundDigit && foundLower && foundSpecialChar && foundUpper && (n >= 10))
            {
                Result = "Strong";
            }
            else if (foundUpper && foundLower && (foundSpecialChar || foundDigit) && (n >= 8))
            {
                Result = "Average";
            }
            else if (foundUpper && foundLower && (foundSpecialChar || foundDigit) && (n >= 6))
            {
                Result = "Weak";
            }
            else
            {
                Result = "Very Weak";
            }
            return Result;
        }

        public bool checkUserName(string username)
        {
            bool ValidUserName = true;
            foreach (char item in username.ToCharArray())
            {
                if (char .IsSymbol(item))
                {
                    ValidUserName = false;
                }
            }
            return ValidUserName;
        }

        public string getHashSha256(string rawPassword)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(rawPassword);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public void sendAlertMail()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("examsignalameen@gmail.com");
                msg.To.Add(mail);
                msg.Subject = "Security Alert!";
                msg.Body = "A new sign-in on " + hostName + "\n" + "IP Address: " + myIP + "\n" +"Time: "+ DateTime.Now.ToString();

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "examsignalameen@gmail.com";
                ntcd.Password = "qhnrugzejxvxwgob";
                smt.Credentials = ntcd;
                smt.EnableSsl = true;
                smt.Port = 587;
                smt.Send(msg);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CheckMailAddress(string emailAaddress)
        {
            bool IsValidEmail = false;
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"; 
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(emailAaddress))
            {
                IsValidEmail = true;
            }
            return IsValidEmail;
        }

        public void resetPassword(string email, string newPassword)
        {
            string[] checkfile = File.ReadAllLines(filepath);
            foreach (var item in checkfile)
            {
                string[] data = item.Split(',');
                if (email == data[4])
                {
                    data[7] = newPassword;
                }
            }
        }
    }
}