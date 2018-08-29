using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using IpgwCore.MVVMBase;

namespace IpgwCore.Controls.AreaWindow {
    public class YT_TitleBar : ContentControl {
        #region AttachedWindow
        private Window attachedWindow;
        public Window AttachedWindow {
            get => attachedWindow;
            set { attachedWindow = value; }
        }
        #endregion

        #region Methods Overrides
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            AttachedWindow.DragMove();
            base.OnMouseLeftButtonDown(e);
        }

        public override void EndInit() {
            DependencyObject RootElement = this;
            while (!(RootElement is Window)) { RootElement = VisualTreeHelper.GetParent(RootElement); }
            AttachedWindow = RootElement as Window;

            base.EndInit();
        }
        #endregion

        #region EventActions
        public CommandBase CloseCommand {
            get { return (CommandBase)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(CommandBase),
                typeof(YT_TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public CommandBase MinCommand {
            get { return (CommandBase)GetValue(MinCommandProperty); }
            set { SetValue(MinCommandProperty, value); }
        }
        public static readonly DependencyProperty MinCommandProperty =
            DependencyProperty.Register("MinCommand", typeof(CommandBase),
                typeof(YT_TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        public CommandBase MaxCommand {
            get { return (CommandBase)GetValue(MaxCommandProperty); }
            set { SetValue(MaxCommandProperty, value); }
        }
        public static readonly DependencyProperty MaxCommandProperty =
            DependencyProperty.Register("MaxCommand", typeof(CommandBase),
                typeof(YT_TitleBar), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));

        #endregion

        #region Method
        private void InitCommands (){
            CloseCommand = new CommandBase(obj => {

                // TODO:
                App.Current.MainWindow.Close();
                
            });
            MinCommand = new CommandBase(obj => { });
            MaxCommand = new CommandBase(obj => { });
        }
        #endregion

        #region Constructors
        public YT_TitleBar() {
            InitCommands();
        }

        static YT_TitleBar() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_TitleBar), new FrameworkPropertyMetadata(typeof(YT_TitleBar)));
        }
        #endregion
    }
}
