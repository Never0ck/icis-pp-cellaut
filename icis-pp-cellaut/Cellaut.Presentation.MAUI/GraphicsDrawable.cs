namespace Cellaut.Presentation.MAUI;

public class GraphicsDrawable : IDrawable
{
    public int X_COUNT_CELLS { get; set; }
    public int Y_COUNT_CELLS { get; set; }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        canvas.StrokeColor = Colors.Wheat;
        canvas.StrokeSize = 2;

        var CELL_WIDTH = dirtyRect.Width / X_COUNT_CELLS;
        var CELL_HEIGHT = dirtyRect.Height / Y_COUNT_CELLS;

        for (var y = -CELL_HEIGHT; y < dirtyRect.Height; y += CELL_HEIGHT)
        {
            for (var x = -CELL_WIDTH; x < dirtyRect.Width; x += CELL_WIDTH)
            {
                canvas.DrawLine(new PointF(0, y + CELL_HEIGHT), new PointF(dirtyRect.Width, y + CELL_HEIGHT));
                canvas.DrawLine(new PointF(x + CELL_WIDTH, 0), new PointF(x + CELL_WIDTH, dirtyRect.Height));
            }
        }
    }
}