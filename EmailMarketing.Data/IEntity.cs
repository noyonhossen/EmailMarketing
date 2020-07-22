using System;

namespace EmailMarketing.Data
{
    public abstract class IEntity<Tkey>
    {
        public Tkey Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public IEntity()
        {
            this.IsDeleted = false;
            this.IsActive = true;
        }
    }
}
