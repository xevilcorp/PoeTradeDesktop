using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace PoeTradeDesktop.Controllers
{
    public class BaseControl : INotifyPropertyChanged
    {
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        internal void RaisePropertyChanged(object property)
        {
            string prop = property.GetType().Name;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //Extra Stuff, shows why a base ViewModel is useful
        bool? _CloseWindowFlag;
        public bool? CloseWindowFlag
        {
            get { return _CloseWindowFlag; }
            set
            {
                _CloseWindowFlag = value;
                RaisePropertyChanged("CloseWindowFlag");
            }
        }

        public virtual void CloseWindow(bool? result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CloseWindowFlag = CloseWindowFlag == null ? true : !CloseWindowFlag;
            }));
        }

        public event EventHandler ClosingRequest;

        protected void Close()
        {
            if (this.ClosingRequest != null)
            {
                this.ClosingRequest(this, EventArgs.Empty);
            }
        }

        public static NavigationService Nav;
    }
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;
        public RelayCommand(Action<object> execute)
            : this(execute, DefaultCanExecute)
        {
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }
            this.canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }
        private event EventHandler CanExecuteChangedInternal;
        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            execute(parameter);
        }
        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
        public void Destroy()
        {
            canExecute = _ => false;
            execute = _ => { return; };
        }
        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
    }
}
