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
    
    public partial class C02a_AppResults
    {
        public int ID { get; set; }
        public int AppListID { get; set; }
        public string Result { get; set; }
        public string Description { get; set; }
    
        public virtual C02_AppLists C02_AppLists { get; set; }
    }
}
