Microservices Architecture with .NET 8.0

This project demonstrates a comprehensive Microservices Architecture using .NET 8.0. It encompasses various services, each responsible for distinct aspects of the application, communicating asynchronously and synchronously, secured via an identity server, and employing patterns such as Domain-Driven Design (DDD) and Command Query Responsibility Segregation (CQRS).

Services Overview
Catalog Microservice
Function: Manages and serves information related to our courses.
Database: MongoDB
Relationships: Implements One-To-Many/One-To-One relationships.

Basket Microservice
Function: Responsible for handling shopping cart operations.
Database: RedisDB

Discount Microservice
Function: Manages discount coupons assigned to users.
Database: PostgreSQL

Order Microservice
Function: Handles order processing.
Approach: Developed using Domain-Driven Design approach.
Patterns: Implements CQRS using the MediatR library.
Database: SQL Server

FakePayment Microservice
Function: Responsible for handling payment operations.

IdentityServer Microservice
Database: SQL Server
Function: Manages user data, token, and refresh token generation.

PhotoStock Microservice
Function: Manages and serves course photographs.
Infrastructure Components
API Gateway
Technology: Ocelot Library
Message Broker
Technology: RabbitMQ
Library: MassTransit for communication with RabbitMQ.
Identity Server
Features: Token/RefreshToken generation, securing microservices with Access Token, compliance with OAuth 2.0/OpenID Connect protocols.

Asp.Net Core MVC Microservice
Role: UI service for interacting with users and displaying data from microservices.
Key Learnings

Microservice Architecture with .NET 8.0
Asynchronous and Synchronous communication between Microservices
API Gateway implementation using Ocelot Library
Message queuing and event-driven architecture with RabbitMQ
Containerization with Docker & Docker Compose
Authentication and Authorization with IdentityServer4
Implementing Domain-Driven Design
CQRS Pattern
Database integration with PostgreSQL, MongoDB, and SQL Server