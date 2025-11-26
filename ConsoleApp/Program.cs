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

