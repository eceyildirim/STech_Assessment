using MongoDB.Driver;
using PhoneDirectory.DAL.Interfaces;
using PhoneDirectory.Entity.Base;
using Report.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static PhoneDirectory.DAL.Interfaces.IMongoRepository;

namespace PhoneDirectory.DAL.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        public MongoRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }
        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public RepositoryResponse DeleteById(string id)
        {
            var response = new RepositoryResponse { };

            try
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.UUID, id);
                _collection.FindOneAndDelete(filter);
            }
            catch (Exception ex)
            {
                response.Successed = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public RepositoryResponse<List<TDocument>> FilterBy(Expression<Func<TDocument, bool>> filterExpression, int page = 1, int pageSize = 100)
        {
            var response = new RepositoryResponse<List<TDocument>> { };

            try
            {
                response.Result = _collection.Find(filterExpression)
                    .Skip((page - 1) * pageSize)
                    .Limit(pageSize)
                    .ToList();
            }
            catch (Exception ex)
            {
                response.Successed = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public RepositoryResponse<TDocument> FindById(string id)
        {
            var response = new RepositoryResponse<TDocument> { };

            try
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.UUID, id);
                response.Result = _collection.Find(filter).SingleOrDefault();
            }
            catch (Exception ex)
            {
                response.Successed = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public RepositoryResponse<TDocument> InsertOne(TDocument document)
        {
            var response = new RepositoryResponse<TDocument> { };

            try
            {
                _collection.InsertOne(document);
                response.Result = document;
            }
            catch (Exception ex)
            {
                response.Successed = false;
                response.Message = ex.Message;
            }

            return response;
        }
        public RepositoryResponse<TDocument> ReplaceOne(TDocument document)
        {
            var response = new RepositoryResponse<TDocument> { };
            try
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.UUID, document.UUID);
                _collection.FindOneAndReplace(filter, document);
                response.Result = document;
            }
            catch (Exception ex)
            {
                response.Successed = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
