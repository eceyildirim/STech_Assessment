using MongoDB.Driver;
using PhoneDirectory.Core.Responses;
using PhoneDirectory.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectory.DAL.Interfaces
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        RepositoryResponse<TDocument> InsertOne(TDocument document);
        RepositoryResponse<TDocument> ReplaceOne(TDocument document);
        RepositoryResponse DeleteById(string id);
        RepositoryResponse<TDocument> FindById(string id);
        RepositoryResponse<List<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression, int page = 1, int pageSize = 100);
        IAggregateFluent<TDocument> Aggregate(AggregateOptions options = null);
        RepositoryResponse<TDocument> FindOne(Expression<Func<TDocument, bool>> filterExpression);
    }
}
