namespace Cellaut.Presentation.MAUI.Models;

public class Cell : BaseModel
{
    private bool _isAlive;

    public bool IsAlive
    {
        get => _isAlive;
        set => SetField(ref _isAlive, value);
    }

    public void Togle()
    {
        IsAlive = !IsAlive;
    }
}