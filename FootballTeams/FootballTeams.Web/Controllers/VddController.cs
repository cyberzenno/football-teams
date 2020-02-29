using System.Web.Mvc;

namespace FootballTeams.Web.Controllers
{
    public class VddController : BaseController
    {
        public FileResult Map(int id = 0)
        {
            var fileVddMapFileName = "FootballTeams_VDD_Map.pdf";

            var vddDiagramPath = Server.MapPath("~/App_Data/" + fileVddMapFileName);

            //note: this makes the file to be viewed first on the browser
            Response.AppendHeader("Content-Disposition", "inline; filename=" + fileVddMapFileName);

            return File(vddDiagramPath, "application/pdf");
        }
    }
}