using System;

#pragma warning disable CS0219 

namespace Sandbox;

static class CSharp11Sandbox
{
    public static void Sandbox()
    {
        ReadOnlySpan<byte> utf8Literal = "0123456789ABCDEF"u8;
        string rawString = """This is a "raw string literal". It can contain characters like \, ' and ".""";
    }
}

#pragma warning restore CS0219