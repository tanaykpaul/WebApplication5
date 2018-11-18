using System.Web.Http;

namespace Readify.Controllers
{
    public class TokenController : ApiController
    {
        public Response Get()
        {
            const string result = "b8e5cbe3-1dfa-427e-af22-99249b767e5b";
            return HelperMethods.GetResponse(Status.Success, ResponseCode.Ok,
                $"My Token is {result}.",
                new Result
                {
                    Input = null,
                    Output = $"{result}"
                }
                );
        }
    }
}