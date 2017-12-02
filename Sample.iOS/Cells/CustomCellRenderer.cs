using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using Sample.Cells;
using Sample.iOS.Cells;

[assembly: ExportRenderer(typeof(CustomCell), typeof(CustomCellRenderer))]
namespace Sample.iOS.Cells
{
    public class CustomCellRenderer : CellRenderer // FormsのCellがViewCell派生ならViewCellRendererを使う
    {
        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var nativeCell = reusableCell as NativeCustomCell;

            if (nativeCell == null)
            {
                //リサイクルでなければセルを生成する
                nativeCell = new NativeCustomCell(item);
            }

            //リサイクル前のFormsCellのPropertyChangedを解除する
            nativeCell.Cell.PropertyChanged -= nativeCell.CellPropertyChanged;

            //NativeCellに持たせてあるFormsCellへの参照を更新する（リサイクル前の値のままにしない）
            nativeCell.Cell = item;

            //リサイクル後のFormsCellのProe
            item.PropertyChanged += nativeCell.CellPropertyChanged;

            //セル内容の更新
            nativeCell.UpdateCell();

            return nativeCell;
        }
    }
}
