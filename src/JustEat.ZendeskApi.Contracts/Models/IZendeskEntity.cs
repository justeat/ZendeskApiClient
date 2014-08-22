using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace JustEat.ZendeskApi.Contracts.Models
{
    public interface IZendeskEntity
    {
        long? Id { get; set; }
}
}