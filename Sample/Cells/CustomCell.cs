using System;
using Xamarin.Forms;
using System.Windows.Input;

namespace Sample.Cells
{
    public class CustomCell:Cell //ContextActionを使いたい場合は継承元をViewCellにする
    {
        //タイトルの文字
        public static BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(CustomCell),
                default(string),
                defaultBindingMode: BindingMode.OneWay
            );

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        //タイトルの文字色
        public static BindableProperty TitleColorProperty =
            BindableProperty.Create(
                nameof(TitleColor),
                typeof(Color),
                typeof(CustomCell),
                default(Color),
                defaultBindingMode: BindingMode.OneWay
            );

        public Color TitleColor
        {
            get { return (Color)GetValue(TitleColorProperty); }
            set { SetValue(TitleColorProperty, value); }
        }

        public static BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(CustomCell),
                default(ICommand),
                defaultBindingMode: BindingMode.OneWay
            );

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
    }
}
