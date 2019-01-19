﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TimesheetDbContext : DbContext
    {
        public TimesheetDbContext()
            : base("name=TimesheetDbContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C01_DesignProject> C01_DesignProject { get; set; }
        public virtual DbSet<C02_BIMstaff> C02_BIMstaff { get; set; }
        public virtual DbSet<C04_DMC_PMlist> C04_DMC_PMlist { get; set; }
        public virtual DbSet<C05_BIMtarget> C05_BIMtarget { get; set; }
        public virtual DbSet<C05a_TargetDetail> C05a_TargetDetail { get; set; }
        public virtual DbSet<C06_ProjectType> C06_ProjectType { get; set; }
        public virtual DbSet<C06a_ProjectType> C06a_ProjectType { get; set; }
        public virtual DbSet<C07_CTCCompanyList> C07_CTCCompanyList { get; set; }
        public virtual DbSet<C11_OutsourceList> C11_OutsourceList { get; set; }
        public virtual DbSet<C12_SiteList> C12_SiteList { get; set; }
        public virtual DbSet<C13_Location> C13_Location { get; set; }
        public virtual DbSet<C14_Descipline> C14_Descipline { get; set; }
        public virtual DbSet<C15_TimeSheet> C15_TimeSheet { get; set; }
        public virtual DbSet<C16_WorkType> C16_WorkType { get; set; }
        public virtual DbSet<C16a_WorkTypeGroup> C16a_WorkTypeGroup { get; set; }
        public virtual DbSet<C17_CTCDepartment> C17_CTCDepartment { get; set; }
        public virtual DbSet<C18_CourseList> C18_CourseList { get; set; }
        public virtual DbSet<C18_CourseList_Detail> C18_CourseList_Detail { get; set; }
        public virtual DbSet<C19_SubContractList> C19_SubContractList { get; set; }
        public virtual DbSet<C20_StudentList> C20_StudentList { get; set; }
        public virtual DbSet<C21_SubjectList> C21_SubjectList { get; set; }
        public virtual DbSet<C22_CheckList> C22_CheckList { get; set; }
        public virtual DbSet<C23_Hinhthucdaotao> C23_Hinhthucdaotao { get; set; }
        public virtual DbSet<C24_IssueList> C24_IssueList { get; set; }
        public virtual DbSet<C25_BIM_RnDproject> C25_BIM_RnDproject { get; set; }
        public virtual DbSet<C27_Topic> C27_Topic { get; set; }
        public virtual DbSet<C28_NTP> C28_NTP { get; set; }
        public virtual DbSet<C29_MucTieuTraining> C29_MucTieuTraining { get; set; }
        public virtual DbSet<C30_NTP_ProjectDetails> C30_NTP_ProjectDetails { get; set; }
        public virtual DbSet<C31_ProjectPhaseDetails> C31_ProjectPhaseDetails { get; set; }
        public virtual DbSet<C32_ProjectPhase> C32_ProjectPhase { get; set; }
        public virtual DbSet<C33_SolutionOffer> C33_SolutionOffer { get; set; }
        public virtual DbSet<AppVersion> AppVersions { get; set; }
    }
}
