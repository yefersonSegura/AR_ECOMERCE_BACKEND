using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AR.Repository.Base
{
    public abstract class EntityRepository: BaseRepository
    {
        protected EntityRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
