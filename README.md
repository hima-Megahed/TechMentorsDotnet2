# Appointment Management in a Modular Monolith

This repository demonstrates an implementation of **Appointment Management** in a **Modular Monolith** architecture. The project explores different integration styles and testing methodologies to provide insights into modern software design and development practices.

## Key Features

1. **Integration Styles**  
   We utilized two primary integration styles:
   - **Direct Method Calls:** For tightly coupled module interactions.
   - **Event-Based Asynchronous Communication:** To decouple modules and ensure loose coupling where appropriate.

2. **Testing Approach**  
   Tests were written to cover functional parts of the system. For components that depend heavily on other modules, we opted not to test them directly to avoid tight coupling in the tests.  
   
   The testing methodologies explored include:
   - **Chicago School (Classical):** Emphasizing integrated, end-to-end tests to verify behavior.
   - **London School (Mockist):** Focused on unit testing using mocks to isolate and validate individual components.

## Architecture

- **Shared Project**  
  The shared project contains cross-cutting concerns such as common utilities, services, and infrastructure. It serves as the communication layer between different modules.

- **Module Structure**  
  Each module follows this structure:
  - **Internal:** Core architecture and functionality of the module.
  - **Shared:** Contains shared components and interfaces used to communicate with other modules.
  - **Tests:** Includes both unit and integration tests.

### Module-specific Architecture

1. **Doctor Availability Module**  
   Implemented using **Layered Architecture**, where the concerns are separated into different layers such as presentation, service, and data access layers.

2. **Appointment Booking Module**  
   Follows the **Clean Architecture** with **CQRS (Command Query Responsibility Segregation)** patterns. Additionally, we implemented **Domain Events** and **Integration Events** using an **in-memory service bus broker**.

3. **Doctor Appointment Management Module**  
   Adopts the **Hexagonal Architecture** (also known as Ports and Adapters), focusing on maintaining clear boundaries between the core domain and external systems.

4. **Appointment Confirmation Module**  
   This module is a **Notification Module** implemented as a single project, responsible for confirming appointments and notifying the users.

## Lessons Learned

Building this project was a fun and educational experience. It allowed us to explore different architectural and testing styles, demonstrating their strengths and trade-offs in real-world scenarios.


## Solution Structure
```bash
├───Bootstrapper
├───Modules
│   ├───AppointmentBooking
│   │   ├───Internal
│   │   │   ├───AppointmentBooking.Api
│   │   │   │   ├───Endpoints
│   │   │   │   │   ├───BookAppointment
│   │   │   │   │   └───GetAvailableSlots
│   │   │   ├───AppointmentBooking.Application
│   │   │   │   ├───BookAppointment
│   │   │   │   │   ├───Commands
│   │   │   │   │   └───EventHandlers
│   │   │   │   ├───Contracts
│   │   │   │   │   └───Services
│   │   │   │   ├───GetAvailableSlots
│   │   │   │   │   ├───DTOs
│   │   │   │   │   └───Queries
│   │   │   ├───AppointmentBooking.Domain
│   │   │   │   ├───Entities
│   │   │   └───AppointmentBooking.Infrastructure
│   │   │       ├───Facade
│   │   │       ├───Gateways
│   │   │       │   └───DoctorAvailability
│   │   │       ├───Persistence
│   │   │       │   ├───DbContext
│   │   │       │   └───Migrations
│   │   │       ├───Registrar
│   │   │       ├───Repositories
│   │   │       └───Services
│   │   ├───Shared
│   │   │   └───AppointmentBooking.Shared
│   │   │       ├───Facade
│   │   │       ├───Gateways
│   │   │       │   └───DoctorAvailability
│   │   └───Tests
│   │       └───AppointmentBooking.UnitTests
│   │           ├───ArchitectureTests
│   │           └───RepositoriesTests
│   ├───AppointmentConfirmation
│   │   └───AppointmentConfirmation.Notification
│   │       └───AppointmentConfirmation.Notification
│   │           ├───IntegrationEventsHandler
│   │           └───Registrar
│   ├───DoctorAppointmentManagement
│   │   ├───Internal
│   │   │   ├───Core
│   │   │   │   ├───DoctorAppointmentManagement.Application
│   │   │   │   │   ├───Ports
│   │   │   │   │   │   ├───Driven
│   │   │   │   │   │   └───Driving
│   │   │   │   │   └───Usecases
│   │   │   │   └───DoctorAppointmentManagement.Domain
│   │   │   └───Shell
│   │   │       ├───DoctorAppointmentManagement.Api
│   │   │       │   ├───Endpoints
│   │   │       │   │   ├───ChangeAppointmentStatus
│   │   │       │   │   │   └───Models
│   │   │       │   │   └───ViewMyAppointments
│   │   │       │   └───Registrar
│   │   │       └───DoctorAppointmentManagement.Infrastructure
│   │   │           ├───Gateways
│   │   └───Tests
│   │       └───DoctorAppointmentManagement.UnitTests
│   └───DoctorAvailability
│       ├───Internal
│       │   ├───DoctorAvailability.Business
│       │   │   ├───Facade
│       │   │   ├───Repositories
│       │   │   └───Services
│       │   │       └───DoctorSlot
│       │   │           └───Models
│       │   ├───DoctorAvailability.Data
│       │   │   ├───DbContext
│       │   │   │   ├───Configurations
│       │   │   │   └───Migrations
│       │   │   ├───Models
│       │   └───DoctorAvailability.Presentation
│       │       ├───Endpoints
│       │       │   ├───AddSlot
│       │       │   └───GetMySlots
│       │       └───Registrar
│       ├───Shared
│       │   ├───Facade
│       └───Tests
│           └───DoctorAvailability.UnitTests
│               ├───ArchitectureTests
│               ├───RepositoriesTests
│               └───ServicesTests
└───Shared
    ├───Domain
    │   ├───Entities
    │   ├───Enums
    │   └───Interceptors
    ├───DomainEvents
    │   └───Events
    ├───DTOs
    │   ├───AppointmentBooking
    │   └───DoctorAvailability
    ├───Extensions
    ├───IntegrationEvents
```


