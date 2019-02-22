using MyProgramJournal_DB.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyProgramJournal_DB.ViewModel
{

    class ViewModel : ObservableObject
    {
        private string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                RaisePropertyChangedEvent("text");
            }
        }


        private void gona()
        {
            text = "324234234";
        }

        // Команда кек
        public ICommand kek
        {
            get
            {
                return new DelegateCommand(gona);
            }
        }
    }

}
