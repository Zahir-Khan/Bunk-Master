using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.SfChart.XForms;


namespace Bunk_Master
{
    public class GraphGen
    {
        public GraphGen()
        {

        }

        public LineSeries GenerateLineGraph(object bindingcontext, IEnumerable itemsource, string ypath, string xpath)
        {
            LineSeries line = new LineSeries()
            {
                BindingContext = bindingcontext,
                ItemsSource = itemsource,
                YBindingPath = ypath,
                XBindingPath = xpath,
                ListenPropertyChange = true,
                
            };
            
            line.DataMarker = new ChartDataMarker();
            line.EnableAnimation = true;
            line.AnimationDuration = 2;
            //line.DataMarker.MarkerType = DataMarkerType.Ellipse;
            return line;
        }

        public FastLineSeries GenerateFastGraph(object bindingcontext,IEnumerable itemsource,string ypath, string xpath)
        {
            FastLineSeries line = new FastLineSeries()
            {
                StrokeDashArray = new double[2] { 20, 5 },
                BindingContext = bindingcontext,
                ItemsSource = itemsource,
                YBindingPath = ypath,
                XBindingPath = xpath,
                ListenPropertyChange = true,
            };
                        
            return line;
        }
    }
}
