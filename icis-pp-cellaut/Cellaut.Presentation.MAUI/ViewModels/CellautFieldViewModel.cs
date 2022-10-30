using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cellaut.Presentation.MAUI.ViewModels
{
    public class CellautFieldViewModel : BaseViewModel
    {
        private int _cellCountX;
        public int CellCountX
        {
            get => _cellCountX;
            set => SetField(ref _cellCountX, value);
        }

        private int _cellCountY;
        public int CellCountY
        {
            get => _cellCountY;
            set => SetField(ref _cellCountY, value);
        }

        private GraphicsDrawable _graphicsDrawable;

        public GraphicsDrawable GraphicsDrawable
        {
            get => _graphicsDrawable; 
            set => SetField(ref _graphicsDrawable, value);
        }

        private ICommand _createFieldCommand;
        public ICommand CreateFieldCommand => _createFieldCommand ??= new Command(
            execute: CreateField
            );

        private void CreateField()
        {
            GraphicsDrawable = new GraphicsDrawable
            {
                X_COUNT_CELLS = CellCountX, 
                Y_COUNT_CELLS = CellCountY
            };
        }
    }
}
