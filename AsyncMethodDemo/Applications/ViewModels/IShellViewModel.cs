using System;

namespace AsyncMethodDemo.Applications.ViewModels
{
    interface IShellViewModel
    {
        #region Properties

        string CurrentTime
        {
            get; set;
        }

        System.Windows.Input.ICommand ExitCommand
        {
            get;
        }

        DemoDomainObjects.ObservableSortedList<DemoDomainObjects.FileInformation> Files
        {
            get; set;
        }

        System.Windows.Input.ICommand IncrementCommand
        {
            get;
        }

        string LabelText
        {
            get;
        }

        System.Windows.Input.ICommand StartExecutionCommand
        {
            get;
        }

        string Title
        {
            get;
        }

        int TotalFiles
        {
            get;
        }

        System.Windows.Input.Cursor WindowCursor
        {
            get;
        }

        #endregion Properties

        #region Methods

        void Show();

        #endregion Methods
    }
}