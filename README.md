# WinForm2WASM
Migrate WinForm.NET Code to Blazor WASM Platform. 
<br />Long-term goal: To migrate WinForm.NET programs to the Blazor WASM platform by modifying less than 5%-10% of the code volume.
# 如何将WinForm.NET代码迁移到Blazor WASM平台上
<span style="blue">How to Migrate WinForm.NET Code to Blazor WASM Platform</span>

南京都昌信息科技有限公司 袁永福 2025-12-3
<br/><span style="color:blue">Nanjing Duchang Information Technology Co., Ltd. Yuan Yongfu 2025-12-3</span>

## 1.前言<span style="color:blue"> Preface</span>

自从基于MS .NET Framework的WinForm.NET开发模式的诞生，20多年来IT业界开发了大量的WinForm.NET软件。
<br/><span style="color:blue">Since the birth of the WinForm.NET development model based on MS .NET Framework, the IT industry has developed a large number of WinForm.NET software over the past 20 years.</span>

但近几年，随着toB软件快速从CS模式转变为BS模式，叠加国产信创的泰山压顶。
<br/><span style="color:blue">However, in recent years, with the rapid transformation of toB software from CS mode to BS mode, coupled with the pressure of domestic independent innovation.</span>

大量的WinForm.NET软件遇到生死局，数千万行C#代码可能会废弃，大量开发组织和用户面临重大价值损失。
<br/><span style="color:blue">A large number of WinForm.NET software are facing a life-or-death situation, tens of millions of lines of C# code may be abandoned, and a large number of development organizations and users are facing significant value losses.</span>

我们也遇到这个难题，在过去2年的时间，我们花费了很大的精力，成功的将DCWriter编辑器控件WinForm.NET版迁移到Blazor WASM平台上，完美的解决了这个生死难题。
<br/><span style="color:blue">We also encountered this problem. Over the past 2 years, we have spent a lot of effort to successfully migrate the WinForm.NET version of the DCWriter editor control to the Blazor WASM platform, perfectly solving this life-or-death problem.</span>

在此说明其中的技术原理，展示我们是如何做到的，希望能给遇到类似情况的开发者提供一个参考意见。
<br/><span style="color:blue">Here we explain the technical principles, show how we did it, and hope to provide a reference for developers who encounter similar situations.</span>

## 2.基本原理<span style="color:blue"> Basic Principles</span>

下图是WinForm.NET程序的基础架构图：
<br/><span style="color:blue">The following figure is the basic architecture diagram of the WinForm.NET program:</span>
<br/><img src="images/1.png?raw=true" /><br/>
在这个架构图中，System.Windows.Forms.Control和System.Drawings.Graphics是最核心的类型。
<br/><span style="color:blue">In this architecture diagram, System.Windows.Forms.Control and System.Drawings.Graphics are the core types.</span>

System.Windows.Forms.Control类型用于将鼠标键盘事件发送给DCWriter核心模块来完成用户互动的操作。
<br/><span style="color:blue">The System.Windows.Forms.Control type is used to send mouse and keyboard events to the DCWriter core module to complete user interaction operations.</span>

而DCWriter核心模块则使用System.Drawings.Graphics类型来绘制用户界面，使得软件和用户互动，形成一个闭环。
<br/><span style="color:blue">The DCWriter core module uses the System.Drawings.Graphics type to draw the user interface, enabling the software to interact with users and form a closed loop.</span>

参考WinForm.NET程序的基本原理，我们摸索出如下的程序架构：
<br/><span style="color:blue">Referring to the basic principles of the WinForm.NET program, we have explored the following program architecture:</span>
<br/><img src="images/2.png?raw=true" /><br/>
要实现这个架构，核心是模拟出System.Windows.Forms.Control和System.Drawing.Graphics类型，只要模拟得足够像，则DCWriter核心模块无需大改就能运行起来。
<br/><span style="color:blue">To implement this architecture, the core is to simulate the System.Windows.Forms.Control and System.Drawing.Graphics types. As long as the simulation is similar enough, the DCWriter core module can run without major modifications.</span>

这样DCWriter核心模块和用户操作也能形成闭环，达成实时互动。这就是将WinForm.NET程序迁移到Blazor WASM平台上的基本原理。
<br/><span style="color:blue">In this way, the DCWriter core module and user operations can also form a closed loop to achieve real-time interaction. This is the basic principle of migrating WinForm.NET programs to the Blazor WASM platform.</span>

## 3.实现过程<span style="color:blue"> Implementation Process</span>

按照这个架构图，我们如下进行分步实现的：
<br/><span style="color:blue">According to this architecture diagram, we implemented it step by step as follows:</span>

### 3.1.模拟System.Windows.Forms.Control类型 | <span style="color:blue"> Simulate the System.Windows.Forms.Control Type</span>

Blazor WASM架构中是没有System.Windows.Forms.dll的，因此我们来创建一个C#类型 public class Control{}。
<br/><span style="color:blue">There is no System.Windows.Forms.dll in the Blazor WASM architecture, so we create a C# type: public class Control{}.</span>
<br/><img src="images/3.png?raw=true" /><br/>
该类型包含以下成员：
<br/><span style="color:blue">This type contains the following members:</span>

这个成员函数完整模拟了核心模块所依赖的标准System.Windows.Forms.Control的成员。
<br/><span style="color:blue">This member function completely simulates the members of the standard System.Windows.Forms.Control that the core module depends on.</span>

在代码中使用了很多Blazor WASM标准库中不存在的类型，比如Color、Rectange、Cursor、KeyEventArgs、KeyPressEventArgs、MouseEventArgs、PaintEventArgs之类的，都需要我们扩散定义。
<br/><span style="color:blue">The code uses many types that do not exist in the Blazor WASM standard library, such as Color, Rectange, Cursor, KeyEventArgs, KeyPressEventArgs, MouseEventArgs, PaintEventArgs, etc., which all need us to define extensively.</span>

然后我们跟着定义周边的类型，比如ScrollableControl、UserControl之类的。由于我们的DCWriter编辑器控件是派生自UserControl，于是一部分DCWriter核心模块形式上开始迁移过来了。
<br/><span style="color:blue">Then we define the surrounding types accordingly, such as ScrollableControl, UserControl, etc. Since our DCWriter editor control is derived from UserControl, part of the DCWriter core module has formally started to be migrated over.</span>

#### 3.1.1.模拟键盘事件 <span style="color:blue"> Simulate Keyboard Events</span>

WinForm.NET程序是靠重写Control.OnKeyUp/OnKeyPres/OnKeyDown虚函数来实现键盘事件。事件参数类型是System.Windows.Forms.KeyEventArgs/KeyPressEventArgs。
<br/><span style="color:blue">WinForm.NET programs implement keyboard events by overriding the virtual functions Control.OnKeyUp/OnKeyPres/OnKeyDown. The event parameter types are System.Windows.Forms.KeyEventArgs/KeyPressEventArgs.</span>
首先定义一个鼠标事件转发器。其代码如下：
<br/><span style="color:blue">First, define a mouse event forwarder. Its code is as follows:</span>
<br/><img src="images/4.png?raw=true" /><br/>

这是一个标记了[JSInvokeable]的函数，这个函数接受JS端传过来的事件参数，将其转换为一个System.Windows.Forms.KeyEventArgs，然后根据事件名称触发控件的键盘事件。
<br/><span style="color:blue">This is a function marked with [JSInvokeable]. This function accepts the event parameters passed from the JS side, converts them into a System.Windows.Forms.KeyEventArgs, and then triggers the keyboard event of the control according to the event name.</span>

然后在JavaScript端，我们对一个`<input type=text>`绑定了键盘事件处理：
<br/><span style="color:blue">Then on the JavaScript side, we bind keyboard event handling to an `<input type=text>` element:</span>
<br/><img src="images/5.png?raw=true" /><br/>
在这个键盘事件处理的JS代码中，我们使用invokeMethod()通过JSInterop调用了在C#端定义的EditorHandleKeyEvent()函数，并将事件参数传递给C#端。
<br/><span style="color:blue">In the JS code for this keyboard event handling, we use invokeMethod() to call the EditorHandleKeyEvent() function defined on the C# side through JSInterop, and pass the event parameters to the C# side.</span>
<br/><img src="images/6.png?raw=true" /><br/>
通过这种方式，我们打通了“HTML元素键盘事件->转换器->KeyEventArgs->Control.OnKeyDown->DCWriter核心模块”的事件传递通道。
<br/><span style="color:blue">In this way, we have opened up the event transmission channel of "HTML element keyboard event -> converter -> KeyEventArgs -> Control.OnKeyDown -> DCWriter core module".</span>

通过类似的方式，我们打通了鼠标点击、移动、拖拽事件的传递通道。
<br/><span style="color:blue">In a similar way, we have opened up the transmission channel for mouse click, movement, and drag events.</span>

#### 3.1.2.模拟Control.Invalidate(Rectangle) | <span style="color:blue"> Simulate Control.Invalidate(Rectangle)</span>

在 WinForm.NET程序中，Control.Invalidate()也是一个非常重要的成员方法需要模拟出来，为此我们定义以下方法：
<br/><span style="color:blue">In WinForm.NET programs, Control.Invalidate() is also a very important member method that needs to be simulated. For this purpose, we define the following methods:</span>

1.定义C#方法void Control.Invalidate( Rectangle )方法，则该方法内部使用一个List<Rectangle>来存储多个无效矩形区域，并进行矩形区域的合并操作，减少重绘的工作量。然后通过JSInterop调用无参数的JS函数NeedUpdateView()。
<br/><span style="color:blue">1. Define the C# method void Control.Invalidate(Rectangle). Internally, this method uses a List<Rectangle> to store multiple invalid rectangular areas, and performs merging operations on the rectangular areas to reduce the workload of redrawing. Then call the parameterless JS function NeedUpdateView() through JSInterop.</span>

2.定义C#方法[JSInvokeable]public byte[] GetInvalidateViewData()，该函数检索无效矩形列表，获得第一个无效矩形作为ClipRectangle，然后创建Graphcis对象，创建一个System.Windows.Forms.PaintEventArgs对象，调用核心模块的绘图模块来绘制图形，返回包含绘图指令的字节数组，删除第一个无效矩形对象。如果没有任何无效矩形区域，则返回null。
<br/><span style="color:blue">2. Define the C# method [JSInvokeable] public byte[] GetInvalidateViewData(). This function retrieves the invalid rectangle list, obtains the first invalid rectangle as ClipRectangle, then creates a Graphics object and a System.Windows.Forms.PaintEventArgs object, calls the drawing module of the core module to draw graphics, returns a byte array containing drawing instructions, and deletes the first invalid rectangle object. If there are no invalid rectangular areas, return null.</span>

3.定义JS方法 function NeedUpdateView()函数，该函数使用window.setTimeout()来延时调用另外一个JS函数 function DrawContent()。
<br/><span style="color:blue">3. Define the JS method function NeedUpdateView(), which uses window.setTimeout() to delay calling another JS function function DrawContent().</span>

4.定义JS方法function DrawContent()，该函数通过JSInterop调用C#函数GetInvalidateViewData()，尝试获得绘图指令字节数组。如果存在则调用JS类PageCotentDrawer在HtmlCanvasElement上的指定区域绘制图形。
<br/><span style="color:blue">4. Define the JS method function DrawContent(), which calls the C# function GetInvalidateViewData() through JSInterop to try to obtain the drawing instruction byte array. If it exists, call the JS class PageCotentDrawer to draw graphics in the specified area on the HtmlCanvasElement.</span>

通过这4个方法以Control.Invalidate() 牵头串联在一起，共同完成用户界面的主动局部重绘的功能。
<br/><span style="color:blue">These 4 methods are connected together led by Control.Invalidate() to jointly complete the function of active partial redrawing of the user interface.</span>

### 3.2.模拟System.Drawing.Graphcis类型 | <span style="color:blue"> Simulate the System.Drawing.Graphics Type</span>

我们在C#端定义了Graphcis类型，其包含的成员如下所示：
<br/><span style="color:blue">We defined the Graphics type on the C# side, which contains the following members:</span>
<br/><img src="images/7.png?raw=true" /><br/>
在各个绘图函数内部，会将绘图指定和使用的参数值转储到一个内存字节流中。比如对于DrawLine()其代码如下：
<br/><span style="color:blue">Inside each drawing function, the drawing instructions and the parameter values used are dumped into an in-memory byte stream. For example, the code for DrawLine() is as follows:</span>
<br/><img src="images/8.png?raw=true" /><br/>
这样当所有的绘图操作完成，Graphics内部一结算，就可以获得一个包含绘图指令的字节数组，然后返回给JS端。
<br/><span style="color:blue">In this way, when all drawing operations are completed, the internal settlement of Graphics can obtain a byte array containing drawing instructions, which is then returned to the JS side.</span>

#### 3.2.1.模拟Graphics.MeasureString() | <span style="color:blue"> Simulate Graphics.MeasureString()</span>

这里还有一个非常重要的成员方法MeasureString()需要进行模拟，这个方法用于测量字符的显示宽度，直接决定了文档的排版结果。
<br/><span style="color:blue">There is another very important member method MeasureString() that needs to be simulated. This method is used to measure the display width of characters, which directly determines the typesetting result of the document.</span>

由于文档中可能包括十万个字符，如果依赖浏览器的measureText()，则由于频繁的JSInterop操作降低性能，而且各个浏览器之间运算结果可能不一致。为此我们采用如下方法：
<br/><span style="color:blue">Since a document may contain 100,000 characters, relying on the browser's measureText() will reduce performance due to frequent JSInterop operations, and the calculation results may be inconsistent between different browsers. For this reason, we adopt the following methods:</span>

1.使用WinForm.NET开发一个专用工具解析字体TTC/TTF文件，提取所有字符的宽度数值。
<br/><span style="color:blue">1. Use WinForm.NET to develop a special tool to parse font TTC/TTF files and extract the width values of all characters.</span>

2.将字符的Uncode编码根据相同的字符宽度来划分区域，以此将这些宽度数值高度压缩为“字体快照信息”。例如宋体字体文件simsun.ttc有18MB大小，由于是等宽字体，其快照信息仅 1KB。
<br/><span style="color:blue">2. Divide the Unicode encoding of characters into regions according to the same character width, thereby highly compressing these width values into "font snapshot information". For example, the SimSun font file simsun.ttc is 18MB in size, and its snapshot information is only 1KB because it is a monospaced font.</span>

3.由于快照信息很小，所以我们干脆将快照信息硬编码到程序中，为了方便维护，我们放置到JS中，其代码如下图所示。
<br/><span style="color:blue">3. Since the snapshot information is very small, we simply hard-code the snapshot information into the program. For easy maintenance, we place it in JS, and its code is shown in the following figure.</span>
<br/><img src="images/9.png?raw=true" /><br/>
4.基于这些字体快照信息，我们就可以模拟出MeasureString()。实践证明这个方法的计算速度非常快，而且其计算结果与原生MeasureString()的计算结果高度一致。
<br/><span style="color:blue">4. Based on this font snapshot information, we can simulate MeasureString(). Practice has proved that this method has a very fast calculation speed, and its calculation results are highly consistent with those of the native MeasureString().</span>

#### 3.2.2.打印 <span style="color:blue"> Printing</span>

我们会在JS中使用window.print()方法来执行打印，但打印HtmlCanvasElement会出现结果模糊的问题，这是由于打印机DPI远超显示器DPI而导致。因此我们使用SVG的模式进行高清打印。
<br/><span style="color:blue">We use the window.print() method in JS to perform printing, but printing HtmlCanvasElement will result in blurry results, which is caused by the printer's DPI being much higher than the display's DPI. Therefore, we use SVG mode for high-definition printing.</span>

为此，我们在C#端使用一个SVG指令翻译器来实现该功能。对于Graphics新增SVG打印模式。比如对于Graphics.DrawLine()，当Graphics处于SVG打印模式，则输出的不是二进制数据，而是输出SVG代码，例如<line x1="74" y1="121.5" x2="720" y2="121.5" stroke="Black"></line>。最后将SVG代码字符串传递到JS端，然后使用动态创建SVG元素来承载这些SVG代码，实现高清打印。
<br/><span style="color:blue">To this end, we use an SVG instruction translator on the C# side to implement this function. Add SVG printing mode for Graphics. For example, for Graphics.DrawLine(), when Graphics is in SVG printing mode, it outputs not binary data but SVG code, such as <line x1="74" y1="121.5" x2="720" y2="121.5" stroke="Black"></line>. Finally, pass the SVG code string to the JS side, and then use dynamically created SVG elements to carry the SVG code to achieve high-definition printing.</span>

#### 3.2.3. JS端DCBinaryReader类 | <span style="color:blue"> DCBinaryReader Class on JS Side</span>

我们定义了一个JS类 class DCBinaryReader {}。它在DataView的基础上实现了一个向前的二进制数据读取器。用于简化后续操作。
<br/><span style="color:blue">We defined a JS class class DCBinaryReader {}. It implements a forward binary data reader based on DataView, which is used to simplify subsequent operations.</span>
<br/><img src="images/10.png?raw=true" /><br/>
#### 3.2.4. JS端PageContentDrawer类 | <span style="color:blue">3.2.4 PageContentDrawer Class on JS Side</span>

我们定义了一个JS类class PageContentDrawer{}。它获得C#端Graphics对象生成的二进制数组，使用DCBinaryReader封装一下，然后在一个HTML的CANVAS元素上进行绘制。主体代码如图：
<br/><span style="color:blue">We defined a JS class class PageContentDrawer{}. It obtains the binary array generated by the Graphics object on the C# side, encapsulates it with DCBinaryReader, and then draws on an HTML CANVAS element. The main code is shown in the figure:</span>

在这个循环体中，首先读取绘图指令编号，然后在绘图函数集中获得编号的绘图函数，然后进行调用。比如对于5号指令，这是绘制线段的功能，其功能代码如下：
<br/><span style="color:blue">In this loop body, first read the drawing instruction number, then obtain the drawing function of the number in the drawing function set, and then call it. For example, for instruction No. 5, which is the function of drawing line segments, its function code is as follows:</span>

在这个函数中，程序首先获得int16数据，这是画笔对象编号，从this.PenTable中获得画笔对象。然后使用DrawLine函数来绘制线段。这里调用了4次ReaderInt32()，这是获取线段的坐标信息，也就是x1,y1,x2,y2。
<br/><span style="color:blue">In this function, the program first obtains int16 data, which is the brush object number, and obtains the brush object from this.PenTable. Then use the DrawLine function to draw the line segment. Here, ReaderInt32() is called 4 times to obtain the coordinate information of the line segment, namely x1, y1, x2, y2.</span>

这样，我们就能将C#端Graphis.DrawLine()转换为JS端的canvas.drawLine()，实现了在一个HTMLCanvasElement上绘制图形。
<br/><span style="color:blue">In this way, we can convert Graphis.DrawLine() on the C# side to canvas.drawLine() on the JS side, realizing drawing graphics on an HTMLCanvasElement.</span>
<br/><img src="images/11.png?raw=true" /><br/>
至此，我们就模拟出System.Drawing.Graphics类型。
<br/><span style="color:blue">At this point, we have simulated the System.Drawing.Graphics type.</span>

## 4.大变活JS <span style="color:blue"> Transform to JS</span>

使用我们开发的【结界.NET】（https://github.com/dcsoft-yyf/JIEJIE.NET）进行程序集混淆加密，使用【BlazorWASMPackager】(https://github.com/dcsoft-yyf/BlazorWASMPackager)将Blazor WASM的编译结果打包成一个独立运行的JS文件。
<br/><span style="color:blue">Use the [JIEJIE.NET] (https://github.com/dcsoft-yyf/JIEJIE.NET) developed by us to obfuscate and encrypt the assembly, and use [BlazorWASMPackager] (https://github.com/dcsoft-yyf/BlazorWASMPackager) to package the compiled result of Blazor WASM into an independently runnable JS file.</span>

这样我们一顿操作猛如虎，将原先的WinForm.NET的EXE文件转变为一个15MB的JS文件，面对BS架构和信创，变得更加的根正苗红，无懈可击了。
<br/><span style="color:blue">In this way, through a series of efficient operations, we transformed the original WinForm.NET EXE file into a 15MB JS file, which is more compliant and impeccable for BS architecture and domestic independent innovation requirements.</span>

## 5. 最终效果<span style="color:blue"> Final Effect</span>

经过上述操作，我们成功的将DCWriter编辑器的WinForm.NET版本迁移到Blazor WASM平台上，实现了一个纯前端的符合信创的编辑器组件。一个文档在WinForm.NET版本的显示如下图所示：
<br/><span style="color:blue">After the above operations, we successfully migrated the WinForm.NET version of the DCWriter editor to the Blazor WASM platform, realizing a pure front-end editor component that complies with domestic independent innovation requirements. The display of a document in the WinForm.NET version is shown in the following figure:</span>
<br/><img src="images/12.png?raw=true" /><br/>
同一个文档在谷歌浏览器中的显示效果如下图所示：
<br/><span style="color:blue">The display effect of the same document in Google Chrome is shown in the following figure:</span>
<br/><img src="images/13.png?raw=true" /><br/>
这个文档在FireFox浏览器中的显示如下图所示：
<br/><span style="color:blue">The display of this document in FireFox browser is shown in the following figure:</span>
<br/><img src="images/14.png?raw=true" /><br/>
可以看出，同一个文档，在三种平台中排版和显示的结果完全一致，其键盘和鼠标事件处理行为也高度一致，而且通过了一些国产操作系统厂家的适配认证。
<br/><span style="color:blue">It can be seen that the same document has exactly the same typesetting and display results on the three platforms, and its keyboard and mouse event handling behaviors are also highly consistent, and it has passed the adaptation certification of some domestic operating system manufacturers.</span>

这样的结果达到我们的预期，让我们的产品有幸能活下来继续给客户带来持续的价值。
<br/><span style="color:blue">This result meets our expectations, allowing our product to survive and continue to bring sustained value to customers.</span>
