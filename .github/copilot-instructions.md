# Copilot Instructions for LearnBlazor

## Overview
This project is a Blazor application that utilizes Razor components to create interactive web pages. The architecture is based on components that encapsulate UI and logic, allowing for reusable and maintainable code.

## Key Components
- **Razor Components**: All UI elements are defined in `.razor` files. Each component is compiled into a C# class, enabling strong typing and IntelliSense support.
- **Counter Component**: The `Counter.razor` component demonstrates state management and event handling. It maintains a `currentCount` variable and updates it through the `IncrementCount` method when the button is clicked.

## Developer Workflows
- **Building and Running**: Use `dotnet watch run` to start the application in development mode. This command watches for file changes and automatically rebuilds the project.
- **Testing**: Ensure to write unit tests for components to validate their behavior. Testing can be done using xUnit or similar frameworks.
- **Debugging**: Utilize the built-in debugging tools in Visual Studio or Visual Studio Code. Set breakpoints in the C# code blocks within `.razor` files to inspect component state.

## Project Conventions
- **Component Parameters**: Parameters are defined using public properties with the `[Parameter]` attribute. This allows data to be passed into components when they are instantiated.
- **Event Handling**: Use the `@onclick` directive to bind button clicks to methods defined in the `@code` block. This pattern is consistent across components.

## Integration Points
- **External Dependencies**: The project may include libraries such as Bootstrap for styling. Ensure to reference these in the `wwwroot` directory.
- **Cross-Component Communication**: Use cascading parameters or event callbacks to facilitate communication between parent and child components.

## Examples
- **Using Components**: To use the `Counter` component in another component, include it as follows:
  ```html
  <Counter />
  ```
- **Defining Parameters**: Example of a parameter in a component:
  ```csharp
  [Parameter] public string Title { get; set; }
  ```

## Conclusion
These instructions provide a foundational understanding of the LearnBlazor project structure and conventions. For further details, refer to the specific component files and the overall project documentation.