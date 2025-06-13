// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright 2025, MavFi Foundation and the MavFiFoundation.SourceGenerators contributors

namespace MavFiFoundation.SourceGenerators.IntegrationTests;

public static class Helpers
{

#if NET481
    public static bool ShouldSkipOnUnSupportedPlatforms()
    {
        var skip = false;

        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
            case PlatformID.MacOSX:
                skip = true;
                break;
        }

        return skip;
    }
#endif

}
