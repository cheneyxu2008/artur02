using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PublicSite.Models;

namespace PublicSite
{
    public static class Category
    {
        public const string ComputerScience = "Computer Science";
        public const string SoftwareEngineering = "Software Engineering";
        public const string Programming = "Programming";
        public const string Experience = "Experience";
        public const string Knowledge = "Knowledge";
    }

    public enum CriterionLevel
    {
        [Description("Level 0")] Level0 = 0,
        [Description("Level 1")] Level1 = 1,
        [Description("Level 2")] Level2 = 2,
        [Description("Level 3")] Level3 = 3
    }

    public static class Logic
    {
        public static int Empty()
        {
            using (var context = new SelfEvaluationContainer())
            {
                var a = context.Criteria;

                foreach (var criterion in a)
                {
                    context.DeleteObject(criterion);
                }

                return context.SaveChanges();
            }
        }

        public static int InitDatabase()
        {
            using (var context = new SelfEvaluationContainer())
            {
                if (context.Criteria.Count() == 0)
                {

                    var path = HttpContext.Current.Server.MapPath("~/Data.xml");

                    XElement criteria = XElement.Load(path);
                    XNamespace ns1 = "http://tempuri.org/XMLSchema1.xsd";

                    var a = from c in criteria.Descendants(ns1 + "criterion")
                            select c;

                    foreach (var criterion in a)
                    {
                        var newCriterion = new Criterion()
                                               {
                                                   Name = criterion.Element(ns1 + "name").Value,
                                                   Identifier = criterion.Element(ns1 + "identifier").Value,
                                                   Category = criterion.Element(ns1 + "category").Value,
                                                   Levels = new EntityCollection<Level>()
                                                                {
                                                                    new Level(CriterionLevel.Level0)
                                                                        {
                                                                            Description = criterion.Element(ns1+"level0").Value
                                                                        },
                                                                        new Level(CriterionLevel.Level1)
                                                                        {
                                                                            Description = criterion.Element(ns1+"level1").Value
                                                                        },
                                                                        new Level(CriterionLevel.Level2)
                                                                        {
                                                                            Description = criterion.Element(ns1+"level2").Value
                                                                        },
                                                                        new Level(CriterionLevel.Level3)
                                                                        {
                                                                            Description = criterion.Element(ns1+"level3").Value
                                                                        }
                                                                }
                                               };
                        
                        var comment = criterion.Element(ns1+"comment");
                        if (comment != null)
                            newCriterion.Comment = comment.Value;

                        context.AddToCriteria(newCriterion);
                    }

                    context.SaveChanges();
                }
            }

            return 0;
        }

        public static List<Criterion> GetAllCriteria()
        {
            using (var context = new SelfEvaluationContainer())
            {
                return context.Criteria.Include("Levels").ToList();
            }
        }
    }
}