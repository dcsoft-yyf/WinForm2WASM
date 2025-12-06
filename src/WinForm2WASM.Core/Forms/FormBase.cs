using WinForm2WASM.Core.Controls;

namespace WinForm2WASM.Core.Forms;

/// <summary>
/// Base implementation for WinForm-compatible forms.
/// </summary>
public abstract class FormBase : IForm
{
    private readonly List<IControl> _controls = [];
    private string _text = string.Empty;
    private int _width = 800;
    private int _height = 600;

    /// <inheritdoc/>
    public string Text
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
    public int Width
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
    public int Height
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
    public IReadOnlyList<IControl> Controls => _controls.AsReadOnly();

    /// <inheritdoc/>
    public void AddControl(IControl control)
    {
        ArgumentNullException.ThrowIfNull(control);
        _controls.Add(control);
        OnControlAdded(control);
    }

    /// <inheritdoc/>
    public void RemoveControl(IControl control)
    {
        ArgumentNullException.ThrowIfNull(control);
        if (_controls.Remove(control))
        {
            OnControlRemoved(control);
        }
    }

    /// <summary>
    /// Event raised when a property value changes.
    /// </summary>
    public event EventHandler<string>? PropertyChanged;

    /// <summary>
    /// Event raised when a control is added to the form.
    /// </summary>
    public event EventHandler<IControl>? ControlAdded;

    /// <summary>
    /// Event raised when a control is removed from the form.
    /// </summary>
    public event EventHandler<IControl>? ControlRemoved;

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, propertyName);
    }

    /// <summary>
    /// Called when a control is added to the form.
    /// </summary>
    /// <param name="control">The control that was added.</param>
    protected virtual void OnControlAdded(IControl control)
    {
        ControlAdded?.Invoke(this, control);
    }

    /// <summary>
    /// Called when a control is removed from the form.
    /// </summary>
    /// <param name="control">The control that was removed.</param>
    protected virtual void OnControlRemoved(IControl control)
    {
        ControlRemoved?.Invoke(this, control);
    }

    /// <summary>
    /// Called when the form is loaded.
    /// </summary>
    protected virtual void OnLoad()
    {
    }

    /// <summary>
    /// Called when the form is shown.
    /// </summary>
    protected virtual void OnShown()
    {
    }

    /// <summary>
    /// Initializes the form. Call this method to trigger the Load event.
    /// </summary>
    public void Initialize()
    {
        OnLoad();
        OnShown();
    }
}
