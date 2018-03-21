using System;
using MyInventoryApp.Services;
using System.Text.RegularExpressions;

using ReactiveUI;
using Splat;

namespace MyInventoryApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

        public LoginViewModel(ILoginService loginService, IScreen screen = null)
            : base(screen)
        {
            _loginService = loginService;

            Init();
        }

        private string _username;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ReactiveCommand LoginCommand { get; private set; }

        private ObservableAsPropertyHelper<bool> _validForm;
        public bool ValidForm
        {
            get { return _validForm?.Value?? false; }
        }

        private void Init()
        {
            this.WhenAnyValue(x => x.Username, x => x.Password,
                (email, password) =>
                (
                    ///Validate the password
                    !string.IsNullOrEmpty(password) && password.Length > 5
                )
                &&
                (
                    ///Validate teh email.
                    !string.IsNullOrEmpty(email)
                            &&
                     Regex.Matches(email, "^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$").Count == 1
                ))
                .ToProperty(this, v => v.ValidForm, out _validForm);
            
            LoginCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var authenticated = await _loginService.Authenticate(Username, Password);

                if(authenticated){
                    HostScreen.Router
                              .Navigate
                              .Execute(new ItemsViewModel(Locator.CurrentMutable.GetService<IItemService>()))
                              .Subscribe();
                }
            }, this.WhenAnyValue(x => x.ValidForm, x => x.ValidForm, (validLogin, valid) => ValidForm && valid));
        }
    }
}
