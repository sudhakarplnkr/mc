namespace MicroCredential.Infrastructure
{
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System;

    public static class RedisExtensions
    {
        public static T Get<T>(this IDatabase cache, string key)
        {
            var value = cache.StringGet(key);
            if (value.IsNull)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public static void Set(this IDatabase cache, string key, object value, TimeSpan experation)
        {
            cache.StringSet(key, JsonConvert.SerializeObject(value), experation);
        }
    }
}
