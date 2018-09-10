using IpgwCore.Controls.ChartControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IpgwCore.Model.BasicModel {
    public class ChartPointItem : IChartPointItem {

        #region Properties
        public double Value { get; set; }

        public Ellipse Content {get; set; }

        #endregion

        public ChartPointItem() {
            Content = new Ellipse();
            Content.Fill = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255));
            Content.Stroke = new SolidColorBrush(Colors.Transparent);
            Content.Width = 12;
            Content.Height = 12;
            Content.StrokeThickness = 6;
        }

    }

    public class ChartLineItem : IChartLineItem {
        #region Properties
        public double Value {get;set; }
        public Line Content { get; set; }
        #endregion

        public ChartLineItem() {
            Content = new Line();
            Content.Stroke = new SolidColorBrush(Color.FromArgb(160, 255, 255, 255));
            Content.StrokeThickness = 1;
        }
    }

    public class ChartRectItem : IChartRectItem {
        #region Properties
        public double Value { get; set; }
        public Rectangle Content { get; set; }

        #endregion

        public ChartRectItem() {
            Content = new Rectangle();
            Content.Fill = new SolidColorBrush(Color.FromArgb(200,255,255,255));
            Content.Width = 2;
        }
    }

}
