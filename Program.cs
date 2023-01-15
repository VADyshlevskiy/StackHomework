using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Stack
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = new Stack("a", "b", "c");
            // size = 3, Top = 'c'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            var deleted = s.Pop();
            // Извлек верхний элемент 'c' Size = 2
            Console.WriteLine($"Извлек верхний элемент '{deleted}' Size = {s.Size}");
            s.Add("d");
            // size = 3, Top = 'd'
            Console.WriteLine($"size = {s.Size}, Top = '{s.Top}'");
            s.Pop();
            s.Pop();
            s.Pop();
            // size = 0, Top = null
            Console.WriteLine($"size = {s.Size}, Top = {(s.Top == null ? "null" : s.Top)}");
            s.Pop();

            // доп. задание 1
            var s1 = new Stack("a", "b", "c");
            Console.WriteLine("в стеке s теперь элементы - a, b, c, 3, 2, 1 <- верхний");
            s1.Merge(new Stack("1", "2", "3"));
            Console.WriteLine($"size = {s1.Size}, Top = {(s1.Top == null ? "null" : s1.Top)}");
            Console.WriteLine(s1.printStack());                                                                                     // отладка Выводит элементы стека с вверху вниз

            // доп. задание 2
            var s2 = Stack.Concat(new Stack("a", "b", "c"), new Stack("1", "2", "3"), new Stack("А", "Б", "В"));
            Console.WriteLine("в стеке s теперь элементы - c, b, a 3, 2, 1, В, Б, А <- верхний");
            Console.WriteLine($"size = {s2.Size}, Top = {(s2.Top == null ? "null" : s2.Top)}");
            Console.WriteLine(s2.printStack());                                                                                     // отладка Выводит элементы стека с вверху вниз
        }
    }

    public class Stack
    {
        //List<string> stackString = new List<string>();
        private int _size;
        private string _top;
        StackItem stackItem = new StackItem()
        {
            CurrentItem = null,
            PreviousItem = null
        };

        public Stack(params string[] str)
        {
            foreach (var item in str)
            {
                StackItem newStackItem = new StackItem()
                {
                    CurrentItem = item,
                    PreviousItem = stackItem
                };
                stackItem = newStackItem;
            }

            _size = str.Length;
            _top = str.Length != 0 ? stackItem.CurrentItem : "null";
        }

        public void Add(string newElement)
        {
            //stackString.Add(newElement);
            StackItem newStackItem = new StackItem()
            {
                CurrentItem = newElement,
                PreviousItem = stackItem
            };
            stackItem = newStackItem;
            _size++;
            _top = stackItem.CurrentItem;
        }

        public string Pop()
        {
            try
            {
                var result = stackItem.CurrentItem;
                //stackString.RemoveAt(stackString.Count - 1);
                if (stackItem.PreviousItem != null)
                {
                    stackItem.CurrentItem = stackItem.PreviousItem.CurrentItem;
                    stackItem.PreviousItem = stackItem.PreviousItem.PreviousItem;
                }
                else throw new Exception();
                _size--;
                _top = stackItem.CurrentItem == null ? null : stackItem.CurrentItem;
                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("\nСтек пустой\n");
            }
            return "";
        }

        public static Stack Concat(params Stack[] s)                                                                                // Доп. задание 2
        {
            Stack result = new Stack();

            for (int i = 0; i < s.Length; i++)
            {
                while (s[i].Top != null)
                {
                    result.Add(s[i].Pop());
                }
            }

            return result;
        }

        class StackItem
        {
            public string CurrentItem { get; set; }
            public StackItem PreviousItem { get; set; }

        }

        public int Size
        {
            get => _size;
            set => _size = value;
        }

        public string Top
        {
            get => _top;
            set => _top = value;
        }


        // --------------- Отладка --------------  Выводит элементы стека с вверху вниз
        public string printStack()
        {
            string str = "";
            StackItem printStackItem = stackItem;

            while (stackItem.PreviousItem != null)
            {
                str += "\"" + printStackItem.CurrentItem + "\" ";

                stackItem.CurrentItem = stackItem.PreviousItem.CurrentItem;
                stackItem.PreviousItem = stackItem.PreviousItem.PreviousItem;
            }

            return str;
        }
        // ---------------------------------------

    }

    public static class StackExtensions                                                                                             // Доп. задание 1
    {
        public static Stack Merge(this Stack s1, Stack s2)
        {
            while (s2.Top != null)
            {
                s1.Add(s2.Pop());
            }

            return s1;
        }
    }

}