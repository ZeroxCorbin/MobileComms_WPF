using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MobileComms_WPF.ViewModels
{
    public class ARCLJobQueueItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }


        public ObservableCollection<string> MapGoals { get; set; } = new ObservableCollection<string>();
        public List<string> SegmentTypes = new List<string>() { "pickup", "dropoff" };

        public ARCLJobQueueItemViewModel(List<string> mapGoals)
        {
            foreach (string s in mapGoals)
                MapGoals.Add(s);
        }

        public void UpdateGoalsList(List<string> mapGoals)
        {
            MapGoals.Clear();

            foreach (string s in mapGoals)
                MapGoals.Add(s);
        }

    }
}
