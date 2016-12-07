# Curse Backend Team Take-Home Project

The purpose of this project is to evaluate your skill as a developer. It should take you about 2 hours to complete though you may take as long as you wish.

## Getting Started

To ensure consistency, specific tools and technologies must be used to complete this project.

### Data Contract Format

All HTTP and Websocket communication should be in JSON format.

### Hosting

The project is set up to use OWIN self-hosting on http://localhost:9000 which can be changed in ```Program.cs``` as desired.

### API Interface

The API interface must be created using ASP.NET Web API 2+ and/or ASP.NET MVC 5+. The project is already set up to use Web API 2 via Nuget packages.

### Websocket Interface

The Websocket interface must use Microsoft.WebSockets. The project is already set up to use this via Nuget packages. 

Scaffolding code to use web sockets is included in the project. An instance of the ```Models/WebSocketConnection.cs``` class will be created for every web socket request. This class has ```OnClose```, ```OnOpen```, and ```OnMessage``` stubs for you to implement as needed. 
The ```SendMessage``` function sends a string as a web socket frame to the connected client. 

The default buffer size for the Websocket code is ```1024``` and is a constant that can be changed in the ```WebSocketHandler``` class. The code as written assumes that complete data is received over the websocket and does not handle partial messages out of the box.

### Data Storage

Persistent data must be stored in Redis 3.2.6. This is available via Docker+Kitematic (https://www.docker.com) and is .

## Spec

The goal of this project is to create a simple chat system with a NoSQL database for persistence and Websockets for real-time notifications triggered by data changes.

The basic model of the system is as follows:
- Clients can create users
  - Users are persisted between service restarts
  - Clients can connect via Websocket and identify as a specific users
- Clients can create and delete chat rooms
  - Chat rooms are persisted between service restarts
  - All connected web sockets representing a user with membership to a chat room should receive a message when the room is deleted
- Clients can add and remove members from chat rooms
  - Chat room membership is persisted between service restarts
  - All connected clients representing a user with membership to a chat room should receive a message that a member was added/removed
  - All connected clients representing the added/removed member should receive a message that they have been added/removed
- Clients can send messages to chat rooms
  - Users with no membership to a chat room should not be able to send a message to that room
  - All connected clients representing a user with membership to a chat room should receive chat messages for that room
  - Messages do not need to be persisted

### Design
 
The provided project has the following folders where the majority of work is expected to be done:
- Contracts
- Controllers
- Models 

Additional folders/subfolders can be created to better organize the code however you see fit. Changes to the provided files outside of those folders should not be needed, but you are free to make any changes you want to that code as well.

## Bonus, Not Required
- Authentication
- Authorize only people with membership to a chat room to add/remove members
- Authorize only the creator of a chat room to delete the room
- Generate/Create documentation for the API/Websocket interfaces

