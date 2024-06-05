
using Avitals_project.Models;
using Avitals_project.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Avitals_project.ViewModels
{
    public class RegisterPageViewModel : ViewModel
    {
        #region Private fields
        private string firstname;
        private string lastname;
        private string username;
        private string password;
        private string email;
        private DateTime birthday;


        #region error messages
        private bool showfnameerror;
        private bool showlnameerror;
        private const string nameerrormessage = "Must begin with a capital letter";
        private bool showbdayerror = false;
        private const string bdayerrormessage = "Invalid date";
        private bool showerrormessage = false;
        private const string errormessage = "Registeration failed";
        #endregion

        #endregion

        #region Properties
        public string ErrorMessage { get { return errormessage; } }
        public bool ShowErrorMessage { get { return showerrormessage; } set { if (showerrormessage != value) { showerrormessage = value; OnPropertyChange(); } } }
        public string NameErrorMessage { get { return nameerrormessage; } }
       
        public bool ShowFNameError { get { return showfnameerror; } set { if (showfnameerror != value) { showfnameerror = value; OnPropertyChange(); } } }
        public bool ShowLNameError { get { return showlnameerror; } set { if (showlnameerror != value) { showlnameerror = value; OnPropertyChange(); } } }
        public bool ShowBdayError { get { return showbdayerror; } set { if (showbdayerror != value) { showbdayerror = value; OnPropertyChange(); } } }

        public string Firstname { get { return firstname; } set { if (firstname != value ) {  firstname = value; OnPropertyChange(); if (ValidateName(firstname)) { ShowFNameError = false; } else { ShowFNameError = true; } } } } 

        public string Lastname { get { return lastname; } set  { if (lastname != value) { lastname = value; OnPropertyChange(); if (ValidateName(lastname)) { ShowLNameError = false; } else { ShowLNameError = true; } } } }           
          
        public string Username { get { return username; } set { if (username != value){ username = value; OnPropertyChange(); } } }

        public string Password { get { return password; } set { if (password != value ) { password = value; OnPropertyChange(); } } }

        public string Email { get { return email; } set { if (email!=value && ValidateEmail(email)) { email = value; OnPropertyChange(); } } }

        public ICommand RegisterCommand { get; set; }
        #endregion

        #region Validation methods
        public bool ValidateName(string name)
        {
            if (name == null)
                name = "";
            return name != "" && (!(name[0] < 'A' || name[0] > 'Z'));
                
            
        }
        public bool ValidateUserName(string username)
        {
            return username != null && username.Length >= 3; 
        }
        public bool ValidatePassword(string password)
        {
            return password != null && password.Length >= 6;
        }

        public bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        //public bool ValidateBirthday (string date)
        //{
        //    try
        //    {
        //       DateOnly d= DateOnly.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
        //        return true;
        //    }
        //    catch (FormatException )
        //    {
        //        showbdayerror = true;
        //        return false;
        //    }
        //}
        #endregion

        public RegisterPageViewModel(UserService service)
        {
            RegisterCommand = new Command(async () =>
            {
                try
                {
                    var response = await service.RegisterAsync(new User() {  Email = email, Firstname = firstname, Lastname = lastname, Passwrd = password, UserName = username });
                    if (response == true)
                    {
                        await AppShell.Current.GoToAsync("Login");
                       
                    }
                    else
                    {
                        
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }); 
        }
        
    }
}
