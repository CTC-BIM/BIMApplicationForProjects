using System;
using System.ComponentModel.DataAnnotations;

namespace BIMApplicationForProjects.Models
{
    #region C01_Projects
    [MetadataType(typeof(C01_Projects.C01_ProjectsMetadata))]
    public partial class C01_Projects
    {
        internal class C01_ProjectsMetadata
        {
            private C01_ProjectsMetadata() { }

            [Required(ErrorMessage = "Không để trống | Not blank")]
            public string ProjectID;

            [Required(ErrorMessage = "Không để trống | Not blank")]
            public string ProjectName;

            [Display(Name = "Kiến trúc|ARC")]
            public string ARCdesigner;

            [Display(Name = "Include Model")]
            [UIHint("MyBool")]
            public bool ARCmakeModel;

            [Display(Name = "Kết cấu|STR")]
            public string STRdesigner;

            [Display(Name = "Include Model")]
            [UIHint("MyBool")]
            public bool STRmakeModel;



        }
    }

    #endregion
    #region C02_AppLists
    [MetadataType(typeof(C02_AppLists.C02_AppListsMetadata))]
    public partial class C02_AppLists
    {
        internal class C02_AppListsMetadata
        {
            private C02_AppListsMetadata() { }

            [Display(Name = "Tên ứng dụng | Aplication Name")]
            public string Name;

            [Display(Name = "Chú thích | Description")]
            public string Description;

            [Display(Name = "Giai đoạn dự án | Project Phase")]
            public Nullable<int> ProjectPhase;

        }

    }
    #endregion

    #region C02a_AppResult
    [MetadataType(typeof(C02a_AppResults.C02a_AppResultsMetadata))]
    public partial class C02a_AppResults
    {
        internal class C02a_AppResultsMetadata
        {
            private C02a_AppResultsMetadata() { }

        }
    }
    #endregion

    #region C02b_AppWorkDetail
    [MetadataType(typeof(C02b_AppWorkDetails.C02b_AppWorkDetailsMetadata))]
    public partial class C02b_AppWorkDetails
    {
        internal class C02b_AppWorkDetailsMetadata
        {
            private C02b_AppWorkDetailsMetadata() { }

        }
    }
    #endregion

    #region C03_ProjectAppDetails
    [MetadataType(typeof(C03_ProjectAppDetails.C03_ProjectAppDetailsMetadata))]
    public partial class C03_ProjectAppDetails
    {
        internal class C03_ProjectAppDetailsMetadata
        {
            private C03_ProjectAppDetailsMetadata() { }

            [Display(Name = "Ngày Yêu cầu")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd")]
            [DataType(DataType.Date)]
            public DateTime DateRequest;

            [Display(Name = "Ngày hoàn thành")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd")]
            [DataType(DataType.Date)]
            public DateTime DeadLine;

            [DataType(DataType.MultilineText)]
            public string OtherRequest;

            [DataType(DataType.MultilineText)]
            public string Resource;
        }
    }
    #endregion


}