# Developer Evaluation – Sales Module

## Candidate  
**Celio Santos Franca**

---

## Overview

This project contains the implementation of the **Sales module** based on the template provided for the technical evaluation.

The template itself was extremely helpful, as it already provides a well-structured foundation, clear architectural boundaries and good practices. This allowed me to focus my effort on understanding the problem domain, implementing the business rules correctly and extending the solution in a consistent way, instead of spending time on initial project setup.

The main objective was not only to deliver features, but to follow a clean and maintainable architecture, applying good practices that I normally use in real projects, such as **DDD concepts**, **clear separation of responsibilities**, and **business rules placed in the correct layer**.

---

## Implemented Features

The following features were implemented:

- Sales creation  
- Logical cancellation of a sale (soft cancel)  
- Sales listing with pagination, filtering and sorting  
- Discount rules based on the quantity of items  
- Business rule validation at the Domain level  
- Unit tests covering the most critical business rules  

I focused on implementing the most relevant use cases to demonstrate architectural decisions, business logic handling and consistency with the existing template.

---

## Architecture and Project Structure

The solution follows a layered architecture inspired by **Domain-Driven Design (DDD)** and is fully aligned with the structure proposed by the provided template.

### WebApi
This layer is responsible only for handling HTTP requests and responses.

- Controllers are intentionally thin  
- No business logic is implemented at this level  
- Requests are forwarded using **MediatR**  

### Application
This layer contains the application use cases.

- Commands, Queries, Handlers, Validators and DTOs  
- Responsible for orchestrating the flow of the application  
- Uses **MediatR** to decouple controllers from the execution logic  

### Domain
This is the core of the system.

- Contains entities, value objects and business rules  
- All discount rules and validations are implemented here  
- Ensures consistency and makes the core logic easy to test and evolve  

### Infrastructure (ORM)
This layer handles persistence concerns.

- Uses **EF Core**  
- PostgreSQL as the relational database  
- Includes DbContext, entity mappings and repositories  
- No business rules are implemented here  

---

## Mediator Pattern (MediatR)

The Mediator pattern was used to keep controllers decoupled from business logic, following the approach already encouraged by the template.

- Controllers send Commands or Queries to the Mediator  
- The Mediator resolves the appropriate Handler based on the Command type  
- Handlers execute the use case and interact with the Domain layer  

This approach improves readability, testability and maintainability, especially as the application grows.

---

## Business Rules – Discounts

Discount rules are applied **per item**, based on quantity:

- Less than 4 items: no discount  
- From 4 to 9 items: 10% discount  
- From 10 to 20 items: 20% discount  
- More than 20 items: operation is not allowed  

These rules are enforced inside the **Domain layer**, ensuring they are always applied consistently.

---

## Database and Persistence

- PostgreSQL is used as the relational database  
- EF Core is used as the ORM  
- Sales are modeled as aggregate roots with child SaleItems  
- Logical cancellation is used instead of physical deletion to preserve data history  

---

## Unit Tests

- Unit tests were implemented using **xUnit**  
- Tests focus on validating critical business rules, especially discount calculation  
- Mocking frameworks were not required in this scenario, since the Domain layer is isolated and easily testable  

---

## Items Not Implemented (Scope Decisions)

Some items were intentionally not implemented due to scope and time considerations:

- MongoDB integration was not included, as the project focuses on PostgreSQL  
- Faker was not used for data generation, since the main goal was validating business logic and architecture  

These decisions were made to prioritize clarity, correctness and adherence to the proposed structure.

---

## How to Run the Project

1. Set **Ambev.DeveloperEvaluation.WebApi** as the startup project  
2. Run the application using Visual Studio or via command line:

```bash
dotnet run
```

3. Access the API documentation via Swagger after startup  

---

## Final Considerations

This implementation focuses on clean architecture, correct business rule handling and maintainability.

The provided template was very well designed and made it easier to keep the solution consistent, organized and aligned with good development practices. The goal was to extend it naturally, following the same patterns and conventions, as would be expected in a real project scenario.
