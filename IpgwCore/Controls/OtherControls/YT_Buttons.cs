using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace IpgwCore.Controls {

    public class YT_IconButton : Button {

        #region IconOnly
        public Visibility ContentTextVisiblity {
            get { return (Visibility)GetValue(ContentTextVisiblityProperty); }
            set { SetValue(ContentTextVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ContentTextVisiblityProperty =
            DependencyProperty.Register("ContentTextVisiblity", typeof(Visibility), typeof(YT_IconButton),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region TextOnly
        public Visibility IconVisiblity {
            get { return (Visibility)GetValue(IconVisiblityProperty); }
            set { SetValue(IconVisiblityProperty, value); }
        }
        public static readonly DependencyProperty IconVisiblityProperty =
            DependencyProperty.Register("IconVisiblity", typeof(Visibility),
                typeof(YT_IconButton), new PropertyMetadata(Visibility.Visible));
        #endregion

        #region LabelPadding
        public Thickness LabelPadding {
            get { return (Thickness)GetValue(LabelPaddingProperty); }
            set { SetValue(LabelPaddingProperty, value); }
        }
        public static readonly DependencyProperty LabelPaddingProperty =
            DependencyProperty.Register("LabelPadding", typeof(Thickness),
                typeof(YT_IconButton), new PropertyMetadata(new Thickness(0)));
        #endregion

        #region Textloc
        public bool Textloc {
            get { return (bool)GetValue(TextlocProperty); }
            set { SetValue(TextlocProperty, value); }
        }
        public static readonly DependencyProperty TextlocProperty =
            DependencyProperty.Register("Textloc", typeof(bool),
                typeof(YT_IconButton), new PropertyMetadata(false));
        #endregion

        #region ButtonColor
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(YT_IconButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(YT_IconButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(YT_IconButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(YT_IconButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(YT_IconButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(YT_IconButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        #region ContentText
        public string ContentText {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }
        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string), typeof(YT_IconButton),
                new PropertyMetadata(""));
        #endregion

        #region ContentTextAlignment
        public TextAlignment ContentTextAlignment {
            get { return (TextAlignment)GetValue(ContentTextAlignmentProperty); }
            set { SetValue(ContentTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentTextAlignmentProperty =
            DependencyProperty.Register("ContentTextAlignment", typeof(TextAlignment), typeof(YT_IconButton),
                new PropertyMetadata(TextAlignment.Center));
        #endregion

        #region ContentHorizontalAlignment
        public HorizontalAlignment ContentHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty); }
            set { SetValue(ContentHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentHorizontalAlignmentProperty =
            DependencyProperty.Register("ContentHorizontalAlignment", typeof(HorizontalAlignment), typeof(YT_IconButton),
                new PropertyMetadata(HorizontalAlignment.Center));
        #endregion

        #region ContentVerticalAlignment
        public VerticalAlignment ContentVerticalAlignment {
            get { return (VerticalAlignment)GetValue(ContentVerticalAlignmentProperty); }
            set { SetValue(ContentVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentVerticalAlignmentProperty =
            DependencyProperty.Register("ContentVerticalAlignment", typeof(VerticalAlignment), typeof(YT_IconButton),
                new PropertyMetadata(VerticalAlignment.Center));
        #endregion

        #region ContentTextFontWeight
        public double ContentTextFontSize {
            get { return (double)GetValue(ContentTextFontSizeProperty); }
            set { SetValue(ContentTextFontSizeProperty, value); }
        }
        public static readonly DependencyProperty ContentTextFontSizeProperty =
            DependencyProperty.Register("ContentTextFontSize", typeof(double), typeof(YT_IconButton),
                new PropertyMetadata(0.0));
        #endregion

        #region AllowShadow
        public double AllowShadow {
            get { return (double)GetValue(AllowShadowProperty); }
            set { SetValue(AllowShadowProperty, value); }
        }
        public static readonly DependencyProperty AllowShadowProperty =
            DependencyProperty.Register("AllowShadow", typeof(double), typeof(YT_IconButton), new PropertyMetadata(0.0));
        #endregion

        #region AllowAni
        public bool AllowAni {
            get { return (bool)GetValue(AllowAniProperty); }
            set { SetValue(AllowAniProperty, value); }
        }
        public static readonly DependencyProperty AllowAniProperty =
            DependencyProperty.Register("AllowAni", typeof(bool),
                typeof(YT_IconButton), new PropertyMetadata(false));
        #endregion

        #region RrotateAngle
        public double RrotateAngle {
            get { return (double)GetValue(RrotateAngleProperty); }
            set { SetValue(RrotateAngleProperty, value); }
        }
        public static readonly DependencyProperty RrotateAngleProperty =
            DependencyProperty.Register("RrotateAngle", typeof(double),
                typeof(YT_IconButton), new PropertyMetadata(0.0));
        #endregion

        #region ForeTip
        public string ForeToolTip {
            get { return (string)GetValue(ForeToolTipProperty); }
            set { SetValue(ForeToolTipProperty, value); }
        }
        public static readonly DependencyProperty ForeToolTipProperty =
            DependencyProperty.Register("ForeToolTip", typeof(string), typeof(YT_IconButton),
                new PropertyMetadata(""));
        #endregion

        #region ToolTipVisiblity
        public Visibility ToolTipVisiblity {
            get { return (Visibility)GetValue(ToolTipVisiblityProperty); }
            set { SetValue(ToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisiblityProperty =
            DependencyProperty.Register("ToolTipVisiblity", typeof(Visibility), typeof(YT_IconButton),
                new PropertyMetadata(Visibility.Visible));
        #endregion

        static YT_IconButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_IconButton), new FrameworkPropertyMetadata(typeof(YT_IconButton)));
        }
    }

    public class YT_IconToggelButton : ToggleButton {

        #region LabelPadding
        public Thickness LabelPadding {
            get { return (Thickness)GetValue(LabelPaddingProperty); }
            set { SetValue(LabelPaddingProperty, value); }
        }
        public static readonly DependencyProperty LabelPaddingProperty =
            DependencyProperty.Register("LabelPadding", typeof(Thickness),
                typeof(YT_IconToggelButton), new PropertyMetadata(new Thickness(0)));
        #endregion

        #region Textloc
        public bool Textloc {
            get { return (bool)GetValue(TextlocProperty); }
            set { SetValue(TextlocProperty, value); }
        }
        public static readonly DependencyProperty TextlocProperty =
            DependencyProperty.Register("Textloc", typeof(bool),
                typeof(YT_IconToggelButton), new PropertyMetadata(false));
        #endregion

        #region IconOnly
        public Visibility LabelVisibility {
            get { return (Visibility)GetValue(LabelVisibilityProperty); }
            set { SetValue(LabelVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LabelVisibilityProperty =
            DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(YT_IconToggelButton),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region TextOnly
        public Visibility IconVisiblity {
            get { return (Visibility)GetValue(IconVisiblityProperty); }
            set { SetValue(IconVisiblityProperty, value); }
        }
        public static readonly DependencyProperty IconVisiblityProperty =
            DependencyProperty.Register("IconVisiblity", typeof(Visibility),
                typeof(YT_IconToggelButton), new PropertyMetadata(Visibility.Visible));
        #endregion

        #region ButtonColor
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconMaskS {
            get { return (Brush)GetValue(IconMaskSProperty); }
            set { SetValue(IconMaskSProperty, value); }
        }
        public static readonly DependencyProperty IconMaskSProperty =
            DependencyProperty.Register("IconMaskS", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconS {
            get { return (Brush)GetValue(IconSProperty); }
            set { SetValue(IconSProperty, value); }
        }
        public static readonly DependencyProperty IconSProperty =
            DependencyProperty.Register("IconS", typeof(Brush), typeof(YT_IconToggelButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        #region AllowShadow
        public double AllowShadow {
            get { return (double)GetValue(AllowShadowProperty); }
            set { SetValue(AllowShadowProperty, value); }
        }
        public static readonly DependencyProperty AllowShadowProperty =
            DependencyProperty.Register("AllowShadow", typeof(double), typeof(YT_IconToggelButton), new PropertyMetadata(0.0));
        #endregion

        #region AllowAni
        public bool AllowAni {
            get { return (bool)GetValue(AllowAniProperty); }
            set { SetValue(AllowAniProperty, value); }
        }
        public static readonly DependencyProperty AllowAniProperty =
            DependencyProperty.Register("AllowAni", typeof(bool),
                typeof(YT_IconToggelButton), new PropertyMetadata(false));
        #endregion

        #region Labelfontsize
        public double LabelFontSize {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double),
                typeof(YT_IconToggelButton), new PropertyMetadata(10.0));
        #endregion

        #region BackLabelText
        public string BackText {
            get { return (string)GetValue(BackTextProperty); }
            set { SetValue(BackTextProperty, value); }
        }
        public static readonly DependencyProperty BackTextProperty =
            DependencyProperty.Register("BackText", typeof(string), typeof(YT_IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region ContentTextAlignment
        public TextAlignment ContentTextAlignment {
            get { return (TextAlignment)GetValue(ContentTextAlignmentProperty); }
            set { SetValue(ContentTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentTextAlignmentProperty =
            DependencyProperty.Register("ContentTextAlignment", typeof(TextAlignment), typeof(YT_IconToggelButton),
                new PropertyMetadata(TextAlignment.Center));
        #endregion

        #region ContentHorizontalAlignment
        public HorizontalAlignment ContentHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty); }
            set { SetValue(ContentHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentHorizontalAlignmentProperty =
            DependencyProperty.Register("ContentHorizontalAlignment", typeof(HorizontalAlignment), typeof(YT_IconToggelButton),
                new PropertyMetadata(HorizontalAlignment.Center));
        #endregion

        #region ContentVerticalAlignment
        public VerticalAlignment ContentVerticalAlignment {
            get { return (VerticalAlignment)GetValue(ContentVerticalAlignmentProperty); }
            set { SetValue(ContentVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentVerticalAlignmentProperty =
            DependencyProperty.Register("ContentVerticalAlignment", typeof(VerticalAlignment), typeof(YT_IconToggelButton),
                new PropertyMetadata(VerticalAlignment.Center));
        #endregion

        #region ForeIcon
        public string ForeIcon {
            get { return (string)GetValue(ForeIconProperty); }
            set { SetValue(ForeIconProperty, value); }
        }
        public static readonly DependencyProperty ForeIconProperty =
            DependencyProperty.Register("ForeIcon", typeof(string), typeof(YT_IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region BackIcon
        public string BackIcon {
            get { return (string)GetValue(BackIconProperty); }
            set { SetValue(BackIconProperty, value); }
        }
        public static readonly DependencyProperty BackIconProperty =
            DependencyProperty.Register("BackIcon", typeof(string), typeof(YT_IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region ForeTip
        public string ForeToolTip {
            get { return (string)GetValue(ForeToolTipProperty); }
            set { SetValue(ForeToolTipProperty, value); }
        }
        public static readonly DependencyProperty ForeToolTipProperty =
            DependencyProperty.Register("ForeToolTip", typeof(string), typeof(YT_IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region BackTip
        public string BackToolTip {
            get { return (string)GetValue(BackToolTipProperty); }
            set { SetValue(BackToolTipProperty, value); }
        }
        public static readonly DependencyProperty BackToolTipProperty =
            DependencyProperty.Register("BackToolTip", typeof(string), typeof(YT_IconToggelButton),
                new PropertyMetadata(""));
        #endregion

        #region ToolTipVisiblity
        public Visibility ToolTipVisiblity {
            get { return (Visibility)GetValue(ToolTipVisiblityProperty); }
            set { SetValue(ToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisiblityProperty =
            DependencyProperty.Register("ToolTipVisiblity", typeof(Visibility), typeof(YT_IconToggelButton),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region IconRotate
        public RotateTransform IconRotate {
            get { return (RotateTransform)GetValue(IconRotateProperty); }
            set { SetValue(IconRotateProperty, value); }
        }
        public static readonly DependencyProperty IconRotateProperty =
            DependencyProperty.Register("IconRotate", typeof(RotateTransform),
                typeof(YT_IconToggelButton), new PropertyMetadata(null));
        #endregion

        static YT_IconToggelButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_IconToggelButton), new FrameworkPropertyMetadata(typeof(YT_IconToggelButton)));
        }
    }

    public class YT_IconRadioButton : RadioButton {

        #region LabelPadding
        public Thickness LabelPadding {
            get { return (Thickness)GetValue(LabelPaddingProperty); }
            set { SetValue(LabelPaddingProperty, value); }
        }
        public static readonly DependencyProperty LabelPaddingProperty =
            DependencyProperty.Register("LabelPadding", typeof(Thickness),
                typeof(YT_IconRadioButton), new PropertyMetadata(new Thickness(0)));
        #endregion

        #region Textloc
        public bool Textloc {
            get { return (bool)GetValue(TextlocProperty); }
            set { SetValue(TextlocProperty, value); }
        }
        public static readonly DependencyProperty TextlocProperty =
            DependencyProperty.Register("Textloc", typeof(bool),
                typeof(YT_IconRadioButton), new PropertyMetadata(false));
        #endregion

        #region IconOnly
        public Visibility LabelVisibility {
            get { return (Visibility)GetValue(LabelVisibilityProperty); }
            set { SetValue(LabelVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LabelVisibilityProperty =
            DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(YT_IconRadioButton),
                new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region TextOnly
        public Visibility IconVisiblity {
            get { return (Visibility)GetValue(IconVisiblityProperty); }
            set { SetValue(IconVisiblityProperty, value); }
        }
        public static readonly DependencyProperty IconVisiblityProperty =
            DependencyProperty.Register("IconVisiblity", typeof(Visibility),
                typeof(YT_IconRadioButton), new PropertyMetadata(Visibility.Visible));
        #endregion

        #region ButtonColor
        public Brush IconMaskN {
            get { return (Brush)GetValue(IconMaskNProperty); }
            set { SetValue(IconMaskNProperty, value); }
        }
        public static readonly DependencyProperty IconMaskNProperty =
            DependencyProperty.Register("IconMaskN", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconMaskR {
            get { return (Brush)GetValue(IconMaskRProperty); }
            set { SetValue(IconMaskRProperty, value); }
        }
        public static readonly DependencyProperty IconMaskRProperty =
            DependencyProperty.Register("IconMaskR", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(80, 0, 0, 0))));

        public Brush IconMaskP {
            get { return (Brush)GetValue(IconMaskPProperty); }
            set { SetValue(IconMaskPProperty, value); }
        }
        public static readonly DependencyProperty IconMaskPProperty =
            DependencyProperty.Register("IconMaskP", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconMaskS {
            get { return (Brush)GetValue(IconMaskSProperty); }
            set { SetValue(IconMaskSProperty, value); }
        }
        public static readonly DependencyProperty IconMaskSProperty =
            DependencyProperty.Register("IconMaskS", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromArgb(160, 0, 0, 0))));

        public Brush IconN {
            get { return (Brush)GetValue(IconNProperty); }
            set { SetValue(IconNProperty, value); }
        }
        public static readonly DependencyProperty IconNProperty =
            DependencyProperty.Register("IconN", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconR {
            get { return (Brush)GetValue(IconRProperty); }
            set { SetValue(IconRProperty, value); }
        }
        public static readonly DependencyProperty IconRProperty =
            DependencyProperty.Register("IconR", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconP {
            get { return (Brush)GetValue(IconPProperty); }
            set { SetValue(IconPProperty, value); }
        }
        public static readonly DependencyProperty IconPProperty =
            DependencyProperty.Register("IconP", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public Brush IconS {
            get { return (Brush)GetValue(IconSProperty); }
            set { SetValue(IconSProperty, value); }
        }
        public static readonly DependencyProperty IconSProperty =
            DependencyProperty.Register("IconS", typeof(Brush), typeof(YT_IconRadioButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        #endregion

        #region Labelfontsize
        public double LabelFontSize {
            get { return (double)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(double),
                typeof(YT_IconRadioButton), new PropertyMetadata(10.0));
        #endregion

        #region BackText
        public string BackText {
            get { return (string)GetValue(BackTextProperty); }
            set { SetValue(BackTextProperty, value); }
        }
        public static readonly DependencyProperty BackTextProperty =
            DependencyProperty.Register("BackText", typeof(string), typeof(YT_IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region ContentTextAlignment
        public TextAlignment ContentTextAlignment {
            get { return (TextAlignment)GetValue(ContentTextAlignmentProperty); }
            set { SetValue(ContentTextAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentTextAlignmentProperty =
            DependencyProperty.Register("ContentTextAlignment", typeof(TextAlignment), typeof(YT_IconRadioButton),
                new PropertyMetadata(TextAlignment.Center));
        #endregion

        #region ContentHorizontalAlignment
        public HorizontalAlignment ContentHorizontalAlignment {
            get { return (HorizontalAlignment)GetValue(ContentHorizontalAlignmentProperty); }
            set { SetValue(ContentHorizontalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentHorizontalAlignmentProperty =
            DependencyProperty.Register("ContentHorizontalAlignment", typeof(HorizontalAlignment), typeof(YT_IconRadioButton),
                new PropertyMetadata(HorizontalAlignment.Center));
        #endregion

        #region ContentVerticalAlignment
        public VerticalAlignment ContentVerticalAlignment {
            get { return (VerticalAlignment)GetValue(ContentVerticalAlignmentProperty); }
            set { SetValue(ContentVerticalAlignmentProperty, value); }
        }
        public static readonly DependencyProperty ContentVerticalAlignmentProperty =
            DependencyProperty.Register("ContentVerticalAlignment", typeof(VerticalAlignment), typeof(YT_IconRadioButton),
                new PropertyMetadata(VerticalAlignment.Center));
        #endregion

        #region AllowShadow
        public double AllowShadow {
            get { return (double)GetValue(AllowShadowProperty); }
            set { SetValue(AllowShadowProperty, value); }
        }
        public static readonly DependencyProperty AllowShadowProperty =
            DependencyProperty.Register("AllowShadow", typeof(double), typeof(YT_IconRadioButton), new PropertyMetadata(0.0));
        #endregion

        #region AllowAni
        public bool AllowAni {
            get { return (bool)GetValue(AllowAniProperty); }
            set { SetValue(AllowAniProperty, value); }
        }
        public static readonly DependencyProperty AllowAniProperty =
            DependencyProperty.Register("AllowAni", typeof(bool),
                typeof(YT_IconRadioButton), new PropertyMetadata(false));
        #endregion

        #region ForeIcon
        public string ForeIcon {
            get { return (string)GetValue(ForeIconProperty); }
            set { SetValue(ForeIconProperty, value); }
        }
        public static readonly DependencyProperty ForeIconProperty =
            DependencyProperty.Register("ForeIcon", typeof(string), typeof(YT_IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region BackIcon
        public string BackIcon {
            get { return (string)GetValue(BackIconProperty); }
            set { SetValue(BackIconProperty, value); }
        }
        public static readonly DependencyProperty BackIconProperty =
            DependencyProperty.Register("BackIcon", typeof(string), typeof(YT_IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region ForeTip
        public string ForeToolTip {
            get { return (string)GetValue(ForeToolTipProperty); }
            set { SetValue(ForeToolTipProperty, value); }
        }
        public static readonly DependencyProperty ForeToolTipProperty =
            DependencyProperty.Register("ForeToolTip", typeof(string), typeof(YT_IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region BackTip
        public string BackToolTip {
            get { return (string)GetValue(BackToolTipProperty); }
            set { SetValue(BackToolTipProperty, value); }
        }
        public static readonly DependencyProperty BackToolTipProperty =
            DependencyProperty.Register("BackToolTip", typeof(string), typeof(YT_IconRadioButton),
                new PropertyMetadata(""));
        #endregion

        #region ToolTipVisiblity
        public Visibility ToolTipVisiblity {
            get { return (Visibility)GetValue(ToolTipVisiblityProperty); }
            set { SetValue(ToolTipVisiblityProperty, value); }
        }
        public static readonly DependencyProperty ToolTipVisiblityProperty =
            DependencyProperty.Register("ToolTipVisiblity", typeof(Visibility), 
                typeof(YT_IconRadioButton),new PropertyMetadata(Visibility.Collapsed));
        #endregion

        #region RoatAngel
        public double RoatAngel {
            get { return (double)GetValue(RoatAngelProperty); }
            set { SetValue(RoatAngelProperty, value); }
        }
        public static readonly DependencyProperty RoatAngelProperty =
            DependencyProperty.Register("RoatAngel", typeof(double),
                typeof(YT_IconRadioButton), new PropertyMetadata(0.0, new PropertyChangedCallback(OnRoatAngelChanged)));
        private static void OnRoatAngelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((YT_IconRadioButton)d).IconRotate.Angle = (double)e.NewValue;
        }
        #endregion

        #region IconRotate
        public RotateTransform IconRotate {
            get { return (RotateTransform)GetValue(IconRotateProperty); }
            set { SetValue(IconRotateProperty, value); }
        }
        public static readonly DependencyProperty IconRotateProperty =
            DependencyProperty.Register("IconRotate", typeof(RotateTransform),
                typeof(YT_IconRadioButton), new PropertyMetadata(null));
        #endregion

        static YT_IconRadioButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YT_IconRadioButton), new FrameworkPropertyMetadata(typeof(YT_IconRadioButton)));
        }
    }
}
