using System;
using System.Collections.Generic;

namespace Higgs.Server.Utilities
{
    public static class CollectionUpdater
    {
        /// <summary>
        ///     Used to wrap extract logic to handle merging a source collection (for example, from the database)
        ///     with a supplied collection (for example, from user input)
        /// </summary>
        public static void UpdateCollection<TKey, TSource, TNew>(
            IDictionary<TKey, TSource> sourceItems,
            IDictionary<TKey, TNew> newItems,
            Action<TNew> onNew,
            Action<TSource, TNew> onUpdate,
            Action<TSource> onDelete
        )
        {
            foreach (var newItem in newItems)
            {
                if (sourceItems.ContainsKey(newItem.Key))
                {
                    onUpdate(sourceItems[newItem.Key], newItem.Value);
                }
                else
                {
                    onNew(newItem.Value);
                }
            }
            foreach (var item in sourceItems)
            {
                if (!newItems.ContainsKey(item.Key))
                {
                    onDelete(item.Value);
                }
            }
        }
    }
}