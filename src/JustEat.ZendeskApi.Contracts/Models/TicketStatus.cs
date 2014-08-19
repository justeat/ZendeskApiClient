﻿using System.ComponentModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [DataContract]
    public enum TicketStatus
    {
        [EnumMember(Value = "new")]
        New,
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "hold")]
        Hold,
        [EnumMember(Value = "solved")]
        Closed,
        [EnumMember(Value = "closed")]
        Solved,

    }
}