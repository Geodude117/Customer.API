using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Business
{
    public static class TaskSummary
    {
        public static bool TaskBoolSummary(bool[] TaskList)
        {
            foreach (var xTask in TaskList)
            {
                if (!xTask)
                    return xTask;
            }
            return true;
        }
    }
}
