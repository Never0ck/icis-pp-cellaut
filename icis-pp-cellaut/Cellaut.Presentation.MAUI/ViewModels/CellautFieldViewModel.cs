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

        private int _fieldWidth;
        public int FieldWidth
        {
            get => _fieldWidth;
            set => SetField(ref _fieldWidth, value);
        }

        private int _fieldHeiht;
        public int FieldHeiht
        {
            get => _fieldHeiht;
            set => SetField(ref _fieldHeiht, value);
        }

        private int _cellCountY;
        public int CellCountY
        {
            get => _cellCountY;
            set => SetField(ref _cellCountY, value);
        }
        private int _cellBorderWidth;
        public int CellBorderWidth
        {
            get => _cellBorderWidth;
            set => SetField(ref _cellBorderWidth, value);
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
        
        public void ClickOnField(object args, TouchEventArgs touchEventArgs)
        {
            var graphics = new GraphicsDrawable
            {
                X_COUNT_CELLS = GraphicsDrawable.X_COUNT_CELLS,
                Y_COUNT_CELLS = GraphicsDrawable.Y_COUNT_CELLS,
                CellBorderWidth = GraphicsDrawable.CellBorderWidth,
                ColleredPoints = GraphicsDrawable.ColleredPoints,
                CELL_WIDTH = GraphicsDrawable.CELL_WIDTH,
                CELL_HEIGHT = GraphicsDrawable.CELL_HEIGHT
            };
            foreach (var touch in touchEventArgs.Touches)
            {
                graphics.CollerPoint(touch);
            }

            GraphicsDrawable = graphics;
        }

        private void CreateField()
        {
            GraphicsDrawable = new GraphicsDrawable
            {
                Y_COUNT_CELLS = CellCountY,
                X_COUNT_CELLS = CellCountX,
                CellBorderWidth = CellBorderWidth,
                ColleredPoints = new List<PointF>(),
            };
        }
    }
}
