//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIMApplicationForProjects.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class C06a_ProjectType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C06a_ProjectType()
        {
            this.C01_DesignProject = new HashSet<C01_DesignProject>();
        }
    
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C01_DesignProject> C01_DesignProject { get; set; }
    }
}
