using System;
using Android.Content;
using Xamarin.Forms;
using Sample.Cells;
using Android.Widget;
using Android.Views;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;

namespace Sample.Droid.Cells
{
    public class NativeCustomCell : Android.Widget.RelativeLayout, INativeElementView
    {
        public Cell Cell { get; set; }
        public Element Element => Cell;
        public CustomCell CustomCell => Cell as CustomCell;

        TextView _titleLabel;

        public NativeCustomCell(Context context, Cell formsCell) : base(context)
        {
            Cell = formsCell;

            SetMinimumHeight((int)context.ToPixels(44));

            //Layout呼び出して親とドッキングする（AddViewする必要無し）
            var contentView = LayoutInflater.FromContext(context).Inflate(Resource.Layout.NativeCustomCellLayout, this, true);
            //必要部品を取り出す
            _titleLabel = contentView.FindViewById<TextView>(Resource.Id.TitleLabel);

            _titleLabel.Click += (sender, e) =>
            {
                CustomCell.Command?.Execute(_titleLabel.Text);
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _titleLabel.Dispose();
                _titleLabel = null;
                Cell = null;
                CustomCell.PropertyChanged -= CellPropertyChanged;
            }
            base.Dispose(disposing);
        }

        //リサイクル時に呼ばれるセル内容全更新メソッド
        public void UpdateCell()
        {
            UpdateTitle();
            UpdateTitleColor();

            Invalidate();
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
            _titleLabel.SetTextColor(CustomCell.TitleColor.ToAndroid());
        }
    }
}
