using DBConnector;

Console.WriteLine("Database REPL Console");
Console.WriteLine("====================");
Console.WriteLine();

// Get database type
Console.Write("Select database type (mongodb/postgresql): ");
var dbType = Console.ReadLine()?.Trim().ToLower();

if (string.IsNullOrEmpty(dbType) || (dbType != "mongodb" && dbType != "postgresql"))
{
    Console.WriteLine("Invalid database type. Please specify 'mongodb' or 'postgresql'.");
    return;
}

// Get connection string
Console.Write("Enter connection string: ");
var connectionString = Console.ReadLine()?.Trim();

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Connection string cannot be empty.");
    return;
}

// Create connector based on type
IDBConnector? connector = null;
try
{
    if (dbType == "mongodb")
    {
        connector = new MongoConnector(connectionString);
        Console.WriteLine("MongoDB connector created.");
    }
    else if (dbType == "postgresql")
    {
        connector = new PostgresConnector(connectionString);
        Console.WriteLine("PostgreSQL connector created.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error creating connector: {ex.Message}");
    return;
}

// Automatically ping the database to test connection
Console.WriteLine();
Console.WriteLine("Testing database connection...");
if (connector != null)
{
    var pingResult = await connector.ping();
    if (pingResult)
    {
        Console.WriteLine("✓ Connection successful!");
    }
    else
    {
        Console.WriteLine("✗ Connection failed!");
    }
}

// REPL loop
Console.WriteLine();
Console.WriteLine("REPL started. Available commands:");
Console.WriteLine("  ping - Test database connection");
Console.WriteLine("  exit - Exit the REPL");
Console.WriteLine();

while (true)
{
    Console.Write("> ");
    var command = Console.ReadLine()?.Trim().ToLower();

    if (string.IsNullOrEmpty(command))
        continue;

    if (command == "exit")
    {
        Console.WriteLine("Exiting REPL...");
        break;
    }

    if (command == "ping")
    {
        if (connector != null)
        {
            var result = await connector.ping();
            Console.WriteLine($"Ping result: {(result ? "Success" : "Failed")}");
        }
    }
    else
    {
        Console.WriteLine($"Unknown command: {command}. Available commands: ping, exit");
    }
}
