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
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

using validation_t = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Onigaku
{
    /// <summary> RegularExpressioAttribute
    /// Логика взаимодействия для signupPage.xaml
    /// </summary>
    
    public class get_t
    {
        [Required(ErrorMessage = "1 Email cannot be empty(but i can)"), MinLength(6)]
        [EmailAddress(ErrorMessage = "1 Email doesn't meet policy requirements")]
        public string email;
        [Required(ErrorMessage = "2 Password cannot be empty(but i can)"), MinLength(6)]
        [RegularExpression("[.]", ErrorMessage = "2 password doesn't meet policy requirements")]
        public string pwd;
        [Required(ErrorMessage = "You must repeat password"), MinLength(6)]
        public string pwd_repeat;
        [Required(ErrorMessage = "3 Login cannot be empty(but my fridge, heart and soul can)"), MinLength(6)]
        [RegularExpression("[.]", ErrorMessage = "4 password doesn't meet policy requirements")]
        public string login;
    }


    public partial class signupPage : Page
    {
        public signupPage() 
        {
            InitializeComponent();
        }
        
        private void login(object sender, RoutedEventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipContent = new StackPanel();
           /* if(validate_string(this.loginBox.Text))
            {
                toolTipContent.Children.Add(new TextBlock { Text = "Логин не должен быть пустым!", FontSize = 20 });

                loginBox.BorderBrush = Brushes.Red;
                loginBox.Focus();
                loginBox.ToolTip = toolTip;
            } else {
                if (validate_string(this.pwBox.Password)) {                    
                    
                    toolTipContent.Children.Add(new TextBlock { Text = "Пароль не должен быть пустым!", FontSize = 20 });

                    pwBox.BorderBrush = Brushes.Red;
                    pwBox.Focus();
                    pwBox.ToolTip = toolTip;
                } else {
                    //login
                }
            }*/
        }

        private void signUp(object sender, RoutedEventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            StackPanel toolTipContent = new StackPanel();

            var x = new get_t
            {
                email = this.emailBox.Text,
                pwd = this.pwBox.Password,
                login = this.loginBox.Text
            };

            object obj = x as object;
            var ret = new List<validation_t>();
            var ctx = new ValidationContext(obj);
            if(!Validator.TryValidateObject(obj, ctx, ret, true))
            {
                toolTipContent = new StackPanel();
                foreach(var entry in ret)
                {
                    switch(entry.ErrorMessage[0])
                    {
                        case '1':
                            emailBox.BorderThickness = new Thickness(5,5,5,5);
                            emailBox.BorderBrush = Brushes.Red;
                            emailBox.Focus();
                            emailBox.ToolTip = toolTip;
                            break;
                        case '2':
                            loginBox.BorderThickness = new Thickness(5,5,5,5);
                            loginBox.BorderBrush = Brushes.Red;
                            loginBox.Focus();
                            loginBox.ToolTip = toolTip;
                            break;
                        case '3':
                            pwBox.BorderThickness = new Thickness(5,5,5,5);
                            pwBox.BorderBrush = Brushes.Red;
                            pwBox.Focus();
                            pwBox.ToolTip = toolTip;
                            break;
                        default: break;
                    }
                    toolTipContent.Children.Add(new TextBlock { Text = entry.ErrorMessage, FontSize = 20 });
                }
            } 
            else
            {
                if (repeatpwBox.Password != pwBox.Password)
                {
                    toolTipContent.Children.Add(new TextBlock { Text = "Пароли не совпадают!", FontSize = 20 });

                    repeatpwBox.BorderBrush = Brushes.Red;
                    repeatpwBox.Focus();
                    repeatpwBox.ToolTip = toolTip;
                }
                else
                {
                    
                    user userr = new user();
                    userr.email = emailBox.Text;
                    userr.username = loginBox.Text;
                    userr.password = pwBox.Password;
                    var db_ctx = MLS_DB.GetContext();
                    db_ctx.users.Add(userr);
                    db_ctx.SaveChanges();
                    MessageBox.Show("New user added");
                }
            }
        }

        private void emailBox_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                new MailAddress(this.emailBox.Text);
            }
            catch (Exception)
            {
                this.emailBox.Text = "";
            }
        }

        private void emailBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                new MailAddress(this.emailBox.Text);
            }
            catch(Exception)
            {
                this.emailBox.Text = "Электронная почта";
            }
        }

        private void signUp_MoveBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void loginBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.loginBox.Text.Length < 6)
            this.loginBox.Text = "";
        }

        private void loginBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.loginBox.Text.Length < 6)
            {
                this.loginBox.Text = "Логин";
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            pwBox.Visibility = Visibility.Hidden;
            pwBoxPlaceholder.Text = pwBox.Password;
            pwBoxPlaceholder.Visibility = Visibility.Visible;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            pwBoxPlaceholder.Visibility = Visibility.Hidden;
            pwBox.Password = pwBoxPlaceholder.Text;
            pwBox.Visibility = Visibility.Visible;
        }

        private void pwBoxPlaceholder_GotFocus(object sender, RoutedEventArgs e)
        {
            pwBoxPlaceholder.Text = "";
            pwBox.Focus();
        }

        private void pwBoxPlaceholder_LostFocus(object sender, RoutedEventArgs e)
        {
            pwBoxPlaceholder.Text = "Пароль";
        }
    }
}
