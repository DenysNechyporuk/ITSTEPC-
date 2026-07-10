namespace InterfacesHW3
{
    interface IDisk
    {
        string Read();
        void Write(string text);
    }

    interface IRemoveableDisk
    {
        bool HasDisk { get; }
        void Insert();
        void Reject();
    }

    interface IPrintInformation
    {
        string GetName();
        void Print(string str);
    }

    class Disk : IDisk
    {
        public string Memory { get; set; }
        public int MemSize { get; set; }

        public Disk() { }

        public Disk(string memory, int memSize)
        {
            Memory = memory;
            MemSize = memSize;
        }

        public virtual string GetName() => "Disk";

        public string Read()
        {
            return Memory;
        }

        public void Write(string text)
        {
            Memory = text;
        }
    }

    class HDD : Disk
    {
        public override string GetName() => "HDD";
    }

    class CD : Disk, IRemoveableDisk
    {
        private bool hasDisk;
        public bool HasDisk { get => hasDisk; }

        public override string GetName() => "CD";

        public void Insert()
        {
            hasDisk = true;
            Console.WriteLine("CD inserted");
        }

        public void Reject()
        {
            hasDisk = false;
            Console.WriteLine("CD rejected");
        }
    }

    class Flash : Disk, IRemoveableDisk
    {
        private bool hasDisk;
        public bool HasDisk { get => hasDisk; }

        public override string GetName() => "Flash";

        public void Insert()
        {
            hasDisk = true;
            Console.WriteLine("Flash inserted");
        }

        public void Reject()
        {
            hasDisk = false;
            Console.WriteLine("Flash rejected");
        }
    }

    class DVD : Disk, IRemoveableDisk
    {
        private bool hasDisk;
        public bool HasDisk { get => hasDisk; }

        public override string GetName() => "DVD";

        public void Insert()
        {
            hasDisk = true;
            Console.WriteLine("DVD inserted");
        }

        public void Reject()
        {
            hasDisk = false;
            Console.WriteLine("DVD rejected");
        }
    }

    class Printer : IPrintInformation
    {
        public string GetName() => "Printer";

        public void Print(string str)
        {
            Console.WriteLine("Printer: " + str);
        }
    }

    class Monitor : IPrintInformation
    {
        public string GetName() => "Monitor";

        public void Print(string str)
        {
            Console.WriteLine("Monitor: " + str);
        }
    }

    class Comp
    {
        private int countDisk;
        private int countPrintDevice;
        private Disk[] disks;
        private IPrintInformation[] printDevice;

        public Comp(int d, int pd)
        {
            countDisk = 0;
            countPrintDevice = 0;
            disks = new Disk[d];
            printDevice = new IPrintInformation[pd];
        }

        public void AddDisk(int index, Disk d)
        {
            disks[index] = d;
            countDisk++;
        }

        public void AddDevice(int index, IPrintInformation si)
        {
            printDevice[index] = si;
            countPrintDevice++;
        }

        public bool CheckDisk(string device)
        {
            foreach (Disk d in disks)
            {
                if (d != null && d.GetName() == device)
                    return true;
            }
            return false;
        }

        public void InsertReject(string device, bool b)
        {
            foreach (Disk d in disks)
            {
                if (d != null && d.GetName() == device && d is IRemoveableDisk)
                {
                    IRemoveableDisk rd = (IRemoveableDisk)d;
                    if (b) rd.Insert();
                    else rd.Reject();
                }
            }
        }

        public bool PrintInfo(string text, string device)
        {
            foreach (IPrintInformation p in printDevice)
            {
                if (p != null && p.GetName() == device)
                {
                    p.Print(text);
                    return true;
                }
            }
            return false;
        }

        public string ReadInfo(string device)
        {
            foreach (Disk d in disks)
            {
                if (d != null && d.GetName() == device)
                    return d.Read();
            }
            return "Not found";
        }

        public bool WriteInfo(string text, string device)
        {
            foreach (Disk d in disks)
            {
                if (d != null && d.GetName() == device)
                {
                    d.Write(text);
                    return true;
                }
            }
            return false;
        }

        public void ShowDisk()
        {
            Console.WriteLine("Disks:");
            foreach (Disk d in disks)
            {
                if (d != null)
                    Console.WriteLine("  " + d.GetName() + " | Memory: " + d.Memory);
            }
        }

        public void ShowPrintDevice()
        {
            Console.WriteLine("Print devices:");
            foreach (IPrintInformation p in printDevice)
            {
                if (p != null)
                    Console.WriteLine("  " + p.GetName());
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Comp comp = new Comp(4, 2);

            comp.AddDisk(0, new HDD());
            comp.AddDisk(1, new CD());
            comp.AddDisk(2, new Flash());
            comp.AddDisk(3, new DVD());

            comp.AddDevice(0, new Printer());
            comp.AddDevice(1, new Monitor());

            comp.WriteInfo("Hello HDD", "HDD");
            comp.WriteInfo("Hello Flash", "Flash");

            comp.ShowDisk();
            comp.ShowPrintDevice();

            Console.WriteLine("\nRead from HDD: " + comp.ReadInfo("HDD"));
            Console.WriteLine("Check Flash: " + comp.CheckDisk("Flash"));

            Console.WriteLine();
            comp.InsertReject("CD", true);
            comp.InsertReject("CD", false);

            Console.WriteLine();
            comp.PrintInfo("Test print", "Printer");
            comp.PrintInfo("Test print", "Monitor");
        }
    }
}