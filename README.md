# UDP-Client-Server

This is a multithreaded UDP client and server application developed in C#. The server can operate in two main modes:

## Server

The server has a single consumer thread that processes all messages sent to a client, and N producer threads, where N is the number of requests being processed by the server at any given time.

### Server Modes

1. **File Mode**: When the message starts with `file:`, the server attempts to find the requested file in the mocked file system and returns its content.
   - Example: `file:/images/image1.png`

2. **Message Processing Mode**: When the message does not start with `file:`, the server responds with the received message converted to uppercase and appends the text: " [Processed by Server]".
   - Example: If the received message is "hello", the response will be "HELLO [Processed by Server]".

### Mocked File System

The server has a mocked file system with the following files:

- `/images/image1.png` with base64 encoded content
- `/notes.txt` with informational text
- `/status.txt` with text about the system status

## Client

The client can send UDP messages to the server and display the responses received.

## Project Structure

- **`UDP_Client_Server.Common.Client`**: Contains the client logic.
- **`UDP_Client_Server.Common.Server`**: Contains the server logic and mocked file system.
- **`UDP_Client_Server.Common.Message`**: Defines the `Message` class used for communication between client and server.
- **`UDP_Client_Server.Program`**: Entry point of the application that allows choosing between running the client or the server.

## Usage Instructions

### Running the Server

1. Compile the project.
2. Run the server with the command:

   ```bash
   dotnet run <IP> <Port> server
   
