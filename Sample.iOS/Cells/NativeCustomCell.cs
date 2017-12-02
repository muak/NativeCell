using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using Sample.Cells;
using UIKit;


namespace Sample.iOS.Cells
{
    public class NativeCustomCell : CellTableViewCell
    {
        CustomCell CustomCell => Cell as CustomCell;
        UILabel _titleLabel;

        public NativeCustomCell(Cell formsCell) : base(UIKit.UITableViewCellStyle.Default, formsCell.GetType().FullName)
        {
            Cell = formsCell;

            //UILabelの生成と配置
            _titleLabel = new UILabel();

            ContentView.AddSubview(_titleLabel);
            _titleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            _titleLabel.CenterXAnchor.ConstraintEqualTo(ContentView.CenterXAnchor).Active = true;
            _titleLabel.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;

            var tap = new UITapGestureRecognizer(_ =>
            {
                CustomCell.Command?.Execute(_titleLabel.Text);
            });
            _titleLabel.AddGestureRecognizer(tap);
            _titleLabel.UserInteractionEnabled = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _titleLabel.Dispose();
                _titleLabel = null;
                CustomCell.PropertyChanged -= CellPropertyChanged;
            }
            base.Dispose(disposing);
        }

        //リサイクル時に呼ばれるセル内容全更新メソッド
        public void UpdateCell()
        {
            UpdateTitle();
            UpdateTitleColor();

            SetNeedsLayout();
        }

        //ProperyChanged対応
        public void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CustomCell.TitleProperty.PropertyName)
            {
                UpdateTitle();
            }
            else if (e.PropertyName == CustomCell.TitleColorProperty.PropertyName)
            {
                UpdateTitleColor();
            }
        }

        //タイトル文字の更新
        void UpdateTitle()
        {
            _titleLabel.Text = CustomCell.Title;
        }

        //タイトル文字色の更新
        void UpdateTitleColor()
        {
            _titleLabel.TextColor = CustomCell.TitleColor.ToUIColor();
        }
    }
}
