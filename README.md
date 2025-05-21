# VL.OpenEXR

[OpenEXR](https://www.openexr.com/) (.exr) and Radiance HDR (.hdr) image loader and .exr file writer.

Uses [OpenEXRCoreAPI](https://openexr.com/en/latest/OpenEXRCoreAPI.html) for reading and writing of .exr files and [radiant](https://docs.rs/radiant/latest/radiant/) for reading .hdr files.

For use with vvvv, the visual live-programming environment for .NET: http://vvvv.org

## Getting started
- Install as [described here](https://thegraybook.vvvv.org/reference/hde/managing-nugets.html) via commandline:

    `nuget install VL.OpenEXR`

- Usage examples and more information are included in the pack and can be found via the [Help Browser](https://thegraybook.vvvv.org/reference/hde/findinghelp.html)

## Useful tools
- [OpenEXR Viewer](https://github.com/afichet/openexr-viewer)

## Contributing
- Report issues on [the vvvv forum](https://forum.vvvv.org/c/vvvv-gamma/28)
- For custom development requests, please [get in touch](mailto:devvvvs@vvvv.org)
- When making a pull-request, please make sure to read the general [guidelines on contributing to vvvv libraries](https://thegraybook.vvvv.org/reference/extending/contributing.html)

## Development
- The OpenEXRCore interop code is generated with [ClangSharpPInvokeGenerator](https://github.com/dotnet/clangsharp/?tab=readme-ov-file#generating-bindings) (`dotnet ClangSharpPInvokeGenerator @generate.rsp`)
- The OpenEXRCore binaries are from [vcpk](https://openexr.com/en/latest/install.html#windows), current version is 3.3.2 (`vcpkg install openexr`)

## Credits
Development of this library was partially sponsored by:  
* [Refik Anadol Studio](https://refikanadolstudio.com/)

Initial development by:
* [@torinos-yt](https://github.com/torinos-yt)
