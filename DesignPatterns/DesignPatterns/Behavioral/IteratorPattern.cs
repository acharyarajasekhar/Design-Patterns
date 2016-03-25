using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Behavioral
{
    class IteratorPattern
    {
        class MyClass
        {
            public string Data { get; set; }
        }

        public static void ShowDemo()
        {
            MyLinkedList<MyClass> list = new MyLinkedList<MyClass>();
            list.AddAsFirst(new MyClass { Data = "1" });
            list.AddToBack(new MyClass { Data = "2" });
            list.AddToBack(new MyClass { Data = "3" });
            list.AddAsFirst(new MyClass { Data = "0" });

            IEnumerator<MyClass> iterator = list.GetEnumerator();
            do
            {
                Console.WriteLine(iterator.Current.Data);
            } while (iterator.MoveNext());
        }
    }

    class MyLinkedListItem<T>
    {
        public T Data { get; set; }
        public MyLinkedListItem<T> NextItem { get; set; }

        public MyLinkedListItem(T data)
        {
            Data = data;
        }

        public MyLinkedListItem(T data, MyLinkedListItem<T> next)
        {
            Data = data;
            NextItem = next;
        }
    }

    class MyLinkedList<T> : IEnumerable<T>
    {
        private MyLinkedListItem<T> _front;
        private int _size;

        public MyLinkedList()
        {
            _front = null;
            _size = 0;
        }

        public bool AddAsFirst(T item)
        {
            _front = new MyLinkedListItem<T>(item, _front);
            _size++;
            return true;
        }

        public bool AddToBack(T item)
        {
            MyLinkedListItem<T> current = _front;
            while (current.NextItem != null)
                current = current.NextItem;
            current.NextItem = new MyLinkedListItem<T>(item);
            _size++;
            return true;
        }

        public override string ToString()
        {
            MyLinkedListItem<T> current = _front;

            if (current == null)
            {
                return "**** Empty ****";
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                while (current.NextItem != null)
                {
                    sb.Append(current.Data + ", ");
                    current = current.NextItem;
                }

                sb.Append(current.Data);
                return sb.ToString();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MyLinkedListIterator<T>(_front);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class MyLinkedListIterator<T> : IEnumerator<T>
    {
        private MyLinkedListItem<T> _current;

        public MyLinkedListIterator(MyLinkedListItem<T> current)
        {
            _current = current;
        }

        public T Current
        {
            get { return _current.Data; }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return _current.Data; }
        }

        public bool MoveNext()
        {
            _current = _current.NextItem;
            return _current != null;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
