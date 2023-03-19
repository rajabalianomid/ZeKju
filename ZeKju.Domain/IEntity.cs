using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeKju.Domain
{
    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; }
    }

    public interface IEntity
    {
    }
}
