using System.Collections.Generic;

namespace Zadatak_3
{
    public interface IGenericList<X> : IEnumerable<X>
    {
        void Add(X item); // Adds an item to the collection.
        bool Remove(X item); // Removes the first occurrence of an item from the collection.
        // If the item was not found, method does nothing and returns false.
        bool RemoveAt(int index); // Removes the item at the given index in the collection.
        // Throws IndexOutOfRange exception if index out of range.
        X GetElement(int index); // Returns the item at the given index in the collection .
        // Throws IndexOutOfRange exception if index out of range.
        int IndexOf(X item); // Returns the index of the item in the collection.
        // If item is not found in the collection, method returns -1.
        int Count { get; } // Readonly property. Gets the number of items contained in the collection.
        void Clear(); // Removes all items from the collection.
        bool Contains(X item); // Determines whether the collection contains a specific value.
    }
}