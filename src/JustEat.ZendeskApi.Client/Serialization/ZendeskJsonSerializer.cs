using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Client.Serialization
{
    public class ZendeskJsonSerializer : ISerializer
    {
        public T Deserialize<T>(string data)
        {
            List<string> errors;
            var jsonSettings = GetJsonSettings(out errors);
            var obj = JsonConvert.DeserializeObject<T>(data, jsonSettings);
            if (errors.Count <= 0)
                return obj;
            throw new JsonSerializationException(string.Format(CultureInfo.InvariantCulture, "Error deserializing type '{0}', see inner exception for more details", new object[]
            {
                typeof (T)
            }), new JsonSerializationException(new AggregateException(errors.Select(x => new JsonSerializationException(x)).ToList()).ToString()));
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        private static JsonSerializerSettings GetJsonSettings(out List<string> errors)
        {
            var errList = new List<string>();
            var serializerSettings = new JsonSerializerSettings()
            {
                Error = (sender, args) =>
                {
                    if (args.CurrentObject != args.ErrorContext.OriginalObject)
                        return;
                    errList.Add(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                },
                Converters =
                {
                    new StringEnumConverter { AllowIntegerValues = false, CamelCaseText = false }
                },
                
            };
            errors = errList;
            return serializerSettings;
        }
    }
}