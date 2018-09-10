using IpgwCore.Controls.ChartControl;
using IpgwCore.Model.BasicModel;
using IpgwCore.MVVMBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace IpgwCore.ViewModel {
    public class ChartViewModel :ViewModelBase{
        #region Properties
        /// <summary>
        /// 表格数据最大值
        /// </summary>
        private double _maxium;
        public double Maxium {
            get => _maxium;
            set => SetValue(out _maxium, value, Maxium);
        }

        /// <summary>
        /// 表格竖线
        /// </summary>
        private ObservableCollection<double> _vTick;
        public ObservableCollection<double> VTick {
            get => _vTick;
            set => SetValue(out _vTick, value, VTick);
        }

        /// <summary>
        /// 表格横线
        /// </summary>
        private ObservableCollection<double> _hTick;
        public ObservableCollection<double> HTick {
            get => _hTick;
            set => SetValue(out _hTick, value, HTick);
        }

        /// <summary>
        /// 表格数据
        /// </summary>
        private ObservableCollection<IChartItem> _item;
        public ObservableCollection<IChartItem> Item {
            get => _item;
            set => SetValue(out _item, value, Item);
        }


        private ObservableCollection<IChartItem> _item1;

        public ObservableCollection<IChartItem> Item1 {
            get => _item1;
            set => SetValue(out _item1, value, Item1);
        }

        #endregion

        #region Methods
        private void InitRes() {
            VTick = new ObservableCollection<double>();
            HTick = new ObservableCollection<double>();
            Item = new ObservableCollection<IChartItem>();
            Item1 = new ObservableCollection<IChartItem>();
        }

        public void LoadData() {
            Item.Clear();
            Item1.Clear();
            VTick.Clear();
            HTick.Clear();
            for (int i = 0; i < 31; i++) {
                int cal = new Random(new ChartRectItem().GetHashCode()).Next(10);
                ChartPointItem cpi = new ChartPointItem { Value = cal };
                Item1.Add(new ChartLineItem { Value = cal });
                cpi.Content.MouseEnter += Content_MouseEnter;
                cpi.Content.MouseLeave += Content_MouseLeave;
                Item.Add(cpi);
                
            }
            for (int i = 0; i < 31; i++) {
                VTick.Add(2);
            }
            for (int i = 0; i < 6; i++)
            {
                HTick.Add(2);
            }
            Maxium = 10;
        }

        private void Content_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            (sender as Ellipse).Stroke = new SolidColorBrush(Colors.Transparent);
        }

        private void Content_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            (sender as Ellipse).Stroke = new SolidColorBrush(Color.FromArgb(200, 20, 255, 120));
        }
        #endregion

        #region Constructors
        public ChartViewModel() {
            InitRes();
        }
        #endregion
    }

}
