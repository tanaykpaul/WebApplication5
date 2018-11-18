using System;
using System.Web.Http;

namespace Readify.Controllers
{
    public class FibonacciController : ApiController
    {
        public Response Get(string n)
        {
            try
            {
                var input = long.Parse(n);
                
                var result = GetNthFibonacci(input);
                return HelperMethods.GetResponse(Status.Success, ResponseCode.Ok,
                    $"The {n}th fibonacci number is {result}.",
                    new Result
                    {
                        Input = $"{n}",
                        Output = $"{result}"
                    }
                    );
            }
            catch (Exception)
            {
                return HelperMethods.GetResponse(Status.Failure,
                    ResponseCode.BadReq,
                    "The request is invalid.",
                    new Result
                    {
                        Input = $"{n}",
                        Output = null
                    }
                    );
            }
        }

        private static int GetNthFibonacci(long n)
        {
            if (n < 0)
            {
                n = -1*n;
            }

            var firstSeq = 0;
            var secondSeq = 1;

            for (var i = 0; i < n; i++)
            {
                var temp = firstSeq;
                firstSeq = secondSeq;
                secondSeq = temp + secondSeq;
            }

            if(firstSeq < 0)
                throw new Exception();

            return firstSeq;
        }
    }
}