namespace AsyncMethodDemo.Presentation.Views
{
    using System.ComponentModel.Composition;
    using System.Windows;

    using AsyncMethodDemo.Applications.Views;

    [Export(typeof(IShellView))]
    public partial class ShellWindow : Window, IShellView
    {
        #region Constructors

        public ShellWindow()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
        }

        #endregion Methods
    }
}