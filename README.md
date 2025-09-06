# ServiceBus Function Demo

A serverless Azure Function that demonstrates event-driven message processing using Azure Service Bus. This project showcases how to build reliable, scalable message processing solutions in the cloud.

## 📋 Overview

This Azure Function automatically processes messages from an Azure Service Bus queue whenever they arrive. It's built using .NET 8 and Azure Functions v4, providing a robust foundation for event-driven architectures.


## 🚀 Features

- **Event-Driven Processing**: Automatically triggers when messages arrive
- **Serverless Architecture**: No infrastructure management required
- **Automatic Scaling**: Handles variable message loads
- **Built-in Reliability**: Includes retry logic and dead letter handling
- **Monitoring Integration**: Built-in logging and telemetry

## 🛠️ Technology Stack

- **Platform**: Azure Functions v4
- **Runtime**: .NET 8
- **Trigger**: Azure Service Bus Queue
- **Language**: C#
- **Packages**:
  - `Microsoft.Azure.WebJobs.Extensions.ServiceBus` (5.16.6)
  - `Microsoft.NET.Sdk.Functions` (4.6.0)

## 📁 Project Structure

```
ServiceBusFunctionDemo/
├── ProcessQueueMessage.cs      # Main function code
├── host.json                   # Function app configuration
├── local.settings.json         # Local development settings
├── ServiceBusFunctionDemo.csproj # Project file
├── .gitignore                  # Git ignore rules
├── .vscode/                    # VS Code settings
└── bin/                        # Build output
```

## 🔧 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) (optional)
- Azure subscription with Service Bus namespace

## ⚙️ Setup

### 1. Clone the Repository

```bash
git clone <repository-url>
cd ServiceBusFunctionDemo
```

### 2. Configure Service Bus Connection

Update `local.settings.json` with your Service Bus connection string:

```json
{
    "IsEncrypted": false,
    "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "FUNCTIONS_INPROC_NET8_ENABLED": "1",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet",
        "ServiceBusConnection": ""
    }
}
```

### 3. Create Service Bus Queue

Ensure you have a queue named `testqueue` in your Service Bus namespace, or update the queue name in `ProcessQueueMessage.cs`.

### 4. Install Dependencies

```bash
dotnet restore
```

## 🏃‍♂️ Running Locally

1. Start the Azure Functions runtime:
   ```bash
   func start
   ```

2. The function will be available and listening for messages on the `testqueue`.

3. Send test messages to your Service Bus queue to see the function in action.

## 📝 Code Explanation

### Main Function (`ProcessQueueMessage.cs`)

```csharp
[FunctionName("ProcessQueueMessage")]
public void Run([ServiceBusTrigger("testqueue", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
{
    log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
}
```

**Key Components:**
- `[ServiceBusTrigger]`: Defines the trigger type and queue name
- `Connection`: References the connection string from settings
- `string myQueueItem`: The message content from the queue
- `ILogger log`: For logging function execution

### Message Flow

1. **Message Arrival**: External system sends message to `testqueue`
2. **Function Trigger**: Azure Functions runtime detects the message
3. **Processing**: Function executes and logs the message content
4. **Completion**: Message is automatically acknowledged and removed

## 🔍 Monitoring

The function includes built-in logging that integrates with:
- **Azure Monitor**: For telemetry and performance metrics
- **Application Insights**: For detailed application monitoring
- **Local Console**: For development debugging

View logs in real-time during local development or through Azure Portal when deployed.

## 🚀 Deployment

### Deploy to Azure

1. Create a Function App in Azure:
   ```bash
   az functionapp create --resource-group myResourceGroup --consumption-plan-location westus --runtime dotnet --functions-version 4 --name myFunctionApp --storage-account myStorageAccount
   ```

2. Deploy the function:
   ```bash
   func azure functionapp publish myFunctionApp
   ```

3. Configure the Service Bus connection string in Azure:
   ```bash
   az functionapp config appsettings set --name myFunctionApp --resource-group myResourceGroup --settings "ServiceBusConnection=your-connection-string"
   ```

## 📊 Use Cases

This pattern is perfect for:

- **Order Processing**: E-commerce workflow automation
- **Email Notifications**: Sending notifications after user actions
- **Data Processing**: ETL operations on incoming data
- **System Integration**: Connecting disparate systems asynchronously
- **Microservices Communication**: Decoupled service interactions

## 🔄 Extending the Function

To enhance this function, consider:

1. **Message Parsing**: Parse JSON/XML message content
2. **Database Operations**: Store processed data
3. **External API Calls**: Integrate with other services
4. **Error Handling**: Implement custom retry logic
5. **Message Transformation**: Transform and forward messages

### Example Enhancement

```csharp
[FunctionName("ProcessQueueMessage")]
public async Task Run([ServiceBusTrigger("testqueue", Connection = "ServiceBusConnection")]string myQueueItem, ILogger log)
{
    try
    {
        var messageData = JsonSerializer.Deserialize<MyMessageType>(myQueueItem);
        
        // Process the message
        await ProcessBusinessLogic(messageData);
        
        log.LogInformation($"Successfully processed message: {messageData.Id}");
    }
    catch (Exception ex)
    {
        log.LogError(ex, "Error processing message: {Message}", myQueueItem);
        throw; // Re-throw to trigger retry logic
    }
}
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request


## 📚 Additional Resources

- [Azure Functions Documentation](https://docs.microsoft.com/en-us/azure/azure-functions/)
- [Azure Service Bus Documentation](https://docs.microsoft.com/en-us/azure/service-bus-messaging/)
- [Service Bus Triggers for Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus)
- [Best Practices for Azure Functions](https://docs.microsoft.com/en-us/azure/azure-functions/functions-best-practices)

---

**Happy coding!** 🎉

For questions or issues, please open an issue in this repository.
