using System;

namespace Zadatak_2
{
    class Program
    {
        static void Main(string[] args)
        {
            IGenericList<bool> lista = new GenericList<bool>();
            lista.Add(false);
            lista.Add(true);
            lista.Add(true);
            Console.ReadLine();
        }
    }

    public class GenericList <X> : IGenericList <X>
    {
        private X[] _internalStorage;
        private int sp = -1;
        public GenericList()
        {
            _internalStorage = new X[4];
        }

        public GenericList(int initialSize)
        {
            if (initialSize <= 0) throw new ArgumentOutOfRangeException();
            _internalStorage = new X[initialSize];
        }

        public int Count => sp + 1;

        public void Add(X item)
        {
            sp++;
            if (sp >= _internalStorage.Length)
            {
                X[] tempStorage = new X[_internalStorage.Length];
                Array.Copy(_internalStorage, tempStorage, _internalStorage.Length);
                _internalStorage = new X[2 * _internalStorage.Length];
                Array.Copy(tempStorage, _internalStorage, tempStorage.Length);
            }
            _internalStorage[sp] = item;
        }

        public void Clear()
        {
            _internalStorage = new X[_internalStorage.Length];
            sp = -1;
        }

        public bool Contains(X item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_internalStorage[i] != null && _internalStorage[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public X GetElement(int index)
        {
            if (index >= _internalStorage.Length)
            {
                throw new IndexOutOfRangeException();
            }
            return _internalStorage[index];
        }

        public int IndexOf(X item)
        {
            for (int i = 0; i < _internalStorage.Length; i++)
            {
                if (_internalStorage[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Remove(X item)
        {
            if (IndexOf(item) < 0)
            {
                return false;
            }
            return (RemoveAt(IndexOf(item)));
        }
        public bool RemoveAt(int index)
        {
            if (index >= _internalStorage.Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            X[] tempStorage = new X[_internalStorage.Length];
            Array.Copy(_internalStorage, tempStorage, index);
            Array.Copy(_internalStorage, index + 1, tempStorage, index, _internalStorage.Length - (index + 1));
            _internalStorage = tempStorage;
            sp--;
            return true;
        }
    }
}