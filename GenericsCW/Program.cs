using System.Collections;

MyList<int> numbers = new MyList<int>();

numbers.Add(10);
numbers.Add(20);
numbers.Add(30);
numbers.Insert(1, 15);
numbers.Remove(30);
numbers.Add(40);

Console.WriteLine("List:");
foreach (int number in numbers)
{
    Console.Write(number + " ");
}

Console.WriteLine();
Console.WriteLine($"Count: {numbers.Count}");
Console.WriteLine($"Capacity: {numbers.Capacity}");
Console.WriteLine($"Index of 20: {numbers.IndexOf(20)}");
Console.WriteLine($"Last index of 20: {numbers.LastIndexOf(20)}");
Console.WriteLine($"Contains 15: {numbers.Contains(15)}");
Console.WriteLine($"Find > 15: {numbers.Find(x => x > 15)}");

List<int> foundNumbers = numbers.FindAll(x => x >= 20);

Console.WriteLine("FindAll >= 20:");
foreach (int number in foundNumbers)
{
    Console.Write(number + " ");
}

Console.WriteLine();

interface IMyList<T>
{
    T this[int index] { get; set; }
    int IndexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

interface IMyCollection<T>
{
    int Count { get; }
    int Capacity { get; }
    void Clear();
    bool Contains(T item);
    bool Remove(T item);
    void Add(T element);
    void RemoveAt(int index);
    void Insert(int index, T element);
}

class MyList<T> : IMyList<T>, IMyCollection<T>, IEnumerable<T>
{
    private T[] items;

    public int Count { get; private set; }
    public int Capacity => items.Length;

    public MyList()
    {
        items = new T[4];
    }

    public T this[int index]
    {
        get
        {
            CheckIndex(index);
            return items[index];
        }
        set
        {
            CheckIndex(index);
            items[index] = value;
        }
    }

    public void Add(T element)
    {
        if (Count == Capacity)
        {
            Resize();
        }

        items[Count] = element;
        Count++;
    }

    public void Insert(int index, T element)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }

        if (Count == Capacity)
        {
            Resize();
        }

        for (int i = Count; i > index; i--)
        {
            items[i] = items[i - 1];
        }

        items[index] = element;
        Count++;
    }

    public void RemoveAt(int index)
    {
        CheckIndex(index);

        for (int i = index; i < Count - 1; i++)
        {
            items[i] = items[i + 1];
        }

        Count--;
        items[Count] = default!;
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);

        if (index == -1)
        {
            return false;
        }

        RemoveAt(index);
        return true;
    }

    public void Clear()
    {
        for (int i = 0; i < Count; i++)
        {
            items[i] = default!;
        }

        Count = 0;
    }

    public bool Contains(T item)
    {
        return IndexOf(item) != -1;
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < Count; i++)
        {
            if (Equals(items[i], item))
            {
                return i;
            }
        }

        return -1;
    }

    public int LastIndexOf(T element)
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            if (Equals(items[i], element))
            {
                return i;
            }
        }

        return -1;
    }

    public List<T> FindAll(Predicate<T> match)
    {
        List<T> result = new List<T>();

        for (int i = 0; i < Count; i++)
        {
            if (match(items[i]))
            {
                result.Add(items[i]);
            }
        }

        return result;
    }

    public T Find(Predicate<T> match)
    {
        for (int i = 0; i < Count; i++)
        {
            if (match(items[i]))
            {
                return items[i];
            }
        }

        return default!;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private void Resize()
    {
        T[] newItems = new T[Capacity * 2];

        for (int i = 0; i < Count; i++)
        {
            newItems[i] = items[i];
        }

        items = newItems;
    }

    private void CheckIndex(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }
    }
}
