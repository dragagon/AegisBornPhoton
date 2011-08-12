﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace KoderHack.Collections
{
    public class MultiValueSortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable
    {
        private SortedDictionary<TKey, List<TValue>> _SortedDictionary;
        private int _Count;
        private bool _IsModified;

        /// <summary>
        /// Initializes a new instance of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// class that is empty and uses the default System.Collections.Generic.IComparer&lt;TKey&gt;
        /// implementation to compare keys.
        /// </summary>
        public MultiValueSortedDictionary()
        {
            _SortedDictionary = new SortedDictionary<TKey, List<TValue>>();
        }

        /// <summary>
        /// Initializes a new instance of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// class that is empty and uses the specified System.Collections.Generic.IComparer&lt;TKey&gt;
        /// implementation to compare keys.
        /// </summary>
        /// <param name="comparer">
        /// The System.Collections.Generic.IComparer&lt;TKey&gt; implementation to use when comparing
        /// keys, or null to use the default System.Collections.Generic.Comparer&lt;TKey&gt; for
        /// the type of the key.
        /// </param>
        public MultiValueSortedDictionary(IComparer<TKey> comparer)
        {
            _SortedDictionary = new SortedDictionary<TKey, List<TValue>>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// class that contains elements copied from the specified System.Collections.Generic.IDictionary&lt;TKey,TValue&gt;
        /// and uses the default System.Collections.Generic.IComparer&lt;T&gt; implementation
        /// for the key type.
        /// </summary>
        /// <param name="dictionary">
        /// The System.Collections.Generic.IDictionary&lt;TKey,TValue&gt; whose elements are
        /// copied to the new KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </param>
        /// <exception cref="System.ArgumentNullException">dictionary is null</exception>
        public MultiValueSortedDictionary(IDictionary<TKey, TValue> dictionary)
            : this()
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                this.Add(pair.Key, pair.Value);
            _IsModified = false;
        }

        /// <summary>
        /// Initializes a new instance of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// class that contains elements copied from the specified System.Collections.Generic.IDictionary&lt;TKey,TValue&gt;
        /// and uses the specified System.Collections.Generic.IComparer&lt;TKey&gt; implementation
        /// to compare keys.
        /// </summary>
        /// <param name="dictionary">
        /// The System.Collections.Generic.IDictionary&lt;TKey,TValue&gt; whose elements are
        /// copied to the new KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </param>
        /// <param name="comparer">
        /// The System.Collections.Generic.IComparer&lt;T&gt; implementation to use when comparing
        /// keys, or null to use the default System.Collections.Generic.Comparer&lt;TKey&gt; for
        /// the type of the key.
        /// </param>
        /// <exception cref="System.ArgumentNullException">dictionary is null</exception>
        public MultiValueSortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
            : this(comparer)
        {
            if (dictionary == null) throw new ArgumentNullException("dictionary");

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
                this.Add(pair.Key, pair.Value);
            _IsModified = false;
        }

        /// <summary>
        /// Gets the System.Collections.Generic.IComparer&lt;TKey&gt; used to order the elements
        /// of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <returns>
        /// The System.Collections.Generic.IComparer&lt;TKey&gt; used to order the elements of
        /// the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// </returns>
        public IComparer<TKey> Comparer { get { return _SortedDictionary.Comparer; } }

        /// <summary>
        /// Gets the number of key/value pairs contained in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <returns>
        /// The number of key/value pairs contained in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </returns>
        public int Count { get { return _Count; } }

        /// <summary>
        /// Gets a collection containing the keys in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <returns>
        /// A KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.KeyCollection
        /// containing the keys in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </returns>
        public SortedDictionary<TKey, List<TValue>>.KeyCollection Keys { get { return _SortedDictionary.Keys; } }

        /// <summary>
        /// Gets a collection containing the values in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <returns>
        /// A KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection
        /// containing the keys in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </returns>
        public ValueCollection Values { get { return new ValueCollection(this); } }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// <summary>
        /// <param name="key">
        /// The key of the value to get or set.
        /// </param>
        /// <returns>
        /// The value associated with the specified key. If the specified key is not
        /// found, a get operation throws a System.Collections.Generic.KeyNotFoundException,
        /// and a set operation creates a new element with the specified key.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// The property is retrieved and key does not exist in the collection.
        /// </exception>
        public TValue this[TKey key]
        {
            get
            {
                if (key == null) throw new ArgumentNullException("key");

                List<TValue> values;
                if (!_SortedDictionary.TryGetValue(key, out values))
                    throw new KeyNotFoundException();
                TValue value = default(TValue);
                if (values != null)
                    value = values[0];
                return value;
            }
            set
            {
                if (key == null) throw new ArgumentNullException("key");

                Add(key, value);
            }
        }

        /// <summary>
        /// Adds an element with the specified key and value into the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="key">
        /// The key of the element to add.
        /// </param>
        /// <param name="value">
        /// The value of the element to add. The value can be null for reference types.
        /// </param>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public void Add(TKey key, TValue value)
        {
            if (key == null) throw new ArgumentNullException("key");
            
            List<TValue> values;

            if (!_SortedDictionary.TryGetValue(key, out values))
            {
                values = new List<TValue>();
                _SortedDictionary[key] = values;
            }

            values.Add(value);
            ++_Count;
            _IsModified = true;
        }

        /// <summary>
        /// Removes all elements from the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// <summary>
        public void Clear()
        {
            _SortedDictionary.Clear();
            _Count = 0;
            _IsModified = true;
        }

        /// <summary>
        /// Determines whether the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// contains an element with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to locate in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </param>
        /// <returns>
        /// true if the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt; contains
        /// an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public bool ContainsKey(TKey key)
        {
            return _SortedDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// contains an element with the specified value.
        /// </summary>
        /// <param name="value">
        /// The value to locate in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// The value can be null for reference types.
        /// </param>
        /// <returns>
        /// true if the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt; contains
        /// an element with the specified value; otherwise, false.
        /// <returns>
        public bool ContainsValue(TValue value)
        {
            bool contains = false;
            foreach (TValue val in Values)
            {
                if (val.Equals(value))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }

        /// <summary>
        /// Copies the elements of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// to the specified array of System.Collections.Generic.KeyValuePair<TKey,TValue>
        /// structures, starting at the specified index.
        /// <summary>
        /// <param name="dictionary">
        /// The one-dimensional array of System.Collections.Generic.KeyValuePair<TKey,TValue>
        /// structures that is the destination of the elements copied from the current
        /// KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt; The array must have
        /// zero-based indexing.
        /// </param>
        /// <param name="index">
        /// The zero-based index in array at which copying begins.
        /// </param>
        /// <exception cref="System.ArgumentNullException">array is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// index is equal to or greater than the length of array.  -or- index is less
        /// than 0.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// The number of elements in the source KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;
        /// is greater than the available space from index to the end of the destination
        /// array.
        /// </exception>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            else if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            else if (index >= array.Length || _Count > array.Length - index)
                throw new ArgumentException("index");

            int copyIndex = index;
            foreach (KeyValuePair<TKey, TValue> pair in this)
                array[copyIndex++] = new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <returns>
        /// A KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.Enumerator for
        /// the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </returns>
        public Enumerator GetEnumerator()
        {
            _IsModified = false;
            return new Enumerator(this);
        }

        /// <summary>
        /// Removes the element with the specified key from the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="dictionary">
        /// The key of the element to remove.
        /// </param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false. This method
        /// also returns false if key is not found in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (item.Key == null ||
                item.Key == null)
                throw new ArgumentException();

            bool removed = false;
            List<TValue> values;
            if (_SortedDictionary.TryGetValue(item.Key, out values) &&
                values.Remove(item.Value))
            {
                --_Count;
                if (values.Count == 0)
                    _SortedDictionary.Remove(item.Key);
                removed = true;
                _IsModified = true;
            }
            return removed;
        }

        /// <summary>
        /// Removes the elements with the specified key from the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        /// <param name="dictionary">
        /// The key of the element to remove.
        /// </param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false. This method
        /// also returns false if key is not found in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is null.</exception>
        public bool Remove(TKey key)
        {
            if (key == null) throw new ArgumentNullException();

            bool removed = false;
            List<TValue> values;
            if (_SortedDictionary.TryGetValue(key, out values) &&
                _SortedDictionary.Remove(key))
            {
                _Count -= values.Count;
                _IsModified = removed = true;
            }
            return removed;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key of the value to get.
        /// </param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if
        /// the key is found; otherwise, the default value for the type of the value
        /// parameter.
        /// </param>
        /// <returns>
        /// true if the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt; contains
        /// an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// key is null.
        /// </exception>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null) throw new ArgumentNullException();

            value = default(TValue);
            bool found = false;
            List<TValue> values;
            if (_SortedDictionary.TryGetValue(key, out values))
            {
                value = values[0];
                found = true;
            }
            return found;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key of the value to get.
        /// </param>
        /// <param name="dictionary">
        /// When this method returns, the values associated with the specified key, if
        /// the key is found; otherwise null.
        /// </param>
        /// <returns>
        /// true if the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt; contains
        /// an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// key is null.
        /// </exception>
        public bool TryGetValue(TKey key, out IEnumerable<TValue> values)
        {
            if (key == null) throw new ArgumentNullException();

            values = null;
            bool found = false;
            List<TValue> valuesList;
            if (_SortedDictionary.TryGetValue(key, out valuesList))
            {
                values = valuesList;
                found = true;
            }
            return found;
        }

        #region IDictionary members

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get { return Keys; }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get { return Values; }
        }

        bool IDictionary.IsFixedSize { get { return false; } }
        bool IDictionary.IsReadOnly { get { return false; } }
        ICollection IDictionary.Keys { get { return _SortedDictionary.Keys; } }
        ICollection IDictionary.Values { get { return new ValueCollection(this); } }
        object IDictionary.this[object key]
        {
            get
            {
                if (key == null) throw new ArgumentNullException("key");

                List<TValue> values;
                if (!_SortedDictionary.TryGetValue((TKey)Convert.ChangeType(key, typeof(TKey)), out values))
                    throw new KeyNotFoundException();
                return values[0];
            }
            set
            {
                if (key == null) throw new ArgumentNullException("key");

                Add((TKey)Convert.ChangeType(key, typeof(TKey)), (TValue)Convert.ChangeType(value, typeof(TValue)));
            }
        }
        void IDictionary.Add(object key, object value)
        {
            if (key == null) throw new ArgumentNullException("key");

            Add((TKey)Convert.ChangeType(key, typeof(TKey)), (TValue)Convert.ChangeType(value, typeof(TValue)));
        }

        bool IDictionary.Contains(object key)
        {
            if (key == null) throw new ArgumentNullException("key");

            return ContainsKey((TKey)Convert.ChangeType(key, typeof(TKey)));
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            _IsModified = false;
            return new Enumerator(this);
        }

        void IDictionary.Remove(object key)
        {
            if (key == null) throw new ArgumentNullException("key");

            this.Remove((TKey)Convert.ChangeType(key, typeof(TKey)));
        }

        #endregion

        #region ICollection members

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly { get { return false; } }
        bool ICollection.IsSynchronized { get { return false; } }
        object ICollection.SyncRoot { get { return this; } }
        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            else if (index < 0)
                throw new ArgumentOutOfRangeException("index");
            else if (index >= array.Length || _Count > array.Length - index)
                throw new ArgumentException("index");

            int copyIndex = index;
            foreach (KeyValuePair<TKey, TValue> pair in this)
                array.SetValue(pair, copyIndex++);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            bool done = false;
            List<TValue> values;
            if (_SortedDictionary.TryGetValue(item.Key, out values) &&
                values.Contains(item.Value))
            {
                if (values.Remove(item.Value))
                {
                    --_Count;
                    _IsModified = done = true;
                    if (values.Count == 0)
                        _SortedDictionary.Remove(item.Key);
                }
            }
            return done;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            List<TValue> values;
            return _SortedDictionary.TryGetValue(item.Key, out values) &&
                values.Contains(item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        #endregion

        #region IEnumerable members

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Represents the collection of values in a KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// This class cannot be inherited
        /// </summary>
        [Serializable]
        [DebuggerDisplay("Count = {Count}")]
        //[DebuggerTypeProxy(typeof(System_DictionaryValueCollectionDebugView<,>))]
        public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, ICollection, IEnumerable
        {
            private MultiValueSortedDictionary<TKey, TValue> _Dictionary;

            /// <summary>
            /// Initializes a new instance of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection
            /// class that reflects the values in the specified KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
            /// </summary>
            /// <param name="dictionary">
            /// The KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt; whose values
            /// are reflected in the new KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
            /// </param>
            /// <exception cref="System.ArgumentNullException">
            /// dictionary is null.
            /// </exception>
            public ValueCollection(MultiValueSortedDictionary<TKey, TValue> dictionary)
            {
                if (dictionary == null) throw new ArgumentNullException();

                _Dictionary = dictionary;
                //foreach (KeyValuePair<TKey, List<TValue>> pair in dictionary._SortedDictionary)
                //    _values.AddRange(pair.Value);
            }

            /// <summary>
            /// Gets the number of elements contained in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
            /// </summary>
            /// <returns>
            /// The number of elements contained in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
            /// </returns>
            public int Count { get { return _Dictionary.Count; } }

            /// <summary>
            /// Copies the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection
            /// elements to an existing one-dimensional array, starting at the specified
            /// array index.
            /// </summary>
            /// <param name="array">
            /// The one-dimensional array that is the destination of the elements copied
            /// from the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
            /// The array must have zero-based indexing.
            /// </param>
            /// <param name="index">
            /// The zero-based index in array at which copying begins.
            /// </param>
            /// <exception cref="System.ArgumentNullException">
            /// array is null.
            /// </exception>
            /// <exception cref="System.ArgumentOutOfRangeException">
            /// index is less than 0.
            /// </exception>
            /// <exception cref="System.ArgumentException">
            /// index is equal to or greater than the length of array.  -or- The number of
            /// elements in the source KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection
            /// is greater than the available space from index to the end of the destination
            /// array.
            /// </exception>
            public void CopyTo(TValue[] array, int index)
            {
                if (array == null)
                    throw new ArgumentNullException("array");
                else if (index < 0)
                    throw new ArgumentOutOfRangeException("index");
                else if (index >= array.Length || Count > array.Length - index)
                    throw new ArgumentException("index");

                int copyIndex = index;
                foreach (TValue value in this)
                    array[copyIndex++] = value;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
            /// </summary>
            /// <returns>
            /// A KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.Enumerator
            /// structure for the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
            /// </returns>
            public Enumerator GetEnumerator()
            {
                _Dictionary._IsModified = false;
                return new Enumerator(_Dictionary);
            }

            #region ICollection member

            /// <summary>
            /// Gets a value indicating whether the KoderHack.MultiValueSortedDictionary.ValueCollection is read-only.
            /// </summary>
            /// <returns>
            /// true if the KoderHack.MultiValueSortedDictionary.ValueCollection is read-only; otherwise false.
            /// </returns>
            bool ICollection<TValue>.IsReadOnly { get { return true; } }

            void ICollection<TValue>.Add(TValue item)
            {
                throw new InvalidOperationException();
            }

            void ICollection<TValue>.Clear()
            {
                throw new InvalidOperationException();
            }

            bool ICollection<TValue>.Remove(TValue item)
            {
                throw new InvalidOperationException();
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                    throw new ArgumentNullException("array");
                else if (index < 0)
                    throw new ArgumentOutOfRangeException("index");
                else if (index >= array.Length || Count > array.Length - index)
                    throw new ArgumentException("index");

                int copyIndex = index;
                foreach (TValue value in this)
                    array.SetValue(value, copyIndex++);
            }

            bool ICollection<TValue>.Contains(TValue item)
            {
                return _Dictionary.ContainsValue(item);
            }

            /// <summary>
            /// Gets a value indicating whether access to the System.Collections.ICollection
            /// is synchronized (thread safe).
            /// </summary>
            /// <returns>
            /// true if access to the System.Collections.ICollection is synchronized (thread
            /// safe); otherwise, false.
            /// </returns>
            bool ICollection.IsSynchronized { get { return false; } }

            /// <summary>
            /// Gets an object that can be used to synchronize access to the System.Collections.ICollection.
            /// </summary>
            /// <returns>
            /// An object that can be used to synchronize access to the System.Collections.ICollection.
            /// </returns>
            object ICollection.SyncRoot { get { return this; } }

            #endregion

            #region  IEnumerator members
            
            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }            
            
            #endregion

            /// <summary>
            /// Enumerates the elements of a KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
            /// </summary>
            public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
            {
                private MultiValueSortedDictionary<TKey, TValue> _Dictionary;
                private MultiValueSortedDictionary<TKey, TValue>.Enumerator _Enumerator;

                internal Enumerator(MultiValueSortedDictionary<TKey, TValue> dictionary)
                {
                    _Dictionary = dictionary;
                    _Enumerator = new MultiValueSortedDictionary<TKey, TValue>.Enumerator(_Dictionary);
                }

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                /// <returns>
                /// The element in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection
                /// at the current position of the enumerator.
                /// </returns>
                public TValue Current { get { return _Enumerator.Current.Value; } }

                /// <summary>
                /// Releases all resources used by the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.Enumerator.
                /// </summary>
                public void Dispose()
                {
                }

                /// <summary>
                /// Advances the enumerator to the next element of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection.
                /// </summary>
                /// <returns>
                /// true if the enumerator was successfully advanced to the next element; false
                /// if the enumerator has passed the end of the collection.
                /// </returns>
                /// <exception cref="System.InvalidOperationException">
                /// The collection was modified after the enumerator was created.
                /// </exception>
                public bool MoveNext()
                {
                    return _Enumerator.MoveNext();
                }

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                /// <returns>
                /// The element in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection
                /// at the current position of the enumerator.
                /// </returns>
                object IEnumerator.Current
                {
                    get { return Current; }
                }

                /// <summary>
                /// Sets the enumerator to its initial position, which is before the first element
                /// in the collection.
                /// </summary>
                /// <exception cref="System.InvalidOperationException">
                /// The collection was modified after the enumerator was created.
                /// </exception>
                void IEnumerator.Reset()
                {
                    (_Enumerator as IEnumerator).Reset();
                }
            }
        }

        /// <summary>
        /// Enumerates the elements of a KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
        /// </summary>
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IDictionaryEnumerator, IEnumerator
        {
            private readonly KeyValuePair<TKey, TValue> DefaultCurrent;
            private MultiValueSortedDictionary<TKey, TValue> _Dictionary;
            private IEnumerator<KeyValuePair<TKey, List<TValue>>> _Enumerator1;
            private IEnumerator<TValue> _Enumerator2;
            private KeyValuePair<TKey, TValue> _Current;

            //private bool _Disposed;
            private bool _Valid;

            public Enumerator(MultiValueSortedDictionary<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                    throw new ArgumentNullException("dictionary");

                _Dictionary = dictionary;
                _Enumerator1 = dictionary._SortedDictionary.GetEnumerator();
                _Enumerator2 = null;
                DefaultCurrent = new KeyValuePair<TKey, TValue>(default(TKey), default(TValue));
                _Current = DefaultCurrent;
                //_Disposed = false;
                _Valid = true;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// <returns>
            /// The element in the System.Collections.Generic.SortedDictionary<TKey,TValue>
            /// at the current position of the enumerator.
            /// </returns>  
            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    if (_Current.Equals(DefaultCurrent) ||
                        !_Valid)
                        throw new InvalidOperationException();

                    return _Current;
                }
            }

            /// <summary>
            /// Gets both the key and the value of the current dictionary entry.
            /// </summary>
            /// <returns>
            /// A System.Collections.DictionaryEntry containing both the key and the value
            /// of the current dictionary entry.
            /// </returns>
            /// <exception cref="System.InvalidOperationException">
            /// The System.Collections.IDictionaryEnumerator is positioned before the first
            /// entry of the dictionary or after the last entry.
            /// </exception>
            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    return new DictionaryEntry(Current.Key, Current.Value);
                }
            }

            /// <summary>
            /// Gets the key of the current dictionary entry.
            /// </summary>
            /// <returns>
            /// The key of the current element of the enumeration.
            /// </returns>
            /// <exception cref="System.InvalidOperationException">
            /// The System.Collections.IDictionaryEnumerator is positioned before the first
            /// entry of the dictionary or after the last entry.
            /// </exception>
            object IDictionaryEnumerator.Key
            {
                get
                {
                    return Current.Key;
                }
            }

            /// <summary>
            /// Gets the value of the current dictionary entry.
            /// </summary>
            /// <returns>
            /// The value of the current element of the enumeration.
            /// </returns>
            /// <exception cref="System.InvalidOperationException">
            /// The System.Collections.IDictionaryEnumerator is positioned before the first
            /// entry of the dictionary or after the last entry.
            /// </exception>
            object IDictionaryEnumerator.Value
            {
                get
                {
                    return Current.Value;
                }
            }

            /// <summary>
            /// Releases all resources used by the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.Enumerator.
            /// </summary>
            public void Dispose()
            {
                //_Enumerator1.Dispose();
                //_Enumerator2.Dispose();
                //_Dictionary._IsModified = false;
                //_Dictionary = null;
                //_Disposed = true;
            }

            /// <summary>
            /// Advances the enumerator to the next element of the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false
            /// if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="System.InvalidOperationException">
            /// The collection was modified after the enumerator was created.
            /// </exception>
            public bool MoveNext()
            {
                //if (_Disposed)
                //    throw new InvalidOperationException("The enumerator has already been disposed.");

                if (_Dictionary._IsModified)
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");

                if (!_Valid) return false;
                TKey key = default(TKey);
                TValue value = default(TValue);
                if (_Enumerator2 == null)
                {
                    if (_Enumerator1.MoveNext())
                    {
                        if (_Enumerator1.Current.Value != null)
                            _Enumerator2 = _Enumerator1.Current.Value.GetEnumerator();
                    }
                    else
                        _Valid = false;
                }

                if (!_Valid) return false;

                key = _Enumerator1.Current.Key;
                if (_Enumerator2 != null)
                {
                    if (_Enumerator2.MoveNext())
                        value = _Enumerator2.Current;
                    else
                    {
                        _Enumerator2 = null;
                        return MoveNext();
                    }
                }
                _Current = new KeyValuePair<TKey, TValue>(key, value);
                return true;
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            /// <returns>
            /// The element in the KoderHack.MultiValueSortedDictionary&lt;TKey,TValue&gt;.ValueCollection
            /// at the current position of the enumerator.
            /// </returns>
            object IEnumerator.Current
            {
                get { return Current; }
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element
            /// in the collection.
            /// </summary>
            /// <exception cref="System.InvalidOperationException">
            /// The collection was modified after the enumerator was created.
            /// </exception>
            void IEnumerator.Reset()
            {
                //if (_Disposed)
                //    throw new InvalidOperationException("The enumerator has already been disposed.");
                if (_Dictionary._IsModified)
                    throw new InvalidOperationException("The collection was modified after the enumerator was created.");

                _Enumerator1.Reset();
                _Enumerator2 = null;
                _Valid = true;
            }
        }
    }
}