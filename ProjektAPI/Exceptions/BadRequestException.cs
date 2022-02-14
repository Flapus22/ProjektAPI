using System.Runtime.Serialization;

namespace ProjektAPI.Exceptions
{
    [Serializable]
    internal class BadRequestException : Exception
    {
        public BadRequestException(string message):base(message)
        {

        }

    }
}