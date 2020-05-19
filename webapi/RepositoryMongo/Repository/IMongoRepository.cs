using Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryMongo.Repository
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> GetAll();

        IList<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        Task<IList<TDocument>> FilterByAsync(
            Expression<Func<TDocument, bool>> filterExpression);

        IList<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FindById(long id);

        Task<TDocument> FindByIdAsync(long id);

        void Save(TDocument document);

        Task SaveAsync(TDocument document);

        void SaveMany(ICollection<TDocument> documents);

        Task SaveManyAsync(ICollection<TDocument> documents);

        void Update(TDocument document);

        Task UpdateAsync(TDocument document);

        void DeleteOne(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);
        
        void DeleteById(long id);

        Task DeleteByIdAsync(long id);

        void DeleteMany(Expression<Func<TDocument, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression);
    }
}
