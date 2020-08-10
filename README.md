# Simple Message based chat client
The application is build using c# .net core. it is a very simple message based Client application which follows the NATS's publish-subscribe message distribution model.

## Running the Project
* [1. Prerequisites](#1-prerequisites)
* [2. Run Subscriber](#2-Subscriber)
* [3. Run Publisher](#2-Publisher)
* [3. Run Chat Api](#2-ChatApi)


### 1. Prerequisites
- A running instance of `NATS server` with default configuration
- .NET Core 3.1 sdk
- Extract `ChatApp` Zip  

### 2. Subscriber
Navigate to ChatApp/Subscribe folder then run the following command `dotnet restore` and `dotnet run {subject}`. For simplicity the application expects a subject as the first argument. Subscriber will store all the received message in a text file named message.txt in the Os specified `tmp` folder which will be later read by the api(GET) request. You can create multiple instance of subscribr following the same instruction.Use `ctrl+c` to stop the application.

### 2. Publisher
Navigate to ChatApp/Publish folder then run the following command `dotnet restore` and `dotnet run {subject} {User name}`. For simplicity the application expects a subject as the first argument and user name as the second argument. user will be asked to enter a message.the message then will be published to nats server and all the subscriber who are listening on the subject should receive the message taged with username and timestamp.Use `ctrl+c` to stop the application.

### 3. Chat Api
Navigate to ChatApp/ChatApi and run the following command `dotnet restore` and `dotnet run` .In order to publish a message use this end point(POST) `http://localhost:5000/api/message?userName={userName}&subject={subject}`. `content-type:text/plain` and a message in the request body.

In order to retrieve all the messages that subcriber received, use the following link `http://localhost:500/api/message`.

Use `ctrl+c` to stop the application.