using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Enums
{
    public enum ExpenseType
    {
        Project = 1,
        Snack = 2,
        Lunch = 3,
        Transport = 4
    }

    public class ExpenseTypeSelect
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
