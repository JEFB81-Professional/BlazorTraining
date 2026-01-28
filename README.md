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

### Each time you select the Click me button:

1. The onclick event is fired.
2. The IncrementCount method is called.
3. The currentCount is incremented.
4. The component is rendered to show the updated count.