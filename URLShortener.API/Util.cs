using System.Text;

namespace URLShortener.API
{
    public static class Util
    {

        private const string Letters = "ABCDEFGHIJKLMNPQRSTUVWXY";
        private const string Numbers = "0123456789";
        private static string _pool = $"{Letters}{Numbers}";
        private static Random _random = new();
        private static readonly object ThreadLock = new();
        public static string GetShortCode(int length = 5)
        {
 
            var poolBuilder = new StringBuilder(_pool);
            var pool = poolBuilder.ToString();
            var output = new char[length];
            for (var i = 0; i < length; i++)
                lock (ThreadLock)
                {
                    var charIndex = _random.Next(0, pool.Length);
                    output[i] = pool[charIndex];
                }
 
            return new string(output);
        }
    
        
    }
}
