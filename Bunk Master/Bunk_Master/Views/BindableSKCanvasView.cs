using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bunk_Master
{
    public class BindableSKCanvasView : SKCanvasView
    {

        public static readonly BindableProperty ColorProperty =
                BindableProperty.Create("Color", typeof(SKColor), typeof(BindableSKCanvasView), defaultValue: SKColors.Black, propertyChanged: RedrawCanvas,defaultBindingMode:BindingMode.TwoWay);

        public SKColor Color
        {
            get { return (SKColor)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }


        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            BindableSKCanvasView bindableCanvas = bindable as BindableSKCanvasView;

            bindableCanvas.InvalidateSurface();
        }
    }
}
