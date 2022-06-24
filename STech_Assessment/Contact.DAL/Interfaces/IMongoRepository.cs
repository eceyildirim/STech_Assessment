using MongoDB.Driver;
using Report.Core.Responses;
using Report.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Report.DAL.Interfaces
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        RepositoryResponse<TDocument> InsertOne(TDocument document);
        RepositoryResponse<TDocument> ReplaceOne(TDocument document);
        RepositoryResponse DeleteById(string id);
        RepositoryResponse<TDocument> FindById(string id);
        RepositoryResponse<List<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression, int page = 1, int pageSize = 100);
        IAggregateFluent<TDocument> Aggregate(AggregateOptions options = null);

    }
}
