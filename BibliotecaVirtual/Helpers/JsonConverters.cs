using System;
using System.Globalization;
using Newtonsoft.Json;
using BibliotecaVirtual.Data.Extensions;

namespace BibliotecaVirtual.Helpers
{
    /// <summary>
    /// Conversores padrões de JSON.
    /// </summary>
    public class JsonConverters
    {
        public static JsonSerializerSettings DefaultSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
            {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy(),
            },
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Error,
            Culture = CultureInfo.CurrentCulture,
            Formatting = Formatting.None,
            DateFormatString = "dd/MM/yyyy HH:mm:ss",
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Converters = new System.Collections.Generic.List<JsonConverter>
            {
                new DecimalConverter(CultureInfo.CurrentCulture),
                new StringConverter(CultureInfo.CurrentCulture),
                new DateTimeConverter(CultureInfo.CurrentCulture),
                new DefaultConverter(),
            }
        };

        public static JsonSerializerSettings DefaultSettingsWithNulls = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
            {
                NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy(),
            },
            NullValueHandling = NullValueHandling.Include,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Culture = CultureInfo.CurrentCulture,
            Formatting = Formatting.None,
            DateFormatString = "dd/MM/yyyy HH:mm:ss",
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Converters = new System.Collections.Generic.List<JsonConverter>
            {
                new DecimalConverter(CultureInfo.CurrentCulture),
                new StringConverter(CultureInfo.CurrentCulture),
                new DateTimeConverter(CultureInfo.CurrentCulture),
                new DefaultConverter(),
            }
        };

        protected class DecimalConverter : JsonConverter
        {
            private CultureInfo culture;

            public DecimalConverter(CultureInfo culture)
            {
                this.culture = culture;
            }

            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(decimal) || objectType == typeof(decimal?) ||
                        objectType == typeof(double) || objectType == typeof(double?) ||
                        objectType == typeof(float) || objectType == typeof(float?));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(Convert.ToString(value, culture));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var value = reader.Value;

                return Convert.ToDecimal(value.ToString(), culture);
            }
        }

        protected class StringConverter : JsonConverter
        {
            private CultureInfo culture;

            public StringConverter()
            {
                this.culture = CultureInfo.CurrentCulture;
            }

            public StringConverter(CultureInfo culture)
            {
                this.culture = culture;
            }

            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(String));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                if (value?.ToString().IsBool() == true)
                {
                    writer.WriteValue(Convert.ToString(value, culture).ToLowerInvariant().AsBool());
                }
                else
                {
                    writer.WriteValue(Convert.ToString(value, culture));
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var value = reader.Value;

                if (value?.ToString().IsBool() == true)
                {
                    return value.ToString();
                }
                else
                {
                    return value;
                }
            }
        }

        protected class DateTimeConverter : JsonConverter
        {
            private CultureInfo culture;

            public DateTimeConverter()
            {
                this.culture = CultureInfo.CurrentCulture;
            }

            public DateTimeConverter(CultureInfo culture)
            {
                this.culture = culture;
            }

            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(DateTime));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var Data = DateTime.Parse(value?.ToString());

                if (Data.TimeOfDay.TotalHours > 0)
                {
                    writer.WriteValue(Convert.ToDateTime(value, culture).ToString());
                }
                else
                {
                    writer.WriteValue(Convert.ToDateTime(value, culture).ToShortDateString());
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                var value = reader.Value;

                if (value?.ToString().IsDateTime() == true)
                {
                    return value.ToString();
                }
                else
                {
                    return value;
                }
            }
        }

        protected class DefaultConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return false;
            }

            public override bool CanRead { get { return false; } }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override bool CanWrite { get { return false; } }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
