using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatApp.WPF.Client
{
    public class Validator //TODO
    {
        MainWindowViewModel _mainWindowViewModel;

        public Validator(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public bool Validate()
        {
            List<string> passwords = new List<string>();
            int count = VisualTreeHelper.GetChildrenCount(App.Current.MainWindow);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(App.Current.MainWindow, i);

                if (current.GetType().Equals(typeof(PasswordBox)))
                {
                    PasswordBox passwordBox = current as PasswordBox;
                    passwords.Add(passwordBox.Password);
                }
            }

            if (passwords[0] == passwords[2])
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
