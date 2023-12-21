using CommunityToolkit.Mvvm.ComponentModel;

namespace GameBarClock.Models
{
    public class Property<T> : ObservableObject
    {
        public Property(string name, T value) => (Name, Value) = (name, value);

        private T value;
        private string name;
        public string Name { get => name; set => SetProperty(ref name, value); }
        public T Value { get => value; set => SetProperty(ref this.value, value); }
    }

    public class KeyProperty<T> : Property<T>
    {
        public KeyProperty(string name, T value, string key) : base(name, value) => Key = key;

        public string Key { get; }
    }

    public class IconProperty<T> : Property<T>
    {
        public IconProperty(string name, T value, string icon) : base(name, value) => Icon = icon;

        private string icon;
        public string Icon { get => icon; set => SetProperty(ref icon, value); }
    }


}
