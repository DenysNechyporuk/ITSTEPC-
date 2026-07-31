using System.Collections;

namespace GenericsHW1
{
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
        private T[] data;
        private int count;

        public int Count => count;
        public int Capacity => data.Length;

        public MyList()
        {
            data = new T[4];
            count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException();
                return data[index];
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException();
                data[index] = value;
            }
        }

        private void Resize()
        {
            T[] newData = new T[data.Length * 2];
            for (int i = 0; i < count; i++)
                newData[i] = data[i];
            data = newData;
        }

        public void Add(T element)
        {
            if (count == data.Length)
                Resize();
            data[count] = element;
            count++;
        }

        public void Insert(int index, T item)
        {
            if (count == data.Length)
                Resize();
            for (int i = count; i > index; i--)
                data[i] = data[i - 1];
            data[index] = item;
            count++;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < count - 1; i++)
                data[i] = data[i + 1];
            data[count - 1] = default;
            count--;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        public void Clear()
        {
            data = new T[4];
            count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
                if (data[i].Equals(item))
                    return i;
            return -1;
        }

        public int LastIndexOf(T item)
        {
            for (int i = count - 1; i >= 0; i--)
                if (data[i].Equals(item))
                    return i;
            return -1;
        }

        public List<T> FindAll(Predicate<T> match)
        {
            List<T> result = new List<T>();
            for (int i = 0; i < count; i++)
                if (match(data[i]))
                    result.Add(data[i]);
            return result;
        }

        public T Find(Predicate<T> match)
        {
            for (int i = 0; i < count; i++)
                if (match(data[i]))
                    return data[i];
            return default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < count; i++)
                yield return data[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main()
        {
            MyList<int> list = new MyList<int>();

            list.Add(10);
            list.Add(20);
            list.Add(30);
            list.Add(20);
            list.Add(50);

            Console.WriteLine("Count :: " + list.Count);
            Console.WriteLine("Capacity :: " + list.Capacity);

            Console.WriteLine("IndexOf(20) :: " + list.IndexOf(20));
            Console.WriteLine("LastIndexOf(20) :: " + list.LastIndexOf(20));
            Console.WriteLine("Contains(30) :: " + list.Contains(30));

            list.Insert(1, 99);
            Console.WriteLine("After Insert(1, 99) :: ");
            foreach (int item in list)
                Console.Write(item + " ");
            Console.WriteLine();

            list.RemoveAt(0);
            Console.WriteLine("After RemoveAt(0) :: ");
            foreach (int item in list)
                Console.Write(item + " ");
            Console.WriteLine();

            list.Remove(20);
            Console.WriteLine("After Remove(20) :: ");
            foreach (int item in list)
                Console.Write(item + " ");
            Console.WriteLine();

            List<int> found = list.FindAll(x => x > 25);
            Console.Write("FindAll(x > 25) :: ");
            foreach (int item in found)
                Console.Write(item + " ");
            Console.WriteLine();

            int f = list.Find(x => x > 25);
            Console.WriteLine("Find(x > 25) :: " + f);

            list.Clear();
            Console.WriteLine("After Clear, Count :: " + list.Count);
        }
    }
}