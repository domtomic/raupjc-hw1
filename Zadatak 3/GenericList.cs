using System;
using System.Collections;
using System.Collections.Generic;

namespace Zadatak_3
{
    public class GenericListEnumerator<X> : IEnumerator<X>
    {
        private GenericList<X> _list;
        private int _sp;
        private X _item;

        public GenericListEnumerator(GenericList<X> list)
        {
            _sp = -1;
            _list = list;
        }
        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            _sp++;
            if (_sp >= _list.Count)
            {
                return false;
            }
            _item = _list.GetElement(_sp);
            return true;
        }

        public void Reset()
        {
            _sp = -1;
        }

        public X Current => _item;
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
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
        public IEnumerator<X> GetEnumerator()
        {
            return new GenericListEnumerator<X>(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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