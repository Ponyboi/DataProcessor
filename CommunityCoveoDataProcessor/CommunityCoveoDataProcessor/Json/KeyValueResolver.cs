using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityCoveoDataProcessor.Json
{
        public class KeyValueResolver : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                List<KeyValuePair<string, string>> list = value as List<KeyValuePair<string, string>>;
                writer.WriteStartArray();
                foreach (var item in list)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName(item.Key);
                    writer.WriteValue(item.Value);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
            }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<KeyValuePair<string, string>>);
        }
    }
}
