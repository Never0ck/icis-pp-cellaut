using System.ComponentModel;

namespace Cellaut.Presentation.MAUI.Models;

public class CellField : BaseModel, IDrawable
{
    private int _borderWidth = 1;
    private int _countX = 10;
    private int _countY = 10;
    private GraphicsView _view;

    private float _cellWidth;
    private float _cellHeight;

    /// <summary>
    /// Двумерный массив клеток, где внешний массив -- X, где внутренний Y
    /// </summary>
    public List<List<Cell>> Field { get; set; }

    public CellField()
    {
        Field = new List<List<Cell>> { new() { new Cell() } };
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
            Resize();
        }
    }

    public int CountY
    {
        get => _countY;
        set
        {
            SetField(ref _countY, value);
            Resize();
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
            var cell = GetCellByPoint(touch);
            cell.Togle();
            OnPropertyChanged(nameof(Field));
        }
    }

    private Cell GetCellByPoint(PointF point)
    {
        var x = (int)(point.X / _cellWidth) - 1;
        if (point.X % _cellWidth > 0) x++;

        var y = (int)(point.Y / _cellHeight) - 1;
        if (point.Y % _cellHeight > 0) y++;

        return Field[x][y];
    }

    protected override void OnPropertyChanged(string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);
        View.Invalidate();
    }

    private void Resize()
    {
        if (CountX < Field.Count)
        {
            Field.RemoveRange(CountX, Field.Count - CountX);
        } else if (CountX > Field.Count)
        {
            if (CountX > Field.Capacity)
                Field.Capacity = CountX;

            Field.AddRange(Enumerable.Range(0, CountX - Field.Count).Select(i => new List<Cell>()));
        }

        foreach (var col in Field)
        {
            if (CountY < col.Count)
            {
                col.RemoveRange(CountY, col.Count - CountY);
            }
            else if (CountY > col.Count)
            {
                if (CountY > col.Capacity)
                    col.Capacity = CountY;

                col.AddRange(Enumerable.Range(0, CountY - col.Count).Select(i => new Cell()));
            }
        }
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Wheat;
        canvas.FillColor = Colors.Azure;
        canvas.StrokeSize = BorderWidth;

        _cellWidth = dirtyRect.Width / CountX;
        _cellHeight = dirtyRect.Height / CountY;

        canvas.DrawLine(new PointF(0, 0), new PointF(dirtyRect.Width, 0));
        canvas.DrawLine(new PointF(0, 0), new PointF(0, dirtyRect.Height));

        var y = 0f;
        var x = 0f;

        foreach (var rows in Field)
        {
            y = 0f;
            foreach (var cell in rows)
            {
                canvas.DrawLine(new PointF(0, y + _cellHeight), new PointF(dirtyRect.Width, y + _cellHeight));
                canvas.DrawLine(new PointF(x + _cellWidth, 0), new PointF(x + _cellWidth, dirtyRect.Height));
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
}