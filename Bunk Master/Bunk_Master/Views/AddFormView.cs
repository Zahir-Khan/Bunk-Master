using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Extensions;

namespace Bunk_Master
{
    public class AddFormView : ContentPage
    {
        public static BindableSKCanvasView clrview = new BindableSKCanvasView { EnableTouchEvents = true};

        public AddFormView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = ViewModel.ClassVMInstance;

            //****INIT****
            AbsoluteLayout alay0 = new AbsoluteLayout() { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            AbsoluteLayout skalay = new AbsoluteLayout { BackgroundColor = Color.FromHex("ffa6a5ff"), VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            AbsoluteLayout uialay = new AbsoluteLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            AbsoluteLayout calay = new AbsoluteLayout { /*WidthRequest = 50, HeightRequest = 50*/ HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.FillAndExpand };
            ContentView container0 = new ContentView { };
            Grid grid1 = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand, RowSpacing = 0 };
            Grid grid2 = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand, RowSpacing = 0 };
            Grid grid3 = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand, RowSpacing = 0 };
            Grid grid4 = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand, RowSpacing = 0 };
            Grid grid5 = new Grid { HorizontalOptions = LayoutOptions.CenterAndExpand, RowSpacing = 0 };

            ScheSwitch scheSwitch = new ScheSwitch(false) { };
            Grid grid_title = new Grid { };
            Label label_sched = new Label { Text = "Schedule It!", FontSize = 25, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White };
            Label label_title = new Label { Text = "Add Class", FontSize = 30, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, InputTransparent = true };
            Label label_name = new Label { Text = "Class Name", FontSize = 16, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            Label label_color = new Label { Text = "Select Color", FontSize = 16, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            Label label_present = new Label { Text = "Present Classes", FontSize = 16, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            Label label_total = new Label { Text = "Total Classes", FontSize = 16, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            Label label_min = new Label { Text = "Minumum Percent To Maintain", FontSize = 16, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            Label label_date = new Label { Text = "First Day Of Class", FontSize = 16, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            Label label_count = new Label { Text = "Class Count In A Week", FontSize = 16, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            Entry entry_name = new Entry() { WidthRequest = 150 };
            Entry entry_tot = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 50, HorizontalOptions = LayoutOptions.Center, Margin = new Thickness(5, 0, 0, 0) };
            Entry entry_pre = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 50, HorizontalOptions = LayoutOptions.Center };
            Entry entry_min = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 50, HorizontalOptions = LayoutOptions.Center };
            Button button_add = new Button() { Margin = 0, WidthRequest = 70, HeightRequest = 70, Image = "done_icon.png", BackgroundColor = Color.Transparent };
            Button button_remove = new Button() { Margin = 0, WidthRequest = 70, HeightRequest = 70, Image = "cancel_btn.png", BackgroundColor = Color.Transparent };
            DatePicker datePicker = new DatePicker { Format = "d-MMMM-yyyy", HorizontalOptions = LayoutOptions.CenterAndExpand };
            Switch toggle = new Switch { };
            SKCanvasView skview = new SKCanvasView();




            grid1.RowDefinitions.Add(new RowDefinition { });
            grid1.RowDefinitions.Add(new RowDefinition { });
            grid1.ColumnDefinitions.Add(new ColumnDefinition { });
            grid1.ColumnDefinitions.Add(new ColumnDefinition { });

            grid2.RowDefinitions.Add(new RowDefinition { });
            grid2.RowDefinitions.Add(new RowDefinition { });
            grid2.ColumnDefinitions.Add(new ColumnDefinition { });
            grid2.ColumnDefinitions.Add(new ColumnDefinition { });



            grid3.RowDefinitions.Add(new RowDefinition { });
            grid3.ColumnDefinitions.Add(new ColumnDefinition { });

            grid4.RowDefinitions.Add(new RowDefinition { });
            grid4.ColumnDefinitions.Add(new ColumnDefinition { });

            grid5.RowDefinitions.Add(new RowDefinition { });
            grid5.ColumnDefinitions.Add(new ColumnDefinition { });

            //****EVENTS****

            button_add.Clicked += AddButton_Clicked;
            button_remove.Clicked += Button_remove_Clicked;
            skview.PaintSurface += Skview_PaintSurface;
            clrview.PaintSurface += Clrview_PaintSurface;
            clrview.Touch += Clrview_Touch;

            //****FLAGS****
            AbsoluteLayout.SetLayoutFlags(skview, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutFlags(clrview, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutFlags(skalay, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutFlags(uialay, AbsoluteLayoutFlags.All);
            //AbsoluteLayout.SetLayoutFlags(calay, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(label_title, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(label_sched, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(label_date, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(datePicker, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(scheSwitch, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(label_count, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(grid1, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(grid2, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(grid3, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(grid4, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(grid5, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(button_add, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutFlags(button_remove, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(skview, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutBounds(clrview, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutBounds(skalay, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutBounds(uialay, new Rectangle(0, 0, 1, 1));

            //****BINDINGS****
            entry_name.SetBinding(Entry.TextProperty, "classname");
            entry_tot.SetBinding(Entry.TextProperty, "totnos");
            entry_pre.SetBinding(Entry.TextProperty, "presentnos");
            entry_min.SetBinding(Entry.TextProperty, "mincutoff");
            datePicker.SetBinding(DatePicker.DateProperty, "startdate");
            clrview.SetBinding(BindableSKCanvasView.ColorProperty, "color");

            #region Font

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    label_sched.FontFamily = "Handlee-Regular.ttf";
                    entry_min.FontFamily = "Handlee-Regular.ttf";
                    entry_name.FontFamily = "Handlee-Caps.ttf";
                    entry_pre.FontFamily = "Handlee-Regular.ttf";
                    entry_tot.FontFamily = "Handlee-Regular.ttf";
                    button_add.FontFamily = "Handlee-Regular.ttf";
                    break;
                case Device.Android:
                    label_sched.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry_min.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry_name.FontFamily = "Fonts/Handlee-Caps.ttf#Handlee-Caps";
                    entry_pre.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry_tot.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    button_add.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
                case Device.UWP:
                    label_sched.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry_min.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry_name.FontFamily = "Assets/Fonts/Handlee-Caps.ttf#Handlee-Caps";
                    entry_pre.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry_tot.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    button_add.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
                default:
                    break;
            }

            #endregion


            calay.Children.Add(clrview);

            grid1.Children.Add(label_color, 0, 0);
            grid1.Children.Add(calay, 0, 1);
            grid1.Children.Add(label_name, 1, 0);
            grid1.Children.Add(entry_name, 1, 1);

            grid2.Children.Add(label_present, 0, 0);
            grid2.Children.Add(label_total, 1, 0);
            grid2.Children.Add(entry_pre, 0, 1);
            grid2.Children.Add(entry_tot, 1, 1);

            grid3.Children.Add(label_min, 0, 0);
            grid3.Children.Add(entry_min, 0, 1);

            grid4.Children.Add(label_date, 0, 0);
            grid4.Children.Add(datePicker, 0, 1);

            grid5.Children.Add(label_count, 0, 0);
            grid5.Children.Add(scheSwitch, 0, 1);

            skalay.Children.Add(skview);
            alay0.Children.Add(skalay);
            alay0.Children.Add(uialay);
            uialay.Children.Add(label_title, new Point(.5, .05));
            uialay.Children.Add(grid5, new Point(.5, .95));
            uialay.Children.Add(grid4, new Point(.5, .77));
            uialay.Children.Add(grid3, new Point(.5, .45));
            uialay.Children.Add(grid2, new Point(.5, .3));
            uialay.Children.Add(grid1, new Point(.5, .15));

            uialay.Children.Add(label_sched, new Point(.5, .66));

            uialay.Children.Add(button_add, new Point(.975, 0.025));
            uialay.Children.Add(button_remove, new Point(0.025, 0.025));
            //alay0.Children.Add(label_title,new Point(.5,.05));
            //alay0.Children.Add(grid1, new Point(.5, .15));
            //alay0.Children.Add(grid2, new Point(.5,.3));
            //alay0.Children.Add(grid3, new Point(.5, .45));
            //alay0.Children.Add(grid4, new Point(.5, .77));
            //alay0.Children.Add(grid5, new Point(.5, .95));
            //alay0.Children.Add(label_sched, new Point(.5, .66));

            //alay0.Children.Add(button_add, new Point(1, 0));

            container0.Content = alay0;
            Content = container0;

        }

        private async void Clrview_Touch(object sender, SKTouchEventArgs e)
        {
            //await Navigation.PushPopupAsync(new ColorView());
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ColorView());
            //Task task = new task(PushAsync(PopupPage ColorView, bool animate = true);
        }

        //****CALLBACKS**** 


        private void Clrview_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as BindableSKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint sKPaint = new SKPaint { Color = canvasobj.Color, Style = SKPaintStyle.Fill };
            SKPaint sKPaint_outline = new SKPaint { Color = SKColors.Black, Style = SKPaintStyle.Stroke, StrokeWidth = 5f, IsAntialias = true };

            canvas.DrawCircle(width / 2, height / 2, (height/2)-10, sKPaint);
            canvas.DrawCircle(width / 2, height / 2, (height/2)-5, sKPaint_outline);

        }

        private void Skview_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as SKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            SKPaint kPBottm = new SKPaint { Color = SKColors.LightSlateGray, Style = SKPaintStyle.Fill, IsAntialias = true };
            canvas.DrawRect(new SKRect(0, height * .66f, width, height), kPBottm);
            canvas.DrawCircle(width / 2, height * 1.19f, width, kPBottm);


        }


        private void AddButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.ClassVMInstance.Add();
            ViewModel.ClassVMInstance.ClearForm();
            Navigation.PopModalAsync();
        }

        private void Button_remove_Clicked(object sender, EventArgs e)
        {
            ViewModel.ClassVMInstance.ClearForm();
            Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            ViewModel.ClassVMInstance.ClearForm();
            return base.OnBackButtonPressed();
        }
    }
}