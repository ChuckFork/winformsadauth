using Microsoft.Identity.Client;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace AuthDemoWinForms
{
    static class Program
    {

        public static string ClientId = "";
        public static string TenantId= "";
        private static IPublicClientApplication clientApp;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeAuth();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        public static IPublicClientApplication PublicClientApp { get { return clientApp; } }

        private static void InitializeAuth()
        {
            ClientId= ConfigurationManager.AppSettings[nameof(ClientId)];
            TenantId= ConfigurationManager.AppSettings[nameof(TenantId)];

            clientApp = PublicClientApplicationBuilder.Create(ClientId)
                    .WithRedirectUri("https://login.microsoftonline.com/common/oauth2/nativeclient")
                    .WithAuthority(AzureCloudInstance.AzurePublic, TenantId)
                    .Build();

            TokenCacheHelper.EnableSerialization(clientApp.UserTokenCache);
        }
    }
}