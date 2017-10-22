using System;

namespace Zadatak_1
{
    class Program
    {
        static void Main(string[] args)
        {
            IIntegerList lista = new IntegerList();
            Console.ReadLine();
        }
    }

    public class IntegerList : IIntegerList
    {
        private int[] _internalStorage;
        private int sp = -1;
        public IntegerList()
        {
            _internalStorage = new int[4];
        }

        public IntegerList(int initialSize)
        {
            if (initialSize <= 0) throw  new ArgumentOutOfRangeException();
            _internalStorage = new int[initialSize];
        }

        public int Count => sp+1;

        public void Add(int item)
        {
            sp++;
            if (sp >= _internalStorage.Length)
            {
                int[] tempStorage = new int[_internalStorage.Length];
                Array.Copy(_internalStorage, tempStorage, _internalStorage.Length);
                _internalStorage = new int[2 * _internalStorage.Length];
                Array.Copy(tempStorage, _internalStorage, tempStorage.Length);
            }
            _internalStorage[sp] = item;
        }

        public void Clear()
        {
            _internalStorage = new int[_internalStorage.Length];
            sp = -1;
        }

        public bool Contains(int item)
        {
            foreach (int t in _internalStorage)
            {
                if (t == item)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetElement(int index)
        {
            if (index >= _internalStorage.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return _internalStorage[index];
        }

        public int IndexOf(int item)
        {
            for (int i = 0; i < _internalStorage.Length; i++)
            {
                if (_internalStorage[i] == item)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Remove(int item)
        {
            if (IndexOf(item) < 0)
            {
                return false;
            }
            return(RemoveAt(IndexOf(item)));
        }

        public bool RemoveAt(int index)
        {
            if (index >= _internalStorage.Length || index < 0)
            {
                throw new IndexOutOfRangeException(); 
            }

            int[] tempStorage = new int[_internalStorage.Length];
            Array.Copy(_internalStorage, tempStorage, index);
            Array.Copy(_internalStorage, index+1, tempStorage, index, _internalStorage.Length-(index+1));
            _internalStorage = tempStorage;
            sp--;
            return true;
        }
    }
}