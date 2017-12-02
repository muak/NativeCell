using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Sample.iOS.Cells
{
    public class CellBaseRenderer<TnativeCell> : CellRenderer where TnativeCell : NativeCellBase
    {
        internal static class InstanceCreator<T1, TInstance>
        {
            public static Func<T1, TInstance> Create { get; } = CreateInstance();

            private static Func<T1, TInstance> CreateInstance()
            {
                var constructor = typeof(TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
                    new[] { typeof(T1) }, null);
                var arg1 = Expression.Parameter(typeof(T1));
                return Expression.Lambda<Func<T1, TInstance>>(Expression.New(constructor, arg1), arg1).Compile();
            }
        }

        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            TnativeCell nativeCell = reusableCell as TnativeCell;
            if (nativeCell == null)
            {
                nativeCell = InstanceCreator<Cell, TnativeCell>.Create(item);
            }

            nativeCell.Cell.PropertyChanged -= nativeCell.CellPropertyChanged;

            nativeCell.Cell = item;

            item.PropertyChanged += nativeCell.CellPropertyChanged;

            nativeCell.UpdateCell();

            return nativeCell;
        }
    }

    public abstract class NativeCellBase : CellTableViewCell
    {
        public NativeCellBase(Cell formsCell) : base(UITableViewCellStyle.Default, formsCell.GetType().FullName)
        {
            Cell = formsCell;
        }

        public abstract void CellPropertyChanged(object sender, PropertyChangedEventArgs e);

        public abstract void UpdateCell();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Cell.PropertyChanged -= CellPropertyChanged;
            }
            base.Dispose(disposing);
        }
    }
}
