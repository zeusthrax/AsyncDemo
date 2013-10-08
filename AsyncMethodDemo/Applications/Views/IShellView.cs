namespace AsyncMethodDemo.Applications.Views
{
    using System.Waf.Applications;

    internal interface IShellView : IView
    {
        #region Methods

        void Close();

        void Show();

        #endregion Methods
    }
}