using JournalForSchool.Database_Source;
using JournalForSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JournalForSchool.Components
{
    public partial class PasswordRecoveryWindow : Window
    {
        public MainWindow mainWindow;
        public User user;
        UnitOfWork unitOfWork;
      
        string login = "";
        string password = "";

        public PasswordRecoveryWindow(MainWindow _mainwindow, User u, string _login)
        {
            InitializeComponent();
            unitOfWork = UnitOfWork.GetInstance();

            mainWindow = _mainwindow;
            user = u;
            login = _login;
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }
 
        private void GetRandomPassword()
        {
            string ch = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] pass = new char[10];
            for (int i = 0; i < pass.Length; i++)
                pass[i] = ch[random.Next(ch.Length)];
            password = new string(pass);
        }

        private void UpdatePassword()
        {
            User user = unitOfWork.Users.GetUserByLogin(login);
            GetRandomPassword();
            user.Password = password;
            unitOfWork.Users.Update(user);
            unitOfWork.Save();
        }
        private void SendData()
        {

            MailAddress fromMailAddress = new MailAddress(Properties.Settings.Default.mail, "Электронный журнал");
            MailAddress toMailAddress = new MailAddress(mailTextBox.Text);

            using (MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress))
            using (SmtpClient smtpClient = new SmtpClient(Properties.Settings.Default.host, 587))
            {
                string messageForMail = "Здравствуйте, " + user.FirstName + " " + user.LastName + "! Данные для входа в систему 'Электронный журнал' обновлены. \nВаш новый пароль: " + password;
                mailMessage.Subject = "Восстановление пароля";
                mailMessage.Body = messageForMail;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new NetworkCredential(fromMailAddress.Address, Properties.Settings.Default.mail_password);
                smtpClient.Send(mailMessage);
            }

        }

        private bool MailValidation()
        {
            bool test = true;
            string regex = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            string mail = mailTextBox.Text;
            if (Regex.IsMatch(mail, regex))
            {
                test = true;
            }
            else
            {
                test = false;
            }
            return test;
        }

        private void Password_Click(object sender, RoutedEventArgs e)
        {
            if (IsConnectedToInternet())
            {
                if (MailValidation())
                {
                    UpdatePassword();
                    SendData();
                    MessageBox.Show("Ваш пароль сброшен! Новый пароль отправлен на почту!");
                }
                else
                {
                    MessageBox.Show("Неверный адрес электронный почты!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Проверьте подключение к Интернету!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите закрыть окно?", "Подвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                  
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
