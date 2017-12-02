using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace Sample.Droid.Cells
{
    public class CellBaseRenderer<TnativeCell> : CellRenderer where TnativeCell : NativeCellBase
    {
        internal static class InstanceCreator<T1, T2, TInstance>
        {
            public static Func<T1, T2, TInstance> Create { get; } = CreateInstance();

            private static Func<T1, T2, TInstance> CreateInstance()
            {
                var argsTypes = new[] { typeof(T1), typeof(T2) };
                var constructor = typeof(TInstance).GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder,
                       argsTypes, null);
                var args = argsTypes.Select(Expression.Parameter).ToArray();
                return Expression.Lambda<Func<T1, T2, TInstance>>(Expression.New(constructor, args), args).Compile();
            }
        }

        protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            TnativeCell nativeCell = convertView as TnativeCell;
            if (nativeCell == null)
            {
                nativeCell = InstanceCreator<Context, Xamarin.Forms.Cell, TnativeCell>.Create(context, item);
            }

            nativeCell.Cell.PropertyChanged -= nativeCell.CellPropertyChanged;

            nativeCell.Cell = item;

            item.PropertyChanged += nativeCell.CellPropertyChanged;

            nativeCell.UpdateCell();

            return nativeCell;
        }
    }

    public abstract class NativeCellBase : Android.Widget.RelativeLayout, INativeElementView
    {
        public Cell Cell { get; set; }
        public Element Element => Cell;

        public NativeCellBase(Context context, Cell formCell) : base(context)
        {
            Cell = formCell;
        }

        public abstract void CellPropertyChanged(object sender, PropertyChangedEventArgs e);

        public abstract void UpdateCell();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Cell.PropertyChanged -= CellPropertyChanged;
                Cell = null;
            }
            base.Dispose(disposing);
        }
    }
}
