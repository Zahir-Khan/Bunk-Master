using System;
using System.Collections.Generic;
using System.Text;
using Syncfusion.SfChart.XForms;

namespace Bunk_Master
{
    class AnalysisView
    {
        AnalysisView()
        {
            GraphGen GInstance = new GraphGen();
            SfChart chart = new SfChart { };

            DateTimeAxis primaryAxis = new DateTimeAxis()
            {
                //Maximum = DateTime.Today.AddDays(7),
                IntervalType = DateTimeIntervalType.Days,
                Interval = 1,
                LabelRotationAngle = 15,

            };
            chart.PrimaryAxis = primaryAxis;

            //Initializing Secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis() { Minimum = 0, Maximum = 100 };
            chart.SecondaryAxis = secondaryAxis;

            //Zooming
            ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior
            {
                ZoomMode = ZoomMode.X
            };

            chart.ChartBehaviors.Add(zoomPanBehavior);

            //var AnalysisGraph = GInstance.
        }
    }
}
