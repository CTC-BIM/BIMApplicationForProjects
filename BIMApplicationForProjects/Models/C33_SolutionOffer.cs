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
    
    public partial class C33_SolutionOffer
    {
        public string IssueCode { get; set; }
        public string ProjectID { get; set; }
        public string ConstructorName { get; set; }
        public string SolutionOffer { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }
        public string Status { get; set; }
        public int ID { get; set; }
    
        public virtual C24_IssueList C24_IssueList { get; set; }
    }
}