using Cellaut.Presentation.MAUI.Models;

namespace Cellaut.Presentation.MAUI;

public class GraphicsDrawable : IDrawable
{
    private readonly CellField _cellField;
    public int X_COUNT_CELLS { get; set; }
    public int Y_COUNT_CELLS { get; set; }
    public int CellBorderWidth { get; set; }
    public List<PointF> ColleredPoints { get; set; }
    public float CELL_HEIGHT { get; set; }
    public float CELL_WIDTH { get; set; }

    public GraphicsDrawable(CellField cellField)
    {
        _cellField = cellField ?? throw new ArgumentNullException(nameof(cellField));
    }


    public void CollerPoint(PointF pointF)
    {
        var startX = pointF.X - pointF.X % CELL_WIDTH;
        var startY = pointF.Y - pointF.Y % CELL_HEIGHT;
        var startPoint = new PointF(startX, startY);
        if (ColleredPoints.Contains(startPoint))
            ColleredPoints.Remove(startPoint);
        else
            ColleredPoints.Add(startPoint);
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Wheat;
        canvas.FillColor = Colors.Azure;
        canvas.StrokeSize = CellBorderWidth;

        CELL_WIDTH = dirtyRect.Width / X_COUNT_CELLS;
        CELL_HEIGHT = dirtyRect.Height / Y_COUNT_CELLS;

        for (var y = -CELL_HEIGHT; y < dirtyRect.Height; y += CELL_HEIGHT)
        {
            for (var x = -CELL_WIDTH; x < dirtyRect.Width; x += CELL_WIDTH)
            {
                canvas.DrawLine(new PointF(0, y + CELL_HEIGHT), new PointF(dirtyRect.Width, y + CELL_HEIGHT));
                canvas.DrawLine(new PointF(x + CELL_WIDTH, 0), new PointF(x + CELL_WIDTH, dirtyRect.Height));
                foreach (var point in ColleredPoints)
                {
                    if ((point.X < x + CELL_WIDTH && point.X >= x) && 
                        (point.Y < y + CELL_HEIGHT && point.Y >= y))
                    {
                        canvas.FillRectangle(x, y, CELL_WIDTH, CELL_HEIGHT);
                    }
                }
                
            }
        }
    }
}