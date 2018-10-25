using Acr.UserDialogs;
using Plugin.Notifications;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.ListView.XForms;
using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Bunk_Master
{
    public class HomeTabPage : ContentPage
    {
        public static AddFormView AddFormViewInstance = new AddFormView();
        ClassModel obj = new ClassModel();

        public HomeTabPage()
        {
            ViewModel.Scheduler.AutoDB();
            Title = "\t\tBunk Master";
            BindingContext = ViewModel.ClassVMInstance;
            ToolbarItems.Add(new ToolbarItem { Text = "Add+\t", Command = new Command(AddPagePopup_Tapped) });

            AbsoluteLayout pabsoluteLayout = new AbsoluteLayout();
            StackLayout pstackLayout = new StackLayout { BackgroundColor = Color.FromHex("#53515b"), VerticalOptions = LayoutOptions.FillAndExpand };
            Grid FooterGrid = new Grid { Margin = 0 };

            AbsoluteLayout.SetLayoutFlags(pstackLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutFlags(FooterGrid, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(pstackLayout, new Rectangle(0, 0, 1, 1));

            var listView = new SfListView { ItemSize = 120, ItemSpacing = new Thickness(0, 10, 0, 0) };
            listView.ItemsSource = ViewModel.ClassVMInstance.Records;
            listView.HeaderSize = 120;
            listView.IsStickyHeader = true;
            listView.FooterSize = 70;
            listView.IsStickyFooter = true;
            listView.SelectionMode = SelectionMode.None;
            listView.AllowSwiping = true;
            

            listView.SwipeEnded += ListView_SwipeEnded;

            listView.ItemTemplate = new DataTemplate(() =>
            {
                var listItemWidth = this.Width;
                BindingContext = ViewModel.ClassVMInstance;

                //****INIT****
                var alay0 = new AbsoluteLayout { Margin = new Thickness(20, 0, 0, 0),VerticalOptions=LayoutOptions.FillAndExpand,HorizontalOptions=LayoutOptions.FillAndExpand };//root alay
                var cView = new BindableSKCanvasView { HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.FillAndExpand };

                var gridBtn = new Grid();
                var missedButton = new Button { Image = "missed_icon.png", BorderWidth = 0, BackgroundColor = Color.Transparent, Margin = new Thickness(0) };
                var cancelButton = new Button { Image = "cancel_icon.png", BackgroundColor = Color.Transparent, };
                var extraButton = new Button { Image = "extra_icon.png", BackgroundColor = Color.Transparent, };

                var classnamelabel = new Label { FontSize = 30, HorizontalOptions = LayoutOptions.CenterAndExpand };

                var percentLabel = new Label
                {
                    Text = "00%",
                    TextColor = Color.Black,
                    FontSize = 30,
                    WidthRequest = 90,
                    HeightRequest = 70,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                var presentLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                };
                var absentLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 13,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                };
                var totalLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 13,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    VerticalTextAlignment = TextAlignment.Start,
                    HorizontalTextAlignment = TextAlignment.Start,
                };

                var calay = new AbsoluteLayout { HorizontalOptions= LayoutOptions.FillAndExpand,VerticalOptions= LayoutOptions.FillAndExpand};


                //****FLAGS****

                AbsoluteLayout.SetLayoutFlags(gridBtn, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutFlags(classnamelabel, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutFlags(percentLabel, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutFlags(absentLabel, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutFlags(presentLabel, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutFlags(totalLabel, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutFlags(cView, AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutFlags(calay, AbsoluteLayoutFlags.PositionProportional);

                AbsoluteLayout.SetLayoutBounds(cView, new Rectangle(.5, .5, listItemWidth-25, 120));

                //****DEFINITIONS****
                gridBtn.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(40)
                });
                gridBtn.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(40)
                });
                gridBtn.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(40)
                });

                gridBtn.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
                gridBtn.ColumnSpacing = 0;
                gridBtn.RowSpacing = 0;
                gridBtn.Margin = new Thickness(40, 0);

                //****BINDINGS****
                classnamelabel.SetBinding(Label.TextProperty, "classname");
                absentLabel.SetBinding(Label.TextProperty, ".", BindingMode.OneWay, new IGetPresentConverter());
                presentLabel.SetBinding(Label.TextProperty, "presentnos", BindingMode.Default, null, stringFormat: "Present: {0}");
                totalLabel.SetBinding(Label.TextProperty, "totnos", BindingMode.Default, null, stringFormat: "TOTAL\n\t\t{0}");
                percentLabel.SetBinding(Label.TextProperty, ".", BindingMode.OneWay, new IGetPercentConverter());
                cView.SetBinding(BindableSKCanvasView.ColorProperty, "color", BindingMode.OneWay, new IStrToSFColorConverter());

                //****GESTURES & EVENTS****
                var gesturealay = new TapGestureRecognizer();
                gesturealay.Tapped += (s, e) => { AlayRecognizer_Tapped(s, e); };
                alay0.GestureRecognizers.Add(gesturealay);

                missedButton.Clicked += (sender, e) =>
                {
                    var item = sender as Button;
                    obj = item.Parent.BindingContext as ClassModel;
                    ViewModel.ClassVMInstance.DatesVMInstance.SelDB(obj.ID, false);
                    ViewModel.ClassVMInstance.DatesVMInstance.Missed(DateTime.Today);
                    UserDialogs.Instance.Toast(new ToastConfig("Class Missed Today"));
                    CrossNotifications.Current.Vibrate();
                    
                };

                cancelButton.Clicked += (sender, e) =>
                {
                    var item = sender as Button;
                    obj = item.Parent.BindingContext as ClassModel;
                    ViewModel.ClassVMInstance.DatesVMInstance.SelDB(obj.ID, false);
                    ViewModel.ClassVMInstance.DatesVMInstance.Cancelled(DateTime.Today);
                    UserDialogs.Instance.Toast(new ToastConfig("Class Cancelled Today"));
                    CrossNotifications.Current.Vibrate();
                };

                extraButton.Clicked += (sender, e) =>
                {
                    var item = sender as Button;
                    obj = item.Parent.BindingContext as ClassModel;
                    ViewModel.ClassVMInstance.DatesVMInstance.SelDB(obj.ID, false);
                    ViewModel.ClassVMInstance.DatesVMInstance.Extra(DateTime.Today);
                    UserDialogs.Instance.Toast(new ToastConfig("Extra Class Added"));
                    CrossNotifications.Current.Vibrate();
                };

                cView.PaintSurface += CView_PaintSurface;


                //****Fonts****
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        classnamelabel.FontFamily = "Handlee-Regular.ttf";
                        percentLabel.FontFamily = "Handlee-Regular.ttf";
                        absentLabel.FontFamily = "Handlee-Regular.ttf";
                        presentLabel.FontFamily = "Handlee-Regular.ttf";
                        totalLabel.FontFamily = "Handlee-Regular.ttf";
                        break;
                    case Device.Android:
                        classnamelabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        percentLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        absentLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        presentLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        totalLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        break;
                    case Device.UWP:
                        classnamelabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        percentLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        absentLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        presentLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        totalLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                        break;
                    default:
                        break;
                }

                //****SET CHILD****
                gridBtn.Children.Add(missedButton, 0, 0);
                gridBtn.Children.Add(cancelButton, 1, 0);
                gridBtn.Children.Add(extraButton, 2, 0);

                calay.Children.Add(cView);

                alay0.Children.Add(calay, new Point(0, 0));
                alay0.Children.Add(gridBtn, new Point(1, .8));
                alay0.Children.Add(classnamelabel, new Point(.1, .2));
                alay0.Children.Add(percentLabel, new Point(.7, .1));
                //alay0.Children.Add(infoLabel, new Point(0.1, .75));
                alay0.Children.Add(presentLabel, new Point(0.1, .6));
                alay0.Children.Add(absentLabel, new Point(0.1, .8));
                alay0.Children.Add(totalLabel, new Point(0.35, .75));

                
                //****RET****
                return alay0;
            });


            listView.LeftSwipeTemplate = new DataTemplate(() =>
            {

                var grid = new Grid();

                var grid1 = new Grid()
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Fill
                };
                var deleteGrid = new Grid() { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
                var deleteImage = new Image() { BackgroundColor = Color.Transparent, HeightRequest = 50, WidthRequest = 50 };
                deleteImage.Source = ImageSource.FromResource("Bunk_Master.delete_32px.png");
                deleteGrid.Children.Add(deleteImage);
                grid1.Children.Add(deleteGrid);

                grid.Children.Add(grid1);
                var delete_gesture = new TapGestureRecognizer();
                delete_gesture.Tapped += Delete_gesture_Tapped;
                deleteImage.GestureRecognizers.Add(delete_gesture);

                return grid;
            });


            listView.HeaderTemplate = new DataTemplate(() =>
              {
                  BindingContext = ViewModel.ClassVMInstance;

                  //****INIT****

                  var alay0 = new AbsoluteLayout { Margin = new Thickness(20, 0, 0, 0), };//root alay

                  var frame = new Frame { CornerRadius = 20, HasShadow = true, IsClippedToBounds = true, Padding = new Thickness(-1, -5), BackgroundColor = Color.Transparent };
                  var btn = new Button { Text = "Bunked Today", TextColor = Color.White, BackgroundColor = Color.Transparent };

                  var classnamelabel = new Label { Text = "Overall", TextColor = Color.White, FontSize = 30, HorizontalOptions = LayoutOptions.CenterAndExpand };

                  var percentLabel = new Label
                  {
                      Text = "00%",
                      TextColor = Color.White,
                      FontSize = 30,
                      WidthRequest = 90,
                      HeightRequest = 70,
                      VerticalOptions = LayoutOptions.CenterAndExpand,
                      HorizontalOptions = LayoutOptions.CenterAndExpand,
                      VerticalTextAlignment = TextAlignment.Center,
                      HorizontalTextAlignment = TextAlignment.Center
                  };

                  var presentLabel = new Label
                  {
                      TextColor = Color.White,
                      FontSize = 12,
                      HorizontalOptions = LayoutOptions.CenterAndExpand,
                      VerticalOptions = LayoutOptions.CenterAndExpand,
                      VerticalTextAlignment = TextAlignment.Start,
                      HorizontalTextAlignment = TextAlignment.Start,
                  };

                  var absentLabel = new Label
                  {
                      TextColor = Color.White,
                      FontSize = 13,
                      HorizontalOptions = LayoutOptions.CenterAndExpand,
                      VerticalOptions = LayoutOptions.CenterAndExpand,
                      VerticalTextAlignment = TextAlignment.Start,
                      HorizontalTextAlignment = TextAlignment.Start,
                  };

                  var totalLabel = new Label
                  {
                      TextColor = Color.White,
                      FontSize = 13,
                      HorizontalOptions = LayoutOptions.CenterAndExpand,
                      VerticalOptions = LayoutOptions.CenterAndExpand,
                      VerticalTextAlignment = TextAlignment.Start,
                      HorizontalTextAlignment = TextAlignment.Start,
                  };


                  //****FLAGS****

                  AbsoluteLayout.SetLayoutFlags(frame, AbsoluteLayoutFlags.PositionProportional);
                  AbsoluteLayout.SetLayoutFlags(classnamelabel, AbsoluteLayoutFlags.PositionProportional);
                  AbsoluteLayout.SetLayoutFlags(percentLabel, AbsoluteLayoutFlags.PositionProportional);
                  AbsoluteLayout.SetLayoutFlags(absentLabel, AbsoluteLayoutFlags.PositionProportional);
                  AbsoluteLayout.SetLayoutFlags(presentLabel, AbsoluteLayoutFlags.PositionProportional);
                  AbsoluteLayout.SetLayoutFlags(totalLabel, AbsoluteLayoutFlags.PositionProportional);


                  //****BINDINGS****

                  absentLabel.SetBinding(Label.TextProperty, "Overab", BindingMode.Default, null, "Absent: {0}");
                  presentLabel.SetBinding(Label.TextProperty, "Overpres", BindingMode.Default, null, "Present: {0}");
                  totalLabel.SetBinding(Label.TextProperty, "Overtot", BindingMode.Default, null, "TOTAL\n\t\t{0}");
                  percentLabel.SetBinding(Label.TextProperty, "Overperc", BindingMode.Default, null, "{0}");

                  //****GESTURES & EVENTS****

                  var gesturealay = new TapGestureRecognizer();
                  gesturealay.Tapped += (s, e) => { HeaderRecognizer_Tapped(s, e); };
                  alay0.GestureRecognizers.Add(gesturealay);

                  btn.Clicked += Btn_Clicked;

                  //****Fonts****

                  switch (Device.RuntimePlatform)
                  {
                      case Device.iOS:
                          classnamelabel.FontFamily = "Handlee-Regular.ttf";
                          percentLabel.FontFamily = "Handlee-Regular.ttf";
                          absentLabel.FontFamily = "Handlee-Regular.ttf";
                          presentLabel.FontFamily = "Handlee-Regular.ttf";
                          totalLabel.FontFamily = "Handlee-Regular.ttf";
                          break;
                      case Device.Android:
                          classnamelabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          percentLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          absentLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          presentLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          totalLabel.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          break;
                      case Device.UWP:
                          classnamelabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          percentLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          absentLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          presentLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          totalLabel.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                          break;
                      default:
                          break;
                  }

                  //****SET CHILD****
                  frame.Content = btn;

                  alay0.Children.Add(frame, new Point(.8, .8));
                  alay0.Children.Add(classnamelabel, new Point(.1, .2));
                  alay0.Children.Add(percentLabel, new Point(.7, .1));
                  alay0.Children.Add(presentLabel, new Point(0.1, .6));
                  alay0.Children.Add(absentLabel, new Point(0.1, .8));
                  alay0.Children.Add(totalLabel, new Point(0.35, .75));

                  //****RET****
                  return alay0;
              });

            listView.ItemTapped += (sender, e) =>
            {
                //better to create properties in graphinfo and set them rathar than creating a new object every time SEE UP
                //use this to get id, handle failure !

                //syncfusion problem have to handle the clicks for button and item separately in android
                listView_ItemTapped(sender, e);

            };

            pstackLayout.Children.Add(listView);
            pabsoluteLayout.Children.Add(pstackLayout);

            Content = pabsoluteLayout;
        }




        //****CALL-BACKS****

        private void Btn_Clicked(object sender, EventArgs e)
        {
            ViewModel.ClassVMInstance.DatesVMInstance.DayAbsent();
        }

        private void CView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvasobj = sender as BindableSKCanvasView;
            SKImageInfo info = e.Info;
            float width = info.Width;
            float height = info.Height;
            var ribbonHeight = height / 3;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            SKPath sKBackGrnd = new SKPath();
            SKPath sKRibbon = new SKPath { Convexity = SKPathConvexity.Convex };
            SKPath sKfold = new SKPath { };

            sKBackGrnd.AddRoundRect(new SKRect(10f, .9f * height, width - 30f, 10f), 20f, 20f);

            sKRibbon.MoveTo(.5f * info.Width, .5f * info.Height);//start
            sKRibbon.LineTo(sKRibbon.LastPoint.X + 30f, sKRibbon.LastPoint.Y + ribbonHeight/2);//left frst diag
            sKRibbon.LineTo(sKRibbon.LastPoint.X - 30f, sKRibbon.LastPoint.Y + ribbonHeight/2);//left second diag
            sKRibbon.LineTo(info.Width, sKRibbon.LastPoint.Y);//bottom straight

            sKfold.MoveTo(info.Width, .5f * info.Height + ribbonHeight);//start the fold point from corner
            sKfold.LineTo(info.Width - 30f, sKfold.LastPoint.Y + 30f);//fold diag
            sKfold.LineTo(sKfold.LastPoint.X, sKfold.LastPoint.Y - 30f);//fold up
            sKfold.LineTo(info.Width + 30f, sKfold.LastPoint.Y);//fold upper straight
            sKfold.Close();

            sKRibbon.ArcTo(info.Width, .5f * info.Height, .5f * info.Width, .5f * info.Height, 30f);
            sKRibbon.Close();

            SKPaint sKPaintBG = new SKPaint { Color = canvasobj.Color };
            SKPaint sKPaintRibbon = new SKPaint { Color = SKColors.PaleVioletRed, StrokeJoin = SKStrokeJoin.Round };
            SKPaint sKPaintFold = new SKPaint { Color = SKColor.FromHsl(340f, 46f, 58f), StrokeJoin = SKStrokeJoin.Round };

            canvas.DrawPath(sKBackGrnd, sKPaintBG);
            canvas.DrawPath(sKRibbon, sKPaintRibbon);
            canvas.DrawPath(sKfold, sKPaintFold);
        }


        private void AddPagePopup_Tapped()
        {
            //NavigationPage inputView = new NavigationPage(new PopupView());
            ViewModel.ClassVMInstance.ClearForm(); // to clear repeat data from graph view settings
            Navigation.PushModalAsync(AddFormViewInstance);
        }

        private void Delete_gesture_Tapped(object sender, EventArgs e)
        {
            ViewModel.ClassVMInstance.DeleteCommand.Execute(obj.ToString());

        }

        private void listView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                e.Handled = true;

            }
        }

        private void AlayRecognizer_Tapped(object sender, EventArgs e)
        {
            var alay_t = sender as AbsoluteLayout;
            var item = alay_t.BindingContext as ClassModel;
            Navigation.PushAsync(new GraphInfo(item.ToString()));
        }

        private void HeaderRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OverallPage());

        }

        private void ListView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            obj = e.ItemData as ClassModel;
            if (e.SwipeOffset >= 100)
            {
                var lv = sender as SfListView;
                lv.ResetSwipe();
            }
        }
    }
}