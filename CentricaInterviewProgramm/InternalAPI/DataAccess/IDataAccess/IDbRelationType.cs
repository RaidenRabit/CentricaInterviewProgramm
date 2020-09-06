using InternalAPI.Models;
using System.Collections.Generic;

namespace InternalAPI.DataAccess.IDataAccess
{
    public interface IDbRelationType
    {
        RelationType GetRelationTypeById(int relationTypeId);
    }
}
