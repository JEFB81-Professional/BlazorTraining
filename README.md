## EditContext on EditForm
The EditForm tracks the state of the current object that is acting as a model, including which fields were changed and their current values, by using an EditContext object.
The EditContext object is passed to the submit events as a parameter. An event handler can use the Model field in this object to retrieve the user's input.
```csharp
<EditForm Model="@shirt" OnSubmit="ValidateData">
    <!-- Omitted for brevity -->
    <input type="submit" class="btn btn-primary" value="Save"/>
    <p></p>
    <div>@Message</div>
</EditForm>

@code {
    private string Message = String.Empty;

    // Omitted for brevity

    private async Task ValidateData(EditContext editContext)
    {
        if (editContext.Model isn't Shirt shirt)
        {
            Message = "T-Shirt object is invalid";
            return;
        }

        if (shirt is { Color: ShirtColor.Red, Size: ShirtSize.ExtraLarge })
        {
            Message = "Red T-Shirts not available in Extra Large size";
            return;
        }

        if (shirt is { Color: ShirtColor.Blue, Size: <= ShirtSize.Medium)
        {
            Message = "Blue T-Shirts not available in Small or Medium sizes";
            return;
        }

        if (shirt is { Color: ShirtColor.White, Price: > 50 })
        {
            Message = "White T-Shirts must be priced at 50 or lower";
            return;
        }

        // Data is valid
        // Save the data
        Message = "Changes saved";
    }
}
```
# submit events
When an EditForm is submitted, it runs these three events:

- OnValidSubmit: This event is triggered if the input fields successfully pass the validation rules defined by their validation attributes.
- OnInvalidSubmit: This event is triggered if any of the input fields on the form fail the validation defined by their validation attributes.
- OnSubmit: This event occurs when the EditForm is submitted regardless of whether all of the input fields are valid or not.

The OnValidSubmit and OnInvalidSubmit events are useful for an EditForm that implements basic validation at the individual input field level. 
-  cross-checking one input field against another to ensure a valid combination of values, then consider using the OnSubmit event.
** An EditForm can either handle the OnValidSubmit and OnInvalidSubmit pair of events or the OnSubmit event but not all three. 
# JavaScript or Blazor
You can also trap JavaScript events such as onchange and oninput, and the Blazor equivalent @onchange and @oninput events for many controls in an EditForm. 
- to examine and validate data programmatically, on a field-by-field basis, before the user submits the form. 
# Handle form submission
-  When the changes are complete, you can submit the form to validate the data on the server and save the changes.
Blazor supports two types of validation; declarative and programmatic. 

## declarative
- Declarative validation rules operate on the client, in the browser. 
They're useful for performing basic client-side validation before data is transmitted to the server.
- The client-side validation traps basic user input errors and prevents many cases of invalid data being sent to the server for processing. 
## programmatic
-  Server-side validation is useful for handling complex scenarios that aren't available with declarative validation, such as cross-checking the data in a field against data from other sources. 
-  Server-side validation ensures that a user request to save data doesn't attempt to bypass data validation and store incomplete or corrupt data.
# using model in EditForm
.NET class
```csharp
public enum ShirtColor
{
    Red, Blue, Yellow, Green, Black, White
};

public enum ShirtSize
{
    Small, Medium, Large, ExtraLarge
};

public class Shirt
{
    public ShirtColor Color { get; set; }
    public ShirtSize Size { get; set; }
    public decimal Price;
}
// razor page using the model
<EditForm Model="@shirt">
  <label>
        <h3>Size</h3>
        <InputRadioGroup Name="size" @bind-Value=shirt.Size> // bind to the value of the model element
            @foreach(var shirtSize in Enum.GetValues(typeof(ShirtSize)))
            {
                <label>@shirtSize:
                    <InputRadio Name="size" Value="@shirtSize"></InputRadio>
                </label>
                <br />
            }
        </InputRadioGroup>
    </label>
    ...
    // the same for the color
     <label>
        <h3>Color</h3>
        <InputRadioGroup Name="color" @bind-Value=shirt.Color>
            @foreach(var shirtColor in Enum.GetValues(typeof(ShirtColor)))
            {
                <label>@shirtColor:
                    <InputRadio Name="color" Value="@shirtColor"></InputRadio>
                </label>
                <br />
            }
        </InputRadioGroup>
    </label>
    ... 
    <label>
        <h3>Price</h3>
        <InputNumber @bind-Value=shirt.Price min="0" max="100" step="0.01"></InputNumber>
    </label>
</EditForm>

@code {
    private Shirt shirt = new Shirt
    {
        Size = ShirtSize.Large,
        Color = ShirtColor.Blue,
        Price = 9.99M
    };
}
# Blazor input controls
Blazor has its own set of components designed to work specifically with the <EditForm> element and support data binding among other features. 
| Input component            | Rendered as (HTML)                 |
|----------------------------|------------------------------------|
| InputCheckbox              | <input type="checkbox">            |
| InputDate<TValue>          | <input type="date">                |
| InputFile                  | <input type="file">                |
| InputNumber<TValue>        | <input type="number">              |
| InputRadio<TValue>         | <input type="radio">               |
| InputRadioGroup<TValue>    | Group of child radio buttons       |
| InputSelect<TValue>        | <select>                           |
| InputText                  | <input>                            |
| InputTextArea              | <textarea>                         |

Any unrecognized non-Blazor attributes are passed unchanged to the HTML renderer. This means you can utilize HTML input element attributes.
you can add the min, max, and step attributes to an InputNumber component, and they function correctly as part of the rendered <input type="number"> element. 
# Blazor Forms
- The facilities the <form> and <input> elements provide are simple but relatively primitive. Blazor extends the capabilities of forms with its <EditForm> component. Additionally, Blazor provides a series of specialized input elements that you can use to format and validate the data the user enters
## What is an EditForm?
An EditForm is a Blazor component that fulfills the role of an HTML form on a Blazor page. The main differences between an EditForm and an HTML form are:

- Data binding: You can associate an object with an EditForm. 
  The EditForm acts like a view of the object for data entry and display purposes.
- Validation: An EditForm provides extensive and extensible validation capabilities. 
  You can add attributes to the elements in an EditForm that specify validation rules. 
  The EditForm applies these rules automatically. This functionality is described in a later unit in this module.
- Form submission: An HTML form sends a post request to a form handler when the form is submitted. 
  This form handler is expected to perform the submit process, and then display any results. 
  An EditForm follows the Blazor event model; 
  you specify a C# event handler that captures the OnSubmit event. 
  The event handler performs the submit logic.
- Input elements: An HTML form uses an <input> control to gather user input, and a submit button to post the form for processing. 
  An EditForm can use these same elements, but Blazor provides a library of input components that have other features, such as built-in validation and data binding.
## Create an EditForm with data binding
The <EditForm> element supports data binding with the Model parameter. 
You specify an object as the argument for this parameter. 
The input elements in the EditForm can bind to properties and fields exposed by the model by using the @bind-Value parameter. 
Model
```csharp
public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary { get; set; }
}
```
- razor page
```csharp
@page "/fetchdata"

@using WebApplication.Data
@inject WeatherForecastService ForecastService

<h1>Weather forecast</h1>

<input type="number" width="2" min="0" max="@upperIndex" @onchange="ChangeForecast" value="@index"/>

<EditForm Model=@currentForecast>
    <InputDate @bind-Value=currentForecast.Date></InputDate>
    <InputNumber @bind-Value=currentForecast.TemperatureC></InputNumber>
    <InputText @bind-Value=currentForecast.Summary></InputText>
</EditForm>

@code {
    private WeatherForecast[] forecasts;
    private WeatherForecast currentForecast;
    private int index = 0;
    private int upperIndex = 0;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
        currentForecast = forecasts[index];
        upperIndex = forecasts.Count() - 1;
    }

    private async Task ChangeForecast(ChangeEventArgs e)
    {
        index = int.Parse(e.Value as string);
        if (index <= upperIndex && index >= 0)
        {
            currentForecast = forecasts[index];
        }
    }
}
- the OnInitialized event populates an array of WeatherForecast objects by using an external service.
- The currentForecast variable is set to the first item in the array; 
  which is the object displayed by the EditForm. The user can cycle through the array using the numeric input field above the EditForm on the page. 
  This field's value is used as an index of the array, and the currentForecast variable is set to the object found at that index by using the ChangeForecast method.
```
## to-way-binding
The EditForm component implements two-way data binding. The form displays the values retrieved from the mode. However, if the user updates these values in the form, the values are pushed back to the model.



# Razor components?
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

# Share information by using cascading parameters
Component parameters work well when you want to pass a value to the immediate child of a component. Things become awkward when you have a deep hierarchy with children of children and so on. Component parameters aren't automatically passed to grandchild components from ancestor components or further down the hierarchy. To handle this problem elegantly, Blazor includes cascading parameters. When you set the value of a cascading parameter in a component, its value is automatically available to all descendant components to any depth.

# syntax cascading parameter value
Using Cascading Values to share data with descendant components.
- Razor interprets Value="Buy One Get One Free" as C# code, not a literal.
- you need to wrap the string in parentheses and double quotes to indicate that it is a string literal.
- the component parameters must be explicitly marked as C# strings using @"".
<CascadingValue Name="DealName" Value="@\"Buy One Get One Free\"">
    <SpecialOffer />
</CascadingValue>
- other
<CascadingValue Name="DealName" Value="@("Buy One Get One Free")">
    <SpecialOffer />
</CascadingValue>


# Share information by using AppState
Another approach to sharing information between different components is to use the AppState pattern. You create a class that defines the properties you want to store, and register it as a scoped service. In any component where you want to set or use the AppState values, you inject the service, and then you can access its properties. 
Unlike component parameters and cascading parameters, values in AppState are available to all components in the application, even components that aren't children of the component that stored the value.
```csharp
// create state class 
public class PizzaSalesState
{
    public int PizzasSoldToday { get; set; }
}
You would add the class as a scoped service in the Program.cs file:
...
// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add the AppState class
builder.Services.AddScoped<PizzaSalesState>();
...

// use in class state in the page
@page "/"
@inject PizzaSalesState SalesState

<h1>Welcome to Blazing Pizzas</h1>

<p>Today, we've sold this many pizzas: @SalesState.PizzasSoldToday</p>

<button @onclick="IncrementSales">Buy a Pizza</button>

@code {
    private void IncrementSales()
    {
        SalesState.PizzasSoldToday++;
    }
}

```