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
    
    public class get_t
    {
        [Required(ErrorMessage = "1: Email cannot be empty(but i can)."), MinLength(6)]
        [EmailAddress(ErrorMessage = "1: Email doesn't meet policy requirements.")]
        public string email { get; set; }
        
        [Required(ErrorMessage = "2: Login cannot be empty(but my fridge, heart and soul can)."), MinLength(6)]
        [RegularExpression(@"^[a-zA-Z]\w*$", ErrorMessage = "2: Login doesn't meet policy requirements.")]
        public string login { get; set; }
        
        [Required(ErrorMessage = "3: Password cannot be empty(but i can)."), MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "3: Password doesn't meet policy requirements.")]
        public string pwd { get; set; }

        [Required(ErrorMessage = "4: You must repeat password."), MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "4: Repeated password doesn't meet policy requirements.")]
        [Compare(nameof(pwd), ErrorMessage = "4: Passwords don't match.")]
        public string pwd_repeat { get; set; }
    }


    public partial class signupPage : Page
    {
        public signupPage() 
        {
            InitializeComponent();
        }

        private void signUp(object sender, RoutedEventArgs e)
        {

            List<AutoCompleteTextBox.Editors.AutoCompleteTextBox> tb_list =
                new List<AutoCompleteTextBox.Editors.AutoCompleteTextBox>() { emailBox, loginBox };
            foreach (var tb in tb_list)
            {
                tb.BorderBrush = Brushes.White;
                tb.BorderThickness = new Thickness(2);
                tb.ToolTip = null;
            }

            List<PasswordBox> pb_list = new List<PasswordBox>() { pwBox, repeatpwBox };
            foreach (var pb in pb_list)
            {
                pb.BorderBrush = Brushes.White;
                pb.BorderThickness = new Thickness(2);
                pb.ToolTip = null;
            }

            var x = new get_t
            {
                email = emailBox.Text,
                login = loginBox.Text,
                pwd = pwBox.Password,
                pwd_repeat = repeatpwBox.Password
            };

            object obj = x as object;
            var ret = new List<validation_t>();
            var ctx = new ValidationContext(obj);
            
            

            if (!Validator.TryValidateObject(obj, ctx, ret, true))
            {
                StackPanel toolTipContent;
                foreach (var entry in ret)
                {
                    toolTipContent = new StackPanel();
                    switch(entry.ErrorMessage[0])
                    {
                        case '1':
                            emailBox.BorderThickness = new Thickness(4);
                            emailBox.BorderBrush = Brushes.Red;
                            emailBox.Focus();
                            emailBox.ToolTip = toolTipContent;
                            break;
                        case '2':
                            loginBox.BorderThickness = new Thickness(4);
                            loginBox.BorderBrush = Brushes.Red;
                            loginBox.Focus();
                            loginBox.ToolTip = toolTipContent;
                            break;
                        case '3':
                            pwBox.BorderThickness = new Thickness(4);
                            pwBox.BorderBrush = Brushes.Red;
                            pwBox.Focus();
                            pwBox.ToolTip = toolTipContent;
                            break;
                        case '4':
                            repeatpwBox.BorderThickness = new Thickness(4);
                            repeatpwBox.BorderBrush = Brushes.Red;
                            repeatpwBox.Focus();
                            repeatpwBox.ToolTip = toolTipContent;
                            break;

                        default: break;
                    }
                    toolTipContent.Children.Add(new TextBlock { Text = entry.ErrorMessage, FontSize = 20 });
                }
            } 
            else
            {
                user userr = new user();
                userr.email = emailBox.Text;
                userr.username = loginBox.Text;
                userr.password = pwBox.Password;
                userr.access_level = 1;
                var db_ctx = MLS_DB.GetContext();
                db_ctx.users.Add(userr);
                db_ctx.SaveChanges();
                MessageBox.Show("New user added");
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
                this.emailBox.Watermark = "";
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
                this.emailBox.Watermark = "Электронная почта";
            }
        }

        private void signUp_MoveBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            pwBoxPlaceholder.Visibility = Visibility.Visible;
            pwBox.Visibility = Visibility.Hidden;
            pwBoxPlaceholder.Text = pwBox.Password;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            pwBox.Visibility = Visibility.Visible;
            pwBoxPlaceholder.Visibility = Visibility.Hidden;
        }
    }
}
