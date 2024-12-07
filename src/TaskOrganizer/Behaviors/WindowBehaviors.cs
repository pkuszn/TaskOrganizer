using System.Windows;
using System.Windows.Input;

namespace TaskOrganizer.Behaviors;
public static class WindowBehaviors
{
    private const string ClosePropertyName = "Close";
    private const string HidePropertyName = "Hide";
    private const string FullPropertyName = "Full";
    private const string NormalPropertyName = "Normal";
    private const string MovePropertyName = "Move";
    public static readonly DependencyProperty CloseProperty =
        DependencyProperty.RegisterAttached(ClosePropertyName,
            typeof(bool),
            typeof(WindowBehaviors),
            new UIPropertyMetadata(false, OnClose));

    public static readonly DependencyProperty HideProperty =
        DependencyProperty.RegisterAttached(HidePropertyName,
            typeof(bool),
            typeof(WindowBehaviors),
            new UIPropertyMetadata(false, OnHide));


    public static readonly DependencyProperty FullProperty =
        DependencyProperty.RegisterAttached(FullPropertyName,
            typeof(bool),
            typeof(WindowBehaviors),
            new UIPropertyMetadata(false, OnFull));

    public static readonly DependencyProperty NormalProperty =
        DependencyProperty.RegisterAttached(NormalPropertyName,
            typeof(bool),
            typeof(WindowBehaviors),
            new UIPropertyMetadata(false, OnNormal));

    public static readonly DependencyProperty MoveProperty =
        DependencyProperty.RegisterAttached(MovePropertyName,
            typeof(bool),
            typeof(WindowBehaviors),
            new UIPropertyMetadata(false, OnMove));

    public static void SetClose(DependencyObject target, bool value)
    {
        target.SetValue(CloseProperty, value);
    }

    public static void SetHide(DependencyObject target, bool value)
    {
        target.SetValue(HideProperty, value);
    }

    public static void SetFull(DependencyObject target, bool value)
    {
        target.SetValue(FullProperty, value);
    }

    public static void SetNormal(DependencyObject target, bool value)
    {
        target.SetValue(NormalProperty, value);
    }

    public static void SetMove(DependencyObject target, bool value)
    {
        target.SetValue(MoveProperty, value);
    }

    private static void OnNormal(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool value && value)
        {
            Window window = GetWindow(sender);

            if (window != null)
            {
                window.WindowState = WindowState.Normal;
            }
        }
    }

    private static void OnFull(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool value && value)
        {
            Window window = GetWindow(sender);

            if (window != null)
            {
                window.WindowState = WindowState.Maximized;
            }
        }
    }

    private static void OnHide(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool value && value)
        {
            Window window = GetWindow(sender);

            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }
    }

    private static void OnClose(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool value && value)
        {
            Window window = GetWindow(sender);
            window?.Close();
        }
    }

    private static void OnMove(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool value && value)
        {
            Window window = GetWindow(sender);
            if (window != null)
            {
                window.MouseLeftButtonDown += Window_MouseLeftButtonDown;
            }
        }
        else
        {
            Window window = GetWindow(sender);
            if (window != null)
            {
                window.MouseLeftButtonDown -= Window_MouseLeftButtonDown;
            }
        }
    }

    private static Window GetWindow(DependencyObject sender)
    {
        Window window = null;
        if (sender is Window senderWindow)
        {
            window = senderWindow;
        }

        window ??= Window.GetWindow(sender);
        return window;
    }

    private static void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            Window window = sender as Window;
            window?.DragMove();
        }
    }
}
