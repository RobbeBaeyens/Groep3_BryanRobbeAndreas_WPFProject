//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GRP3_Project_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Klant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klant()
        {
            this.Event = new HashSet<Event>();
        }
    
        public int KlantID { get; set; }
        public string Naam { get; set; }
        public string Straat { get; set; }
        public int Huisnr { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public string BTWnummer { get; set; }
        public string Contactnaam { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Event { get; set; }
    }
}
