namespace AsyncMethodDemo
{
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Reflection;
    using System.Waf.Foundation;
    using System.Windows;

    using AsyncMethodDemo.Applications.Controllers;
    using AsyncMethodDemo.Applications.ViewModels;
    using AsyncMethodDemo.Presentation.Views;

    public partial class App : Application
    {
        #region Fields

        private AggregateCatalog catalog;
        private CompositionContainer container;
        private ApplicationController controller;

        #endregion Fields

        #region Methods

        protected override void OnExit(ExitEventArgs e)
        {
            controller.Shutdown();
            container.Dispose();
            catalog.Dispose();

            base.OnExit(e);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            catalog = new AggregateCatalog();
            // Add the WpfApplicationFramework assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Model).Assembly));
            // Add the WafApplication assembly to the catalog
            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));

            container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
            CompositionBatch batch = new CompositionBatch();
            batch.AddExportedValue(container);
            container.Compose(batch);
            //container.GetExportedValue<ApplicationController>();
            //controller = new ApplicationController(new AsyncShellViewModel(new ShellWindow()));
            //controller = new ApplicationController(new TaskShellViewModel(new ShellWindow()));
            controller = new ApplicationController(new NoThreadShellViewModel(new ShellWindow()));
            controller.Initialize();
            controller.Run();
        }

        #endregion Methods
    }
}