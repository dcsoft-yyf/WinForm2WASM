namespace WinForm2WASM.Core.Controls;

/// <summary>
/// Represents a WinForm-compatible button control.
/// </summary>
public class Button : ControlBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Button"/> class.
    /// </summary>
    public Button()
    {
        Width = 75;
        Height = 23;
    }

    /// <summary>
    /// Occurs when the button is clicked.
    /// </summary>
    public event EventHandler? Click;

    /// <summary>
    /// Raises the Click event.
    /// </summary>
    public virtual void OnClick()
    {
        Click?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Performs a click operation on the button.
    /// </summary>
    public void PerformClick()
    {
        if (Enabled && Visible)
        {
            OnClick();
        }
    }
}
