namespace MeetingClock
{
    using System.ComponentModel;
    using System.Windows;

    using MeetingClock.ViewModels;
    using MeetingClock.Views;

    using Microsoft.Practices.Unity;

    using Prism.Events;
    using Prism.Unity;

    public class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (ShellView)Shell;

            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance<IUnityContainer>(Container);            

            Container.RegisterType<ShellViewModel>(new ContainerControlledLifetimeManager());            

            ShellViewModel shellVm = new ShellViewModel();

            Container.RegisterInstance<ShellViewModel>(shellVm);
        }
    }
}
