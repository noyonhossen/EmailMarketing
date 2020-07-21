using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.Framework.Entities
{
    public class Group:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
