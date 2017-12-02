using System.ComponentModel;
using Sample.Cells;
using Sample.iOS.Cells;
using UIKit;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CustomCell2), typeof(NativeCell2Renderer))]
namespace Sample.iOS.Cells
{
    public class NativeCell2Renderer : CellBaseRenderer<NativeCell2> { }

    public class NativeCell2 : NativeCellBase
    {
        
        public NativeCell2(Cell formsCell) : base(formsCell)
        {
            //レイアウトの処理
        }

        public override void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //PropertyChangedの処理
        }

        public override void UpdateCell()
        {
            //セル更新処理
        }
    }
}
