
# CQRS Microservice Demo with Clean Architecture

## Project Overview

This project demonstrates a CQRS (Command Query Responsibility Segregation) pattern implemented with Microservices architecture and Clean Architecture principles. It is designed for a product management system with clear separation of command and read responsibilities.

* **Command Side:**
  Uses **SQL Server** for handling write operations (create, update, delete) on product data.
* **Read Side:**
  Uses **MongoDB Atlas** for optimized read queries and fast data retrieval.
* **Event Bus:**
  Utilizes **RabbitMQ** as the messaging broker to propagate events from the Command side to the Read side for data synchronization.

---

## Architectural Overview

* **CQRS:** Separates the write (command) and read (query) operations to enhance scalability and maintainability.
* **Microservices:** Each service is independently deployable, manages its own database, and communicates via events.
* **Clean Architecture:** Clear separation of concerns with layers such as Domain, Application, Infrastructure, and API for easier maintenance and extensibility.
* **RabbitMQ:** Acts as the asynchronous event bus ensuring reliable event delivery and eventual consistency.

---

## Technology Stack

* **SQL Server:** Primary database for write operations on the Command side.
* **MongoDB Atlas:** NoSQL database optimized for read operations on the Query side.
* **RabbitMQ:** Message broker used for event-driven communication between services.
* **.NET (C#):** Backend framework implementing service layers, event handlers, and data access.

---

## Workflow

1. The **Command Service** processes client requests to modify data and writes changes to SQL Server.
2. Upon a successful write, it publishes an event (e.g., `ProductCreatedEvent`) to RabbitMQ.
3. The **Read Service** subscribes to RabbitMQ, listens for these events, and updates the MongoDB Atlas database accordingly.
4. The Read side then serves client read requests with fast query performance.

---

## Getting Started

### Prerequisites

* Running instance of **SQL Server** accessible from the services.
* **RabbitMQ** server running (can be installed locally or run via Docker).
* Configured **MongoDB Atlas** cluster or local MongoDB instance.

### Running RabbitMQ with Docker

```bash
docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

Access the RabbitMQ management UI at [http://localhost:15672](http://localhost:15672) (default username/password: guest/guest).

### Configuration

Update connection strings and RabbitMQ settings in your `appsettings.json` or environment variables:

* RabbitMQ host, port
* SQL Server connection string
* MongoDB connection string

### Running the Services

* Start the **Command Service** to handle write operations and publish events.
* Start the **Read Service** to consume events and update the read database.

---

## Additional Notes

* Use RabbitMQ Management UI to monitor queues and messages for debugging.
* MongoDB model classes use `[BsonRepresentation(BsonType.ObjectId)]` attribute on string IDs to map MongoDB ObjectIds correctly.

---

This project still in process...
And I'm still a newbie, if there's anything wrong please help me.
