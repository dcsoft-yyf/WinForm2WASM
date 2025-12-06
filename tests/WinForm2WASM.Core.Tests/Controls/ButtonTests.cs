using WinForm2WASM.Core.Controls;

namespace WinForm2WASM.Core.Tests.Controls;

public class ButtonTests
{
    [Fact]
    public void Button_DefaultProperties_AreCorrect()
    {
        var button = new Button();

        Assert.Equal(75, button.Width);
        Assert.Equal(23, button.Height);
        Assert.True(button.Enabled);
        Assert.True(button.Visible);
        Assert.Equal(string.Empty, button.Text);
    }

    [Fact]
    public void Button_Click_RaisesEvent()
    {
        var button = new Button();
        var clicked = false;

        button.Click += (sender, e) => clicked = true;
        button.PerformClick();

        Assert.True(clicked);
    }

    [Fact]
    public void Button_ClickWhenDisabled_DoesNotRaiseEvent()
    {
        var button = new Button { Enabled = false };
        var clicked = false;

        button.Click += (sender, e) => clicked = true;
        button.PerformClick();

        Assert.False(clicked);
    }

    [Fact]
    public void Button_ClickWhenNotVisible_DoesNotRaiseEvent()
    {
        var button = new Button { Visible = false };
        var clicked = false;

        button.Click += (sender, e) => clicked = true;
        button.PerformClick();

        Assert.False(clicked);
    }

    [Fact]
    public void Button_SetText_UpdatesProperty()
    {
        var button = new Button { Text = "Click Me" };

        Assert.Equal("Click Me", button.Text);
    }
}
