namespace AsyncMethodDemo.Applications.Controllers
{
    using System.ComponentModel.Composition;

    using AsyncMethodDemo.Applications.ViewModels;

    [Export]
    internal class ApplicationController
    {
        #region Fields

        private readonly IShellViewModel shellViewModel;

        #endregion Fields

        #region Constructors

        [ImportingConstructor]
        public ApplicationController(IShellViewModel shellViewModel)
        {
            this.shellViewModel = shellViewModel;
        }

        #endregion Constructors

        #region Methods

        public void Initialize()
        {
        }

        public void Run()
        {
            shellViewModel.Show();
        }

        public void Shutdown()
        {
        }

        #endregion Methods
    }
}