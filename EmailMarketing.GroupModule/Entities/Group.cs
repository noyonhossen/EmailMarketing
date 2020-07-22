using EmailMarketing.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailMarketing.GroupModule.Entities
{
    public class Group:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
