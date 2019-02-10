using Newtonsoft.Json;

namespace RegistrationForm.IntegrationTests.src.Controllers
{
    public static class SerializationHelper {

        public static string Serialize(this object o) => JsonConvert.SerializeObject(o);

        public static T DeSerialize<T>(this string str) => JsonConvert.DeserializeObject<T>(str);
    }
}