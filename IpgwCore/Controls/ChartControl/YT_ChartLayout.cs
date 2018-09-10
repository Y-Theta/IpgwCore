using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace IpgwCore.Controls.ChartControl {
    public enum ChartForm {
        VTick,
        HTick,
        Point
    }

    public class YT_ChartLayout : Panel {
        #region Properties
        /// <summary>
        /// 类型
        /// </summary>
        public ChartForm ChartKind {
            get { return (ChartForm)GetValue(ChartKindProperty); }
            set { SetValue(ChartKindProperty, value); }
        }
        public static readonly DependencyProperty ChartKindProperty =
            DependencyProperty.Register("ChartKind", typeof(ChartForm),
                typeof(YT_ChartLayout), new PropertyMetadata(ChartForm.HTick));

        /// <summary>
        /// 纵向最大值
        /// </summary>
        public double VMaxium {
            get { return (double)GetValue(VMaxiumProperty); }
            set { SetValue(VMaxiumProperty, value); }
        }
        public static readonly DependencyProperty VMaxiumProperty =
            DependencyProperty.Register("VMaxium", typeof(double),
                typeof(YT_ChartLayout), new PropertyMetadata(0.0));

        /// <summary>
        /// 横轴距
        /// </summary>
        public double HTickSpan { get; set; }

        /// <summary>
        /// 纵轴距
        /// </summary>
        public double VTickSpan { get; set; }
        public int VTickCount {
            get { return (int)GetValue(VTickCountProperty); }
            set { SetValue(VTickCountProperty, value); }
        }
        public static readonly DependencyProperty VTickCountProperty =
            DependencyProperty.Register("VTickCount", typeof(int),
                typeof(YT_ChartLayout), new PropertyMetadata(0));


        #endregion

        #region Methods
        protected override Size MeasureOverride(Size availableSize) {
            foreach (UIElement child in Children)
                child.Measure(availableSize);
            switch (ChartKind)
            {
                case ChartForm.VTick:
                    if (Children.Count > 0)
                        if (VTickSpan == 0)
                        {
                            VTickSpan = (availableSize.Width - Children.Count * Children[0].DesiredSize.Width) / (Children.Count - 1);
                            return availableSize;
                        }
                        else
                            return new Size((Children.Count - 1) * VTickSpan + Children.Count * Children[0].DesiredSize.Width, availableSize.Height);
                    else
                        return availableSize;
                case ChartForm.HTick:
                    if (Children.Count > 0)
                        if (HTickSpan == 0)
                        {
                            HTickSpan = (availableSize.Height - Children.Count * Children[0].DesiredSize.Height) / (Children.Count - 1);
                            return availableSize;
                        }
                        else
                            return new Size(availableSize.Width, (Children.Count - 1) * HTickSpan + Children.Count * Children[0].DesiredSize.Height);
                    else
                        return availableSize;
                case ChartForm.Point:
                    return availableSize;
                default:
                    return base.MeasureOverride(availableSize);
            }
        }

        protected override Size ArrangeOverride(Size finalSize) {
            double span = 0;
            switch (ChartKind)
            {
                case ChartForm.VTick:
                    foreach (UIElement child in Children)
                    {
                        child.Arrange(new Rect(span, 0, child.DesiredSize.Width, finalSize.Height));
                        span += child.DesiredSize.Width + VTickSpan;
                        if (span == finalSize.Width)
                            span -= child.DesiredSize.Width;
                    }
                    break;
                case ChartForm.HTick:
                    foreach (UIElement child in Children)
                    {
                        child.Arrange(new Rect(0, span, finalSize.Width, child.DesiredSize.Height));
                        span += child.DesiredSize.Height + HTickSpan;
                        if (span == finalSize.Height)
                            span -= child.DesiredSize.Height;
                    }
                    break;
                case ChartForm.Point:
                    double scale = finalSize.Height / VMaxium;
                    VTickSpan = finalSize.Width / (VTickCount - 1);
                    if (Children.Count > 0)
                    {
                        if ((Children[0] as ListViewItem)?.Content is IChartPointItem)
                            foreach (UIElement child in Children)
                            {
                                IChartPointItem Point = (IChartPointItem)((ListViewItem)child).Content;
                                if (span == 0)
                                    span = -child.DesiredSize.Width / 2;
                                child.Arrange(new Rect(span, Point.Value * scale <= child.DesiredSize.Height / 2 ? finalSize.Height - child.DesiredSize.Height / 2 : finalSize.Height - Point.Value * scale - child.DesiredSize.Height / 2
                                        , child.DesiredSize.Width, child.DesiredSize.Height));
                                span += VTickSpan;
                            }
                        else if ((Children[0] as ListViewItem)?.Content is IChartLineItem)
                        {
                            double lastpoint = 0;
                            foreach (UIElement child in Children)
                            {
                                IChartLineItem Line = (IChartLineItem)((ListViewItem)child).Content;
                                if (span == 0)
                                {
                                    span += VTickSpan;
                                    lastpoint = Line.Value;
                                    continue;
                                }
                                Line.Content.X1 = span - VTickSpan;
                                Line.Content.Y1 = finalSize.Height - lastpoint * scale;
                                Line.Content.X2 = span;
                                Line.Content.Y2 = finalSize.Height - Line.Value * scale;
                                child.Arrange(new Rect(finalSize));
                                //  child.Arrange(new Rect(Line.Content.X1, Math.Min(Line.Content.Y1, Line.Content.Y2), VTickSpan, Math.Abs(Line.Content.Y1 - Line.Content.Y2)));
                                span += VTickSpan;
                                lastpoint = Line.Value;
                            }
                        }
                        else if ((Children[0] as ListViewItem)?.Content is IChartRectItem)
                        {
                            VTickSpan = (finalSize.Width - Children[0].DesiredSize.Width * Children.Count) / (VTickCount - 1);
                            foreach (UIElement child in Children)
                            {
                                IChartRectItem Rect = (IChartRectItem)((ListViewItem)child).Content;
                                child.Arrange(new Rect(span, finalSize.Height - Rect.Value * scale, child.DesiredSize.Width, Rect.Value * scale));
                                span += VTickSpan + child.DesiredSize.Width;
                            }
                        }
                        else
                            throw new ArgumentException("图表元素必须是IChartItem类型");
                    }
                    break;
            }
            return base.ArrangeOverride(finalSize);
        }
        #endregion

        #region Constructors
        #endregion
    }

}