
using System;


namespace Common
{
    public interface IBaseEntity
    {
        Guid ID { get; set; }
        string Name { get; set; }
    }
}