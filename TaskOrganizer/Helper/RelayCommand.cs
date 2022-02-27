using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskOrganizer
{
    public class RelayCommand : ICommand
    {
        // Interfejs pomiędzy warstwą prezentacji, a warstwą logiki biznesowej
        //Zamiast


        //CanExecute - sprawdzane cały czas w pętli programu.

        public event EventHandler CanExecuteChanged;
        private Action mAction;

        public RelayCommand(Action action)
        {
            mAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}
