using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Cellaut.Presentation.MAUI.Views;

public partial class CelluatFieldPage : ContentPage
{
    public CelluatFieldPage()
    {
        InitializeComponent();
    }

    private void GraphicsView_OnEndInteraction(object sender, TouchEventArgs e)
    {
        var context = BindingContext as ViewModels.CellautFieldViewModel;
        context.ClickOnField(sender, e);
    }
}