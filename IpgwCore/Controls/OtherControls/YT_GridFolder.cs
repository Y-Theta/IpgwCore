using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IpgwCore.Controls
{
    public class YT_GridFolder : ContentControl, ICommand {
        #region Properties
        private double OriginGridSize;

        #region GridType
        public GridUnitType AimGridType {
            get { return (GridUnitType)GetValue(AimGridTypeProperty); }
            set { SetValue(AimGridTypeProperty, value); }
        }
        public static readonly DependencyProperty AimGridTypeProperty =
            DependencyProperty.Register("AimGridType", typeof(GridUnitType), 
                typeof(YT_GridFolder),new PropertyMetadata(GridUnitType.Auto));
        #endregion

        #region Title
        public string Title {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), 
                typeof(YT_GridFolder), new PropertyMetadata(""));
        #endregion

        #region TitleSize
        public double TitleSize {
            get { return (double)GetValue(TitleSizeProperty); }
            set { SetValue(TitleSizeProperty, value); }
        }
        public static readonly DependencyProperty TitleSizeProperty =
            DependencyProperty.Register("TitleSize", typeof(double),
                typeof(YT_GridFolder), new PropertyMetadata(12.0));
        #endregion

        #region ContentVis
        public Visibility ContentVis {
            get { return (Visibility)GetValue(ContentVisProperty); }
            set { SetValue(ContentVisProperty, value); }
        }
        public static readonly DependencyProperty ContentVisProperty =
            DependencyProperty.Register("ContentVis", typeof(Visibility), 
                typeof(YT_GridFolder),new PropertyMetadata(Visibility.Visible, ContentVisChanged));
        private static void ContentVisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            YT_GridFolder gf = (YT_GridFolder)d;
            if (e.NewValue.Equals(Visibility.Collapsed))
                gf.GridHide();
            else
                gf.GridShow();
        }
        #endregion

        #region AttachedGrid
        public Grid AttachedGrid {
            get { return (Grid)GetValue(AttachedGridProperty); }
            set { SetValue(AttachedGridProperty, value); }
        }
        public static readonly DependencyProperty AttachedGridProperty =
            DependencyProperty.Register("AttachedGrid", typeof(Grid), 
                typeof(YT_GridFolder),new PropertyMetadata(null));
        #endregion

        #region CommandPara
        public int CommandPara {
            get { return (int)GetValue(CommandParaProperty); }
            set { SetValue(CommandParaProperty, value); }
        }
        public static readonly DependencyProperty CommandParaProperty =
            DependencyProperty.Register("CommandPara", typeof(int),
                typeof(YT_GridFolder),new PropertyMetadata(null));

        public event EventHandler CanExecuteChanged;
        #endregion

        #region IconF
        public string IconF {
            get { return (string)GetValue(IconFProperty); }
            set { SetValue(IconFProperty, value); }
        }
        public static readonly DependencyProperty IconFProperty =
            DependencyProperty.Register("IconF", typeof(string), 
                typeof(YT_GridFolder), new PropertyMetadata(""));
        #endregion

        #region IconB
        public string IconB {
            get { return (string)GetValue(IconBProperty); }
            set { SetValue(IconBProperty, value); }
        }
        public static readonly DependencyProperty IconBProperty =
            DependencyProperty.Register("IconB", typeof(string),
                typeof(YT_GridFolder), new PropertyMetadata(""));
        #endregion

        #region IconSize
        public double IconSize {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double),
                typeof(YT_GridFolder), new PropertyMetadata(12.0));
        #endregion

        #region IconWidth
        public double IconWidth {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double),
                typeof(YT_GridFolder), new PropertyMetadata(24.0));
        #endregion

        #endregion

        #region Methods

        #region FloderCommand
        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            if (ContentVis.Equals(Visibility.Visible))
                ContentVis = Visibility.Collapsed;
            else
                ContentVis = Visibility.Visible;
        }

        private void GridShow() {
            switch (AimGridType)
            {
                case GridUnitType.Auto:
                    AttachedGrid.RowDefinitions[CommandPara].Height = new GridLength(1, GridUnitType.Auto);
                    break;
                case GridUnitType.Star:
                    AttachedGrid.RowDefinitions[CommandPara].Height = new GridLength(1, GridUnitType.Star);
                    break;
                case GridUnitType.Pixel:
                    AttachedGrid.RowDefinitions[CommandPara].Height = new GridLength(OriginGridSize, GridUnitType.Pixel);
                    break;
            }
        }

        private void GridHide() {
            switch (AimGridType)
            {
                case GridUnitType.Pixel:
                    OriginGridSize = AttachedGrid.RowDefinitions[CommandPara].Height.Value;
                    break;
            }
            AttachedGrid.RowDefinitions[CommandPara].Height = new GridLength(0, GridUnitType.Pixel);
        }

        #endregion

        #endregion

        #region Constructors
        static YT_GridFolder() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_GridFolder), new FrameworkPropertyMetadata(typeof(YT_GridFolder)));
        }
        #endregion
    }

}
