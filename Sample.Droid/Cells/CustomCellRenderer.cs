using System;
using Sample.Cells;
using Sample.Droid.Cells;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomCell), typeof(CustomCellRenderer))]
namespace Sample.Droid.Cells
{
    public class CustomCellRenderer:CellRenderer // FormsのCellがViewCell派生ならViewCellRendererを使う
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            var nativeCell = convertView as NativeCustomCell;

            if (nativeCell == null)
            {
                //リサイクルでなければセルを生成する
                nativeCell = new NativeCustomCell(context, item);
            }

            //リサイクル前のFormsCellのPropertyChangedを解除する
            nativeCell.Cell.PropertyChanged -= nativeCell.CellPropertyChanged;

            //NativeCellに持たせてあるFormsCellへの参照を更新する（リサイクル前の値のままにしない）
            nativeCell.Cell = item;

            //リサイクル後のFormsCellのPropertyChangedを購読する
            item.PropertyChanged += nativeCell.CellPropertyChanged;

            //セル内容の更新
            nativeCell.UpdateCell();

            return nativeCell;
        }
    }
}
