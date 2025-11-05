using System.Collections.Generic;

public class PriorityQueue<TElement, TPriority>
{
    private SortedDictionary<TPriority, Queue<TElement>> dictionary = new SortedDictionary<TPriority, Queue<TElement>>();
    public int Count { get; private set; }

    public void Enqueue(TElement element, TPriority priority)
    {
        if (!dictionary.ContainsKey(priority))
        {
            dictionary[priority] = new Queue<TElement>();
        }
        dictionary[priority].Enqueue(element);
        Count++;
    }

    public TElement Dequeue()
    {
        if (dictionary.Count == 0)
        {
            throw new System.InvalidOperationException("queue is empty.");
        }

        var firstKey = GetFirstKey();
        var queue = dictionary[firstKey];
        var element = queue.Dequeue();
        if (queue.Count == 0)
        {
            dictionary.Remove(firstKey);
        }
        Count--;
        return element;
    }

    public bool Contains(TElement element)
    {
        foreach (var kvp in dictionary)
        {
            if (kvp.Value.Contains(element))
            {
                return true;
            }
        }
        return false;
    }

    private TPriority GetFirstKey()
    {
        foreach (var kvp in dictionary)
        {
            return kvp.Key;
        }
        throw new System.InvalidOperationException("queue is empty.");
    }
}