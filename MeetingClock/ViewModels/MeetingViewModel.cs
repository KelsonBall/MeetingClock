using Prism.Mvvm;
using System.Media;
using MeetingClock.Models;
using Prism.Commands;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeetingClock.ViewModels
{    
    public class MeetingViewModel : BindableBase
    {
        #region Fields

        private const double _updateSeconds = 0.05;

        private int _alerts = 1;

        private double _billingRate;

        private int _participants;

        private int _alertInterval;

        private double _meetingCost;
        private bool _isRunning;
        private bool _markedForReset = false;
        private MeetingModel _markedForLoad = null;

        #endregion

        #region Properties

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

        #endregion

        #region Commands

        public ICommand StartMeetingCommand { get; set; }
        public ICommand StopMeetingCommand { get; set; }
        public ICommand AddPriorityCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        #endregion

        #region Constructors

        public MeetingViewModel()
        {
            this.StartMeetingCommand = new DelegateCommand(this.StartMeeting);
            this.StopMeetingCommand = new DelegateCommand(this.StopMeeting);
            this.ResetCommand = new DelegateCommand(this.Reset);
        }

        #endregion

        #region Private Methods

        private async Task MeetingLoop()
        {
            while (this.IsRunning)
            {
                Task<double> task = Task.Factory.StartNew(
                    () =>
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Thread.Sleep((int)(MeetingViewModel._updateSeconds * 100));
                            if (!this.IsRunning)
                                return this.MeetingCost;
                        }
                        double r = this.MeetingCost + ((this.BillingRate / 3600) * this.Participants * MeetingViewModel._updateSeconds);
                        if (this.MeetingCost / this.AlertInterval > this._alerts)
                        {
                            this._alerts++;
                            SystemSounds.Beep.Play();
                        }
                        return r;
                    });
                this.MeetingCost = await task;
            }
            if (this._markedForReset)
                this.MeetingCost = 0;
            if (this._markedForLoad != null)
                this.Load(this._markedForLoad);

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

        private void Reset()
        {
            if (this._isRunning)
            {
                this.IsRunning = false;
                this._markedForReset = true;
            }
            else
            {
                this.FinishReset();
            }
        }

        private void FinishReset()
        {
            this.MeetingCost = 0;
            this._markedForReset = false;
        }
        #endregion

        #region Public Methods

        public MeetingModel Save()
        {
            return new MeetingModel()
            {
                BillingRate = this.BillingRate,
                Participants = this.Participants,
                AlertInterval = this.AlertInterval,
                AlertCount = this._alerts,
                MeetingCost = this.MeetingCost
            };
        }

        public void Load(MeetingModel data)
        {
            if (this._isRunning)
            {
                this._markedForLoad = data;
                this.IsRunning = false;
            }
            else
            {
                this._markedForLoad = null;
                this.BillingRate = data.BillingRate;
                this.Participants = data.Participants;
                this.AlertInterval = data.AlertInterval;
                this._alerts = data.AlertCount;
                this.MeetingCost = data.MeetingCost;
            }            
        }

        #endregion

    }
}
