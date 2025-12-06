using WinForm2WASM.Core.Controls;

namespace WinForm2WASM.Core.Forms;

/// <summary>
/// Base interface for WinForm-compatible forms.
/// </summary>
public interface IForm
{
    /// <summary>
    /// Gets or sets the text displayed in the title bar of the form.
    /// </summary>
    string Text { get; set; }

    /// <summary>
    /// Gets or sets the width of the form.
    /// </summary>
    int Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the form.
    /// </summary>
    int Height { get; set; }

    /// <summary>
    /// Gets the collection of controls contained within the form.
    /// </summary>
    IReadOnlyList<IControl> Controls { get; }

    /// <summary>
    /// Adds a control to the form.
    /// </summary>
    /// <param name="control">The control to add.</param>
    void AddControl(IControl control);

    /// <summary>
    /// Removes a control from the form.
    /// </summary>
    /// <param name="control">The control to remove.</param>
    void RemoveControl(IControl control);
}
