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
    public interface IMongoRepository
    {
        public interface IMongoRepository<TDocument> where TDocument : IDocument
        {
            RepositoryResponse<TDocument> InsertOne(TDocument document);

            Task<RepositoryResponse<TDocument>> InsertOneAsync(TDocument document);

            RepositoryResponse<List<TDocument>> InsertMany(ICollection<TDocument> documents);

            Task<RepositoryResponse<List<TDocument>>> InsertManyAsync(ICollection<TDocument> documents);

            RepositoryResponse<TDocument> ReplaceOne(TDocument document);

            Task<RepositoryResponse<TDocument>> ReplaceOneAsync(TDocument document);

            RepositoryResponse<TDocument> GetDataCount(Expression<Func<TDocument, bool>> filterExpression);

            RepositoryResponse DeleteById(string id);

            RepositoryResponse DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

            RepositoryResponse DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

            Task<RepositoryResponse> DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);

            RepositoryResponse<TDocument> FindOne(Expression<Func<TDocument, bool>> filterExpression);

            RepositoryResponse<TDocument> FindById(string id);

            RepositoryResponse<List<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression, int page = 1, int pageSize = 100);

            RepositoryResponse<List<TDocument>> FilterByAsQueryable(Expression<Func<TDocument, bool>> filterExpression, int page = 1, int pageSize = 100);

            IAggregateFluent<TDocument> Aggregate(AggregateOptions options = null);

            IMongoCollection<TDocument> GetCollection();

            RepositoryResponse<List<TDocument>> Searching(List<string> searchingCols, string value);
        }
    }
}
