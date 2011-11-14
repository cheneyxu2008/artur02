using System.Linq;
using System.Web.Mvc;
using PublicSite.Models;

namespace PublicSite.Controllers
{
    public class SelfEvalController : Controller
    {
        //
        // GET: /SelfEval/

        public ActionResult Index()
        {
            //using (var context = new SelfEvaluationContext())
            //{
            //    return View(context.Criterias.ToList());
            //}

            Logic.InitDatabase();


            return View(Logic.GetAllCriteria());
        }

    }
}
