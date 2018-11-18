using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Readify.Controllers
{
    public class ReverseWordsController : ApiController
    {
        public Response Get(string sentence)
        {
            //if (string.IsNullOrWhiteSpace(sentence))
            //{
            //    return HelperMethods.GetResponse(Status.Failure,
            //        ResponseCode.Ok,
            //        "Your input is not valid. Please give a valid sentense with valid letters.",
            //        new Result
            //        {
            //            Input = $"{sentence}",
            //            Output = null
            //        }
            //        );
            //}
            try
            {
                var result = GetReverseWords(sentence);
                return HelperMethods.GetResponse(Status.Success, ResponseCode.Ok,
                    $"Reverse the letters of each word of {sentence} is: {result}.",
                    new Result
                    {
                        Input = $"{sentence}",
                        Output = $"{result}"
                    }
                    );
            }
            catch (Exception)
            {
                return HelperMethods.GetResponse(Status.Failure,
                    ResponseCode.BadReq,
                    $"Sorry, your input {sentence} might have any unknown characters!",
                    new Result
                    {
                        Input = $"{sentence}",
                        Output = null
                    }
                    );
            }
        }

        private static string GetReverseWords(string sentense)
        {
            var output = string.Empty;
            if (string.IsNullOrWhiteSpace(sentense)) return sentense;

            const string pattern = @"\w(?<!\d)[\w'-]*";
            var matches = new Regex(pattern).Matches(sentense);

            var lastPosition = 0;
            foreach (var matchObj in matches.Cast<Match>())
            {
                output = $"{output}{sentense.Substring(lastPosition, matchObj.Index - lastPosition)}{Reverse(matchObj.Value)}";
                lastPosition = matchObj.Index + matchObj.Length;
            }
            if (lastPosition < sentense.Length)
            {
                output = $"{output}{sentense.Substring(lastPosition)}";
            }

            return output;
        }

        private static string Reverse(string s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}