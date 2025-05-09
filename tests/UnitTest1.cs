using OpenEXR.Interop;
using static OpenEXR.Interop.exr;

namespace VL.OpenEXR.Tests;

public unsafe class Tests
{
    public Tests()
    {
        Utils.LoadOpenEXRCore();
    }

    [Test]
    public void LoadDwabImage()
    {
        var filePath = Path.Combine(Utils.GetAssetsPath(), "dwab.exr");
        using var ctx = ExrContext.OpenRead(filePath);
        Assert.Pass();
    }
}