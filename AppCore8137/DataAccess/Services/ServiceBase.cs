

using AppCore8137.DataAccess.Results;
using AppCore8137.DataAccess.Results.Bases;
using AppCore8137.Records.Bases;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppCore8137.DataAccess.Services
{
    // new ServiceBase<Oyun>(); +
    // new ServiceBase<Urun>(); +
    // new ServiceBase<int>(); -   
    // new ServiceBase<string>(); -
    public abstract class ServiceBase<TEntity> : IDisposable where TEntity : Record, new() // Repository Pattern
    {
        protected readonly DbContext _dbContext;

        const string _errorMessage = "Changes not saved!";

        protected ServiceBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // ServiceBase service = new ServiceBase<Kategoir>();
        // service.Query()
        public virtual IQueryable<TEntity> Query() // sadece sorguyu oluşturur, çalıştırmaz
        {
            return _dbContext.Set<TEntity>();
        }

        public virtual List<TEntity> GetList() // ToList() oluşturulan soruguyu çalıştırır
        {
            return Query().ToList();
        }

        // db.Oyunlar.Where(oyun => oyun.Adi == "GTA V").ToList().SingleOrDefault();
        public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return Query().Where(predicate).ToList();
        }

        public virtual TEntity GetItem(int id)
        {
            return Query().SingleOrDefault(q => q.Id == id); // kayıt bulunamazsa null döner, birden çok kayıt dönerse exception fırlatıyor
            //return Query().Find(id); // sadece DbSet tipindeki kolleksiyonlarda kullanılabilir, SingleOrDefault gibi davranır
            //return Query().Single(q => q.Id == id); // kayıt bulunamazsa exception fırlatır, birden çok kayıt dönerse exception fırlatıyor
            //return Query().FirstOrDefault(q => q.Id == id); // ilk kaydı döner
            //return Query().LastOrDefault(q => q.Id == id); // son kaydı döner
            //return Query().First(q => q.Id == id);
            //return Query().Last(q => q.Id == id); 
        }

        // db.Oyunlar.Add(oyun);
        // return new SuccessResult();
        // return new ErrorResult("GTA V adında oyun mevcuttur!");
        public virtual Result Add(TEntity entity, bool save = true)
        {
            _dbContext.Set<TEntity>().Add(entity);
            if (save)
            {
                Save();
                return new SuccessResult("Added successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual Result Update(TEntity entity, bool save = true)
        {
            _dbContext.Set<TEntity>().Update(entity);
            if (save)
            {
                Save();
                return new SuccessResult("Updated successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual Result Delete(TEntity entity, bool save = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (save)
            {
                Save();
                return new SuccessResult("Deleted successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual Result Delete(Expression<Func<TEntity, bool>> predicate, bool save = true) // Delete(e => e.Id == 5); service.Delete(e => e.Markasi == "Audi");
        {
            var entities = Query().Where(predicate).ToList();
            foreach (var entity in entities)
            {
                Delete(entity, false);
            }
            if (save)
            {
                Save();
                return new SuccessResult("Deleted successfully.");
            }
            return new ErrorResult(_errorMessage);
        }

        public virtual int Save()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception exc)
            {

                throw exc;
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
