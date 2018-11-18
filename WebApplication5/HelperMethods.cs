using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Readify
{
    public static class HelperMethods
    {
        public static Response GetResponse(Status status, ResponseCode code, string message, Result result)
        {
            return new Response
            {
                Code = code,
                Status = status.GetDescription(),
                Message = message,
                Result = result
            };
        }

        private static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (!(e is Status)) return null;
            var type = e.GetType();
            var values = Enum.GetValues(type);

            return (from int val in values
                where val == e.ToInt32(CultureInfo.InvariantCulture)
                select type.GetMember(type.GetEnumName(val))
                into memInfo
                select memInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false)
                    .FirstOrDefault()).OfType<DescriptionAttribute>()
                .Select(descriptionAttribute => descriptionAttribute.Description)
                .FirstOrDefault();
        }
    }
}