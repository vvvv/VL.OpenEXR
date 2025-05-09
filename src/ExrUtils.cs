using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using static OpenEXR.Interop.exr;

namespace VL.OpenEXR;

internal static class ExrUtils
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    [StackTraceHidden]
    public static void ThrowIfError(this int errorCode)
    {
        if (errorCode != 0)
            throw new ExrException(errorCode);
    }
}

public sealed class ExrException : Exception
{
    private readonly int errorCode;

    public ExrException(int errorCode)
    {
        this.errorCode = errorCode;
    }

    public unsafe override string Message => new string(exr_get_default_error_message(errorCode));
}