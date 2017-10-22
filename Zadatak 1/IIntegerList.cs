namespace Zadatak_1
{
    public interface IIntegerList
    {
        void Add(int item); // Adds an item to the collection.
        bool Remove(int item); // Removes the first occurrence of an item from the collection.
        // If the item was not found, method does nothing and returns false.
        bool RemoveAt(int index); // Removes the item at the given index in the collection.
        // Throws IndexOutOfRange exception if index out of range.
        int GetElement(int index); // Returns the item at the given index in the collection .
        // Throws IndexOutOfRange exception if index out of range.
        int IndexOf(int item); // Returns the index of the item in the collection.
        // If item is not found in the collection, method returns -1.
        int Count { get; } // Readonly property. Gets the number of items contained in the collection.
        void Clear(); // Removes all items from the collection.
        bool Contains(int item); // Determines whether the collection contains a specific value.
    }
}