# CleanDDD

A small reference project demonstrating how I structure a .NET application using Clean Architecture, Domain-Driven Design principles, CQRS and MediatR.

The purpose of this repository is not to represent a complete production system. It is a practical playground and reference implementation for experimenting with application boundaries, request pipelines, validation and domain-oriented design.

## Architecture

The solution is separated into multiple projects to keep responsibilities clear:

- **Domain**
  - Core entities
  - Domain abstractions
  - Result/Error models
  - Domain-specific exceptions

- **Application**
  - CQRS commands and queries
  - MediatR handlers
  - FluentValidation validators
  - Pipeline behaviors
  - Application-level abstractions

- **Persistence**
  - Database access and persistence-related implementations

- **Infrastructure**
  - External and infrastructure concerns
  - Service implementations

- **Presentation**
  - API controllers
  - Presentation-related components

- **WebApi**
  - Application composition root
  - Dependency injection
  - Middleware configuration
  - Swagger
  - Serilog request logging
  - Global exception handling

## Main Concepts

This repository demonstrates:

- Clean Architecture
- Domain-Driven Design fundamentals
- CQRS
- MediatR
- Command / Query abstractions
- MediatR Pipeline Behaviors
- FluentValidation
- Result Pattern
- Repository abstraction
- Unit of Work abstraction
- Dependency Injection
- Global exception handling
- Structured logging with Serilog
- Swagger / OpenAPI

## Request Flow

A typical request follows this flow:

```text
HTTP Request
     |
     v
Controller
     |
     v
MediatR
     |
     v
Pipeline Behaviors
     |
     +---- Validation
     |
     v
Command / Query Handler
     |
     v
Domain / Repository
     |
     v
Persistence
```

This keeps controllers thin and moves application logic into dedicated commands, queries and handlers.

## Project Structure

```text
CleanDDD
│
├── Domain
│   ├── Entities
│   ├── Abstractions
│   ├── Exceptions
│   ├── Primitives
│   └── Shared
│
├── Application
│   ├── Abstractions
│   │   └── Messaging
│   ├── Behaviors
│   └── Webinars
│       ├── Commands
│       └── Queries
│
├── Persistence
├── Infrastructure
├── Presentation
└── WebApi
```

## Validation Pipeline

Validation is implemented as a MediatR pipeline behavior.

Instead of manually validating every request inside controllers or handlers, the request passes through `ValidationBehavior<TRequest, TResponse>` before reaching its handler.

```text
Request
   |
   v
ValidationBehavior
   |
   +---- Invalid -> Validation Exception
   |
   v
Handler
```

This keeps validation concerns centralized and avoids duplicating validation logic across endpoints.

## Why CQRS?

Commands and queries are separated because they represent different intents:

```text
Command
   -> changes application state

Query
   -> retrieves application state
```

This separation makes application flows easier to understand and allows cross-cutting concerns such as validation, logging and caching to be applied through the MediatR pipeline.

CQRS is not necessary for every project. I use this repository mainly to experiment with the pattern and understand where the additional abstraction is useful and where it would introduce unnecessary complexity.

## Technologies

- C#
- ASP.NET Core
- MediatR
- FluentValidation
- Serilog
- Swagger / OpenAPI

## Status

This repository is a learning and reference project and is still evolving.

Some application flows are intentionally incomplete while I experiment with different architectural approaches and patterns.

## Goal

The main goal of this repository is to keep a small, readable example of architectural patterns I use or study in larger .NET applications.

Rather than building a feature-heavy application, the focus is on:

- Separation of concerns
- Maintainable application boundaries
- Reusable request pipeline behaviors
- Domain-oriented application structure
- Explicit command/query flows

---

Built as part of my ongoing .NET architecture studies.
