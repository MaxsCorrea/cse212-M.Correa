using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities and dequeue them all
    // Expected Result: Items should be dequeued in order of highest priority first
    // Defect(s) Found: The loop in Dequeue method uses 'index < _queue.Count - 1' instead of 'index < _queue.Count', 
    // causing the last item to never be checked for highest priority. Also, the item is never removed from the queue.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);
        
        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple items with same highest priority and verify FIFO order for same priority
    // Expected Result: Items with same priority should be dequeued in FIFO order (first added, first removed)
    // Defect(s) Found: Same defects as above - loop bounds issue and item not removed from queue
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First High", 5);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Second High", 5);
        priorityQueue.Enqueue("Third High", 5);
        
        Assert.AreEqual("First High", priorityQueue.Dequeue());
        Assert.AreEqual("Second High", priorityQueue.Dequeue());
        Assert.AreEqual("Third High", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

[TestMethod]
    // Scenario: Try to dequeue from empty queue
    // Expected Result: InvalidOperationException should be thrown
    // Defect(s) Found: No defects found for this scenario - exception handling works correctly
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                string.Format("Unexpected exception of type {0} caught: {1}",
                               e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Add single item and dequeue it
    // Expected Result: Should return the single item
    // Defect(s) Found: Same defects as test 1 - loop bounds and item not removed
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Only Item", 3);
        
        Assert.AreEqual("Only Item", priorityQueue.Dequeue());
        
        // Verify queue is now empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown for empty queue.");
        }
        catch (InvalidOperationException)
        {
            // Expected behavior
        }
    }

    [TestMethod]
    // Scenario: Add items with negative and zero priorities
    // Expected Result: Higher numbers should still have higher priority, even if negative
    // Defect(s) Found: Same defects as above
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Negative", -5);
        priorityQueue.Enqueue("Zero", 0);
        priorityQueue.Enqueue("Positive", 2);
        
        Assert.AreEqual("Positive", priorityQueue.Dequeue());
        Assert.AreEqual("Zero", priorityQueue.Dequeue());
        Assert.AreEqual("Negative", priorityQueue.Dequeue());
    }
}