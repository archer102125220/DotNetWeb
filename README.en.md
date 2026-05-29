# DotNetWeb

This is a .NET project for learning purposes.

## What are Razor Pages?
Razor Pages is a page-based web development model in ASP.NET Core. It organizes UI logic (HTML/Razor) and the page model (C# code) together within the same file structure (`.cshtml` and `.cshtml.cs`), making development more intuitive and focused on single-page functionality. Compared to the traditional MVC (Model-View-Controller) architecture, it significantly reduces the friction of jumping between different folders.

### When to use Razor Pages?
- **Page-Focused Applications**: When most of your application's logic involves reading and writing data for individual pages (e.g., simple CRUD operations, form submissions).
- **Lightweight Projects and Rapid Development**: Unlike MVC, which requires creating Controllers, Actions, and Views, Razor Pages uses a simpler Page Model design. This speeds up development, making it highly suitable for small to medium-sized web applications.
- **High Cohesion Code Organization**: When you want to bind the HTML structure and backend logic that are highly relevant to a specific page tightly together to improve maintainability.

## Creating the Project
The commands to create .NET Web projects using the CLI are documented below:

### Create a Razor Pages Web Project (DotNetWeb)
```bash
dotnet new webapp -n DotNetWeb
```

### Create an MVC Web Project (DotNetMvcWeb)
```bash
dotnet new mvc -n DotNetMvcWeb
```

## Adding and Configuring `.gitignore`
To add a best-practice `.gitignore` for a .NET project, you can use the built-in .NET CLI command. This eliminates the need to manually configure exclusion rules for build artifacts (like `bin/` and `obj/`) and IDE settings (like Visual Studio and VS Code).

```bash
# Generate a .gitignore file tailored for .NET projects in the project root
dotnet new gitignore
```

## How to Run the Project
You can run this project using the following command:

**Normal Run Mode:**
```bash
dotnet run
```

**Developer Mode (Hot Reload):**
(Recommended for development. When you modify and save the code, the API server will automatically reload without requiring a manual restart.)
```bash
dotnet watch run
```
