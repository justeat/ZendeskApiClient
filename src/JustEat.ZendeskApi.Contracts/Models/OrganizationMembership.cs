﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [Description("Organization Membership")]
    [DataContract]
    public class OrganizationMembership : IZendeskEntity
    {
        [DataMember(EmitDefaultValue = false)]
        public long? Id { get; set; }

        [DataMember(Name = "user_id", EmitDefaultValue = false)]
        public long? UserId { get; set; }

        [DataMember(Name = "organization_id", EmitDefaultValue = false)]
        public long? OrganizationId { get; set; }

        [DataMember(Name = "default", EmitDefaultValue = false)]
        public bool? Default { get; set; }

        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public DateTime? Created { get; set; }

        [DataMember(Name = "updated_at", EmitDefaultValue = false)]
        public DateTime? Updated { get; set; }

    }
}
