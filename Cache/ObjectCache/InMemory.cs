using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectCache
{
    using System.Globalization;
    using System.Runtime.Caching;

    public class InMemory
    {
        // Gets a reference to the default MemoryCache instance. 
        private ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;
        private CacheEntryRemovedCallback callback = null;

        /// <summary>
        /// Add to my cache.
        /// </summary>
        /// <param name="cacheKeyName">
        /// The cache key name.
        /// </param>
        /// <param name="cacheItem">
        /// The cache item.
        /// </param>
        /// <param name="myCacheItemPriority">
        /// The my cache item priority.
        /// </param>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        public void AddToMyCache(string cacheKeyName, object cacheItem, MyCachePriority myCacheItemPriority, List<string> filePath)
        {
            this.callback = new CacheEntryRemovedCallback(this.MyCachedItemRemovedCallback);
            this.policy = new CacheItemPolicy();
            this.policy.Priority = (myCacheItemPriority == MyCachePriority.Default)
                                  ? CacheItemPriority.Default
                                  : CacheItemPriority.NotRemovable;
            this.policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(02.00);                               //.AddHours(01.00);     //.AddSeconds(59.00);
            this.policy.RemovedCallback = this.callback;
            //this.policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePath));

            // Add inside cache 
            this.cache.Set(cacheKeyName, cacheItem, this.policy);
        }

        /// <summary>
        /// Get my cached item.
        /// </summary>
        /// <param name="cacheKeyName">
        /// The cache key name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object GetMyCachedItem(string cacheKeyName)
        {
            return this.cache[cacheKeyName] as object;
        }

        /// <summary>
        /// Remove my cached item.
        /// </summary>
        /// <param name="cacheKeyName">
        /// The cache key name.
        /// </param>
        public void RemoveMyCachedItem(string cacheKeyName)
        { 
            if (this.cache.Contains(cacheKeyName))
            {
                this.cache.Remove(cacheKeyName);
            }
        }

        /// <summary>
        /// The count.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long Count()
        {
            return this.cache.GetCount();
        }

        /// <summary>
        /// The my cached item removed callback.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        private void MyCachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            // Log these values from arguments list 
            string strLog = string.Concat("Reason: ", arguments.RemovedReason.ToString(), "  | Key - Name:", arguments.CacheItem.Key, " | Value - Object:", arguments.CacheItem.Value.ToString());
            Console.WriteLine(this.Count().ToString(CultureInfo.InvariantCulture) + " " + strLog);
        }
    }
}
