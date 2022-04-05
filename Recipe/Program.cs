#nullable enable

namespace Recipe
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Recipe.BusinessRules;
    using Recipe.Controller;
    using Recipe.Models.Db;
    using Recipe.Presenter;
    using Recipe.Views;
    using System;
    using System.Configuration;
    using System.Threading;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //Application.ThreadException += ApplicationOnThreadException;
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;

            _ = Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ServiceCollection services = new();
            ConfigureServices(services);

            var resp = DialogResult.Yes; // MessageBox.Show("Gå til Test Form?", "Test Mode Decision", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            try
            {
                object obj;
                switch (resp)
                {
                    // Only shown on testing
                    case DialogResult.Yes:
                        obj = serviceProvider.GetService(typeof(TestForm));
                        if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(TestForm), "Not defined by ServiceProvider - call IT development");
                        Application.Run((Form)obj);
                        break;
                    default:
                        obj = serviceProvider.GetService(typeof(PresenterBaseNoParent<IMainFormView, IMainformPresenter>));
                        if (obj == null) throw new ArgumentOutOfRangeException("type", nameof(IMainformPresenter), "Not defined by ServiceProvider - call IT development");
                        ((IMainformPresenter)obj).StartApplication();
                        break;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                _ = MessageBox.Show(e.Message + "\r\n" + e.ParamName, "Error loading form", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0058:Expression value is never used", Justification = "<Chain>")]
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<TestForm>()
                .AddScoped<RoundedForm>()
                .AddDbContext<RecipeDbContext>(options => options.UseSqlServer(ConfigurationManager.ConnectionStrings["RecipeDatabase"].ConnectionString), ServiceLifetime.Transient);
            services.AddSingleton<IValidateEntities<Customer>, CustomerModelValidation>()
                .AddSingleton<IMainFormView, MainForm>();
            services.AddTransient<ICustomerController, CustomerController>()
                .AddTransient<ICustomerNameSearchController, CustomerNameSearchController>()
                .AddTransient<ICustomerPhoneSearchController, CustomerPhoneSearchController>()
                .AddTransient<ICommonEntitiesController, CommonEntitiesController>()
                .AddTransient<ICustomerHistoryController, CustomerHistoryController>()
                .AddTransient<IRecipeLineController, RecipeLineController>()
                .AddTransient<ICustomerMainFormView, CustomerMainForm>()
                .AddTransient<IMainformPresenter, MainformPresenter>()
                .AddTransient<ICustomerMainFormPresenter, CustomerMainFormPresenter>()
                .AddTransient<PresenterBaseNoParent<IMainFormView, IMainformPresenter>, MainformPresenter>()
                .AddTransient<PresenterBase<ICustomerMainFormView, ICustomerMainFormPresenter, IMainformPresenter>, CustomerMainFormPresenter>()
                .AddTransient<ICustomerFormSearchPartPresenter, CustomerFormSearchPartPresenter>()
                .AddTransient<ICustomerFormDetailPartPresenter, CustomerFormDetailPartPresenter>()
                .AddTransient<ICustomerFormDetailPartView, CustomerFormDetailedPart>()
                .AddTransient<ICustomerFormSearchPartView, CustomerFormSearchPart>()
                .AddTransient<ICustomerFormHistoryPartPresenter, CustomerFormHistoryPartPresenter>()
                .AddTransient<ICustomerFormHistoryPartView, CustomerFormHistoryPart>();

        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message = "Sorry, something went wrong.\r\n" +
                $"{((Exception)e.ExceptionObject).Message}\r\n" +
                "Please contact support.";

            Console.WriteLine("ERROR {0}: {1}",
                DateTimeOffset.Now, e.ExceptionObject);

            _ = MessageBox.Show(message, "Unexpected Error");

        }

        private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string message = "Sorry, something went wrong.\r\n" +
              $"{e.Exception.Message}\r\n" +
              "Please contact support.";

            Console.WriteLine("ERROR {0}: {1}",
                DateTimeOffset.Now, e.Exception);

            _ = MessageBox.Show(message, "Unexpected Error");
        }
    }
}
