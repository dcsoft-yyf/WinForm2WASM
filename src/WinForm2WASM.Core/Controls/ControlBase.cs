namespace WinForm2WASM.Core.Controls;

/// <summary>
/// Base implementation for WinForm-compatible controls.
/// </summary>
public abstract class ControlBase : IControl
{
    private string _name = string.Empty;
    private string _text = string.Empty;
    private bool _visible = true;
    private bool _enabled = true;
    private int _width = 100;
    private int _height = 23;
    private int _left;
    private int _top;

    /// <inheritdoc/>
    public virtual string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    /// <inheritdoc/>
    public virtual string Text
    {
        get => _text;
        set
        {
            if (_text != value)
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }

    /// <inheritdoc/>
    public virtual bool Visible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }
    }

    /// <inheritdoc/>
    public virtual bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled != value)
            {
                _enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }
    }

    /// <inheritdoc/>
    public virtual int Width
    {
        get => _width;
        set
        {
            if (_width != value)
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
    }

    /// <inheritdoc/>
    public virtual int Height
    {
        get => _height;
        set
        {
            if (_height != value)
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
    }

    /// <inheritdoc/>
    public virtual int Left
    {
        get => _left;
        set
        {
            if (_left != value)
            {
                _left = value;
                OnPropertyChanged(nameof(Left));
            }
        }
    }

    /// <inheritdoc/>
    public virtual int Top
    {
        get => _top;
        set
        {
            if (_top != value)
            {
                _top = value;
                OnPropertyChanged(nameof(Top));
            }
        }
    }

    /// <summary>
    /// Event raised when a property value changes.
    /// </summary>
    public event EventHandler<string>? PropertyChanged;

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, propertyName);
    }
}
