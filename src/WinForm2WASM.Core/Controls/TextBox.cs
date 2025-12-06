namespace WinForm2WASM.Core.Controls;

/// <summary>
/// Represents a WinForm-compatible text box control.
/// </summary>
public class TextBox : ControlBase
{
    private bool _readOnly;
    private int _maxLength = 32767;
    private string _placeholderText = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextBox"/> class.
    /// </summary>
    public TextBox()
    {
        Width = 100;
        Height = 23;
    }

    /// <summary>
    /// Gets or sets a value indicating whether text in the text box is read-only.
    /// </summary>
    public bool ReadOnly
    {
        get => _readOnly;
        set
        {
            if (_readOnly != value)
            {
                _readOnly = value;
                OnPropertyChanged(nameof(ReadOnly));
            }
        }
    }

    /// <summary>
    /// Gets or sets the maximum number of characters the user can type into the text box control.
    /// </summary>
    public int MaxLength
    {
        get => _maxLength;
        set
        {
            if (_maxLength != value)
            {
                _maxLength = value;
                OnPropertyChanged(nameof(MaxLength));
            }
        }
    }

    /// <summary>
    /// Gets or sets the text that is displayed when the control has no text and does not have focus.
    /// </summary>
    public string PlaceholderText
    {
        get => _placeholderText;
        set
        {
            if (_placeholderText != value)
            {
                _placeholderText = value;
                OnPropertyChanged(nameof(PlaceholderText));
            }
        }
    }

    /// <summary>
    /// Occurs when the Text property value changes.
    /// </summary>
    public event EventHandler? TextChanged;

    /// <summary>
    /// Raises the TextChanged event.
    /// </summary>
    protected virtual void OnTextChanged()
    {
        TextChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public override string Text
    {
        get => base.Text;
        set
        {
            if (base.Text != value)
            {
                base.Text = value;
                OnTextChanged();
            }
        }
    }
}
