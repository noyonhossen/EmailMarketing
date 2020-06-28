using System;

namespace EmailMarketing.Data
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
