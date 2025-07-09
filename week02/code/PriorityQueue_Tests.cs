using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add people with different priorities and make sure they come out in correct order
    // Expected Result: The people should be dequeued in order of priority (higher first)
    // Defect(s) Found: Test was not implemented
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Alice", 3);
        priorityQueue.Enqueue("Bob", 5);
        priorityQueue.Enqueue("Charlie", 1);

        Assert.AreEqual("Bob", priorityQueue.Dequeue());
        Assert.AreEqual("Alice", priorityQueue.Dequeue());
        Assert.AreEqual("Charlie", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple people with same priority
    // Expected Result: The people with same priority should come out in order they were added
    // Defect(s) Found: Test was not implemented
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Dave", 2);
        priorityQueue.Enqueue("Eve", 2);
        priorityQueue.Enqueue("Frank", 2);

        Assert.AreEqual("Dave", priorityQueue.Dequeue());
        Assert.AreEqual("Eve", priorityQueue.Dequeue());
        Assert.AreEqual("Frank", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
}