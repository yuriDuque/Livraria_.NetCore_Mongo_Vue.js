using Domain.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryMongo.Repository
{

    /// <summary>
    /// Explicação sobre cada metodo 
    /// https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        #region Private
        private MongoContext _mongoDB;
        private readonly IMongoCollection<TDocument> _collection;

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        private long GetLastId()
        {
            var item = _collection.Find(FilterDefinition<TDocument>.Empty).SortByDescending(x => x.Id).FirstOrDefault();

            if (item == null)
                return 1;

            return item.Id + 1;
        } 

        #endregion



        public MongoRepository(MongoContext mongoDB)
        {
            _mongoDB = mongoDB;
            _collection = _mongoDB.DB.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        #region Get

        public virtual IQueryable<TDocument> GetAll()
        {
            return _collection.AsQueryable();
        }

        // var people = _peopleRepository.FilterBy(filter => filter.FirstName != "test");
        //
        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        // var people = _peopleRepository.FilterBy(filter => filter.FirstName != "test", projection => projection.FirstName);
        // o projection serve como um select, retornando apenas os campos em que foram solicitados em vez do objeto inteiro
        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).FirstOrDefault();
        }

        public virtual Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.Find(filterExpression).FirstOrDefaultAsync());
        }

        public virtual TDocument FindById(long id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).SingleOrDefault();
        }

        public virtual Task<TDocument> FindByIdAsync(long id)
        {
            return Task.Run(() =>
            {
                var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
                return _collection.Find(filter).SingleOrDefaultAsync();
            });
        }

        #endregion



        #region Save

        public virtual void Save(TDocument document)
        {
            document.Id = GetLastId();

            _collection.InsertOne(document);
        }

        public virtual Task SaveAsync(TDocument document)
        {
            document.Id = GetLastId();

            return Task.Run(() => _collection.InsertOneAsync(document));
        }

        public void SaveMany(ICollection<TDocument> documents)
        {
            var id = GetLastId();

            foreach (var item in documents)
            {
                item.Id = id;
                id++;
            }

            _collection.InsertMany(documents);
        }

        public virtual async Task SaveManyAsync(ICollection<TDocument> documents)
        {
            var id = GetLastId();

            foreach (var item in documents)
            {
                item.Id = id;
                id++;
            }

            await _collection.InsertManyAsync(documents);
        }

        #endregion



        #region Update

        public void Update(TDocument document)
        {
            _collection.FindOneAndReplace(x => x.Id == document.Id, document);
        }

        public virtual async Task UpdateAsync(TDocument document)
        {
            await _collection.FindOneAndReplaceAsync(x => x.Id == document.Id, document);
        }

        #endregion




        #region Delete

        public void DeleteOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.FindOneAndDeleteAsync(filterExpression));
        }

        public void DeleteById(long id)
        {
            _collection.FindOneAndDelete(x => x.Id == id);
        }

        public Task DeleteByIdAsync(long id)
        {
            return Task.Run(() =>
            {
                _collection.FindOneAndDeleteAsync(x => x.Id == id);
            });
        }

        public void DeleteMany(Expression<Func<TDocument, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            return Task.Run(() => _collection.DeleteManyAsync(filterExpression));
        }

        #endregion
    }
}
