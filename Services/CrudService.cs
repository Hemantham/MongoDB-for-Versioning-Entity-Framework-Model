using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BIG4.Framework.Data;
using System.Linq.Expressions;

namespace BIG4.Framework.Business.Services
{
    public class CrudService : IService
    {
        private IBIG4Context _context;
        private UnitOfWork uow;

        public CrudService()
        {
            uow = new UnitOfWork();
            _context = new BIG4Context();
            // _context = new BIG4MongoContext(6);
        }
        public CrudService(int version)
        {
            _context = new BIG4MongoContext(version);
            uow = new UnitOfWork();
        }

        public void AddDeleteChildren<T, TP, TKey, TIdentity>(IEnumerable<T> itemsToSave,
            TP parent,
            Func<T, TKey> getKey,
            Func<T, TIdentity> getIdentity,
            Func<TP,IEnumerable<T>> getChild,
            //  Expression<Func<T, bool>> parentFilter,
            string childRelationName
          )
            where T : class, new()
            where TP : class ,new()
        {
            var repository = uow.GetRepository<T>();

            var parentRepository = uow.GetRepository<TP>();

            ////
            Type parentType = typeof(TP);
            Type type = typeof(T);

            IEnumerable<T> existingItems = null;

            // check is a identity column exists for the child item. 
            //if so , we have to create it in Mongo DB as well
            var identityName = repository.GetIdentityName();
            int next = 0;


            //MaxIdentity<TC, TCKey>(Func<TEntity, IEnumerable<TC>> getChild, Func<TC, TCKey> getChildKey)

            if (!string.IsNullOrEmpty(identityName))
            {
                var maxIdentityInDB = Convert.ToInt64(repository.Get().Max(getIdentity));
                var maxIdentityInVersions = Convert.ToInt64(parentRepository.MaxIdentity<T, TIdentity>(getChild, getIdentity));
                next = (int) Math.Max(maxIdentityInDB, maxIdentityInVersions) + 1;
            }
            //else
            //{
               
            //    //existingItems = repository.GetByID(getParentKey, childRelationName,version);
            //}

            //getChild(parent);
            existingItems = getChild(parent); //(IEnumerable<T>)parentType.GetProperty(childRelationName).GetValue(parent, null);

            var addedItems = itemsToSave.Except<T, TKey>(existingItems, getKey);

            var deletedItems = existingItems.Except<T, TKey>(itemsToSave, getKey); //, tchr => tchr.RemoteImageID);


            var count = addedItems.Count();

            if (!string.IsNullOrEmpty(identityName))
            {
                for (int i = 0; i < count; i++)
                {
                    type.GetProperty(identityName).SetValue(addedItems.ElementAt(i), next, null);
                    next++;
                }

                //will not do insert untill approved
                //repository.Insert(addedItems.ElementAt(i));

            }

            List<T> saveList = existingItems.Union(addedItems).ToList();

             count = saveList.Count();

            foreach (var deleted in deletedItems)
            {
                for (int i = count - 1; i >= 0; i--)
                {
                    var item = saveList.ElementAt(i);
                    if (getKey(deleted).Equals(getKey(item)))
                    {
                        saveList.RemoveAt(i);
                    }
                }
            }



            // var modifiedItems = images.Except(addedItems, tchr => tchr.RemoteImageID);

            // add or delete child items , based on availability.
            //////var count = addedItems.Count();
            //////for (int i = 0; i < count; i++)
            //////{
            //////    repository.Insert(addedItems.ElementAt(i));
            //////}

            //var count = deletedItems.Count();
            //for (int i = count - 1; i >= 0; i--)
            //{
            //    repository.Delete(deletedItems.ElementAt(i));
            //}


            // if the entity is existing and the primary keys (imageID field. not the unique keys accommodationID and RemoteImageID. ) are not sent along with the 
            //foreach (var existing in existingItems)
            //{
            //    foreach (var tosave in itemsToSave)
            //    {
            //        if (getKey(existing).Equals(getKey(tosave)))
            //        {
            //            //set the key from existing entry to the new entry
            //            repository.SetKey(existing, tosave);
            //        }
            //    }
            //}

            // when updating the children a new version is created . this can be modified to save children to the save version. 
            // may be usefull when both are saved in one click.
            parentRepository.UpdateChildren<T>(parent, saveList, childRelationName);

        }


        public void AddChildren<T, TP, TKey, TIdentity>(IEnumerable<T> itemsToSave,
         TP parent,
         Func<T, TKey> getKey,
         Func<T, TIdentity> getIdentity,
         Func<TP, IEnumerable<T>> getChild,
            //Expression<Func<T, bool>> parentFilter,
            // Func<TP, TPKey> getParentKey,
         string childRelationName

         )
            where T : class, new()
            where TP : class ,new()
        {
            var repository = uow.GetRepository<T>();

            var parentRepository = uow.GetRepository<TP>();

            ////
            Type parentType = typeof(TP);
            Type type = typeof(T);

            IEnumerable<T> existingItems = null;

            // check is a identity column exists for the child item. 
            //if so , we have to create it in Mongo DB as well
            var identityName = repository.GetIdentityName();
            int next = 0;

            if (!string.IsNullOrEmpty(identityName))
            {
                var maxIdentityInDB = Convert.ToInt64(repository.Get().Max(getIdentity));
                var maxIdentityInVersions = Convert.ToInt64(parentRepository.MaxIdentity<T, TIdentity>(getChild, getIdentity));
                next = (int)Math.Max(maxIdentityInDB, maxIdentityInVersions) + 1;
            }
            //else
            //{
              
            //    //existingItems = repository.GetByID(getParentKey, childRelationName,version);
            //}

             existingItems = (IEnumerable<T>)parentType.GetProperty(childRelationName).GetValue(parent, null);
            var addedItems = itemsToSave.Except<T, TKey>(existingItems, getKey);

            var count = addedItems.Count();

            if (!string.IsNullOrEmpty(identityName))
            {
                for (int i = 0; i < count; i++)
                {
                    type.GetProperty(identityName).SetValue(addedItems.ElementAt(i), next, null);
                    next++;
                }

                //will not do insert untill approved
                //repository.Insert(addedItems.ElementAt(i));

            }

            IEnumerable<T> saveList = existingItems.Union(addedItems).ToList();

            // if the entity is existing and the primary keys (imageID field. not the unique keys accommodationID and RemoteImageID. ) are not sent along with the 
            //foreach (var existing in existingItems)
            //{
            //    foreach (var tosave in itemsToSave)
            //    {
            //        if (getKey(existing).Equals(getKey(tosave)))
            //        {
            //            //set the key from existing entry to the new entry
            //            repository.SetKey(existing, tosave);
            //        }
            //    }
            //}

            // when updating the children a new version is created . this can be modified to save children to the saved version. 
            // may be usefull when both are saved in one click.
            //all new versions are saved in mongo
            parentRepository.UpdateChildren<T>(parent, saveList, childRelationName);

        }

    }

    public interface IService
    {

        void AddDeleteChildren<T, TP, TKey, TIdentity>(IEnumerable<T> itemsToSave,
            TP parent,
            Func<T, TKey> getKey,
            Func<T, TIdentity> getIdentity,
            Func<TP, IEnumerable<T>> getChild,
            //  Expression<Func<T, bool>> parentFilter,
            string childRelationName
            )
            where T : class, new()
            where TP : class, new();

        void AddChildren<T, TP, TKey, TIdentity>(IEnumerable<T> itemsToSave,
            TP parent,
            Func<T, TKey> getKey,
            Func<T, TIdentity> getIdentity,
            Func<TP, IEnumerable<T>> getChild,
            //Expression<Func<T, bool>> parentFilter,
            // Func<TP, TPKey> getParentKey,
            string childRelationName

            )
            where T : class, new()
            where TP : class, new();
    }
}
