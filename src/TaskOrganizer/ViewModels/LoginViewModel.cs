using Serilog;
using System;
using System.Net;
using System.Windows.Input;
using TaskOrganizer.Commands;
using TaskOrganizer.Repository.Interfaces;

namespace TaskOrganizer.ViewModels;
public class LoginViewModel : BaseViewModel
{
    private readonly IUserService UserService;
    private readonly ILogger Logger;
    private string _username;
    private string _password;
    private string _errorMessage;
    private bool _isViewVisible = true;
    public string Username
    {
        get => _username; 
        set { _username = value; OnPropertyChanged(nameof(Username)); }
    }
    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(nameof(Password)); }
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
    }
    public bool IsViewVisible
    {
        get => _isViewVisible;
        set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
    }
    public ICommand LoginCommand { get; }
    public ICommand ResetAccoundCommand { get; }
    public ICommand CreateAccountCommand { get; }
    public ICommand ShowPasswordCommand { get; }
    public LoginViewModel(IUserService userService, ILogger logger)
    {
        LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        ResetAccoundCommand = new RelayCommand(ExecuteResetAccoundCommand);
        CreateAccountCommand = new RelayCommand(ExecuteCreateAccountCommand);
        ShowPasswordCommand = new RelayCommand(ExecuteShowPasswordCommand);
        UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        Logger.Information("Registering login view..."); //temp
    }

    private void ExecuteShowPasswordCommand(object obj)
    {
        throw new NotImplementedException();
    }

    private void ExecuteCreateAccountCommand(object obj)
    {
        throw new NotImplementedException();
    }

    private void ExecuteResetAccoundCommand(object obj)
    {
        throw new NotImplementedException();
    }

    private async void ExecuteLoginCommand(object obj)
    {
        Logger.Information("Authentication started...");
        bool isAuthenticated = await UserService.AuthenticateUserAsync(new NetworkCredential
        {
            Domain="test",
            UserName=_username,
            Password=_password
        }, new System.Threading.CancellationToken());

        if (isAuthenticated)
        {
            Logger.Information($"{_username} is authenticated");
        }
        else 
        {
            Logger.Information("Authentication failed...");
        }
    }

    private bool CanExecuteLoginCommand(object obj)
    {
        Logger.Information($"{nameof(_password)}: {_password}");
        if (string.IsNullOrEmpty(Username) || Username.Length <= 3 || string.IsNullOrEmpty(Password) || Password.Length < 3)
        {
            Logger.Information($"{_username} credentials are invalid");
            return false;
        }
        Logger.Information($"{_username} credentials are valid");
        return true;
    }
}
