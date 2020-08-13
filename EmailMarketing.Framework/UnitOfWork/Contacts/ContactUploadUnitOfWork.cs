using EmailMarketing.Framework.Context;
using EmailMarketing.Framework.Repositories.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailMarketing.Framework.UnitOfWork.Contacts
{
    public class ContactUploadUnitOfWork : EmailMarketing.Data.UnitOfWork, IContactUploadUnitOfWork
    {
        public IContactRepository ContactRepository { get; set; }
        public IContactUploadRepository ContactUploadRepository { get; set; }
        public IFieldMapRepository FieldMapRepository { get; set; }
        public IContactUploadFieldMapRepository ContactUploadFieldMapRepository { get; set; }
        public IContactValueMapRepository ContactValueMapRepository { get; set; }
        public IContactUploadGroupRepository ContactUploadGroupRepository { get; set; }
        public IContactGroupRepository ContactGroupRepository { get; set; }

        public ContactUploadUnitOfWork(FrameworkContext dbContext,
            IContactRepository contactRepository, 
            IContactUploadRepository contactUploadRepository,
            IFieldMapRepository fieldMapRepository,
            IContactUploadFieldMapRepository contactUploadFieldMapRepository, 
            IContactValueMapRepository contactValueMapRepository,
            IContactUploadGroupRepository contactUploadGroupRepository,
            IContactGroupRepository contactGroupRepository) : base(dbContext)
        {
            this.ContactRepository = contactRepository;
            this.ContactUploadRepository = contactUploadRepository;
            this.FieldMapRepository = fieldMapRepository;
            this.ContactUploadFieldMapRepository = contactUploadFieldMapRepository;
            this.ContactValueMapRepository = contactValueMapRepository;
            this.ContactUploadGroupRepository = contactUploadGroupRepository;
            this.ContactGroupRepository = contactGroupRepository;
        }
    }
}
