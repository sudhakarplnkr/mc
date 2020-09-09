namespace MicroCredential.Infrastructure
{
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    public static class RedisExtensions
    {
        public static T Get<T>(this IDistributedCache cache, string key)
        {
            var value = cache.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public static void Set(this IDistributedCache cache, string key, object value)
        {
            cache.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
