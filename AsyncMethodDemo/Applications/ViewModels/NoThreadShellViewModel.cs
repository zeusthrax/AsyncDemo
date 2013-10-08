using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using AsyncMethodDemo.Applications.Views;

using DemoDomainObjects;

using PostSharp.Patterns.Model;

namespace AsyncMethodDemo.Applications.ViewModels
{
    [Export]
    internal class NoThreadShellViewModel : ViewModel<IShellView>, IShellViewModel
    {
        #region Fields

        private const string TYPE = "No Thread Demo";

        private readonly DelegateCommand exitCommand;
        private readonly DelegateCommand _incrementExecutionCommand;
        private readonly DelegateCommand _startExecutionCommand;

        private int clickCount;
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string _currentTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", 0, 0, 0, 0);
        Cursor _cursor;
        private bool _inProgress;
        private string _title;

        #endregion Fields

        #region Constructors

        [ImportingConstructor]
        public NoThreadShellViewModel(IShellView view)
            : base(view)
        {
            Title = TYPE;
            exitCommand = new DelegateCommand(Close);
            _startExecutionCommand = new DelegateCommand(StartExecution, CanExecute);
            _incrementExecutionCommand = new DelegateCommand(IncrementCount);
            Files = new ObservableSortedList<FileInformation>();
            WindowCursor = Cursors.Arrow;
            dt.Tick += new EventHandler(dt_Tick);
            dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        #endregion Constructors

        #region Properties

        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                OnPropertyChanged("CurrentTime");
            }
        }

        public ICommand ExitCommand
        {
            get { return exitCommand; }
        }

        public ObservableSortedList<FileInformation> Files
        {
            get; set;
        }

        public ICommand IncrementCommand
        {
            get { return _incrementExecutionCommand; }
        }

        public string LabelText
        {
            get
            {
                return string.Format("Total Click Count {0:00000}", clickCount);

            }
        }

        public ICommand StartExecutionCommand
        {
            get
            {
                return _startExecutionCommand;
            }
        }

        public string Title
        {
            get { return _title; } private set { _title = value; OnPropertyChanged("Title"); }
        }

        public int TotalFiles
        {
            get
            {
                return Directory.GetFiles(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319").Length;
            }
        }

        public Cursor WindowCursor
        {
            get { return _cursor; }
            private set
            {
                _cursor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WindowCursor"));
            }
        }

        #endregion Properties

        #region Methods

        public void Show()
        {
            ViewCore.Show();
        }

        private bool CanExecute()
        {
            return true;
        }

        private void Close()
        {
            ViewCore.Close();
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private FileInformation GetFileInformation(string path)
        {
            Thread.Sleep(50);
            var file = new FileInfo(path);
            return new FileInformation { FileName = Path.GetFileName(path), FileSize = (file.Length / 1024) };
        }

        private void IncrementCount()
        {
            clickCount += 1;
            OnPropertyChanged("LabelText");
        }

        void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private int PopulateFiles()
        {
            if (!_inProgress)
            {
                _inProgress = true;
                Files.Clear();
                foreach (var item in Directory.GetFiles(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319"))
                {

                    Files.Add(GetFileInformation(item));
                    Title = string.Format("{0} - Counting Files: {1}", TYPE, Files.Count);

                }
                _inProgress = false;
            }

            return Files.Count;
        }

        private void ResetWatch()
        {
            stopWatch.Reset();
            stopWatch.Start();
            dt.Start();
        }

        private void ShowFiles()
        {
            int count = PopulateFiles();
            Title = string.Format("{0} - Total Files: {1}", TYPE, count);
        }

        private void StartExecution()
        {
            WindowCursor = Cursors.Wait;
            ResetWatch();
            IncrementCount();
            ShowFiles();
            StopWatch();
            WindowCursor = Cursors.Arrow;
        }

        private void StopWatch()
        {
            UpdateTime();
            stopWatch.Stop();
            dt.Stop();
        }

        private void UpdateTime()
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                CurrentTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            }
        }

        #endregion Methods
    }
}