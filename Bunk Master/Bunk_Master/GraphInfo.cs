using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using System.Collections.ObjectModel;

namespace Bunk_Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            

            

            HomeTabPage.ClassVMInstance.DatesVMInstance.SelDB(id);
            

            var alay = new AbsoluteLayout();
            SfChart chart = new SfChart { };
            var dtgrid = new Grid();
            var dtbtgrid = new Grid();
            var grid_sch = new Grid();
            var info_grid = new Grid();//Need to use styles , minimumwidth etc to solve all ui problems
            var sched_grid = new ScheSwitch();


            //Initializing Primary Axis
            DateTimeAxis primaryAxis = new DateTimeAxis()
            {
                //Maximum = DateTime.Today.AddDays(7),
                IntervalType=DateTimeIntervalType.Days,
                Interval=1,
                LabelRotationAngle=15,
                
            };
            chart.PrimaryAxis = primaryAxis;

            //Initializing Secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis() {Minimum=0,Maximum=100 };
            chart.SecondaryAxis = secondaryAxis;

            //Zooming
            ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior();
            zoomPanBehavior.ZoomMode = ZoomMode.X;
            
            chart.ChartBehaviors.Add(zoomPanBehavior);


            var DailyGraph = GraphGenInstance.GenerateLineGraph(HomeTabPage.ClassVMInstance.DatesVMInstance, HomeTabPage.ClassVMInstance.DatesVMInstance.Percent, "gpercent", "gdate");
            DailyGraph.StrokeWidth = 1;
            var MinGraph = GraphGenInstance.GenerateFastGraph(HomeTabPage.ClassVMInstance.DatesVMInstance, HomeTabPage.ClassVMInstance.DatesVMInstance.MinInstance, "gpercent", "gdate");
            var PredGraph = GraphGenInstance.GenerateFastGraph(HomeTabPage.ClassVMInstance.DatesVMInstance, HomeTabPage.ClassVMInstance.DatesVMInstance.Predict, "gpercent", "gdate");
            PredGraph.EnableAnimation = true;
            PredGraph.AnimationDuration = .8;
            PredGraph.StrokeWidth = 1;
            chart.Series.Add(DailyGraph);
            chart.Series.Add(MinGraph);
            chart.Series.Add(PredGraph);

            

            var f1 = new Frame {OutlineColor=Color.Black,Padding=0 };
            var f2 = new Frame { OutlineColor = Color.Black, Padding = 0 };
                        
            var lbl_strtdt = new Label
            {
                Text = "Start Date",
                FontSize = 20,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            var lbl_info1 = new Label
            {
                Text = "Absent",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center,
                
            };
            var lbl_info2 = new Label
            {
                Text = "Present",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center
            };
            var lbl_info3 = new Label
            {
                Text = "Total",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center
            };
            var lbl_info4 = new Label
            {
                Text = "Info" + string.Format("{0}100", Environment.NewLine),
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin=new Thickness(2)
            };
            var lbl_info5 = new Label
            {
                Text = "Info",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center
            };
            var lbl_info6 = new Label
            {
                Text = "Info",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center
            };
            var lbl_info7 = new Label
            {
                Text = "Info",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center
            };


            var lbl_sch = new Label { Text = "  Auto Scheduler",FontSize=20,VerticalTextAlignment=TextAlignment.Center };
            var sw_sch = new Switch {  };
            var dtpicker = new DatePicker { Date = startdate, Format = "d-MMMM-yyyy" };
            var bt_editdt = new Button { Text = "Edit Dates" };
            var bt_setg = new Button { Text = "Settings" };
            var bt_back = new Button { Text = "<-" };
            var bt_dtback = new Button { Text = "V" };
            var bt_dtadd = new Button { Text = "Add/Replace Date" };
            var lv_info = new ListView {  ItemsSource = HomeTabPage.ClassVMInstance.DatesVMInstance.Percent };
            var lv_dates = new ListView {  };

            
            
            dtpicker.DateSelected += (sender, e) =>
              {
                  HomeTabPage.ClassVMInstance.UpdateStartDate(id,e.NewDate,e.OldDate);
              };

            lv_dates.BindingContext = HomeTabPage.ClassVMInstance.DatesVMInstance;
            lv_dates.SetBinding(ListView.ItemsSourceProperty,"Dates");
            
            

            f1.Content = lbl_info4;
            f2.Content = lbl_info3;

            dtgrid.ColumnDefinitions.Add(new ColumnDefinition { });
            dtgrid.ColumnDefinitions.Add(new ColumnDefinition { });
            dtgrid.RowDefinitions.Add(new RowDefinition { });
            dtbtgrid.ColumnDefinitions.Add(new ColumnDefinition { });
            dtbtgrid.ColumnDefinitions.Add(new ColumnDefinition { });
            dtbtgrid.RowDefinitions.Add(new RowDefinition { });
            grid_sch.ColumnDefinitions.Add(new ColumnDefinition {Width=50 });
            grid_sch.ColumnDefinitions.Add(new ColumnDefinition { });
            grid_sch.ColumnDefinitions.Add(new ColumnDefinition {Width=50 });
            grid_sch.RowDefinitions.Add(new RowDefinition { });
            info_grid.ColumnDefinitions.Add(new ColumnDefinition {Width=50 });
            info_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            info_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            info_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            info_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            info_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            info_grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            info_grid.RowDefinitions.Add(new RowDefinition {Height=50 });

            dtgrid.Children.Add(lbl_strtdt, 0, 0);
            dtgrid.Children.Add(dtpicker, 1, 0);
            dtbtgrid.Children.Add(bt_dtback, 0, 0);
            dtbtgrid.Children.Add(bt_dtadd, 1, 0);
            grid_sch.Children.Add(bt_back, 0, 0);
            grid_sch.Children.Add(lbl_sch, 1, 0);
            grid_sch.Children.Add(sw_sch, 2, 0);
            info_grid.Children.Add(lbl_info1, 0, 0);
            info_grid.Children.Add(lbl_info2, 1, 0);
            info_grid.Children.Add(f2, 2, 0);
            info_grid.Children.Add(f1, 3, 0);
            info_grid.Children.Add(lbl_info5, 4, 0);
            info_grid.Children.Add(lbl_info6, 5, 0);
            info_grid.Children.Add(lbl_info7, 6, 0);



            bt_dtadd.Clicked += async(sender, e) =>
            {                
                await PopupNavigation.PushAsync(new PopupViewD(id));
            };

            bt_setg.Clicked += (sender, e) =>
             {


                 var ani = new Animation();//0 is current pos of element
                 var ani_1 = new Animation(v => info_grid.TranslationX = v, 0, alay.Width, Easing.BounceOut);
                 var ani_2 = new Animation(v => grid_sch.TranslationX = v, 0, alay.Width, Easing.BounceOut);
                 var ani_3 = new Animation(v => lv_info.TranslationX = v, 0, -alay.Width, Easing.BounceOut);
                 var ani_7 = new Animation(v => sched_grid.TranslationX = v,  0,-alay.Width, Easing.BounceOut);
                 var ani_4 = new Animation(v => dtgrid.TranslationX = v, 0, -alay.Width, Easing.BounceOut);
                 var ani_5 = new Animation(v => bt_setg.TranslationX = v, 0, alay.Width, Easing.BounceOut);
                 var ani_6 = new Animation(v => bt_editdt.TranslationX = v, 0, alay.Width, Easing.BounceOut);


                 ani.Add(0, 1, ani_1);
                 ani.Add(0, 1, ani_2);
                 ani.Add(0, 1, ani_3);
                 ani.Add(0, 1, ani_4);
                 ani.Add(0, 1, ani_5);
                 ani.Add(0, 1, ani_6);
                 ani.Add(0, 1, ani_7);

                 ani.Commit(bt_setg, "Settings", length: 700);

             };


            bt_back.Clicked += (sender, e) =>
            {
                var ani = new Animation();//0 is current pos of element
                var ani_1 = new Animation(v => info_grid.TranslationX = v, alay.Width, 0, Easing.BounceOut);
                var ani_2 = new Animation(v => grid_sch.TranslationX = v, alay.Width, 0, Easing.BounceOut);
                var ani_3 = new Animation(v => lv_info.TranslationX = v, -alay.Width, 0, Easing.BounceOut);
                var ani_7 = new Animation(v => sched_grid.TranslationX = v, -alay.Width, 0, Easing.BounceOut);
                var ani_4 = new Animation(v => dtgrid.TranslationX = v, -alay.Width, 0, Easing.BounceOut);
                var ani_5 = new Animation(v => bt_setg.TranslationX = v, alay.Width, 0, Easing.BounceOut);
                var ani_6 = new Animation(v => bt_editdt.TranslationX = v, alay.Width, 0, Easing.BounceOut);


                ani.Add(0, 1, ani_1);
                ani.Add(0, 1, ani_2);
                ani.Add(0, 1, ani_3);
                ani.Add(0, 1, ani_4);
                ani.Add(0, 1, ani_5);
                ani.Add(0, 1, ani_6);
                ani.Add(0, 1, ani_7);

                ani.Commit(bt_back, "Back", length: 700);
            };

            bt_editdt.Clicked += (sender, e) =>
            {
                double end = alay.Height/2;
                var ani = new Animation();//0 is current pos of element 
                var ani_1 = new Animation(v => grid_sch.TranslationY = v, 0, -end, Easing.BounceOut);
                var ani_6 = new Animation(v => sched_grid.TranslationY = v,  0,-end, Easing.BounceOut);
                var ani_2 = new Animation(v => dtgrid.TranslationY = v, 0,-end, Easing.BounceOut);
                var ani_3 = new Animation(v => bt_editdt.TranslationY = v, 0, -end, Easing.BounceOut);
                var ani_4 = new Animation(v => dtbtgrid.TranslationY = v, 0, - end, Easing.BounceOut);
                var ani_5 = new Animation(v => lv_dates.TranslationY = v, 0, -end, Easing.BounceOut);


                ani.Add(0, 1, ani_1);
                ani.Add(0, 1, ani_2);
                ani.Add(0, 1, ani_3);
                ani.Add(0, 1, ani_4);
                ani.Add(0, 1, ani_5);
                ani.Add(0, 1, ani_6);

                ani.Commit(bt_back, "Edit Date List", length: 700);
            };

            bt_dtback.Clicked += (sender, e) =>
            {
                double end = alay.Height / 2;
                var ani = new Animation();//0 is current pos of element 
                var ani_1 = new Animation(v => grid_sch.TranslationY = v,  -end,0, Easing.BounceOut);
                var ani_6 = new Animation(v => sched_grid.TranslationY = v, -end, 0, Easing.BounceOut);
                var ani_2 = new Animation(v => dtgrid.TranslationY = v,  -end,0, Easing.BounceOut);
                var ani_3 = new Animation(v => bt_editdt.TranslationY = v,  -end,0, Easing.BounceOut);
                var ani_4 = new Animation(v => dtbtgrid.TranslationY = v,  -end,0, Easing.BounceOut);
                var ani_5 = new Animation(v => lv_dates.TranslationY = v,  -end,0, Easing.BounceOut);


                ani.Add(0, 1, ani_1);
                ani.Add(0, 1, ani_2);
                ani.Add(0, 1, ani_3);
                ani.Add(0, 1, ani_4);
                ani.Add(0, 1, ani_5);
                ani.Add(0, 1, ani_6);

                ani.Commit(bt_back, "Edit Date List", length: 700);
            };





            alay.SizeChanged += (sender, e) =>
             {
                 var width = alay.Width;
                 var height = alay.Height;
                 AbsoluteLayout.SetLayoutBounds(chart, new Rectangle(0, 0, width, height / 2));
                 AbsoluteLayout.SetLayoutBounds(info_grid, new Rectangle(0, height / 2, width, 50));
                 AbsoluteLayout.SetLayoutBounds(lv_info, new Rectangle(0, (height / 2) + 50, width, (height / 2) - 100));
                 AbsoluteLayout.SetLayoutBounds(bt_setg, new Rectangle(0, height - 50, width, 50));

                 AbsoluteLayout.SetLayoutBounds(grid_sch, new Rectangle(-width, height / 2, width, 50));
                 AbsoluteLayout.SetLayoutBounds(sched_grid, new Rectangle(width, (height / 2) + 50, width, 100));
                 AbsoluteLayout.SetLayoutBounds(dtgrid, new Rectangle(width,(height / 2)+150, width, 50));
                 AbsoluteLayout.SetLayoutBounds(bt_editdt, new Rectangle(-width, height - 50, width, 50));

                 AbsoluteLayout.SetLayoutBounds(dtbtgrid, new Rectangle(0, height, width, 50));
                 AbsoluteLayout.SetLayoutBounds(lv_dates, new Rectangle(0, height + 50, width, (height / 2) - 50));


                 
                 alay.Children.Add(info_grid);
                 alay.Children.Add(lv_info);
                 alay.Children.Add(bt_setg);

                 alay.Children.Add(grid_sch);
                 alay.Children.Add(sched_grid);
                 alay.Children.Add(dtgrid);
                 alay.Children.Add(bt_editdt);

                 alay.Children.Add(dtbtgrid);
                 alay.Children.Add(lv_dates);
                 alay.Children.Add(chart);
             };

            Content = alay;


        }


    }


}