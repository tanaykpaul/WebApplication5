using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Readify.Controllers
{
    public class TriangleTypeController : ApiController
    {
        public Response Get(string a, string b, string c)
        {
            try
            {
                if (int.Parse(a) <= 0 || int.Parse(b) <= 0 || int.Parse(c) <= 0)
                {
                    return HelperMethods.GetResponse(Status.Failure,
                        ResponseCode.Ok,
                        $"Your inputs are not valid. Please give all positive integers.",
                        new Result
                        {
                            Input = $"{a}, {b}, {c}",
                            Output = "Error"
                        }
                        );
                }
            
                var result = GetTriangleType(int.Parse(a), int.Parse(b), int.Parse(c));
                return HelperMethods.GetResponse(Status.Success, ResponseCode.Ok,
                    $"Side values - {a}, {b}, {c} suggest it is a {result}.",
                    new Result
                    {
                        Input = $"{a}, {b}, {c}",
                        Output = $"{result}"
                    }
                    );
            }
            catch (Exception)
            {
                return HelperMethods.GetResponse(Status.Failure,
                    ResponseCode.BadReq,
                    $"The request is invalid.",
                    new Result
                    {
                        Input = $"{a}, {b}, {c}",
                        Output = null
                    }
                    );
            }
        }

        private static string GetTriangleType(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
            {
                return "Error";
            }

            var values = new[] { a, b, c };

            if (AllThreeSidesAreEqual(values))
            {
                return "Equilateral";
            }

            if (TwoSidesAreEqual(values))
            {
                return "Isosceles";
            }

            return NoSidesAreEqual(values) ? "Scalene" : "Error";
        }

        private static bool AllThreeSidesAreEqual(IEnumerable<int> values)
        {
            return values.Distinct().Count() == 1;
        }

        private static bool TwoSidesAreEqual(IEnumerable<int> values)
        {
            return values.Distinct().Count() == 2;
        }

        private static bool NoSidesAreEqual(IEnumerable<int> values)
        {
            return values.Distinct().Count() == 3;
        }
    }
}