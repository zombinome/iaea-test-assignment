using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SkiRental.Models
{
    public class RequestResult<TValue>
    {
        public bool Ok { get; set; } = true;

        [JsonProperty("result", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TValue Result { get; set; }

        public static JsonResult Success(TValue result)
        {
            return new JsonResult(new RequestResult<TValue> { Ok = true, Result = result });
        }
    }
}
