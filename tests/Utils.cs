using System.Runtime.InteropServices;

namespace VL.OpenEXR.Tests;

internal sealed class Utils
{
    public static string GetRootPath()
    {
        return Path.GetFullPath(Path.Combine(Path.Combine(typeof(Utils).Assembly.Location, "..", "..", "..", "..", "..")));
    }

    public static string GetAssetsPath()
    {
        return Path.Combine(GetRootPath(), "tests", "assets");
    }

    public static void LoadOpenEXRCore()
    {
        var libraryPath = Path.Combine(GetRootPath(), "runtimes", "win-x64", "native", "OpenEXRCore-3_3.dll");
        NativeLibrary.Load(libraryPath);
    }
}
