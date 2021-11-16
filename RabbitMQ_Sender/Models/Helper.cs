using Newtonsoft.Json;

namespace RabbitMQ_Sender.Models
{
    public static class Helper<T> where T : class
    {
        public static byte[] ObjectToByteArray(object obj)
        {
            // proper way to serialize object
            var objToString = System.Text.Json.JsonSerializer.Serialize(obj);
            // convert that that to string with ascii you can chose what ever encoding want
            return System.Text.Encoding.ASCII.GetBytes(objToString);
        }

        public static object ByteArrayToObject(byte[] bytes)
        {
            // make sure you use same type what you use chose during conversation
            var stringObj = System.Text.Encoding.ASCII.GetString(bytes);
            // proper way to Deserialize object
            return System.Text.Json.JsonSerializer.Deserialize<object>(stringObj);
        }

        public static T ByteArrayToGeneric(byte[] bytes)
        {
            var data = System.Text.Encoding.ASCII.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
