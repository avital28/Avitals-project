
namespace Avitals_project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt7QHFqUUdrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRbQlxiSX9Uc0ZhWXpacHE=;MzAzMDQwNUAzMjM0MmUzMDJlMzBUWjI1aklxbWdhTVdGa3F6Nk8vem9JTVE2UU1hNU8vWit6ZHUzamZpenVZPQ==;Mgo+DSMBMAY9C3t2UVhhQlVFfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hTX5Sd0ViXnpdcnRTRmRc;MzAzMDQwN0AzMjM0MmUzMDJlMzBUWjI1aklxbWdhTVdGa3F6Nk8vem9JTVE2UU1hNU8vWit6ZHUzamZpenVZPQ==");
            MainPage = new AppShell();
        }
    }
}