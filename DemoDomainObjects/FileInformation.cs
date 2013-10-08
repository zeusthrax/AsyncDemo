namespace DemoDomainObjects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using PostSharp.Patterns.Model;

    [NotifyPropertyChanged]
    public class FileInformation : IComparable<FileInformation>, INotifyPropertyChanged
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        public string FileName
        {
            get; set;
        }

        public long FileSize
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public int CompareTo(FileInformation other)
        {
            if (FileSize != other.FileSize)
                return FileSize.CompareTo(other.FileSize);

            if (FileName != other.FileName)
                return FileName.CompareTo(other.FileName);

            return 0;
        }

        void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion Methods
    }
}