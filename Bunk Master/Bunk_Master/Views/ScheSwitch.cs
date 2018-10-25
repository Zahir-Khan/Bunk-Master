using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Bunk_Master
{
    public class ScheSwitch : Grid
    {
        public ScheSwitch(bool updateflag, int id = 0)
        {
            BindingContext = ViewModel.ClassVMInstance;
            HorizontalOptions = LayoutOptions.Center;
            ColumnSpacing = 2;
            RowSpacing = 5;
            double fontsize = 20;
            double size = 35;
            Button b1 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b2 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b3 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b4 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b5 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b6 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b7 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };

            Button b8 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b9 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b10 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b11 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b12 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b13 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };
            Button b14 = new Button { TextColor = Color.White, HeightRequest = size, WidthRequest = size, BackgroundColor = Color.Transparent, BorderWidth = 1, BorderColor = Color.White, CornerRadius = 5 };

            Label t1 = new Label { FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, InputTransparent = true };
            Label t2 = new Label { FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, InputTransparent = true };
            Label t3 = new Label { FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, InputTransparent = true };
            Label t4 = new Label { FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, InputTransparent = true };
            Label t5 = new Label { FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, InputTransparent = true };
            Label t6 = new Label { FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, InputTransparent = true };
            Label t7 = new Label { FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalTextAlignment = TextAlignment.Center, InputTransparent = true };

            Label l1 = new Label { Text = "Mo", FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label l2 = new Label { Text = "Tu", FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label l3 = new Label { Text = "We", FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label l4 = new Label { Text = "Th", FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label l5 = new Label { Text = "Fr", FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label l6 = new Label { Text = "Sa", FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label l7 = new Label { Text = "Su", FontSize = fontsize, TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };

            t1.SetBinding(Label.TextProperty, new Binding("Monday"));
            t2.SetBinding(Label.TextProperty, "Tuesday");
            t3.SetBinding(Label.TextProperty, "Wednesday");
            t4.SetBinding(Label.TextProperty, "Thursday");
            t5.SetBinding(Label.TextProperty, "Friday");
            t6.SetBinding(Label.TextProperty, "Saturday");
            t7.SetBinding(Label.TextProperty, "Sunday");

            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    l1.FontFamily = "Handlee-Regular.ttf";
                    l2.FontFamily = "Handlee-Regular.ttf";
                    l3.FontFamily = "Handlee-Regular.ttf";
                    l4.FontFamily = "Handlee-Regular.ttf";
                    l5.FontFamily = "Handlee-Regular.ttf";
                    l6.FontFamily = "Handlee-Regular.ttf";
                    l7.FontFamily = "Handlee-Regular.ttf";
                    break;
                case Device.Android:
                    l1.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l2.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l3.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l4.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l5.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l6.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l7.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
                case Device.UWP:
                    l1.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l2.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l3.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l4.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l5.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l6.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    l7.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
            }

            Children.Add(b1, 0, 0);
            Children.Add(b2, 1, 0);
            Children.Add(b3, 2, 0);
            Children.Add(b4, 3, 0);
            Children.Add(b5, 4, 0);
            Children.Add(b6, 5, 0);
            Children.Add(b7, 6, 0);

            Children.Add(t1, 0, 0);
            Children.Add(t2, 1, 0);
            Children.Add(t3, 2, 0);
            Children.Add(t4, 3, 0);
            Children.Add(t5, 4, 0);
            Children.Add(t6, 5, 0);
            Children.Add(t7, 6, 0);

            Children.Add(l1, 0, 1);
            Children.Add(l2, 1, 1);
            Children.Add(l3, 2, 1);
            Children.Add(l4, 3, 1);
            Children.Add(l5, 4, 1);
            Children.Add(l6, 5, 1);
            Children.Add(l7, 6, 1);

            Children.Add(b8, 0, 1);
            Children.Add(b9, 1, 1);
            Children.Add(b10, 2, 1);
            Children.Add(b11, 3, 1);
            Children.Add(b12, 4, 1);
            Children.Add(b13, 5, 1);
            Children.Add(b14, 6, 1);


            b1.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Monday++;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };


            b2.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Tuesday++;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b3.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Wednesday++;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b4.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Thursday++;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b5.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Friday++;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b6.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Saturday++;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b7.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Sunday++;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b8.Clicked += (s, e) =>
             {
                 ViewModel.ClassVMInstance.Monday--;
                 if (updateflag)
                 {
                     ViewModel.ClassVMInstance.UpdateRepeatData(id);
                 }
             };

            b9.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Tuesday--;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b10.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Wednesday--;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b11.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Thursday--;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b12.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Friday--;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };

            b13.Clicked += (s, e) =>
             {
                 ViewModel.ClassVMInstance.Saturday--;
                 if (updateflag)
                 {
                     ViewModel.ClassVMInstance.UpdateRepeatData(id);
                 }
             };

            b14.Clicked += (s, e) =>
            {
                ViewModel.ClassVMInstance.Sunday--;
                if (updateflag)
                {
                    ViewModel.ClassVMInstance.UpdateRepeatData(id);
                }
            };
        }
    }
}