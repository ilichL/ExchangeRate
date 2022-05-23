using ExchangeRate.Core.Interfaces.Data;
using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.DataAccess
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Context context;
        private readonly DbSet<T> DbSet;

        public Repository(Context context)
        {
            this.context = context;
            DbSet = context.Set<T>();
        }

        public virtual async Task Add(T entity)
        {
            await DbSet.AddAsync(entity);//добавление в таблицу продукта
        }

        public virtual async Task AddRange(IEnumerable<T> entity)
        {
            await DbSet.AddRangeAsync(entity);//добавление в таблицу
        }

        public async Task<T> GetById(Guid Id)
        {
            return await DbSet.AsNoTracking().
                FirstOrDefaultAsync(entity => entity.ID.Equals(Id));
            //AsNoTracking нужен для того. чтобы не отслеживать эту сущность(ускорение выполнения запроса)
            ///AsNoTracking если данные только для чтения, изменений больше не будет
        }
        public async Task<T> GetByIdWithIncludes(Guid Id,
            params Expression<Func<T, object>>[] includes)
        {
            if (includes.Any())
            {
                return await includes.Aggregate(DbSet.Where(entity => entity.ID.Equals(Id)),
                    (current, include) => current.Include(include)).FirstOrDefaultAsync();
            }
            return await GetById(Id);
        }
        public virtual IQueryable<T> Get()
        {
            return DbSet;//возврящает всю таблицу
        }

        public virtual async Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            var result = DbSet.Where(predicate);//передаем условие по которому мы хотим забрать продукты?

            if (includes.Any())
            {//проверем ести ли еще условие с которым мы хотим забрать продукты
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }//если есть забирем эти условия в result
            return result;//вернули продукты со всеми условиями
        }
        public virtual async Task Update(T entity)
        {//обновили Product, теми данными, которые отличаются
            DbSet.Update(entity);
        }

        public virtual async Task СhangeAsync(Guid id, List<PatchModel> patchDtos)
        {
            var model = await DbSet.FirstOrDefaultAsync(entity => entity.ID.Equals(id));//достаем модель
            var nameValuePairProperties = patchDtos
                .ToDictionary(a => a.PropertyName, a => a.PropertyValue);

            var dbEntityEntry = context.Entry(model);//цепляемся к бд и начинаем отслеживать нашу модель
            dbEntityEntry.CurrentValues.SetValues(nameValuePairProperties);//собираем все изменения в эту модель
            dbEntityEntry.State = EntityState.Modified;//модель была изменена
        }

        public virtual async Task Delete(Guid id)
        {
            DbSet.Remove(await DbSet.FindAsync(id));
        }

        public virtual async Task RemoveRange(Expression<Func<T, bool>> predicate)
        {
            var entities = DbSet.Where(predicate);

            DbSet.RemoveRange(entities);
        }

        public void Dispose()
        {//как работает описано в Unit Of Work
            context.Dispose();
            GC.SuppressFinalize(this);

        }
    }
}
