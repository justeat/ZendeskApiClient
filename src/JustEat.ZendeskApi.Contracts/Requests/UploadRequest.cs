using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JustEat.ZendeskApi.Contracts.Models;

namespace JustEat.ZendeskApi.Contracts.Requests
{
    public class UploadRequest
    {
        public HttpPostedFileBase Item { get; set; }
        public string Token { get; set; }
    }
}
