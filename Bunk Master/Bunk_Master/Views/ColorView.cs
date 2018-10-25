using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Lottie.Forms;
using Rg.Plugins.Popup.Extensions;

namespace Bunk_Master
{
    class ColorView : Rg.Plugins.Popup.Pages.PopupPage
    {

        List<SKColor> list_colors = new List<SKColor>
            {
                new SKColor(203,152,203),   //light purple()
                new SKColor(166,166,166),   //light grey()
                new SKColor(255,193,204),   //pink.
                new SKColor(253,213,177),   //salmon.
                new SKColor(188,244,188),   //Lignt green()
                new SKColor(255,161,182),   //orange--
                new SKColor(200,162,200),   //magenta--
                new SKColor(252,241,141),   //yellow()
                new SKColor(179,196,255),   //light blue()
                new SKColor(196,255,179),   //light green()
                new SKColor(69,206,162),    //lawn green.
                new SKColor(96,130,182)     //blue.
            };

        public ColorView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            this.Padding = new Thickness(20, 0, 20, 0);

            Label label = new Label { Text = "Pick a color", FontSize = 30, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Center,VerticalTextAlignment=TextAlignment.Center };

            AbsoluteLayout alay = new AbsoluteLayout {  BackgroundColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center,Padding=new Thickness(0,-25) };


            SKCanvasView sKCanvas1 = new SKCanvasView { };
            SKCanvasView sKCanvas2 = new SKCanvasView { };
            SKCanvasView sKCanvas3 = new SKCanvasView { };
            SKCanvasView sKCanvas4 = new SKCanvasView { };
            SKCanvasView sKCanvas5 = new SKCanvasView { };
            SKCanvasView sKCanvas6 = new SKCanvasView { };
            SKCanvasView sKCanvas7 = new SKCanvasView { };
            SKCanvasView sKCanvas8 = new SKCanvasView { };
            SKCanvasView sKCanvas9 = new SKCanvasView { };
            SKCanvasView sKCanvas10 = new SKCanvasView { };
            SKCanvasView sKCanvas11 = new SKCanvasView { };
            SKCanvasView sKCanvas12 = new SKCanvasView { };

            Grid grid = new Grid { VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.CenterAndExpand,Margin=new Thickness(10,0,10,0) };

            Button button1 = new Button { BackgroundColor=Color.Transparent, };
            Button button2 = new Button { BackgroundColor = Color.Transparent };
            Button button3 = new Button { BackgroundColor = Color.Transparent };
            Button button4 = new Button { BackgroundColor = Color.Transparent };
            Button button5 = new Button { BackgroundColor = Color.Transparent };
            Button button6 = new Button { BackgroundColor = Color.Transparent };
            Button button7 = new Button { BackgroundColor = Color.Transparent };
            Button button8 = new Button { BackgroundColor = Color.Transparent };
            Button button9 = new Button { BackgroundColor = Color.Transparent };
            Button button10 = new Button { BackgroundColor = Color.Transparent };
            Button button11 = new Button { BackgroundColor = Color.Transparent };
            Button button12 = new Button { BackgroundColor = Color.Transparent };

            AnimationView animationView = new AnimationView { Margin=new Thickness(10),Speed=2.5f,InputTransparent = true, AutoPlay = true, Animation = "data.json", Loop = false, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

            

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    label.FontFamily = "Handlee-Regular.ttf";
                    break;
                case Device.Android:
                    label.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
                case Device.UWP:
                    label.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
                default:
                    break;
            } //fonts


            //****FLAGS****
            AbsoluteLayout.SetLayoutFlags(grid, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(grid, new Rectangle(.5, .5, 360, 360));
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 70 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 70 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 70 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 70 });

            grid.RowDefinitions.Add(new RowDefinition { Height = 50 });
            grid.RowDefinitions.Add(new RowDefinition { Height = 70 });
            grid.RowDefinitions.Add(new RowDefinition { Height = 70 });
            grid.RowDefinitions.Add(new RowDefinition { Height = 70 });

            //****EVENTS****

            sKCanvas1.PaintSurface += SKCanvas1_PaintSurface;
            sKCanvas2.PaintSurface += SKCanvas2_PaintSurface;
            sKCanvas3.PaintSurface += SKCanvas3_PaintSurface;
            sKCanvas4.PaintSurface += SKCanvas4_PaintSurface;
            sKCanvas5.PaintSurface += SKCanvas5_PaintSurface;
            sKCanvas6.PaintSurface += SKCanvas6_PaintSurface;
            sKCanvas7.PaintSurface += SKCanvas7_PaintSurface;
            sKCanvas8.PaintSurface += SKCanvas8_PaintSurface;
            sKCanvas9.PaintSurface += SKCanvas9_PaintSurface;
            sKCanvas10.PaintSurface += SKCanvas10_PaintSurface;
            sKCanvas11.PaintSurface += SKCanvas11_PaintSurface;
            sKCanvas12.PaintSurface += SKCanvas12_PaintSurface;

            


            grid.Children.Add(label, 0, 0);



            grid.Children.Add(sKCanvas1, 0, 1);
            grid.Children.Add(sKCanvas2, 1, 1);
            grid.Children.Add(sKCanvas3, 2, 1);
            grid.Children.Add(sKCanvas4, 3, 1);
            grid.Children.Add(sKCanvas5, 0, 2);
            grid.Children.Add(sKCanvas6, 1, 2);
            grid.Children.Add(sKCanvas7, 2, 2);
            grid.Children.Add(sKCanvas8, 3, 2);
            grid.Children.Add(sKCanvas9, 0, 3);
            grid.Children.Add(sKCanvas10, 1, 3);
            grid.Children.Add(sKCanvas11, 2, 3);
            grid.Children.Add(sKCanvas12, 3, 3);

            grid.Children.Add(button1, 0, 1);
            grid.Children.Add(button2, 1, 1);
            grid.Children.Add(button3, 2, 1);
            grid.Children.Add(button4, 3, 1);
            grid.Children.Add(button5, 0, 2);
            grid.Children.Add(button6, 1, 2);
            grid.Children.Add(button7, 2, 2);
            grid.Children.Add(button8, 3, 2);
            grid.Children.Add(button9, 0, 3);
            grid.Children.Add(button10, 1, 3);
            grid.Children.Add(button11, 2, 3);
            grid.Children.Add(button12, 3, 3);

            Grid.SetColumnSpan(label, 4);

            #region ButtonTickEvent

            button1.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 0, 1);
                AddFormView.clrview.Color = list_colors[0];
            };
            button2.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 1, 1);
                AddFormView.clrview.Color = list_colors[1];
            };
            button3.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 2, 1);
                AddFormView.clrview.Color = list_colors[2];
            };
            button4.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 3, 1);
                AddFormView.clrview.Color = list_colors[3];
            };
            button5.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 0, 2);
                AddFormView.clrview.Color = list_colors[4];
            };
            button6.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 1, 2);
                AddFormView.clrview.Color = list_colors[5];
            };
            button7.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 2, 2);
                AddFormView.clrview.Color = list_colors[6];
            };
            button8.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 3, 2);
                AddFormView.clrview.Color = list_colors[7];
            };
            button9.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 0, 3);
                AddFormView.clrview.Color = list_colors[8];
            };
            button10.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 1, 3);
                AddFormView.clrview.Color = list_colors[9];
            };
            button11.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 2, 3);
                AddFormView.clrview.Color = list_colors[10];
            };
            button12.Clicked += (s, e) =>
            {
                grid.Children.Remove(animationView);
                grid.Children.Add(animationView, 3, 3);
                AddFormView.clrview.Color = list_colors[11];
            };
            #endregion

            alay.Children.Add(grid);

            Content = alay;
        }

        
        #region PaintSurfaceCallback


        private void SKCanvas12_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[11], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);


        }

        private void SKCanvas11_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[10], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas10_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[9], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas9_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[8], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas8_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[7], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas7_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[6], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas6_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[5], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas5_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[4], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas4_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[3], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas3_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[2], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas2_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[1], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }

        private void SKCanvas1_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = list_colors[0], IsAntialias = true, Style = SKPaintStyle.Fill };

            canvas.DrawCircle(105, 105, 70, sKPaint);
        }
        #endregion

        #region PopupCallbacks


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
        #endregion
    }
}
