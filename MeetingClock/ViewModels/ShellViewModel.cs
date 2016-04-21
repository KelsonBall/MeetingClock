using Prism.Mvvm;
using System.Media;

namespace MeetingClock.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Microsoft.Practices.Prism.Commands;

    public class ShellViewModel : BindableBase
    {
        private int _alerts = 0;

        private int _billingRate;

        private int _participants;

        private int _alertInterval;

        private double _meetingCost;

        public int BillingRate
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

        public ObservableCollection<Tuple<int,string>> Priorities { get; set; }

        public ICommand StartMeetingCommand { get; set; }

        public ICommand AddPriorityCommand { get; set; }

        public ShellViewModel()
        {
            this.StartMeetingCommand = new DelegateCommand(this.StartMeeting);
            this.AddPriorityCommand = new DelegateCommand(this.AppendPriority);
            this.Priorities = new ObservableCollection<Tuple<int, string>>();
        }

        public async Task MeetingLoop()
        {
            while (true)
            {
                Task<double> task = Task.Factory.StartNew(
                    () =>
                        {
                            Thread.Sleep(1500);
                            double r = (this.MeetingCost + (this.BillingRate / 40.0));
                            if (this.MeetingCost / this.AlertInterval > this._alerts)
                            {
                                this._alerts++;
                                SystemSounds.Asterisk.Play();
                            }
                            return r;
                        });                
                this.MeetingCost = await task;
            }
        }

        private void StartMeeting()
        {
            this.MeetingLoop();
        }

        private void AppendPriority()
        {
            this.Priorities.Add(new Tuple<int, string>(this.Priorities.Count, string.Empty));
        }

    }
}
 