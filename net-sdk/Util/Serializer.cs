using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CB.Util
{
    class Serializer
    {
        internal static JObject Serialize(Dictionary<string,Object> value)
        {
            var json = "";
            try
            {
                Dictionary<string, Object> valueToSerialize = new Dictionary<string, Object>();
                valueToSerialize = (Dictionary<string, Object>)value["document"];
                json = JsonConvert.SerializeObject(valueToSerialize, new KeyValuePairConverter());
                Console.WriteLine(json.ToString());
               
            }catch(JsonException e){

                throw new JsonException(e.Message);
            }
            return (JObject)json;

        }

        internal static Dictionary<string,Object> Deserialize(JObject value)
        {
            return null;
        }

        internal static string getString(string value) { 

        
        // JObject object = new JObject();
            return null;
            
           
        
        
        }

    }
}
