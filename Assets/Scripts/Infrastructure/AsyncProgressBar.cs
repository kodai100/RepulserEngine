using System.Linq;
using System.Reflection;

#if UNITY_EDITOR

using UnityEditor;

public static class AsyncProgressBar
{
    private static MethodInfo m_display = null;
    private static MethodInfo m_clear = null;

    static AsyncProgressBar()
    {
        var type = typeof(Editor).Assembly
            .GetTypes()
            .Where(c => c.Name == "AsyncProgressBar")
            .FirstOrDefault()
        ;

        m_display = type.GetMethod("Display");
        m_clear = type.GetMethod("Clear");
    }

    public static void Display(string progressInfo, float progress)
    {
        var parameters = new object[] { progressInfo, progress };
        m_display.Invoke(null, parameters);
    }

    public static void Clear()
    {
        m_clear.Invoke(null, null);
    }
}

#endif