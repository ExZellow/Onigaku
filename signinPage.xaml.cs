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
using AutoCompleteTextBox.Editors;
using System.ComponentModel.DataAnnotations;

using validation_t = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Onigaku
{
    public partial class signinPage : Page
    {
        public signinPage()
        {
            InitializeComponent();
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new signupPage());
        }

        private void signIn_Click(object sender, RoutedEventArgs e)
        {
            MLS_DB db_ctx = MLS_DB.GetContext();
            foreach (var curr_user in db_ctx.users.ToList()) {
                if (curr_user.username == loginBox.Text)
                {
                    if (curr_user.password == pwBox.Password)
                    {
                        MessageBox.Show("Successfully pseudo-logged in");
                        NavigationService.Navigate(new mainPage());
                        break;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid login or password!");
                    break;
                }
            }
        }
        private void logIn_MoveBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
