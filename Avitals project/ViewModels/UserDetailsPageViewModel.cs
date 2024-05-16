using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avitals_project.Models;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using Avitals_project.Services;

namespace Avitals_project.ViewModels
{
    public class UserDetailsPageViewModel:MediaMethodsViewModel
    {
        User currentuser ;
        private User updateduser=new User();
        #region private fields
        private string firstname;
        private string lastname;
        private string username;
        private string password;
        private string profilepicture;
        private string errormessageupdate;
        private static FileResult currentfile = null;

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
        public string Firstname { get { return firstname; } set { if (firstname != value) { firstname = value; OnPropertyChange(); if (ValidateName(firstname)) { ShowFNameError = false; updateduser.Firstname = firstname; } else { ShowFNameError = true; } } } }

        public string Lastname { get { return lastname; } set { if (lastname != value) { lastname = value; OnPropertyChange(); if (ValidateName(lastname)) { ShowLNameError = false; updateduser.Lastname = lastname; } else { ShowLNameError = true; } } } }

        public string Username { get { return username; } set { if (username != value) { username = value; OnPropertyChange(); updateduser.UserName = username;  } } }

        public string Password { get { return password; } set { if (password != value) { password = value; OnPropertyChange(); updateduser.Passwrd = password; } } }
        
        public string ProfilePicture { get { return profilepicture; } set { if (profilepicture != value) { profilepicture = value; OnPropertyChange(); updateduser.ProfilePicture = profilepicture; } } }

      

        public string ErrorMessage { get { return errormessage; } }
        public bool ShowErrorMessage { get { return showerrormessage; } set { if (showerrormessage != value) { showerrormessage = value; OnPropertyChange(); } } }
        public string NameErrorMessage { get { return nameerrormessage; } }
        public string ErrorMessageUpdate { get => errormessageupdate; set { if (errormessageupdate != value) { errormessageupdate = value; OnPropertyChange(); } } }


        public bool ShowFNameError { get { return showfnameerror; } set { if (showfnameerror != value) { showfnameerror = value; OnPropertyChange(); } } }
        public bool ShowLNameError { get { return showlnameerror; } set { if (showlnameerror != value) { showlnameerror = value; OnPropertyChange(); } } }

        public ICommand UpdateUserCommand { get; set; }
        public ICommand UpdateProfilePicture { get; set; }

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
        public async void CapturePhoto()
        {
            FileResult photo = new FileResult("a");
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {

                    photo = await MediaPicker.Default.CapturePhotoAsync();

                    if (photo.FullPath != "")
                    {

                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                        currentfile = photo;
                        ProfilePicture = localFilePath;
                        IsOpen = false;
                        
                    }
                }


            }
         );
        }

        private async void ChooseFromGallery1()
        {
            FileResult photo = new FileResult("a");
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {

                    photo = await MediaPicker.Default.PickPhotoAsync();
                    if (photo != null)
                    {
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                        ProfilePicture = localFilePath;
                        IsOpen = false;
                        currentfile = photo;

                    }
                }
            });
        }

            #endregion
            public UserDetailsPageViewModel(UserService service): base(service)
        {
            IsOpen= false;  
            currentuser = ((AppShell)Shell.Current).user;
            if (currentuser != null)
            {
                Firstname=currentuser.Firstname; 
                Lastname=currentuser.Lastname;
                Username=currentuser.UserName;
                Password = currentuser.Passwrd;
            }
            Cover = "emptyprofilepicture.jpg";
            UpdateProfilePicture = new Command(async () =>
            {
                try
                {

                }

                catch
                {

                }

            });
            UpdateUserCommand = new Command(async ()  =>
            {
                try
                {
                    if (updateduser != null)
                    {
                        updateduser.Id = currentuser.Id;    
                        var user = await service.UpdateUserAsync(updateduser);
                        if (user is UserDto)
                        {
                            ErrorMessageUpdate = ((UserDto)user).Message;
                        }
                        else 
                        {
                            ((AppShell)Shell.Current).user=user;
                            await SecureStorage.Default.SetAsync("user", JsonSerializer.Serialize(user));
                            await Shell.Current.DisplayAlert("הצלחתי", "התחברתי", "אישור");
                        }
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
