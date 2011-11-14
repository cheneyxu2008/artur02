using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Linq;
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
        public static int InitDatabase()
        {
            using (var context = new SelfEvaluationContainer())
            {
                if (context.Criteria.Count() == 0)
                {
                    var dataStructureCriterion = new Criterion()
                                                     {
                                                         Name = "Data Structures",
                                                         Identifier = "datastruct",
                                                         Category = Category.ComputerScience,
                                                         Levels = new EntityCollection<Level>()
                                                                      {
                                                                          new Level(CriterionLevel.Level0)
                                                                              {
                                                                                  Description =
                                                                                      "Doesn't know the difference between Array and LinkedList."
                                                                              },
                                                                          new Level(CriterionLevel.Level1)
                                                                              {
                                                                                  Description =
                                                                                      "Able to explain and use Arrays, LinkedLists, Dictionaries etc in practical programming tasks"
                                                                              },
                                                                          new Level(CriterionLevel.Level2)
                                                                              {
                                                                                  Description =
                                                                                      "Knows space and time tradeoffs of the basic data structures, Arrays vs LinkedLists, Able to explain how hashtables can be implemented and can handle collisions, Priority queues and ways to implement them etc."
                                                                              },
                                                                          new Level(CriterionLevel.Level3)
                                                                              {
                                                                                  Description =
                                                                                      "Knowledge of advanced data structures like B-trees, binomial and fibonacci heaps, AVL/Red Black trees, Splay Trees, Skip Lists, tries etc."
                                                                              }
                                                                      }
                                                     };
                    context.AddToCriteria(dataStructureCriterion);


                    var algorithmsCriterion = new Criterion()
                                                  {
                                                      Name = "Algorithms",
                                                      Identifier = "alg",
                                                      Category = Category.ComputerScience,
                                                      Levels = new EntityCollection<Level>()
                                                                   {
                                                                       new Level(CriterionLevel.Level0)
                                                                           {
                                                                               Description =
                                                                                   "Unable to find the average of numbers in an array (It's hard to believe but I've interviewed such candidates)."
                                                                           },
                                                                       new Level(CriterionLevel.Level1)
                                                                           {
                                                                               Description =
                                                                                   "Basic sorting, searching and data structure traversal and retrieval algorithms"
                                                                           },
                                                                       new Level(CriterionLevel.Level2)
                                                                           {
                                                                               Description =
                                                                                   "Tree, Graph, simple greedy and divide and conquer algorithms, is able to understand the relevance of the levels of this matrix."
                                                                           },
                                                                       new Level(CriterionLevel.Level3)
                                                                           {
                                                                               Description =
                                                                                   "Able to recognize and code dynamic programming solutions, good knowledge of graph algorithms, good knowledge of numerical computation algorithms, able to identify NP problems etc."
                                                                           }
                                                                   },
                                                      Comment =
                                                          "Working with someone who has a good topcoder ranking would be an unbelievable piece of luck!"
                                                  };
                    context.AddToCriteria(algorithmsCriterion);
                    var systemProgrammingCriterion = new Criterion()
                                                         {
                                                             Name = "System Programming",
                                                             Identifier = "sysprog",
                                                             Category = Category.ComputerScience,
                                                             Levels = new EntityCollection<Level>()
                                                                          {
                                                                              new Level(CriterionLevel.Level0)
                                                                                  {
                                                                                      Description =
                                                                                          "Doesn't know what a compiler, linker or interpreter is"
                                                                                  },
                                                                              new Level(CriterionLevel.Level1)
                                                                                  {
                                                                                      Description =
                                                                                          "Basic understanding of compilers, linker and interpreters. Understands what assembly code is and how things work at the hardware level. Some knowledge of virtual memory and paging."
                                                                                  },
                                                                              new Level(CriterionLevel.Level2)
                                                                                  {
                                                                                      Description =
                                                                                          "Understands kernel mode vs. user mode, multi-threading, synchronization primitives and how they're implemented, able to read assembly code. Understands how networks work, understanding of network protocols and socket level programming."
                                                                                  },
                                                                              new Level(CriterionLevel.Level3)
                                                                                  {
                                                                                      Description =
                                                                                          "Understands the entire programming stack, hardware (CPU + Memory + Cache + Interrupts + microcode), binary code, assembly, static and dynamic linking, compilation, interpretation, JIT compilation, garbage collection, heap, stack, memory addressing..."
                                                                                  }
                                                                          }
                                                         };
                    context.AddToCriteria(systemProgrammingCriterion);

                    var versionControlCriterion = new Criterion()
                                                      {
                                                          Name = "Source Code Version Control",
                                                          Identifier = "scvc",
                                                          Category = Category.SoftwareEngineering,
                                                          Levels = new EntityCollection<Level>()
                                                                       {
                                                                           new Level(CriterionLevel.Level0)
                                                                               {
                                                                                   Description =
                                                                                       "Folder backups by date"
                                                                               },
                                                                           new Level(CriterionLevel.Level1)
                                                                               {
                                                                                   Description =
                                                                                       "VSS and beginning CVS/SVN user"
                                                                               },
                                                                           new Level(CriterionLevel.Level2)
                                                                               {
                                                                                   Description =
                                                                                       "Proficient in using CVS and SVN features. Knows how to branch and merge, use patches setup repository properties etc."
                                                                               },
                                                                           new Level(CriterionLevel.Level3)
                                                                               {
                                                                                   Description =
                                                                                       "Knowledge of distributed VCS systems. Has tried out Bzr/Mercurial/Darcs/Git"
                                                                               }
                                                                       }
                                                      };
                    context.AddToCriteria(versionControlCriterion);
                    var buildAutomationCriterion = new Criterion()
                                                       {
                                                           Name = "Build Automation",
                                                           Identifier = "build",
                                                           Category = Category.SoftwareEngineering,
                                                           Levels = new EntityCollection<Level>()
                                                                        {
                                                                            new Level(CriterionLevel.Level0)
                                                                                {
                                                                                    Description =
                                                                                        "Only knows how to build from IDE"
                                                                                },
                                                                            new Level(CriterionLevel.Level1)
                                                                                {
                                                                                    Description =
                                                                                        "Knows how to build the system from the command line"
                                                                                },
                                                                            new Level(CriterionLevel.Level2)
                                                                                {
                                                                                    Description =
                                                                                        "Can setup a script to build the basic system"
                                                                                },
                                                                            new Level(CriterionLevel.Level3)
                                                                                {
                                                                                    Description =
                                                                                        "Can setup a script to build the system and also documentation, installers, generate release notes and tag the code in source control"
                                                                                }
                                                                        }
                                                       };
                    context.AddToCriteria(buildAutomationCriterion);
                    var automatedTestingCriterion = new Criterion()
                                                        {
                                                            Name = "Automated Testing",
                                                            Identifier = "test",
                                                            Category = Category.SoftwareEngineering,
                                                            Levels = new EntityCollection<Level>()
                                                                         {
                                                                             new Level(CriterionLevel.Level0)
                                                                                 {
                                                                                     Description =
                                                                                         "Thinks that all testing is the job of the tester"
                                                                                 },
                                                                             new Level(CriterionLevel.Level1)
                                                                                 {
                                                                                     Description =
                                                                                         "Has written automated unit tests and comes up with good unit test cases for the code that is being written"
                                                                                 },
                                                                             new Level(CriterionLevel.Level2)
                                                                                 {
                                                                                     Description =
                                                                                         "Has written code in TDD manner"
                                                                                 },
                                                                             new Level(CriterionLevel.Level3)
                                                                                 {
                                                                                     Description =
                                                                                         "Understands and is able to setup automated functional, load/performance and UI tests"
                                                                                 }
                                                                         }
                                                        };
                    context.AddToCriteria(automatedTestingCriterion);

                    return context.SaveChanges();
                }

                return 0;
            }
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