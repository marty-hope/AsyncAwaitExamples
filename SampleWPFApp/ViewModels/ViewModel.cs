using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using SampleWPFApp.Annotations;
using SampleWPFApp.Commands;
using SampleWPFApp.Services;

namespace SampleWPFApp.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IPrimeNumberService _primeService;

        private static object LockObject = new Object();

        public ViewModel() : this(new AuthenticationService(), new PrimeNumberService())
        {
            //This allows the ObservableCollection to work within a threaded context.
            //Otherwise a dispatcher
            BindingOperations.EnableCollectionSynchronization(PrimeNumberCandidates, LockObject);
        }

        public ViewModel(IAuthenticationService authenticationService, IPrimeNumberService primeService)
        {
            _authenticationService = authenticationService;
            _primeService = primeService;
            AuthenticateCommand = new RelayCommand(
                async x =>
                {
                    IsAuthenticated = await (_authenticationService.AuthenticateUser(UserName, SecurePassword?.SecureStringToString()));
                    IsEnabled = IsAuthenticated;

                });

        }

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

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private IList<PrimeNumberCandidate> _candidates = new ObservableCollection<PrimeNumberCandidate>();
        public IList<PrimeNumberCandidate> PrimeNumberCandidates
        {
            get => _candidates;
            set
            {
                if (_candidates.Equals(value))
                {
                    _candidates = value;
                    OnPropertyChanged();
                }
            }

        }

     

        

        public ICommand AuthenticateCommand { get; set; }

        private ICommand _refreshPrimeNumbersCommand;

        public ICommand RefreshPrimeNumbersCommand
        {
            get
            {
                if (_refreshPrimeNumbersCommand == null)
                {
                    _refreshPrimeNumbersCommand = new RelayCommand(async e =>
                        {
                            await DeterminePrimeNumberCandidates();
                        });

                }

                return _refreshPrimeNumbersCommand;

            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task DeterminePrimeNumberCandidates()
        {
            PrimeNumberCandidates.Clear();
            IsEnabled = false;
            await Task.Run(async () =>
            {
                List<int> candidates = new List<int>();
                var numberOfCandidates = Math.Abs(GetRandomInteger()) % 23;
                for (int i = 0; i < numberOfCandidates; i++)
                {
                    var rnd = GetRandomInteger();
                    if(!candidates.Contains(rnd)) candidates.Add(rnd);
                }
                await _primeService.DeterminePrimeCandidates(PrimeNumberCandidates, candidates);

            });
            IsEnabled = true;
        }

        private int GetRandomInteger()
        {

            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                return Math.Abs(BitConverter.ToInt32(rno, 0));
            }
        }


    }
}
