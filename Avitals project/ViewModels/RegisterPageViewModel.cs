using Avitals_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
        private bool shownameerror=false;
        private const string nameerrormessage = "Must begin with a capital letter";
        private bool showbdayerror = false;
        private const string bdayerrormessage = "Invalid date";
        #endregion

        #endregion

        #region Properties
        public string NameErrorMessage { get { return nameerrormessage; } }
       
        public bool ShowNameError { get { return shownameerror; } set { if (shownameerror != value) { shownameerror = value; OnPropertyChange(); } } }

        public string BdayErrorMessage { get { return bdayerrormessage; } }

        public bool ShowBdayError { get { return showbdayerror; } set { if (showbdayerror != value) { showbdayerror = value; OnPropertyChange(); } } }

        public string Firstname { get { return firstname; } set { if (firstname != value && ValidateName(firstname)) { firstname = value; OnPropertyChange(); } } } 

        public string Lastname { get { return lastname; } set  { if (lastname != value && ValidateName(lastname)) { lastname = value; OnPropertyChange(); } } }           
          
        public string Username { get { return username; } set { if (username != value&&ValidateUserName(username)){ username = value; OnPropertyChange(); } } }

        public string Password { get { return password; } set { if (password != value && ValidatePassword(password)) { password = value; OnPropertyChange(); } } }

        public string Email { get { return email; } set { if (email!=value && ValidateEmail(email)) { email = value; OnPropertyChange(); } } }

        public DateTime Birthday { get { return birthday; } set { if (birthday != value && ValidateBirthday(birthday.ToString())){ birthday = value; OnPropertyChange(); } } }
        #endregion

        #region Validation methods
        public bool ValidateName(string name)
        {
            if (name != null)
            {
                if (name[0] < 'A' || name[0] > 'Z')
                {
                    shownameerror = true;
                    return false;
                }
            }
            return true;
        }
        public bool ValidateUserName(string username)
        {
            return username != null && username.Length >= 3; // להוסיף תנאי אם השם משתמש לא תפוס
        }
        public bool ValidatePassword(string password)
        {
            return password != null && password.Length >= 6;
        }

        public bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        public bool ValidateBirthday (string date)
        {
            try
            {
               DateTime d= DateTime.ParseExact(date, "d/M/yyyy", CultureInfo.InvariantCulture);
                return true;
            }
            catch (FormatException )
            {
                showbdayerror = true;
                return false;
            }
        }
        #endregion
        
    }
}
