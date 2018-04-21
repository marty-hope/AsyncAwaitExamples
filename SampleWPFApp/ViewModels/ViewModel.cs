using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Input;
using SampleWPFApp.Annotations;
using SampleWPFApp.Commands;
using SampleWPFApp.Services;

namespace SampleWPFApp.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private IAuthenticationService _authenticationService;

        public ViewModel() :this(new AuthenticationService())
        {}

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }


        public SecureString SecurePassword { private get; set; }


        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set
            {
                if (_isAuthenticated != value)
                {
                    _isAuthenticated = value;
                    OnPropertyChanged();
                }
            }
        }

        public ViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            AuthenticateCommand = new RelayCommand(
                async x =>
                {
                    IsAuthenticated = await (_authenticationService.AuthenticateUser(UserName, SecurePassword?.SecureStringToString()));
                });
            //AuthenticateCommand = new RelayCommand(
            //    x =>
            //    {
            //        Thread.Sleep(5000);
            //        IsAuthenticated = true;
            //    });
        }

        public ICommand AuthenticateCommand { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
