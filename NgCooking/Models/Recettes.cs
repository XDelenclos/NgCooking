//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NgCooking.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recettes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Recettes()
        {
            this.Comment = new HashSet<Comment>();
            this.Ingredient = new HashSet<Ingredient>();
        }
    
        public string id { get; set; }
        public string name { get; set; }
        public Nullable<int> creatorId { get; set; }
        public bool isAvailable { get; set; }
        public string picture { get; set; }
        public Nullable<int> calories { get; set; }
        public string preparation { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual Communaute Communaute { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ingredient> Ingredient { get; set; }
    }
}