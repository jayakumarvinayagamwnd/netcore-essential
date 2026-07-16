# .NET Core Essential

A collection of essential .NET Core concepts, examples, and best practices for building modern applications.

## Overview

This repository serves as a comprehensive resource for .NET Core development, covering fundamental concepts and practical implementations. It includes hands-on examples across multiple project types to demonstrate core .NET Core features and patterns. The solution file [`netcore-essential.slnx`](./netcore-essential.slnx) ties all projects together for easy building and testing.

## Project Structure

```
netcore-essential/
├── .gitignore                   # Git ignore rules
├── netcore-essential.slnx       # Solution file
├── src/
│   ├── CsharpFeatures/          # Console app - C# language features
│   ├── AspNetCoreFeatures/      # Web app (Razor Pages/MVC) - ASP.NET Core features
│   ├── WebApiCoreFeatures/      # Web API - RESTful API development
│   └── EfCoreFeatures/          # Console app - Entity Framework Core features
├── tests/
│   ├── CsharpFeatures.Test/     # Unit tests for CsharpFeatures
│   ├── AspNetCoreFeatures.Test/ # Unit tests for AspNetCoreFeatures
│   ├── WebApiCoreFeatures.Test/ # Unit tests for WebApiCoreFeatures
│   └── EfCoreFeatures.Test/     # Unit tests for EfCoreFeatures
└── docs/                        # Documentation and guides
```

## Projects

### src/CsharpFeatures (Console)
Demonstrates modern C# language features including LINQ, async/await, pattern matching, record types, and more.

### src/AspNetCoreFeatures (Web)
Covers ASP.NET Core fundamentals such as Razor Pages/MVC, middleware pipeline, dependency injection, configuration, and logging.

### src/WebApiCoreFeatures (Web API)
Explores RESTful API development with controllers, routing, model binding, validation, OpenAPI/Swagger, and HTTP client integration.

### src/EfCoreFeatures (Console)
Showcases Entity Framework Core capabilities including database creation, migrations, relationships, queries, and performance optimizations.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0 or later)
- An IDE such as [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://visualstudio.microsoft.com/)

## Getting Started

Clone the repository and explore the examples:

```bash
git clone https://github.com/jayakumarvinayagamwnd/netcore-essential.git
cd netcore-essential
```

Build and restore all projects using the solution file:

```bash
dotnet restore
dotnet build
```

To run a specific project:

```bash
dotnet run --project src/CsharpFeatures
dotnet run --project src/AspNetCoreFeatures
dotnet run --project src/WebApiCoreFeatures
dotnet run --project src/EfCoreFeatures
```

To run tests for a specific project:

```bash
dotnet test tests/CsharpFeatures.Test
dotnet test tests/AspNetCoreFeatures.Test
dotnet test tests/WebApiCoreFeatures.Test
dotnet test tests/EfCoreFeatures.Test
```

Or run all tests at once via the solution:

```bash
dotnet test netcore-essential.slnx
```

## Topic Areas

- **C# Language Features** – Modern C# features, LINQ, async/await, pattern matching, records
- **ASP.NET Core** – Razor Pages/MVC, middleware, dependency injection, configuration, logging
- **Web APIs** – RESTful APIs, controllers, routing, OpenAPI/Swagger, HTTP clients
- **Entity Framework Core** – Database access, migrations, relationships, querying
- **Testing** – Unit testing with xUnit, mocking, test patterns
- **Architecture Patterns** – Clean Architecture, CQRS, Repository Pattern
- **Security** – Authentication, authorization, JWT
- **Performance** – Caching, response compression, optimization techniques

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests to improve the examples and documentation.

## License

This project is licensed under the MIT License.