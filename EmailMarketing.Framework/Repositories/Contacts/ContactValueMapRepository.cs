using EmailMarketing.Data;
using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.Repositories.Contacts
{
    public class ContactValueMapRepository : Repository<ContactValueMap, int, FrameworkContext>, IContactValueMapRepository
    {
        public ContactValueMapRepository(FrameworkContext dbContext)
            : base(dbContext)
        {

        }
    }
}
