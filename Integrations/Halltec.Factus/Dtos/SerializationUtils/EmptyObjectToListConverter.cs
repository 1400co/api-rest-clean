using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos.SerializationUtils
{
    public class EmptyObjectToListConverter<TElement> : JsonConverter<List<TElement>>
    {
        public override List<TElement>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null; // Or `new List<TElement>()` if null should also be an empty list.
                
                case JsonTokenType.StartObject:
                    // Consume the entire object, assuming it's empty or its contents should be ignored.
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndObject)
                        {
                            break;
                        }
                        // Skip properties within the object
                        reader.Skip();
                    }
                    return new List<TElement>(); // Treat empty/unexpected object as an empty list.

                case JsonTokenType.StartArray:
                    // Standard deserialization for an array
                    var list = new List<TElement>();
                    var newOptions = new JsonSerializerOptions(options); // Clone options
                    
                    // It's important to remove this converter from the options for inner elements
                    // to prevent infinite recursion if TElement is also List<T> or similar.
                    // However, for List<object>, this is less of a concern unless object itself is List<object>.
                    // A safer way is to deserialize element by element.

                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndArray)
                        {
                            break;
                        }
                        // Deserialize each element in the array.
                        // If TElement is 'object', this will deserialize JSON objects as JsonElement.
                        list.Add(JsonSerializer.Deserialize<TElement>(ref reader, options)!);
                    }
                    return list;
                
                default:
                    throw new JsonException($"Unexpected token {reader.TokenType} when trying to deserialize List<{typeof(TElement).Name}>. Expected StartArray, StartObject, or Null.");
            }
        }

        public override void Write(Utf8JsonWriter writer, List<TElement> value, JsonSerializerOptions options)
        {
            // Use default serialization for writing
            JsonSerializer.Serialize(writer, value, options);
        }
    }
} 