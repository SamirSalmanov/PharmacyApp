//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PharmacyApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Medicine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medicine()
        {
            this.Medicine_To_Tags = new HashSet<Medicine_To_Tags>();
            this.Orders = new HashSet<Order>();
        }
    
        public int M_ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Quantity { get; set; }
        public System.DateTime ProductionDate { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public string Description { get; set; }
        public bool IsReceipt { get; set; }
        public string Barcode { get; set; }
        public int FirmID { get; set; }
    
        public virtual Firm Firm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicine_To_Tags> Medicine_To_Tags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
