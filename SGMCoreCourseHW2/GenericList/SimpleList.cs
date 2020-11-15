using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GenericList
{
    class SimpleList<T> : System.Collections.Generic.ICollection<T>, System.Collections.Generic.IEnumerable<T>, IEnumerator<T>, System.Collections.Generic.IList<T>, System.Collections.Generic.IReadOnlyCollection<T>, System.Collections.Generic.IReadOnlyList<T>, System.Collections.IList
    {
        private T[] _items;
        private int _size;

        public SimpleList()
        {
            _items = new T[0];
        }

        public SimpleList(int capacity)
        {
            _items = new T[capacity];
        }

        public T this[int index] {
            get => _items[index];
            set => _items[index] = value;

        }

        object IList.this[int index]
        {
            get => _items[index];
            set => _items[index] = (T) value;
        }

        public int Count => _size;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot => this;

        public void Add(T item)
        {
            var newItems = new T[_size + 1];
            for (int i = 0; i < _size; i++)
            {
                newItems[i] = _items[i];
            }

            newItems[_size] = item;
            _size++;
            _items = newItems;
        }

        public int Add(object value)
        {
            if (value is T)
            {
                Add((T) value);
                return _size;
            }
            else
            {
                throw new ArgumentException();
            }
            
        }

        public void Clear()
        {
            _items = new T[0];
            _size = 0;
        }

        public T Find(Predicate<T> predicate)
        {
            foreach (T _item in _items)
            {
                if (predicate(_item))
                {
                    return _item;
                }
            }
            
            throw new IndexOutOfRangeException();
        }
        public bool Contains(T item)
        {
            foreach (T _item in _items)
            {
                if (_item.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Contains(object value)
        {
            if (value is T)
            {
                return Contains((T)value);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length >= _size + arrayIndex)
            {
                for (int i = 0; i < _size; i++)
                {
                    array[arrayIndex + i] = _items[i];
                }
            }
            else
            {
                throw new ArgumentException();
            }

        }

        public void CopyTo(Array array, int index)
        {
            if (array.Length >= _size + index)
            {
                Array.Copy(_items, 0, array, index, _size);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public int IndexOf(object value)
        {
            if (value is T)
            {
                return IndexOf((T)value);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void Insert(int index, T item)
        {
            var newItems = new T[_size + 1];
            for (int i = 0, j = 0; i < _size; i++, j++)
            {
                if (i == index)
                {
                    newItems[j] = item;
                    j++;
                }

                if (_size > 0)
                {
                    newItems[j] = _items[i];
                }
            }
            _size++;
            _items = newItems;
        }

        public void Insert(int index, object value)
        {
            if (value is T)
            {
                Insert(index, (T)value);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                var newItems = new T[_size - 1];
                for (int i = 0, j = 0; i < _size; i++, j++)
                {
                    if (!item.Equals(_items[i]))
                    {
                        newItems[j] = _items[i];
                    }
                    else
                    {
                        j--;
                    }
                }

                _size--;
                _items = newItems;
                return true;
            }

            return false;
        }

        public void Remove(object value)
        {
            if (value is T)
            {
                Remove((T)value);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void RemoveAll()
        {
            Clear();
        }
        public void RemoveAt(int index)
        {
                var newItems = new T[_size - 1];
                for (int i = 0, j = 0; i < _size; i++, j++)
                {
                    if (index != i)
                    {
                        newItems[j] = _items[i];
                    }
                    else
                    {
                        j--;
                    }
                }

                _size--;
                _items = newItems;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        private int _cursor = -1;
        public bool MoveNext()
        {
            _cursor++;
            if (_cursor < _size)
            {
                return true;
            }
            else
            {
                _cursor = -1;
                return false;
            }
        }

        public void Reset()
        {
            _cursor = -1;
        }

        public T Current => _items[_cursor];

        object? IEnumerator.Current => Current;

        public void Dispose()
        {
            _cursor = -1;
        }
    }
}
