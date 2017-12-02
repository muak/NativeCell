using System;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Reactive.Bindings;
using Sample.Cells;
using Xamarin.Forms;
using System.Linq;
using Prism.Services;

namespace Sample.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ObservableCollection<Person> ItemsSource { get; } = new ObservableCollection<Person>();
        public ReactiveCommand<string> Command { get; set; } = new ReactiveCommand<string>();

        public MainPageViewModel(IPageDialogService pageDialog)
        {
            var rand = new Random();
            for (var i = 0; i < 50; i++)
            {
                ItemsSource.Add(new Person { Name = RandomName(rand) });
            }

            Command.Subscribe(async text =>
            {
                await pageDialog.DisplayAlertAsync(text, "", "OK");
            });
        }

        string RandomName(Random rand)
        {
            var FirstName = new char[rand.Next(3, 10)];

            for (var i = 0; i < FirstName.Length; i++)
            {
                var c = i == 0 ? 'A' : 'a';
                FirstName[i] = (char)(c + rand.Next(0, 25));
            }

            var LastName = new char[rand.Next(3, 10)];

            for (var i = 0; i < LastName.Length; i++)
            {
                var c = i == 0 ? 'A' : 'a';
                LastName[i] = (char)(c + rand.Next(0, 25));
            }

            return new string(FirstName) + " " + new string(LastName);
        }


        public class Person
        {
            public string Name { get; set; }
            public Color Color { get; set; } = Color.Black;
        }
    }
}

