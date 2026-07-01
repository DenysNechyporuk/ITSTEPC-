namespace InterfacesHW
{
    class Array : IOutput, IMath
    {
        private int[] numbers;

        public Array(int[] numbers)
        {
            this.numbers = numbers;
        }

        public void Show()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " ");
            }

            Console.WriteLine();
        }

        public void Show(string info)
        {
            Show();
            Console.WriteLine(info);
        }

        public int Max()
        {
            int max = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] > max)
                {
                    max = numbers[i];
                }
            }

            return max;
        }

        public int Min()
        {
            int min = numbers[0];

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < min)
                {
                    min = numbers[i];
                }
            }

            return min;
        }

        public float Avg()
        {
            int sum = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return (float)sum / numbers.Length;
        }

        public bool Search(int valueToSearch)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == valueToSearch)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
