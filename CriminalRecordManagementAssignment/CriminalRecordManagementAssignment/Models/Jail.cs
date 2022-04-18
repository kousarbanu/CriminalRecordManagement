//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CriminalRecordManagementAssignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Jail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jail()
        {
            this.Criminals = new HashSet<Criminal>();
        }

        public int Jail_ID { get; set; }
        [DisplayName("Jail"), Required]
        public string Jail_Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required, StringLength(7, MinimumLength = 6, ErrorMessage = "Pincode should be 6 digits")]
        public string Pincode { get; set; }
        [Required, StringLength(30, MinimumLength = 3, ErrorMessage = "Jailer should have atleast 3 chars")]
        public string Jailer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Criminal> Criminals { get; set; }
    }
}
