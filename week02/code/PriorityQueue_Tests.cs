using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add people with different priorities and make sure they come out in correct order
    // Expected Result: The people should be dequeued in order of priority (higher first)
    // Defect(s) Found: The loop stops at the second-to-last item due to index < _queue.Count - 1 and that skipped the last item in the queue yet it could be the last in the list at times.
    //                  The item with the highest priority was returned but not removed from the queue.
    // Fixed by ensuring the loop goes through the entire list and removing the item after finding it.
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
    // Defect(s) Found: Dequeue() used >= in comparison,If two or more items have the same priority, the loop keeps updating to the later one breaking FIFO order for same-priority items
    // Fixed by using > in the comparison to ensure that the first item with the highest priority is always returned.
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
}