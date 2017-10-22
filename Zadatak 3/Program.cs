using System;

namespace Zadatak_3
{
    class Program
    {
        static void Main(string[] args)
        {
            IGenericList<string> stringList = new GenericList<string>();
            Console.ReadLine();
            stringList.Add(" Hello ");
            stringList.Add(" World ");
            stringList.Add("!");
            foreach (string value in stringList)
            {
                Console.WriteLine(value);
            }
        }
    }
}
