using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace JustEat.ZendeskApi.Contracts.Models
{
    [DataContract]
    public class Ticket
    {
        // ReSharper disable InconsistentNaming
        [DataMember]
        public long Id { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime Created { get; set; }

        [DataMember(Name = "updated_at")]
        public DateTime? Updated { get; set; }

        [DataMember(Name = "due_at")]
        public DateTime? Due { get; set; }

        [DataMember]
        public TicketType? Type { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public TicketStatus Status { get; set; }

        [DataMember(Name = "requester_id")]
        public long RequesterId { get; set; }

        [DataMember(Name = "submitter_id")]
        public long SubmitterId { get; set; }

        [DataMember(Name = "assignee_id")]
        public long? AssigneeId { get; set; }

        [DataMember(Name = "organization_id")]
        public long? OrganisationId { get; set; }

        [DataMember]
        public Uri Url { get; set; }

        [DataMember]
        public string Priority { get; set; }

        [DataMember]
        public object recipient { get; set; }

        [DataMember(Name = "group_id")]
        public int GroupId { get; set; }

        [IgnoreDataMember]
        public long External_Id { get; set; }

        [IgnoreDataMember]
        public object via { get; set; }

        [IgnoreDataMember]
        public List<long> collaborator_ids { get; set; }

        [IgnoreDataMember]
        public long forum_topic_id { get; set; }

        [IgnoreDataMember]
        public long problem_id { get; set; }

        [IgnoreDataMember]
        public bool has_incidents { get; set; }

        [IgnoreDataMember]
        public List<string> tags { get; set; }

        [IgnoreDataMember]
        public List<object> custom_fields { get; set; }

        [IgnoreDataMember]
        public object satisfaction_rating { get; set; }

        [IgnoreDataMember]
        public List<object> sharing_agreement_ids { get; set; }

        [IgnoreDataMember]
        public List<object> fields { get; set; }

        [IgnoreDataMember]
        public int? ticket_form_id { get; set; }

        [IgnoreDataMember]
        public string result_type { get; set; }

        [IgnoreDataMember]
        public List<long> followup_ids { get; set; }
        // ReSharper enable InconsistentNaming
    }
}
