using WinForm2WASM.Core.Controls;
using WinForm2WASM.Core.Forms;

namespace WinForm2WASM.Core.Tests.Forms;

public class TestForm : FormBase
{
    public bool LoadCalled { get; private set; }
    public bool ShownCalled { get; private set; }

    protected override void OnLoad()
    {
        LoadCalled = true;
        base.OnLoad();
    }

    protected override void OnShown()
    {
        ShownCalled = true;
        base.OnShown();
    }
}

public class FormBaseTests
{
    [Fact]
    public void FormBase_DefaultProperties_AreCorrect()
    {
        var form = new TestForm();

        Assert.Equal(800, form.Width);
        Assert.Equal(600, form.Height);
        Assert.Equal(string.Empty, form.Text);
        Assert.Empty(form.Controls);
    }

    [Fact]
    public void FormBase_AddControl_AddsToCollection()
    {
        var form = new TestForm();
        var button = new Button { Name = "button1" };

        form.AddControl(button);

        Assert.Single(form.Controls);
        Assert.Same(button, form.Controls[0]);
    }

    [Fact]
    public void FormBase_RemoveControl_RemovesFromCollection()
    {
        var form = new TestForm();
        var button = new Button { Name = "button1" };
        form.AddControl(button);

        form.RemoveControl(button);

        Assert.Empty(form.Controls);
    }

    [Fact]
    public void FormBase_ControlAdded_RaisesEvent()
    {
        var form = new TestForm();
        var button = new Button();
        IControl? addedControl = null;

        form.ControlAdded += (sender, control) => addedControl = control;
        form.AddControl(button);

        Assert.Same(button, addedControl);
    }

    [Fact]
    public void FormBase_ControlRemoved_RaisesEvent()
    {
        var form = new TestForm();
        var button = new Button();
        IControl? removedControl = null;

        form.AddControl(button);
        form.ControlRemoved += (sender, control) => removedControl = control;
        form.RemoveControl(button);

        Assert.Same(button, removedControl);
    }

    [Fact]
    public void FormBase_Initialize_CallsLoadAndShown()
    {
        var form = new TestForm();

        form.Initialize();

        Assert.True(form.LoadCalled);
        Assert.True(form.ShownCalled);
    }

    [Fact]
    public void FormBase_SetText_UpdatesProperty()
    {
        var form = new TestForm { Text = "My Form" };

        Assert.Equal("My Form", form.Text);
    }

    [Fact]
    public void FormBase_AddNullControl_ThrowsArgumentNullException()
    {
        var form = new TestForm();

        Assert.Throws<ArgumentNullException>(() => form.AddControl(null!));
    }
}
