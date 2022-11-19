using Cellaut.Presentation.MAUI.Models;

namespace Cellaut.Presentation.MAUI.Views;

public partial class CelluarGraphicsView : GraphicsView
{
    public CelluarGraphicsView()
    {
        InitializeComponent();
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        ((CellField)Drawable).View = this;
    }
}