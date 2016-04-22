using Prism.Mvvm;
using System.Media;
using Prism.Commands;

namespace MeetingClock.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;    

    public class ShellViewModel : BindableBase
    {
        private const double _updateSeconds = 0.05;

        private int _alerts = 1;

        private double _billingRate;

        private int _participants;

        private int _alertInterval;

        private double _meetingCost;
        private bool _isRunning;

        public double BillingRate
        {
            get
            {
                return this._billingRate;
            }
            set
            {
                this._billingRate = value;
                this.OnPropertyChanged(() => this.BillingRate);
            }
        }

        public int Participants
        {
            get
            {
                return this._participants;
            }
            set
            {
                this._participants = value;
                this.OnPropertyChanged(() => this.Participants);
            }
        }

        public int AlertInterval
        {
            get
            {
                return this._alertInterval;
            }
            set
            {
                this._alertInterval = value;
                this.OnPropertyChanged(() => this.AlertInterval);
            }
        }

        public double MeetingCost
        {
            get
            {
                return this._meetingCost;
            }
            set
            {
                this._meetingCost = value;
                this.OnPropertyChanged(() => this.MeetingCost);
            }
        }

        public bool IsRunning
        {
            get
            {
                return this._isRunning;
            }
            set
            {
                this._isRunning = value;
                this.OnPropertyChanged(() => this.IsRunning);
            }
        }

        public ObservableCollection<Tuple<int,string>> Priorities { get; set; }

        public ICommand StartMeetingCommand { get; set; }
        public ICommand StopMeetingCommand { get; set; }
        public ICommand AddPriorityCommand { get; set; }        

        public ShellViewModel()
        {
            this.StartMeetingCommand = new DelegateCommand(this.StartMeeting);
            this.StopMeetingCommand = new DelegateCommand(this.StopMeeting);
            this.AddPriorityCommand = new DelegateCommand(this.AppendPriority);
            this.Priorities = new ObservableCollection<Tuple<int, string>>();
        }

        public async Task MeetingLoop()
        {
            while (this.IsRunning)
            {
                Task<double> task = Task.Factory.StartNew(
                    () =>
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                Thread.Sleep((int)(ShellViewModel._updateSeconds * 100));
                                if (!this.IsRunning)
                                    return this.MeetingCost;
                            }
                            double r = this.MeetingCost + ((this.BillingRate/3600) * this.Participants * ShellViewModel._updateSeconds);
                            if (this.MeetingCost / this.AlertInterval > this._alerts)
                            {
                                this._alerts++;
                                SystemSounds.Beep.Play();
                            }
                            return r;
                        });                
                this.MeetingCost = await task;
            }
        }

        private void StartMeeting()
        {
            this.IsRunning = true;
            this.MeetingLoop();
        }

        private void StopMeeting()
        {
            this.IsRunning = false;
        }

        private void AppendPriority()
        {
            this.Priorities.Add(new Tuple<int, string>(this.Priorities.Count, string.Empty));
        }

    }
}
 