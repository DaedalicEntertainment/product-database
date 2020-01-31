using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Daedalic.ProductDatabase.Insights
{
    public class InsightsService
    {
        private readonly List<IInsightCheck> checks = new List<IInsightCheck>();

        public List<IInsightCheck> GetChecks()
        {
            if (checks.Count == 0)
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        if (!type.IsInterface && typeof(IInsightCheck).IsAssignableFrom(type))
                        {
                            IInsightCheck check = (IInsightCheck)Activator.CreateInstance(type);
                            checks.Add(check);
                        }
                    }
                }
            }

            return checks;
        }
    }
}
