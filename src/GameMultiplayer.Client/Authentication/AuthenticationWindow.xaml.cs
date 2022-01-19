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
using System.Windows.Shapes;

namespace GameMultiplayer.Client.Authentication
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window, IConnectionData
    {
        public string Url => txtUrl.Text;

        public string Password => txtPassword.Password;

        public AuthenticationWindow()
        {
            InitializeComponent();
        }

        public static IConnectionData Authenticate()
        {
            AuthenticationWindow window = new AuthenticationWindow();
            if (window.ShowDialog() != true)
                return null;

            return window;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtUrl.Text = string.Empty;
            txtPassword.Password = string.Empty;
            this.DialogResult = false;
            this.Close();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void txtUrl_KeyUp(object sender, KeyEventArgs e)
        {
            Start.IsEnabled = !string.IsNullOrEmpty(txtUrl.Text) && !string.IsNullOrEmpty(txtPassword.Password);
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            Start.IsEnabled = !string.IsNullOrEmpty(txtUrl.Text) && !string.IsNullOrEmpty(txtPassword.Password);
        }
    }
}
