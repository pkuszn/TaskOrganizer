using System.Windows.Controls;
using System.Windows;

namespace TaskOrganizer.Behaviors;
public static class BoxBehavior
{
    public static readonly DependencyProperty BoundPasswordProperty =
        DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(BoxBehavior),
        new PropertyMetadata(string.Empty, OnBoundPasswordChanged));
    public static void SetBoundPassword(DependencyObject d, string value)
    {
        d.SetValue(BoundPasswordProperty, value);
    }

    public static string GetBoundPassword(DependencyObject d)
    {
        return (string)d.GetValue(BoundPasswordProperty);
    }

    private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PasswordBox passwordBox)
        {
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if (e.NewValue is string newPassword && passwordBox.Password != newPassword)
            {
                passwordBox.Password = newPassword;
            }

            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }
    }
    private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is PasswordBox passwordBox)
        {
            string currentPassword = GetBoundPassword(passwordBox);
            string actualPassword = passwordBox.Password;

            if (currentPassword != actualPassword)
            {
                SetBoundPassword(passwordBox, actualPassword);
            }
        }
    }
}