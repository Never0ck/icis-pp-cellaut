namespace Cellaut.Presentation.MAUI.Models;

public class CellField : BaseModel, IDrawable
{
    private int _borderWidth = 1;
    private int _countX = 10;
    private int _countY = 10;
    private GraphicsView _view;

    private float _cellWidth;
    private float _cellHeight;
    private Field _field;
    private readonly IAutomaton _automaton;

    public Field Field
    {
        get => _field;
        set => SetField(ref _field, value);
    }

    public CellField(IAutomaton automaton)
    {
        _field = new Field(CountX, CountY);
        _automaton = automaton;
    }

    public int BorderWidth
    {
        get => _borderWidth;
        set => SetField(ref _borderWidth, value);
    }

    public int CountX
    {
        get => _countX;
        set { 
            SetField(ref _countX, value);
            Field.Resize(CountX, CountY);
        }
    }

    public int CountY
    {
        get => _countY;
        set
        {
            SetField(ref _countY, value);
            Field.Resize(CountX, CountY);
        }
    }

    public GraphicsView View
    {
        get => _view;
        set
        {
            SetField(ref _view, value);
            _view.SizeChanged += (sender, args) => _view.Invalidate();
            _view.EndInteraction += ViewOnEndInteraction;
        }
    }

    private void ViewOnEndInteraction(object sender, TouchEventArgs e)
    {
        foreach (var touch in e.Touches)
        {
            var (x, y) = GetXYFromPoint(touch);
            Field.Togle(x, y);
            // var cell = GetCellByPoint(touch);
            // cell.Togle();
        }
        OnPropertyChanged(nameof(Field));
    }

    private (int, int) GetXYFromPoint(PointF point)
    {
        var x = (int)(point.X / _cellWidth) - 1;
        if (point.X % _cellWidth > 0) x++;

        var y = (int)(point.Y / _cellHeight) - 1;
        if (point.Y % _cellHeight > 0) y++;

        return (x, y);
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        View?.Invalidate();
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.Antialias = false;
        canvas.StrokeColor = Colors.Wheat;
        canvas.FillColor = Colors.Azure;
        canvas.StrokeSize = BorderWidth;

        // _cellWidth = MathF.Round(dirtyRect.Width / CountX);
        _cellWidth = dirtyRect.Width / CountX;
        // _cellHeight = MathF.Round(dirtyRect.Height / CountY);
        _cellHeight = dirtyRect.Height / CountY;

        canvas.DrawLine(new PointF(0, 0), new PointF(dirtyRect.Width, 0));
        canvas.DrawLine(new PointF(0, 0), new PointF(0, dirtyRect.Height));

        var y = 0f;
        var x = 0f;

        foreach (var rows in Field.CellField)
        {
            y = 0f;
            foreach (var cell in rows)
            {
                // canvas.DrawLine(new PointF(0, y + _cellHeight), new PointF(dirtyRect.Width, y + _cellHeight));
                // canvas.DrawLine(new PointF(x + _cellWidth, 0), new PointF(x + _cellWidth, dirtyRect.Height));
                if (cell.IsAlive)
                {
                    canvas.FillRectangle(x, y, _cellWidth, _cellHeight);
                }
                y += _cellHeight;
            }
            x += _cellWidth;
        }
        canvas.DrawLine(new PointF(0, dirtyRect.Height), new PointF(dirtyRect.Width, dirtyRect.Height));
        canvas.DrawLine(new PointF(dirtyRect.Width, 0), new PointF(dirtyRect.Width, dirtyRect.Height));
    }

    public void NextState()
    {
        _automaton.NextGeneration(Field);
        OnPropertyChanged(nameof(Field));
    }

    public void RandomCreateField()
    {
        _field.RandomCreateField();
        OnPropertyChanged(nameof(Field));
    }
}