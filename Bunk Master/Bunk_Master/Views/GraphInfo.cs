using Syncfusion.SfChart.XForms;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;
using Syncfusion.ListView.XForms;

namespace Bunk_Master
{
    public class GraphInfo : ContentPage
    {

        GraphGen GraphGenInstance { get; } = new GraphGen();


        public GraphInfo(string str)
        {

            var itemString = str;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            var id = int.Parse(columns[0]);
            var presentnos = int.Parse(columns[1]);
            var totnos = int.Parse(columns[2]);
            var classname = columns[3];
            var mincutoff = int.Parse(columns[4]);
            var repeat_data = columns[5];
            var startdate = DateTime.Parse(columns[6]);

            //for the sched repeat switches in settings need to put the current data in so that binding works within ScheSwitch()
            #region repeatsplitdata 

            var daydata = repeat_data.Split('.').Select(i => i.Trim()).ToList();
            ViewModel.ClassVMInstance.Monday = int.Parse(daydata[0]);
            ViewModel.ClassVMInstance.Tuesday = int.Parse(daydata[1]);
            ViewModel.ClassVMInstance.Wednesday = int.Parse(daydata[2]);
            ViewModel.ClassVMInstance.Thursday = int.Parse(daydata[3]);
            ViewModel.ClassVMInstance.Friday = int.Parse(daydata[4]);
            ViewModel.ClassVMInstance.Saturday = int.Parse(daydata[5]);
            ViewModel.ClassVMInstance.Sunday = int.Parse(daydata[6]);

            ViewModel.ClassVMInstance.repeat_data = repeat_data;
            #endregion



            this.Title = classname;
            
            ViewModel.ClassVMInstance.DatesVMInstance.SelDB(id, true);


            var alay = new AbsoluteLayout() { BackgroundColor = Color.FromHex("#53515b") };
            SfChart chart = new SfChart { BackgroundColor = Color.FromRgba(255, 255, 255, 20) };
            SfTabView tabView = new SfTabView { VisibleHeaderCount = 3, TabHeight = 5, TabHeaderBackgroundColor = Color.Transparent, DisplayMode = TabDisplayMode.Text, BackgroundColor = Color.Transparent, Margin = new Thickness(0, 0, 0, -70), VerticalOptions = LayoutOptions.FillAndExpand };
            var dtgrid = new Grid();
            var dtbtgrid = new Grid();
            var grid_sch = new Grid();
            var info_grid = new Grid { BackgroundColor = Color.Transparent, RowSpacing = 0 };
            var sched_grid = new ScheSwitch(true, id);
            Grid gridtab1 = new Grid { VerticalOptions = LayoutOptions.StartAndExpand };
            Grid gridtab2 = new Grid { VerticalOptions = LayoutOptions.StartAndExpand };
            Grid gridtab3 = new Grid { VerticalOptions = LayoutOptions.StartAndExpand };

            TabItemCollection tabItemCollection = new TabItemCollection {
            new SfTabItem() {Content=gridtab1},
            new SfTabItem() {Content = gridtab2},
            new SfTabItem() { Content = gridtab3} };

            tabView.Items = tabItemCollection;

            //Initializing Primary Axis
            DateTimeAxis primaryAxis = new DateTimeAxis()
            {
                //Maximum = DateTime.Today.AddDays(7),
                IntervalType = DateTimeIntervalType.Days,
                Interval = 1,
                LabelStyle = new ChartAxisLabelStyle { TextColor = Color.White },
            };
            chart.PrimaryAxis = primaryAxis;

            //Initializing Secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis() { Minimum = 0, Maximum = 100, LabelStyle = new ChartAxisLabelStyle { TextColor = Color.White } };
            chart.SecondaryAxis = secondaryAxis;

            var DailyGraph = GraphGenInstance.GenerateLineGraph(ViewModel.ClassVMInstance.DatesVMInstance, ViewModel.ClassVMInstance.DatesVMInstance.Percent, "gpercent", "gdate");
            DailyGraph.StrokeWidth = 1.5;
            DailyGraph.Color = Color.SpringGreen;
            var MinGraph = GraphGenInstance.GenerateFastGraph(ViewModel.ClassVMInstance.DatesVMInstance, ViewModel.ClassVMInstance.DatesVMInstance.MinInstance, "gpercent", "gdate");
            MinGraph.Color = Color.Black;
            MinGraph.StrokeWidth = 1;
            var PredGraph = GraphGenInstance.GenerateFastGraph(ViewModel.ClassVMInstance.DatesVMInstance, ViewModel.ClassVMInstance.DatesVMInstance.Predict, "gpercent", "gdate");
            PredGraph.EnableAnimation = true;
            PredGraph.Color = Color.FromHex("#ff1717");
            PredGraph.AnimationDuration = 1;
            PredGraph.StrokeWidth = 1.5;
            chart.Series.Add(DailyGraph);
            chart.Series.Add(MinGraph);
            chart.Series.Add(PredGraph);

            Label lbl_strtdt = new Label { Text = "Start Date", TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label lbl_classinfo = new Label { FontSize = 20, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label label1 = new Label { Text = "Attend Today", FontSize = 20, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label label2 = new Label { FontSize = 25, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center };
            Label label3 = new Label { Text = "Bunk Today", FontSize = 20, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label label4 = new Label { FontSize = 25, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center };
            Label label5 = new Label { Text = "Attend Tomorrow", FontSize = 20, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label label6 = new Label { FontSize = 25, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center };
            Label label7 = new Label { Text = "Bunk Tomorrow", FontSize = 20, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            Label label8 = new Label { FontSize = 25, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center };
            Label label9 = new Label { Text = "(i) Assuming You Atteded Today",FontSize = 10, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center };
            Label label10 = new Label { Text = "Custom Calculations\n\n   Coming Soon!", FontSize = 25, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center,VerticalTextAlignment=TextAlignment.Center };

            var lbl_sch = new Label { Text = "Class Count In A Week", FontSize = 20, HorizontalOptions = LayoutOptions.CenterAndExpand, TextColor = Color.White, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.End, InputTransparent = true };
            //var sw_sch = new Switch { };
            var dtpicker = new DatePicker { Date = startdate, TextColor = Color.White, Format = "d-MMMM-yyyy" };
            var bt_editdt = new Button { Text = "Edit Dates", TextColor = Color.White, BackgroundColor = Color.Transparent };
            var bt_setg = new Button { Text = "Settings", TextColor = Color.White, BackgroundColor = Color.Transparent };
            var img_back = new Image { Source = "backbtn.png", Scale = .5 };
            var img_down = new Image { Source = "downbtn.png", Scale = .5, HorizontalOptions = LayoutOptions.StartAndExpand };
            var tap_back = new TapGestureRecognizer();
            var tap_down = new TapGestureRecognizer();
            img_back.GestureRecognizers.Add(tap_back);
            img_down.GestureRecognizers.Add(tap_down);
            var bt_dtadd = new Button { Text = "Add/Replace Date", TextColor = Color.White, BackgroundColor = Color.Transparent };
            var lv_dates = new SfListView {Margin=10 };

            Frame frame1 = new Frame { CornerRadius = 20, HasShadow = true, IsClippedToBounds = true, Padding = -10, Margin = new Thickness(25, 0), BackgroundColor = Color.Transparent };
            frame1.Content = bt_editdt;
            Frame frame2 = new Frame { CornerRadius = 20, HasShadow = true, IsClippedToBounds = true, Padding = -10, Margin = new Thickness(25, 0, 25, 25), BackgroundColor = Color.Transparent };
            frame2.Content = bt_setg;
            Frame frame3 = new Frame { CornerRadius = 20, HasShadow = true, IsClippedToBounds = true, Padding = new Thickness(-1), Margin = new Thickness(0, 5, 75, 5), BackgroundColor = Color.Transparent, HorizontalOptions = LayoutOptions.CenterAndExpand };
            frame3.Content = bt_dtadd;

            dtpicker.DateSelected += (sender, e) =>
              {
                  ViewModel.ClassVMInstance.UpdateStartDate(id, e.NewDate, e.OldDate);
              };

            lv_dates.ItemsSource = ViewModel.ClassVMInstance.DatesVMInstance.Dates;

            
            lv_dates.IsStickyHeader = true;
            lv_dates.HeaderTemplate = new DataTemplate(() =>
            {
                Grid grid = new Grid { Padding = new Thickness(20, 0, 0, 0),BackgroundColor=Color.DodgerBlue };

                var lbl_id = new Label { Text = "ID",FontSize=20, TextColor = Color.White,VerticalTextAlignment=TextAlignment.Center };
                var lbl_dt = new Label { Text = "Date", FontSize = 20, TextColor = Color.White, VerticalTextAlignment = TextAlignment.Center };
                var lbl_ab = new Label { Text = "Absent", FontSize = 20, TextColor = Color.White, VerticalTextAlignment = TextAlignment.Center };
                var lbl_tot = new Label { Text = "Total", FontSize = 20, TextColor = Color.White, VerticalTextAlignment = TextAlignment.Center };

                grid.ColumnDefinitions.Add(new ColumnDefinition {Width=GridLength.Star });
                grid.ColumnDefinitions.Add(new ColumnDefinition { });
                grid.ColumnDefinitions.Add(new ColumnDefinition { });
                grid.ColumnDefinitions.Add(new ColumnDefinition { });
                grid.RowDefinitions.Add(new RowDefinition { });

                grid.Children.Add(lbl_id, 0, 0);
                grid.Children.Add(lbl_dt, 1, 0);
                grid.Children.Add(lbl_ab, 2, 0);
                grid.Children.Add(lbl_tot, 3, 0);

                return grid;
            });

            lv_dates.ItemTemplate = new DataTemplate(() =>
            {
            Grid grid = new Grid { Padding = new Thickness(20, 0, 0, 0) };

            var lbl_id = new Label { TextColor = Color.White };
            var lbl_dt = new Label { TextColor = Color.White };
            var lbl_ab = new Label { TextColor = Color.White };
            var lbl_tot = new Label { TextColor = Color.White };

            grid.ColumnDefinitions.Add(new ColumnDefinition { });
            grid.ColumnDefinitions.Add(new ColumnDefinition { });
            grid.ColumnDefinitions.Add(new ColumnDefinition { });
            grid.ColumnDefinitions.Add(new ColumnDefinition { });
            grid.RowDefinitions.Add(new RowDefinition { });

            lbl_id.SetBinding(Label.TextProperty, "ID");
            lbl_dt.SetBinding(Label.TextProperty, "date", converter: new IDateTextConverter());
            lbl_ab.SetBinding(Label.TextProperty, "dt_ab");
            lbl_tot.SetBinding(Label.TextProperty, "dt_tot");
            grid.SetBinding(BackgroundColorProperty, new Binding(".", converter:new IndexToColorConverter(),converterParameter: lv_dates));

                grid.Children.Add(lbl_id, 0, 0);
                grid.Children.Add(lbl_dt, 1, 0);
                grid.Children.Add(lbl_ab, 2, 0);
                grid.Children.Add(lbl_tot, 3, 0);

                return grid;
            });

            //****BINDINGS****

            //lv_dates.BindingContext = ViewModel.ClassVMInstance.DatesVMInstance;
            //lv_dates.SetBinding(ListView.ItemsSourceProperty, "Dates");

            lbl_classinfo.BindingContext = ViewModel.ClassVMInstance.DatesVMInstance.OtelemetInstance[0].Daycount;
            lbl_classinfo.SetBinding(Label.TextProperty, ".",converter:new IClassNeededConverter());
            label2.BindingContext = ViewModel.ClassVMInstance.DatesVMInstance.TelemetInstance[0].Tomattnd;
            label2.SetBinding(Label.TextProperty, ".", stringFormat: "{0}%");
            label4.BindingContext = ViewModel.ClassVMInstance.DatesVMInstance.TelemetInstance[0].Tdybnk;
            label4.SetBinding(Label.TextProperty, ".", stringFormat: "{0}%");

            label6.BindingContext = ViewModel.ClassVMInstance.DatesVMInstance.TelemetInstance[0].Tomattnd;
            label6.SetBinding(Label.TextProperty, ".", stringFormat: "{0}%");
            label8.BindingContext = ViewModel.ClassVMInstance.DatesVMInstance.TelemetInstance[0].Tombnk;
            label8.SetBinding(Label.TextProperty, ".", stringFormat: "{0}%");

            //****DEFINITIONS****

            gridtab1.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            gridtab1.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            gridtab1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gridtab1.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gridtab2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            gridtab2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            gridtab2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gridtab2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            gridtab2.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            //gridtab3.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            //gridtab3.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            dtgrid.ColumnDefinitions.Add(new ColumnDefinition { });
            dtgrid.ColumnDefinitions.Add(new ColumnDefinition { });
            dtgrid.RowDefinitions.Add(new RowDefinition { });
            dtbtgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            dtbtgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            dtbtgrid.RowDefinitions.Add(new RowDefinition { });
            grid_sch.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            grid_sch.ColumnDefinitions.Add(new ColumnDefinition { });
            //grid_sch.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            grid_sch.RowDefinitions.Add(new RowDefinition { });


            info_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            info_grid.RowDefinitions.Add(new RowDefinition { Height = 50 });
            info_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            info_grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });



            //****ASSIGNMENTS****

            gridtab1.Children.Add(label1, 0, 0);
            gridtab1.Children.Add(label2, 0, 1);
            gridtab1.Children.Add(label3, 1, 0);
            gridtab1.Children.Add(label4, 1, 1);

            gridtab2.Children.Add(label5, 0, 0);
            gridtab2.Children.Add(label6, 0, 1);
            gridtab2.Children.Add(label7, 1, 0);
            gridtab2.Children.Add(label8, 1, 1);
            gridtab2.Children.Add(label9, 0, 2);
            Grid.SetColumnSpan(label9, 2);

            gridtab3.Children.Add(label10);

            dtgrid.Children.Add(lbl_strtdt, 0, 0);
            dtgrid.Children.Add(dtpicker, 1, 0);

            dtbtgrid.Children.Add(img_down, 0, 0);
            dtbtgrid.Children.Add(frame3, 1, 0);
            grid_sch.Children.Add(img_back, 0, 0);

            info_grid.Children.Add(lbl_classinfo, 0, 0);
            info_grid.Children.Add(tabView, 0, 1);
            info_grid.Children.Add(frame2, 0, 3);

            //MUST BE SET AFTER ADDING VIEW
            //Grid.SetColumnSpan(frame3, 2);
            //Grid.SetColumnSpan(lbl_classinfo, 2);
            //Grid.SetColumnSpan(frame1, 2);
            //Grid.SetColumnSpan(frame2, 2);

            dtbtgrid.Scale = .2;
            lv_dates.Scale = .2;
            dtbtgrid.IsEnabled = false;
            lv_dates.IsEnabled = false;
            dtbtgrid.IsVisible = false;
            lv_dates.IsVisible = false;



            //****BUTTON EVENTS****

            bt_dtadd.Clicked += async (sender, e) =>
            {
                await PopupNavigation.Instance.PushAsync(new PopupViewD(id));
            };

            void disable1(double d, bool b)
            {
                grid_sch.IsEnabled = false;
                lbl_sch.IsEnabled = false;
                sched_grid.IsEnabled = false;
                dtgrid.IsEnabled = false;
                frame1.IsEnabled = false;

                grid_sch.IsVisible = false;
                lbl_sch.IsVisible = false;
                sched_grid.IsVisible = false;
                dtgrid.IsVisible = false;
                frame1.IsVisible = false;
            }

            void disable2(double d, bool b)
            {
                dtbtgrid.IsEnabled = false;
                lv_dates.IsEnabled = false;
                dtbtgrid.IsVisible = false;
                lv_dates.IsVisible = false;
            }


            bt_setg.Clicked += (sender, e) =>
             {
                 var ani = new Animation();//0 is current pos of element
                 var ani_1 = new Animation(v => info_grid.TranslationX = v, 0, alay.Width, Easing.BounceOut);
                 var ani_2 = new Animation(v => grid_sch.TranslationX = v, 0, alay.Width, Easing.BounceOut);
                 var ani_3 = new Animation(v => lbl_sch.TranslationX = v, 0, alay.Width, Easing.BounceOut);
                 var ani_7 = new Animation(v => sched_grid.TranslationX = v, 0, -alay.Width, Easing.BounceOut);
                 var ani_4 = new Animation(v => dtgrid.TranslationX = v, 0, -alay.Width, Easing.BounceOut);
                 var ani_6 = new Animation(v => frame1.TranslationX = v, 0, alay.Width, Easing.BounceOut);

                 ani.Add(0, 1, ani_1);
                 ani.Add(0, 1, ani_2);
                 ani.Add(0, 1, ani_3);
                 ani.Add(0, 1, ani_4);
                 ani.Add(0, 1, ani_6);
                 ani.Add(0, 1, ani_7);

                 ani.Commit(bt_setg, "Settings", length: 700);
             };


            tap_back.Tapped += (sender, e) =>
            {
                var ani = new Animation();//0 is current pos of element
                var ani_1 = new Animation(v => info_grid.TranslationX = v, alay.Width, 0, Easing.BounceOut);
                var ani_2 = new Animation(v => grid_sch.TranslationX = v, alay.Width, 0, Easing.BounceOut);
                var ani_3 = new Animation(v => lbl_sch.TranslationX = v, alay.Width, 0, Easing.BounceOut);
                var ani_7 = new Animation(v => sched_grid.TranslationX = v, -alay.Width, 0, Easing.BounceOut);
                var ani_4 = new Animation(v => dtgrid.TranslationX = v, -alay.Width, 0, Easing.BounceOut);
                var ani_6 = new Animation(v => frame1.TranslationX = v, alay.Width, 0, Easing.BounceOut);


                ani.Add(0, 1, ani_1);
                ani.Add(0, 1, ani_2);
                ani.Add(0, 1, ani_3);
                ani.Add(0, 1, ani_4);
                ani.Add(0, 1, ani_6);
                ani.Add(0, 1, ani_7);

                ani.Commit(img_back, "Back", length: 700);
            };

            bt_editdt.Clicked += (sender, e) =>
            {
                dtbtgrid.IsEnabled = true;
                lv_dates.IsEnabled = true;
                dtbtgrid.IsVisible = true;
                lv_dates.IsVisible = true;

                double end = alay.Height / 2;
                var ani = new Animation();
                //var ani2 = new Animation();//0 is current pos of element 
                var ani_1 = new Animation(v => grid_sch.Scale = v, 1, .2, Easing.SinIn);
                var ani_7 = new Animation(v => lbl_sch.Scale = v, 1, .2, Easing.SinIn);
                var ani_6 = new Animation(v => sched_grid.Scale = v, 1, .2, Easing.SinIn);
                var ani_2 = new Animation(v => dtgrid.Scale = v, 1, .2, Easing.SinIn);
                var ani_3 = new Animation(v => frame1.Scale = v, 1, .2, Easing.SinIn);
                //pop out
                var ani_4 = new Animation(v => dtbtgrid.Scale = v, .2, 1, Easing.SinIn);
                var ani_5 = new Animation(v => lv_dates.Scale = v, .2, 1, Easing.SinIn);


                ani.Add(0, 1, ani_1);
                ani.Add(0, 1, ani_2);
                ani.Add(0, 1, ani_3);
                ani.Add(0, 1, ani_6);
                ani.Add(0, 1, ani_7);

                ani.Add(0, 1, ani_4);
                ani.Add(0, 1, ani_5);

                ani.Commit(img_down, "pop in", length: 200, finished: disable1);
            };

            tap_down.Tapped += (sender, e) =>
            {
                grid_sch.IsEnabled = true;
                lbl_sch.IsEnabled = true;
                sched_grid.IsEnabled = true;
                dtgrid.IsEnabled = true;
                frame1.IsEnabled = true;

                grid_sch.IsVisible = true;
                lbl_sch.IsVisible = true;
                sched_grid.IsVisible = true;
                dtgrid.IsVisible = true;
                frame1.IsVisible = true;

                double end = alay.Height / 2;
                var ani = new Animation();//0 is current pos of element 
                var ani_1 = new Animation(v => grid_sch.Scale = v, .2, 1, Easing.SinIn);
                var ani_7 = new Animation(v => lbl_sch.Scale = v, .2, 1, Easing.SinIn);
                var ani_6 = new Animation(v => sched_grid.Scale = v, .2, 1, Easing.SinIn);
                var ani_2 = new Animation(v => dtgrid.Scale = v, .2, 1, Easing.SinIn);
                var ani_3 = new Animation(v => frame1.Scale = v, .2, 1, Easing.SinIn);

                var ani_4 = new Animation(v => dtbtgrid.Scale = v, 1, .2, Easing.SinIn);
                var ani_5 = new Animation(v => lv_dates.Scale = v, 1, .2, Easing.SinIn);

                ani.Add(0, 1, ani_1);
                ani.Add(0, 1, ani_2);
                ani.Add(0, 1, ani_3);
                ani.Add(0, 1, ani_4);
                ani.Add(0, 1, ani_5);
                ani.Add(0, 1, ani_6);
                ani.Add(0, 1, ani_7);

                ani.Commit(img_down, "Edit Date List", length: 200, finished: disable2);
            };


            alay.SizeChanged += (sender, e) =>
             {
                 var width = alay.Width;
                 var height = alay.Height;

                 AbsoluteLayout.SetLayoutBounds(chart, new Rectangle(0, 0, width, height / 2));
                 AbsoluteLayout.SetLayoutBounds(info_grid, new Rectangle(0, height / 2, width, (height / 2)));
                 //AbsoluteLayout.SetLayoutBounds(bt_adv, new Rectangle(0, (height / 2) + 50, width, (height / 2) - 100));
                 //AbsoluteLayout.SetLayoutBounds(bt_setg, new Rectangle(0, height - 50, width, 50));

                 AbsoluteLayout.SetLayoutBounds(grid_sch, new Rectangle(-width, height / 2, width, 50));
                 AbsoluteLayout.SetLayoutBounds(lbl_sch, new Rectangle(-width, (height / 2) + 10, width, 50));
                 AbsoluteLayout.SetLayoutBounds(sched_grid, new Rectangle(width, (height / 2) + 60, width, 120));
                 AbsoluteLayout.SetLayoutBounds(dtgrid, new Rectangle(width, (height / 2) + 145, width, 50));
                 AbsoluteLayout.SetLayoutBounds(frame1, new Rectangle(-width, height - 70, width, 50));

                 AbsoluteLayout.SetLayoutBounds(dtbtgrid, new Rectangle(0, height / 2, width, 50));
                 AbsoluteLayout.SetLayoutBounds(lv_dates, new Rectangle(0, (height / 2) + 50, width, (height / 2) - 50));

                 alay.Children.Add(info_grid);

                 alay.Children.Add(grid_sch);
                 alay.Children.Add(lbl_sch);
                 alay.Children.Add(sched_grid);
                 alay.Children.Add(dtgrid);
                 alay.Children.Add(frame1);

                 alay.Children.Add(dtbtgrid);
                 alay.Children.Add(lv_dates);
                 alay.Children.Add(chart);
             };


            Content = alay;


        }


    }


}