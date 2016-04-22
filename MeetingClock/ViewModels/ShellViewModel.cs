using Prism.Mvvm;
using System.Media;
using MeetingClock.Models;
using Prism.Commands;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace MeetingClock.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;    

    public class ShellViewModel : BindableBase
    {
        private MeetingViewModel _meetingVm;

        #region Fields

       

        #endregion

        #region Properties

        public MeetingViewModel MeetingVm
        {
            get
            {
                return this._meetingVm; 
                
            }
            set
            {
                this._meetingVm = value;
                this.OnPropertyChanged(() => this.MeetingVm);
            }
        }

        #endregion

        #region Commands

        public ICommand SaveCommand { get; set; }
        public ICommand LoadCommand { get; set; }
       
        #endregion

        #region Constructors

        public ShellViewModel()
        {
            this.MeetingVm = new MeetingViewModel();
            this.SaveCommand = new DelegateCommand(this.Save);
            this.LoadCommand = new DelegateCommand(this.Load);
        }

        #endregion

        #region Private Methods

        private void Save()
        {
            MeetingModel state = this.MeetingVm.Save();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.CreatePrompt = true;
            dialog.OverwritePrompt = true;
            dialog.Filter = "Json Files|*.json";            
            if ((bool)dialog.ShowDialog())
            {
                string json = JsonConvert.SerializeObject(state);
                File.WriteAllText(dialog.FileName, json);
            }
        }

        private void Load()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.CheckFileExists = true;
            dialog.CheckPathExists = true;
            dialog.Filter = "Json Files|*.json";
            dialog.AddExtension = true;            
            if ((bool)dialog.ShowDialog())
            {
                string json = File.ReadAllText(dialog.FileName);
                this.MeetingVm.Load(JsonConvert.DeserializeObject<MeetingModel>(json));
            }
        }
        
        #endregion

        #region Public Methods      

        #endregion

    }
}
 