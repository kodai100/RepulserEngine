
namespace ProjectBlue.RepulserEngine
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T instance;

        public static T Instance => instance ?? (instance = new T());
    }
}
