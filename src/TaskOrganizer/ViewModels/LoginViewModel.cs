﻿using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;
using System.Net;
using System.Windows.Input;
using TaskOrganizer.Commands;
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
        bool isAuthenticated = await UserService.AuthenticateUserAsync(new NetworkCredential
        {
            Domain="test",
            UserName=_username,
            Password=_password
        }, new System.Threading.CancellationToken());

        if (isAuthenticated)
        {
            Logger.Information($"{_username} is authenticated");
            MainWindow mainWindow = App.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext= App.Services.GetRequiredService<MainViewModel>();
            mainWindow.Show();

            LoginWindow loginWindow = App.Current.Windows.OfType<LoginWindow>().FirstOrDefault();
            loginWindow?.Close();
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
