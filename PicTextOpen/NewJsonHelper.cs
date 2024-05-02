using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PicTextOpen
{
    public class NewJsonHelper<T> where T : class
    {
        public static T Deserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            else
            {
                T person = JsonSerializer.Deserialize<T>(json);
                return person;
            }
        }

        public static string Serialize(T person)
        {
            string json = JsonSerializer.Serialize(person);
            return json;
        }

    }
}
