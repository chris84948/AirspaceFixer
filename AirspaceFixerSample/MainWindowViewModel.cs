using JustMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirspaceFixerSample
{
    public class MainWindowViewModel : MVVMBase
    {
        public ICommand ShowDialogCommand { get { return new RelayCommand<bool>(ShowDialogExec); } }

        private bool _fixAirspace;
        public bool FixAirspace
        {
            get { return _fixAirspace; }
            set
            {
                _fixAirspace = value;
                OnPropertyChanged();
            }
        }

        private bool _showDialog;
        public bool ShowDialog
        {
            get { return _showDialog; }
            set
            {
                _showDialog = value;
                OnPropertyChanged();
            }
        }

        private void ShowDialogExec(bool show)
        {
            if (show)
            {
                FixAirspace = true;
                ShowDialog = true;
            }
            else
            {
                ShowDialog = false;
                FixAirspace = false;
            }
        }
    }
}
