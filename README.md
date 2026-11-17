# NURLYBEK PRODUCT LAB

**Author:** Sarsenbekuly Nurlibek  
**Project:** ProductValidationMVC

An ASP.NET Core MVC product dashboard and validation lab. The project demonstrates a clean separation between domain entities and form models, layered validation, and persistence with SQLite.

## Technology

- .NET 10 and ASP.NET Core MVC with Razor Views
- Entity Framework Core 10 and SQLite
- Data Annotations and ModelState validation
- jQuery Validation and jQuery Unobtrusive Validation
- Responsive HTML, CSS, ES6+, and Bootstrap utilities

## Architecture

Controllers handle HTTP requests and use dependency injected services. `Product` is the persisted domain entity; `ProductCreateViewModel` and `ValidationDemoViewModel` define the fields users may submit. The create action maps only validated ViewModel fields into a new entity, which prevents overposting.

The dashboard calculates product count, inventory value, category count, quantity, recent products, and category summaries from SQLite. The catalog supports server-side search, category filtering, sorting, and pagination.

## Validation

- Data Annotations provide required, length, range, email, comparison, regular expression, and future-date rules.
- `ModelState.IsValid` gates product creation and the validation demonstration.
- `ModelState.AddModelError` demonstrates a business rule that blocks `admin@example.com`.
- jQuery Unobtrusive Validation reads server-generated attributes for browser feedback.
- The custom `FutureDateNotAllowedAttribute` enforces its rule on the server and publishes an unobtrusive client adapter.
- The Validation Lab form posts to a real MVC action, so its server-side result remains available with JavaScript disabled.

## Database

SQLite uses `productvalidation.db` in the application directory. `EnsureCreated` initializes an empty database at startup. This is suitable for the lab; use EF Core migrations when evolving a deployed schema.

## Run

```powershell
dotnet restore
dotnet run
```

Open the local URL printed by `dotnet run`. The dashboard is the default route. Products, product creation, and the Validation Lab are available from the sidebar.

## Concepts

- **Domain Model vs ViewModel:** The domain entity represents persisted product data. A ViewModel limits and validates submitted form data.
- **Data Annotations:** Declarative constraints generate both server rules and client validation metadata.
- **ModelState.IsValid:** Reports whether binding and validation succeeded for the current request.
- **ModelState.AddModelError:** Adds an action level business rule error to the response.
- **Client-side validation:** Gives immediate feedback in the browser.
- **Server-side validation:** Rechecks untrusted input at the controller boundary.
- **Custom ValidationAttribute:** `FutureDateNotAllowedAttribute` shows how to implement a reusable domain-specific rule.
- **SQLite:** EF Core persists products locally and powers dashboard summaries.

## Knowledge check

1. Which model should a create form bind to? **C) ViewModel**
2. Which attribute marks a required value? **A) [Required]**
