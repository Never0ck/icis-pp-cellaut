using System.Windows.Input;
using Cellaut.Presentation.MAUI.Models;

namespace Cellaut.Presentation.MAUI.ViewModels
{
    public class CellautFieldViewModel : BaseViewModel
    {
        public CellField Field
        {
            get => _field;
            set => SetField(ref _field, value);
        }

        private ICommand _createFieldCommand;
        private CellField _field;

        public CellautFieldViewModel()
        {
            Field = new CellField();
        }

        public ICommand CreateFieldCommand => _createFieldCommand ??= new Command(
            execute: CreateField
        );

        private void CreateField()
        {
            Field.View.Invalidate();
        }
    }
}
