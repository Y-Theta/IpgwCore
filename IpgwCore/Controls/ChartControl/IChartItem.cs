using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace IpgwCore.Controls.ChartControl 
{

    public interface IChartItem {

        double Value { get; set; }
    }

    public interface IChartPointItem : IChartItem {

        Ellipse Content { get; set; }
    }

    public interface IChartLineItem : IChartItem {

        Line Content { get; set; }
    }

    public interface IChartRectItem : IChartItem {

        Rectangle Content { get; set; }
    }
}
