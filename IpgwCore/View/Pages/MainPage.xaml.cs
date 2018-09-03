using IpgwCore.Model.BasicModel;
using IpgwCore.Services.FormatServices;
using IpgwCore.Services.HttpServices;
using IpgwCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IpgwCore.View.Pages {
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page {
        private MainPageViewModel _mpvm;
        DoubleAnimationUsingKeyFrames _row;
        SplineDoubleKeyFrame _rowover;
        Storyboard _rowani;

        public double FluxData {
            get { return (double)GetValue(FluxDataProperty); }
            set { SetValue(FluxDataProperty, value); }
        }
        public static readonly DependencyProperty FluxDataProperty =
            DependencyProperty.Register("FluxData", typeof(double), 
                typeof(MainPage), new PropertyMetadata(0.0));


        #region Methods

        private void GraphAnimation() {
            _rowover.Value = FluxConv.GetFluxPercent(Formater.Instence.IpgwInfo) / 100;
            _rowani.Begin();
        }

        private void InitAnimation() {
            _row = new DoubleAnimationUsingKeyFrames();
            SplineDoubleKeyFrame _rowbegin = new SplineDoubleKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)),
                Value = 0
            };
            _rowover = new SplineDoubleKeyFrame
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500)),
                KeySpline = new KeySpline { ControlPoint1 = new Point(0, 0.4), ControlPoint2 = new Point(1, 1) },
            };
            _row.KeyFrames.Add(_rowbegin);
            _row.KeyFrames.Add(_rowover);
            _rowani = new Storyboard
            {
                FillBehavior = FillBehavior.HoldEnd
            };

            Storyboard.SetTarget(_row, this);
            Storyboard.SetTargetProperty(_row, new PropertyPath(FluxDataProperty));

            _rowani.Children.Add(_row);
        }

        private void _mpvm_CommandOperation(object sender, MVVMBase.CommandArgs args) {

        }

        private bool Instence_IpgwInfoChanged(object op, object np) {
            GraphAnimation();
            return true;
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e) {
            _mpvm.Mainpage = false;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e) {
            _mpvm = (MainPageViewModel)DataContext;
            _mpvm.CommandOperation += _mpvm_CommandOperation;
            _mpvm.Mainpage = true;
            _mpvm.Msg = "流量详情";
            GraphAnimation();
        }

        #endregion

        #region Constructor
        public MainPage() {
            
            InitializeComponent();
            InitAnimation();
            Formater.Instence.IpgwInfoChanged += Instence_IpgwInfoChanged;
            Loaded += MainPage_Loaded;
            Unloaded += MainPage_Unloaded;
        }
        #endregion 
    }
}
