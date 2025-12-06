using WinForm2WASM.Core.Controls;

namespace WinForm2WASM.Core.Tests.Controls;

public class TextBoxTests
{
    [Fact]
    public void TextBox_DefaultProperties_AreCorrect()
    {
        var textBox = new TextBox();

        Assert.Equal(100, textBox.Width);
        Assert.Equal(23, textBox.Height);
        Assert.False(textBox.ReadOnly);
        Assert.Equal(32767, textBox.MaxLength);
        Assert.Equal(string.Empty, textBox.PlaceholderText);
    }

    [Fact]
    public void TextBox_TextChanged_RaisesEvent()
    {
        var textBox = new TextBox();
        var eventRaised = false;

        textBox.TextChanged += (sender, e) => eventRaised = true;
        textBox.Text = "Hello";

        Assert.True(eventRaised);
    }

    [Fact]
    public void TextBox_SameTextAssigned_DoesNotRaiseEvent()
    {
        var textBox = new TextBox { Text = "Hello" };
        var eventRaised = false;

        textBox.TextChanged += (sender, e) => eventRaised = true;
        textBox.Text = "Hello";

        Assert.False(eventRaised);
    }

    [Fact]
    public void TextBox_SetReadOnly_UpdatesProperty()
    {
        var textBox = new TextBox { ReadOnly = true };

        Assert.True(textBox.ReadOnly);
    }

    [Fact]
    public void TextBox_SetMaxLength_UpdatesProperty()
    {
        var textBox = new TextBox { MaxLength = 100 };

        Assert.Equal(100, textBox.MaxLength);
    }

    [Fact]
    public void TextBox_SetPlaceholderText_UpdatesProperty()
    {
        var textBox = new TextBox { PlaceholderText = "Enter text here" };

        Assert.Equal("Enter text here", textBox.PlaceholderText);
    }
}
