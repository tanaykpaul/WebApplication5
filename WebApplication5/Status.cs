using System.ComponentModel;

namespace Readify
{
    public enum Status
    {
        [Description("OK")] Success,
        [Description("Bad Request")] Failure
    }
}