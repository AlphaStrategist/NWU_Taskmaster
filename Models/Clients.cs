using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NWU_Taskmaster.Models
{
    public class Clients
    {
        [Key]
        public int client_id { get; set; }

        public string username { get; set; }
        public string usersurname { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public DateTime created_at { get; set; }
        public string aspnet_user_id { get; set; }
    }
}