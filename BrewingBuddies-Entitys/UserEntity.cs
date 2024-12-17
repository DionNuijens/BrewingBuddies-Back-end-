using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewingBuddies_Entitys
{
    public class UserEntity : BaseEntity
    {
        public string? email {  get; set; }
        public string? naam { get; set; }
        public string? hash {  get; set; }

    }
}
