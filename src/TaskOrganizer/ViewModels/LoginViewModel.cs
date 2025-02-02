using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;
using System.Net;
using System.Windows.Input;
using TaskOrganizer.Commands;
using TaskOrganizer.Helpers;
using TaskOrganizer.Repository.Dtos;
using TaskOrganizer.Repository.Interfaces;
using TaskOrganizer.View.Windows;

namespace TaskOrganizer.ViewModels;
public class LoginViewModel : BaseViewModel
{
    private readonly IUserService UserService;
    private readonly ILogger Logger;
    private string _username;
    private string _password;
    private string _errorMessage;
    private bool _isPasswordIncorrect = false;
    private bool _isLoginAttempted = false;
    public string Username
    {
        get => _username; 
        set { _username = value; }
    }
    public string Password
    {
        get => _password;
        set { _password = value;}
    }
    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
    }

    public bool IsPasswordIncorrect
    {
        get => _isPasswordIncorrect;
        set
        {
            if (_isPasswordIncorrect != value)
            {
                _isPasswordIncorrect = value;
                OnPropertyChanged(nameof(IsPasswordIncorrect));
            }
        }
    }

    public bool IsLoginAttempted
    {
        get => _isLoginAttempted;
        set
        {
            if (_isLoginAttempted != value)
            {
                _isLoginAttempted = value;
                OnPropertyChanged(nameof(IsLoginAttempted));
            }
        }
    }

    public ICommand LoginCommand { get; }
    public ICommand ResetAccountCommand { get; }
    public ICommand CreateAccountCommand { get; }
    public ICommand ShowPasswordCommand { get; }
    public LoginViewModel(IUserService userService, ILogger logger)
    {
        LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand) ?? throw new NullReferenceException();
        ResetAccountCommand = new RelayCommand(ExecuteResetAccountCommand) ?? throw new NullReferenceException();
        CreateAccountCommand = new RelayCommand(ExecuteCreateAccountCommand) ?? throw new NullReferenceException();
        ShowPasswordCommand = new RelayCommand(ExecuteShowPasswordCommand) ?? throw new NullReferenceException();
        UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        Logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    private void ExecuteShowPasswordCommand(object obj)
    {
        throw new NotImplementedException();
    }

    private void ExecuteCreateAccountCommand(object obj)
    {
        throw new NotImplementedException();
    }

    private void ExecuteResetAccountCommand(object obj)
    {
        throw new NotImplementedException();
    }

    private async void ExecuteLoginCommand(object obj)
    {
        Logger.Information("Authentication started...");
        IsLoginAttempted = true;
        NetworkCredential credentials = new()
        {
            Domain = "test",
            UserName = _username,
            Password = _password
        };

        AuthResult userAuthResult = await UserService.AuthenticateUserAsync(credentials);
        if (userAuthResult.IsAuthenticated)
        {
            Logger.Information($"{_username} is authenticated successfully.");

            MainWindow mainWindow = App.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = App.Services.GetRequiredService<MainViewModel>();
            mainWindow.Show();

            ApplicationContext.IdUser = (int)userAuthResult.IdUser;
            App.Current.Windows.OfType<LoginWindow>().FirstOrDefault()?.Close();
            IsPasswordIncorrect = false;
        }
        else if (userAuthResult.Result == Repository.Consts.Enums.AuthResultEnum.InvalidUsername)
        {
            Logger.Warning("Authentication failed. User not found. Please check your credentials.");
            IsPasswordIncorrect = true;
        }
        else if (userAuthResult.Result == Repository.Consts.Enums.AuthResultEnum.InvalidPassword)
        {
            Logger.Warning("Authentication failed. Password is invalid. Please check your credentials.");
            IsPasswordIncorrect = true;
        }
        else
        {
            throw new Exception("Unexpected");
            //TODO
        }
    }

    private bool CanExecuteLoginCommand(object obj)
    {
        bool isInvalid = CredentialValidationPredicate();
        Logger.Information(isInvalid
            ? $"{Username} credentials are valid"
            : $"{Username} credentials are invalid");

        return isInvalid;
    }

    private bool CredentialValidationPredicate()
    {
        return !string.IsNullOrWhiteSpace(Username) && Username.Length > 3
            && !string.IsNullOrWhiteSpace(Password) && Password.Length >= 3;
    }
}
