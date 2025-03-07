using System;
using Microsoft.AspNetCore.Identity;

namespace vina.Server;
public class NPTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public NPTokenProviderOptions()
    {
        Name = "NPTokenProvider";
        TokenLifespan = TimeSpan.FromHours(12);
    }
}
