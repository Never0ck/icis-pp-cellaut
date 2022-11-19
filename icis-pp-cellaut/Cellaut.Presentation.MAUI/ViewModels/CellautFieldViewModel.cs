using System.Timers;
using System.Windows.Input;
using Cellaut.Presentation.MAUI.Models;
using LiveGameAutomaton = Cellaut.Presentation.MAUI.Models.LiveGameAutomaton;

namespace Cellaut.Presentation.MAUI.ViewModels
{
    public class CellautFieldViewModel : BaseViewModel
    {
        private System.Timers.Timer _timer;

        public CellautFieldViewModel()
        {
            Field = new CellField(new LiveGameAutomaton());
            _timer = new System.Timers.Timer(_ms);
            _timer.Elapsed += TimerOnElapsed;
        }

        private CellField _field;
        public CellField Field
        {
            get => _field;
            set => SetField(ref _field, value);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Field.NextState();
        }


        private ICommand _createFieldCommand;
        public ICommand NextStateFieldCommand => _createFieldCommand ??= new Command(Field.NextState);


        private ICommand _startGeneratingCommand;
        public ICommand StartGeneratingCommand => _startGeneratingCommand ??= new Command(_timer.Start);

        private ICommand _endGeneratingCommand;
        public ICommand EndGeneratingCommand => _endGeneratingCommand ??= new Command(_timer.Stop);

        private ICommand _randomCreateFieldCommand;
        public ICommand RandomCreateFieldCommand => _randomCreateFieldCommand ??= new Command(_field.RandomCreateField);

        private int _ms = 1000;

        public int Ms
        {
            get => _ms;
            set
            {
                SetField(ref _ms, value);
                _timer.Interval = value;
            }
        }
    }
}
