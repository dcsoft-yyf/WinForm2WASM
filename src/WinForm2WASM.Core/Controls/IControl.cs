namespace WinForm2WASM.Core.Controls;

/// <summary>
/// Base interface for all WinForm-compatible controls.
/// This provides a common abstraction for WinForm controls that can be rendered in Blazor WASM.
/// </summary>
public interface IControl
{
    /// <summary>
    /// Gets or sets the name of the control.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    string Text { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control can respond to user interaction.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>
    /// Gets or sets the width of the control.
    /// </summary>
    int Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the control.
    /// </summary>
    int Height { get; set; }

    /// <summary>
    /// Gets or sets the distance, in pixels, between the left edge of the control and the left edge of its container.
    /// </summary>
    int Left { get; set; }

    /// <summary>
    /// Gets or sets the distance, in pixels, between the top edge of the control and the top edge of its container.
    /// </summary>
    int Top { get; set; }
}
