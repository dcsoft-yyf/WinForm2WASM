# WinForm2WASM

Migrate WinForm.NET Code to Blazor WASM Platform

## Overview

WinForm2WASM is a library that enables the migration of WinForm.NET applications to Blazor WebAssembly. It provides WinForm-compatible abstractions that can be rendered in a web browser using Blazor WASM.

## Project Structure

```
WinForm2WASM/
├── src/
│   ├── WinForm2WASM.Core/         # Core abstractions and WinForm-compatible controls
│   │   ├── Controls/              # Control implementations (Button, TextBox, Label, etc.)
│   │   └── Forms/                 # Form abstractions
│   └── WinForm2WASM.Web/          # Blazor WebAssembly frontend
└── tests/
    └── WinForm2WASM.Core.Tests/   # Unit tests for the Core library
```

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later

### Build

```bash
dotnet build
```

### Run Tests

```bash
dotnet test
```

### Run the Web Application

```bash
dotnet run --project src/WinForm2WASM.Web
```

## Features

### Core Controls

- **Button**: Standard button control with Click event support
- **TextBox**: Text input control with TextChanged event support
- **Label**: Static text display control

### Forms

- **FormBase**: Base class for creating WinForm-compatible forms with control management

## Usage Example

```csharp
using WinForm2WASM.Core.Controls;
using WinForm2WASM.Core.Forms;

public class MyForm : FormBase
{
    private Button _button;
    private TextBox _textBox;
    private Label _label;

    protected override void OnLoad()
    {
        Text = "My Form";
        
        _label = new Label { Text = "Enter your name:", Left = 10, Top = 10 };
        _textBox = new TextBox { Left = 10, Top = 35, Width = 200 };
        _button = new Button { Text = "Submit", Left = 10, Top = 65 };
        
        _button.Click += (s, e) => 
        {
            _label.Text = $"Hello, {_textBox.Text}!";
        };
        
        AddControl(_label);
        AddControl(_textBox);
        AddControl(_button);
    }
}
```

## License

This project is open source.
