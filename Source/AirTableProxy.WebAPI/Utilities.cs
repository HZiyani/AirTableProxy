using System;

namespace AirTableProxy.WebAPI
{
    public static class Utilities
    {
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}