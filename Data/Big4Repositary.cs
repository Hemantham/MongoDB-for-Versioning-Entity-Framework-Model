using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;
using System.Web.UI.WebControls;
using BIG4.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;
using BIG4.Framework.Data.Helpers;
using BIG4.Framework.Entities.Models;
using MongoDB.Driver.Linq;


namespace BIG4.Framework.Data
{

    public class Big4Repositary<TEntity> where TEntity : class, new()
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;
        internal DataVersionCreator<TEntity> logger;

        public Big4Repositary(DbContext context)
        {
            logger = new DataVersionCreator<TEntity>();

            this.context = context;

            this.dbSet = context.Set<TEntity>();

        }

        /// <summary>
        /// children are only updated in the versioned DB through this method(Mongo DB)
        /// To update child objects to SQL DB use Insert/Update methods
        /// </summary>
        /// <typeparam name="TC"></typeparam>
        /// <param name="saveRecord"></param>
        /// <param name="children"></param>
        /// <param name="childRelationName"></param>
        /// <param name="currentVersion"></param>
        /// <returns></returns>
        public virtual bool UpdateChildren<TC>(TEntity saveRecord, IEnumerable<TC> children,  string childRelationName)
        {
            logger.UpdateWithChildrenAsNewVersion<TC>(saveRecord, 
                children, 
                GetKey(saveRecord), 
                childRelationName
                );
            return true;
        }

        public virtual TCKey MaxIdentity<TC, TCKey>(Func<TEntity, IEnumerable<TC>> getChild, Func<TC, TCKey> getChildKey) 
            where TC : new ()
        {
            return logger.MaxIdentity<TC,TCKey>(getChild,  getChildKey);
        }

        

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        
        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                return query.Count(filter);
            }
            else
            {
                return query.ToList().Count;
            }
        }

        public virtual T Max<T>(
           Expression<Func<TEntity, T>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                return query.Max(filter);
            }
            else
            {
                return default(T);
            }
        }

        public virtual TEntity GetSingle(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "", object id = null, int? version = null)
        {
            if (version == null)
            {
                IQueryable<TEntity> query = dbSet;
                
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (query != null)
                {
                    return query.FirstOrDefault();
                }
            }
            else
            {
                return logger.GetByIdAndVersion(id.ToString(), version.GetValueOrDefault());
            }
            return null;

        }

        public virtual TEntity GetSingle(
           IQueryable<TEntity> query, Expression<Func<TEntity, bool>> filter = null, object id = null, int? version = null)
        {
            if (version == null)
            {
               
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (query != null)
                {
                    return query.FirstOrDefault();
                }
            }
            else
            {
                return logger.GetByIdAndVersion(id.ToString(), version.GetValueOrDefault());
            }
            return null;

        }

      

        public IQueryable<TEntity>  GetQuery()


        {
            return dbSet;
        }



        public virtual TEntity GetByID(object id, string include, int? version = null)
        {
            if (version == null)
            {
                foreach (var includeProperty in include.Split
                                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dbSet.Include(includeProperty);
                }
                return dbSet.Find(id);
            }
            else
            {
                return logger.GetByIdAndVersion(id.ToString(), version.GetValueOrDefault());
            }
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);

            //todo had to brin save changes here 
            context.SaveChanges();

            // add the version
            var key = GetKey(entity);
            logger.SaveVersion(entity, key, UserAction.CREATE, null);
        }

        public string GetKey(TEntity entity)
        {
            var workspace = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            var entityType =
                workspace.GetItems<EntityType>(DataSpace.CSpace).FirstOrDefault(e => e.Name == entity.GetType().Name.Split('_')[0]);

            var keyName = entityType.KeyMembers.Select(k => k.Name).First();

            Type type = typeof(TEntity);

            string key = type.GetProperty(keyName).GetValue(entity, null).ToString();
            return key;
        }

        public string GetIdentityName()
        {
            var workspace = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            var entityType =
                workspace.GetItems<EntityType>(DataSpace.SSpace).FirstOrDefault(e => e.Name == typeof(TEntity).Name.Split('_')[0]);
            var indenty = entityType.KeyMembers.Where(k => k.IsStoreGeneratedIdentity);

            // EdmMember identityColumn = entityType.KeyMembers.First();

            // Facet item;
            // All I get here for Facets is Nullable & DefaultValue
            //if (identityColumn.TypeUsage.Facets.TryGetValue("StoreGeneratedPattern", false, out item))
            //{
            //    var value = ((StoreGeneratedPattern)item.Value) == StoreGeneratedPattern.Identity;
            //}

            if (indenty.Any())
            {
                return indenty.First().Name;
            }
           
            return string.Empty ;
        }

        public bool SetKey(TEntity entityFrom, TEntity entityTo)
        {
            var workspace = ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;

            var entityType =
                workspace.GetItems<EntityType>(DataSpace.CSpace).FirstOrDefault(e => e.Name == entityFrom.GetType().Name.Split('_')[0]);

            var keyName = entityType.KeyMembers.Select(k => k.Name).First();

            Type type = typeof(TEntity);

            var fromkey = type.GetProperty(keyName).GetValue(entityFrom, null);

            type.GetProperty(keyName).SetValue(entityTo, fromkey, null);

            return true;
        }

        

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate, UserAction userAction = UserAction.EDIT, int? version = null, bool isApproved = false)
        {
            if (isApproved)
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;

                //todo had to brin save changes here 
                context.SaveChanges();
            }
            // add the version
            var key = GetKey(entityToUpdate);
            logger.SaveVersion(entityToUpdate, key, userAction, version);
        }
        

    }

    public static class DataExtensions
    {
        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;

        }
    }

    public class UnitOfWork : IDisposable
    {
        private BIG4Context context;

       

        public UnitOfWork()
        {
            context = new BIG4Context(true);
        }


        public Big4Repositary<T> GetRepository<T>() where T : class, new()
        {
            return new Big4Repositary<T>(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}