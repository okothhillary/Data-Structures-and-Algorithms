using System.Diagnostics;


/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // ===========================
        // Test 1
        // Scenario: checking the size of the queue
        // Expected Result: Print max size of the queue (default to 10 if invalid)
        Console.WriteLine("Test 1");
        var cs = new CustomerService(-9);
        Debug.Assert(cs.MaxSize == 10, "Test 1 Failed: MaxSize should default to 10");
        Console.WriteLine(cs);
        // Defect(s) Found: NONE
        Console.WriteLine("=================");


        // ===========================
        // Test 2
        // Scenario: Add a customer to the queue
        // Expected Result: Queue size should be 1
        Console.WriteLine("Test 2");

        // Set up fake input for customer info
        var input2 = new StringReader("Alice\nA001\nCannot log in\n");
        Console.SetIn(input2);
        cs.AddNewCustomer();
        Debug.Assert(cs.QueueSize == 1, "Test 2 Failed: Customer was not added");
        Console.WriteLine(cs);
        // Defect(s) Found: NONE
        Console.WriteLine("=================");


        // ===========================
        // Test 3
        // Scenario: Add a customer when queue is full
        // Expected Result: Error message should display, customer not added
        Console.WriteLine("Test 3");

        var cs3 = new CustomerService(1);
        var input3a = new StringReader("Bob\nB002\nForgot password\n");
        Console.SetIn(input3a);
        cs3.AddNewCustomer(); // 1st customer

        var input3b = new StringReader("Charlie\nC003\nBilling issue\n");
        Console.SetIn(input3b);
        cs3.AddNewCustomer(); // Should trigger "queue full"
        Debug.Assert(cs3.QueueSize == 1, "Test 3 Failed: Queue exceeded max size");
        Console.WriteLine(cs3);
        // Defect(s) Found: FIXED original > to >= in AddNewCustomer
        Console.WriteLine("=================");


        // ===========================
        // Test 4
        // Scenario: Serve a customer from the queue
        // Expected Result: Customer is dequeued and displayed
        Console.WriteLine("Test 4");

        var cs4 = new CustomerService(2);
        var input4 = new StringReader("Dana\nD004\nSystem crash\n");
        Console.SetIn(input4);
        cs4.AddNewCustomer(); // add one customer
        Debug.Assert(cs4.QueueSize == 1, "Test 4 Failed: Customer not added");
        cs4.ServeCustomer();  // serve should display Dana
        Debug.Assert(cs4.QueueSize == 0, "Test 4 Failed: Customer not removed");
        Console.WriteLine(cs4);
        // Defect(s) Found: FIXED ServeCustomer was removing first then printing second
        Console.WriteLine("=================");


        // ===========================
        // Test 5
        // Scenario: Try to serve customer when queue is empty
        // Expected Result: Error message is displayed
        Console.WriteLine("Test 5");

        var cs5 = new CustomerService(2);
        cs5.ServeCustomer(); // should print "No customers in the queue."
        Debug.Assert(cs5.QueueSize == 0, "Test 5 Failed: Queue should still be empty");
        Console.WriteLine(cs5);
        // Defect(s) Found: FIXED crash on ServeCustomer when queue was empty
        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public int MaxSize => _maxSize;
    public int QueueSize => _queue.Count;

    public CustomerService(int maxSize)
    {
        _maxSize = (maxSize <= 0) ? 10 : maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class. Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  
    /// Put the new record into the queue.
    /// </summary>
    public void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    public void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers in the queue.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation 
    /// of the customer service queue object.
    /// </summary>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
