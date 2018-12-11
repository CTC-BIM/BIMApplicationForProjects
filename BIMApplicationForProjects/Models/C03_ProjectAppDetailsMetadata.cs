using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BIMApplicationForProjects.Models
{
    [MetadataType(typeof(C03_ProjectAppDetails.C03_ProjectAppDetailMetaData))]
    public partial class C03_ProjectAppDetails
    {
        internal class C03_ProjectAppDetailMetaData
        {
            private C03_ProjectAppDetailMetaData() { }

            [Display(Name = "Ngày Yêu cầu")]
            //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd")]
            [DataType(DataType.Date)]
            public DateTime DateRequest;

        }
    }
}