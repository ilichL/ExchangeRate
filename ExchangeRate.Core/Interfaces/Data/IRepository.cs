using ExchangeRate.Data;
using ExchangeRate.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRate.Core.Interfaces.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task Add(T entity);//добавление в таблицу 1 продукта
        public Task AddRange(IEnumerable<T> entities); //добавление в таблицу(бд) продуктов
        public Task<T> GetById(Guid Id);//достаем продукт по ID
        public Task<T> GetByIdWithIncludes(Guid Id,
            params Expression<Func<T, object>>[] includes);
        public IQueryable<T> Get();//вернет всю таблицу Продуктов
        //так жечерез Select можем забрать с бд любую проекцию, которая нам нужна(например Name)
        public Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes);
        //мы можем забрать с бд продукты с определенным условием и при этом еще забрать условие
        //(Напрмер хочу забрать все продукты начнающиеся с буквы А с такой-то ценой)
        public Task Update(T entity);//будет перезаписывать Product
        public Task СhangeAsync(Guid id, List<PatchModel> patchDtos);//изменим конкретные поля данной модели
        public Task Delete(Guid id);//удаление продукта по ID
        public Task RemoveRange(Expression<Func<T, bool>> predicate);
        public void Dispose();//нкжен для освобождения оперативной памяти
    }
}
