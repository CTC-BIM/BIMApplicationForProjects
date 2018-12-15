using System;
using System.Text;

namespace BIMApplicationForProjects.Models
{
    public static class ChangeLog
    {
        public static ProjectsDbContext db = new ProjectsDbContext();

        /// <summary>
        /// Hàm ghi Log cho App của dự án
        /// </summary>
        /// <param name="enity">C03_ProjectApp</param>
        /// <param name="userEditName">Current Login username</param>
        /// <returns></returns>
        public static string WriteAppLog(C03_ProjectAppDetails enity, string userEditName, string type)
        {
            string kq = "false";
            if (enity == null) return kq = "No change";
            try
            {
                StringBuilder noidung = new StringBuilder();
                //Get Content
                noidung.Append(" - AppCode - ");
                noidung.Append(enity.AppCode);
                noidung.Append(" - DateRequest - ");
                noidung.Append(enity.DateRequest);
                noidung.Append(" - Deadline - ");
                noidung.Append(enity.DeadLine);
                noidung.Append(" - OtherRequest - ");
                noidung.Append(enity.OtherRequest);
                noidung.Append(" - BIMerName - ");
                noidung.Append(enity.BIMerName);
                noidung.Append(" - StatusID - ");
                noidung.Append(enity.StatusID);
                noidung.Append(" - ResultID - ");
                noidung.Append(enity.ResultID);
                noidung.Append(" - RequestID - ");
                noidung.Append(enity.RequestID);
                noidung.Append(" - Resource - ");
                noidung.Append(enity.Resource);
                //Write Log
                C99_History history = new C99_History();
                history.ProjectID = enity.ProjectID;
                history.AppID = enity.AppID;
                history.Thoigian = DateTime.Now;
                history.UserChange = userEditName;
                history.Type = type;
                history.NoiDung = noidung.ToString();

                db.C99_History.Add(history);
                db.SaveChanges();

                return kq = "OK";
            }
            catch (Exception ex)
            {
                return kq + ex.Message;
            }
        }
        public static string WriteProjectLog(C01_Projects enity, string userEditName, string type)
        {
            string kq = "false";
            if (enity == null) return kq = "No change";
            try
            {
                StringBuilder noidung = new StringBuilder();
                //Get Content
                noidung.Append(" - ProjectName - ");
                noidung.Append(enity.ProjectName);
                noidung.Append(" - ARCdesigner - ");
                noidung.Append(enity.ARCdesigner);
                noidung.Append(" - STRdesigner - ");
                noidung.Append(enity.STRdesigner);
                noidung.Append(" - MEPdesigner - ");
                noidung.Append(enity.MEPdesigner);
                noidung.Append(" - Civildesigner - ");
                noidung.Append(enity.CIvilDesigner);
                noidung.Append(" - LandscapeDesigner - ");
                noidung.Append(enity.LandscapeDesigner);
                noidung.Append(" - Phase - ");
                noidung.Append(enity.Phase);
                noidung.Append(" - BIMtarget - ");
                noidung.Append(enity.BIMtarget);
                noidung.Append(" - PMname - ");
                noidung.Append(enity.PMname);
                noidung.Append(" - ARCmodel - ");
                noidung.Append(enity.ARCmakeModel);
                noidung.Append(" - STRmodel - ");
                noidung.Append(enity.STRmakeModel);
                noidung.Append(" - MEPmodel - ");
                noidung.Append(enity.MEPmakeModel);
                noidung.Append(" - CIVILmodel - ");
                noidung.Append(enity.CivilMakeModel);
                noidung.Append(" - LANDSCAPEmodel - ");
                noidung.Append(enity.LandscapeMakeModel);
                noidung.Append(" - ARCmodelpercent - ");
                noidung.Append(enity.ARCmodelUsingPercent);
                noidung.Append(" - STRmodelpercent - ");
                noidung.Append(enity.STRmodelUsingPercent);
                noidung.Append(" - MEPmodelpercent - ");
                noidung.Append(enity.MEPmodelUsingPercent);
                noidung.Append(" - CIVILmodelpercent - ");
                noidung.Append(enity.CIVILmodelUsingPercent);
                noidung.Append(" - LANDSCAPEmodelpercent - ");
                noidung.Append(enity.LANDSmodelUsingPercent);

                //Write Log
                C99_History history = new C99_History();
                history.ProjectID = enity.ProjectID;
                history.AppID = 99;
                history.Thoigian = DateTime.Now;
                history.UserChange = userEditName;
                history.Type = "Project Info changed";
                history.NoiDung = noidung.ToString();

                db.C99_History.Add(history);
                db.SaveChanges();

                return kq = "OK";
            }
            catch (Exception ex)
            {
                return kq + ex.Message;
            }
        }

    }

}