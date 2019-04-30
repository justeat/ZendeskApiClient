using System;

namespace ZendeskApi.Client.Models
{
    public interface IZendeskEntity
    {
        long Id { get; set; }
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
