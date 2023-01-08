using System.ComponentModel;
using System.Diagnostics;
using CSGAAP.Generics;

namespace CSGAAP.Util
{
    [DebuggerDisplay("{DisplayName}:{Value}")]
    public class Parameter : IDisplayable, INotifyPropertyChanged, ICloneable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string DisplayName { get; }
        public virtual string ToolTipText => DisplayName;
        public virtual string LongDescription => ToolTipText;
        public virtual bool ShowInGUI => true;
        public object DefaultValue { get; }
        public Array PossibleValues { get; }
        private object? _value;
        public object Value
        {
            get => _value ?? DefaultValue;
            set
            {
                _value = value;
                PropertyChanged?.Invoke(this, new(nameof(Value)));
            }
        }

        public void SetStringValue(string val)
        {
            var rt = DefaultValue.GetType();
            var t = Nullable.GetUnderlyingType(rt) ?? rt;
            Value = Convert.ChangeType(val, t);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public Parameter(string displayName, object defaultValue, Array possibleValues)
        {
            DisplayName = displayName;
            DefaultValue = defaultValue;
            PossibleValues = possibleValues;
        }
    }
}
