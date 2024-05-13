using CarRepair.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRepair
{
    /// <summary>
    /// Interaction logic for AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CarRepairContext carRepairContext = new CarRepairContext();
                User? user = carRepairContext.Users.FirstOrDefault(u => u.Login == Login.Text);
                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                }
                else if (user.Password != Password.Text)
                {
                    MessageBox.Show("Пароль не подходит!");
                }
                else
                {
                    MessageBox.Show($"Добро пожаловать {user.Name}");
                    User.ActiveUser = user;
                    MainWindow.Frame.Content = new ViewPage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
