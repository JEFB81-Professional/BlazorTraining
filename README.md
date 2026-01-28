## Razor components?
- the files that make up the project are .razor files.
- At compile time, each Razor component is built into a C# class. 

## Using components
- if you have a component named MyButton.razor, you can add a MyButton component to another component by adding a <MyButton /> tag.

## Component parameters
Components can also have parameters, which allow you to pass data to the component when it's used. Component parameters are defined by adding a public C# property to the component that also has a [Parameter] attribute. You can then specify a value for a component parameter using an HTML-style attribute that matches the property name. The value of the parameter can be any C# expression.

## The @code block
The @code block in a Razor file is used to add C# class members (fields, properties, and methods) to a component. 
You can use the @code to keep track of component state, add component parameters, implement component lifecycle events, and define event handlers.

```csharp
@page "/counter"
@rendermode InteractiveServer

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
``` 
## @ directives
the @page directive at the top, causes the Counter component to render its content. 
The @rendermode directive enables interactive server rendering for the component, so that it can handle user interface events from the browser. 
What @rendermode InteractiveServer Means in Razor
## @rendermode InteractiveServer 
tells ASP.NET that the Razor component should run interactively on the server using Blazor Server’s real‑time connection (SignalR).
In other words:

- The component is rendered initially as static HTML
- Then it becomes interactive via a live SignalR connection back to the server
- All UI updates occur server‑side, but the browser sees changes instantly

To handle UI events from a component and to use data binding, the component must be interactive. 
By default, Blazor components render statically from the server, which means they generate HTML in response to requests and are otherwise unable to handle UI events.
## rendermode to a component instance:
- <Counter @rendermode="InteractiveServer" />

## Alternatively rendermode
Blazor components can use the InteractiveWebAssembly render mode to render interactively from the client.
In this mode, the component code is downloaded to the browser and run client-side using a WebAssembly-based .NET runtime.

### Each time you select the Click me button:

1. The onclick event is fired.
2. The IncrementCount method is called.
3. The currentCount is incremented.
4. The component is rendered to show the updated count.

## The @page directive:
This directive provides a route template to Blazor. At runtime, Blazor locates a page to render by matching this template to the URL that the user requested. In this case, it might match a URL of the form http://yourdomain.com/index.

## The @code directive: 
This directive declares that the text in the following block is C# code. You can put as many code blocks as you need in a component. You can define component class members in these code blocks and set their values from calculation, data lookup operations, or other sources. In this case, the code defines a single component member called welcomeMessage and sets its string value.
## Member access directives: 
If you want to include the value of a member in your rendering logic, use the @ symbol followed by a C# expression, such as the name of the member. In this case, the @welcomeMessage or @counter directive is used to render the value of the welcomeMessage or counter member in the <p> tags.

## events
Blazor components can handle different kinds of UI events using C# and then render updates based on the events using Razor syntax. Blazor provides several patterns for defining event callbacks, including both synchronous and asynchronous callbacks.
Blazor also makes it easy to create two-way data bindings between the values of UI elements and your code.

## Data binding and events
- render the value of a C# expression in Razor, you use a leading @ character. @currentCount
- you can also be explicit about the beginning and ending of the expression using parens.  @(currentCount)
## control flow
- using a C# if-statement @if (currentCount > 3) {}
- loop over data and render a list of items:  
   @foreach (var item in items){ <li>@item.Name</li> }

## Handle events
- Blazor components often handle UI events. To specify an event callback for an event from a UI element, you use an attribute that starts with @on and ends with the event name.
-  like @onchange, @oninput, and so on.
- Event handling methods can be synchronous or asynchronous

## lambda expressions
- <button class="btn btn-primary" @onclick="() => currentCount++">Click me</button>

## event argument 
- you can access the value of an input element that changed,
- Blazor will automatically render the component with its new state, so the message is displayed after the input changes
- like this:

```csharp
<input @onchange="InputChanged" />
<p>@message</p>

@code {
    string message = "";

    void InputChanged(ChangeEventArgs e)
    {
        message = (string)e.Value;
    }
}
```

## data binding
- Often you want the value of a UI element to be bound to a particular value in code. 
  When the value of the UI element changes, the code value should change, and when the code value changes the UI element should display the new value.
  bind a UI element to a particular value in code using the @bind attribute. For example:
  ```csharp
  <input @bind="text" />
  <button @onclick="() => text = string.Empty">Clear</button>
  <p>@text</p>

  @code {
    string text = "";
  }
  ```
  - When you change the value of the input, the text field is updated with the new value. 
    And when you change the value of the text field by clicking the Clear button, the value of the input is also cleared.

## Razor directives
These are reserved keywords in Razor syntax that influence how a Razor file is compiled. 
Razor directives always begin with the @ character. 
Some Razor directives appear at the beginning of a new line, like @page and @code, while other are attributes that can be applied to elements as attributes, like @bind. more: https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-10.0

# creating new component pages
- when creating new pages donot forget to add the directory or file to the _Import.razor page
- @using LearnBlazor.<folder name>

## Blazor hosting models
There are two hosting models for code in Blazor apps:

- Blazor Server: In this model, the app is executed on the web server within an ASP.NET Core app. 
On the client side, UI updates, events, and JavaScript calls, are sent through a SignalR connection between the client and the server.
- Blazor WebAssembly: In this model, the Blazor app, its dependencies, and the .NET runtime are downloaded and run on the browser.

