using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CriminalRecordManagementAssignment.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }
    public class UserMetaData
    {
        [Remote("IsUserNameAvailable", "Users", HttpMethod = "POST", ErrorMessage = "User already exist")]

        public string User_Name { get; set; }
    }
}