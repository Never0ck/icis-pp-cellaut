using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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