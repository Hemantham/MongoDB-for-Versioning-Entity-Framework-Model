﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using BIG4.Framework.Data.Helpers;
using ServiceStack.Common.Utils;

namespace BIG4.Framework.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using BIG4.Framework.Entities;
    
    public interface IBIG4Context
    {
    	IDbSet<ParkDetail> ParkDetails { get; }
    	IDbSet<Location> Locations { get; }
    	IDbSet<State> States { get; }
    	IDbSet<Region> Regions { get; }
    	IDbSet<ParkPreference> ParkPreferences { get; }
    	IDbSet<Country> Countries { get; }
    	IDbSet<Accommodation> Accommodations { get; }
    	IDbSet<AccommodationList> AccommodationLists { get; }
    	IDbSet<Option> Options1 { get; }
    	IDbSet<AccommodationImage> AccommodationImages { get; }
    	IDbSet<Image> Images { get; }
    	IDbSet<BookingPayment> BookingPayments { get; }
    	IDbSet<ParkCopy> ParkCopies { get; }
    	IDbSet<ParkList> ParkLists { get; }
    	IDbSet<Facility> Facilities { get; }
    	IDbSet<ParkFacility> ParkFacilities { get; }
    	IDbSet<MemberBenefit> MemberBenefits { get; }
    	IDbSet<MemberBenefitType> MemberBenefitTypes { get; }
    	IDbSet<SpecialOffer> SpecialOffers { get; }
    	IDbSet<Page> Pages { get; }
    	IDbSet<ATDWCategory> ATDWCategories { get; }
    	IDbSet<ATDWClassification> ATDWClassifications { get; }
    	IDbSet<ATDWExclusion> ATDWExclusions { get; }
    	IDbSet<Attraction> Attractions { get; }
    	IDbSet<GuestBook> GuestBooks { get; }
    	IDbSet<ParkFacebookPage> ParkFacebookPages { get; }
    	IDbSet<PrivateUserPark> PrivateUserParks { get; }
    	IDbSet<PrivateUser> PrivateUsers { get; }
    	IDbSet<PrivateRole> PrivateRoles { get; }
    	IDbSet<FacilityGroup> FacilityGroups { get; }
    	IDbSet<OptionGroup> OptionGroups { get; }
    	IDbSet<AwardType> AwardTypes { get; }
    	IDbSet<ParkAward> ParkAwards { get; }
    	IDbSet<EquipmentType> EquipmentTypes { get; }
    	IDbSet<ParkEquipmentType> ParkEquipmentTypes { get; }
    	IDbSet<Media> Media1 { get; }
    	IDbSet<Entertainment> Entertainments { get; }
    	IDbSet<EntertainmentType> EntertainmentTypes { get; }
    	IDbSet<LocalEvent> LocalEvents { get; }
    	IDbSet<ParkRedirect> ParkRedirects { get; }
    	IDbSet<Banner> Banners { get; }
    	IDbSet<BannerCampaign> BannerCampaigns { get; }
    	IDbSet<BannerStat> BannerStats { get; }
    	IDbSet<ParkWidget> ParkWidgets { get; }
    
    	void Update(Object entity);
    	void SaveChanges();
    }
    
    public partial class BIG4Context : DbContext, IBIG4Context
    {
       
        public BIG4Context(bool isLazyLoadDisabled)
            : base("name=BIG4Context")
        {
            if (isLazyLoadDisabled)
           {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
           }
        }

        public BIG4Context()
            : base("name=BIG4Context")
        {

        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public IDbSet<ParkDetail> ParkDetails { get; set; }
        public IDbSet<Location> Locations { get; set; }
        public IDbSet<State> States { get; set; }
        public IDbSet<Region> Regions { get; set; }
        public IDbSet<ParkPreference> ParkPreferences { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<Accommodation> Accommodations { get; set; }
        public IDbSet<AccommodationList> AccommodationLists { get; set; }
        public IDbSet<Option> Options1 { get; set; }
        public IDbSet<AccommodationImage> AccommodationImages { get; set; }
        public IDbSet<Image> Images { get; set; }
        public IDbSet<BookingPayment> BookingPayments { get; set; }
        public IDbSet<ParkCopy> ParkCopies { get; set; }
        public IDbSet<ParkList> ParkLists { get; set; }
        public IDbSet<Facility> Facilities { get; set; }
        public IDbSet<ParkFacility> ParkFacilities { get; set; }
        public IDbSet<MemberBenefit> MemberBenefits { get; set; }
        public IDbSet<MemberBenefitType> MemberBenefitTypes { get; set; }
        public IDbSet<SpecialOffer> SpecialOffers { get; set; }
        public IDbSet<Page> Pages { get; set; }
        public IDbSet<ATDWCategory> ATDWCategories { get; set; }
        public IDbSet<ATDWClassification> ATDWClassifications { get; set; }
        public IDbSet<ATDWExclusion> ATDWExclusions { get; set; }
        public IDbSet<Attraction> Attractions { get; set; }
        public IDbSet<GuestBook> GuestBooks { get; set; }
        public IDbSet<ParkFacebookPage> ParkFacebookPages { get; set; }
        public IDbSet<PrivateUserPark> PrivateUserParks { get; set; }
        public IDbSet<PrivateUser> PrivateUsers { get; set; }
        public IDbSet<PrivateRole> PrivateRoles { get; set; }
        public IDbSet<FacilityGroup> FacilityGroups { get; set; }
        public IDbSet<OptionGroup> OptionGroups { get; set; }
        public IDbSet<AwardType> AwardTypes { get; set; }
        public IDbSet<ParkAward> ParkAwards { get; set; }
        public IDbSet<EquipmentType> EquipmentTypes { get; set; }
        public IDbSet<ParkEquipmentType> ParkEquipmentTypes { get; set; }
        public IDbSet<Media> Media1 { get; set; }
        public IDbSet<Entertainment> Entertainments { get; set; }
        public IDbSet<EntertainmentType> EntertainmentTypes { get; set; }
        public IDbSet<LocalEvent> LocalEvents { get; set; }
        public IDbSet<ParkRedirect> ParkRedirects { get; set; }
        public IDbSet<Banner> Banners { get; set; }
        public IDbSet<BannerCampaign> BannerCampaigns { get; set; }
        public IDbSet<BannerStat> BannerStats { get; set; }
        public IDbSet<ParkWidget> ParkWidgets { get; set; }
    
    	public void Update(Object entity)
    	{
    		Entry(entity).State = EntityState.Modified;
    	}
    	
    	public new void SaveChanges()
    	{
    		base.SaveChanges();
    	}
    	
    	#region Function Imports
    	#endregion
    }

    public partial class BIG4MongoContext :  IBIG4Context
    {
        private int DataVersion = 0;
        private bool IsParentInPreview;
       
        public BIG4MongoContext(int version,bool  isParentInPreview = false)
        {
            DataVersion = version;
            IsParentInPreview = isParentInPreview;
            // this.Configuration.LazyLoadingEnabled = true;
            // this.Configuration.ProxyCreationEnabled = false;
        }



        public IDbSet<ParkDetail> ParkDetails
        {
            get
            {
              
                var logger = new DataVersionCreator<ParkDetail>();
                return new MongoDbSet<ParkDetail,ParkDetail,int>(logger.GetAllByVersion(DataVersion), DataVersion);
                
            }
        }

        public IDbSet<Location> Locations { get; set; }
        public IDbSet<State> States { get; set; }
        public IDbSet<Region> Regions { get; set; }
        public IDbSet<ParkPreference> ParkPreferences { get; set; }
        public IDbSet<Country> Countries { get; set; }
        public IDbSet<Accommodation> Accommodations
        {
            get
            {
                if (IsParentInPreview)
                {
                    // todo , try to find a way to put this logic in to a factory
                    //currently park Detaiul is hard coded
                    var logger = new DataVersionCreator<ParkDetail>();
                    return new MongoDbSet<Accommodation,ParkDetail, int>(
                        logger.GetAllChildrenByVersion<Accommodation>(DataVersion, p => p.Accommodations), 
                        DataVersion,
                        true,
                        p=>p.Accommodations, 
                        a=>a.AccommodationID);
                }
                else
                {
                    var logger = new DataVersionCreator<Accommodation>();
                    return new MongoDbSet<Accommodation,ParkDetail, int>(logger.GetAllByVersion(DataVersion), DataVersion);
                }

                
               
            }
        }
        public IDbSet<AccommodationList> AccommodationLists { get; set; }
        public IDbSet<Option> Options1 { get; set; }
        public IDbSet<AccommodationImage> AccommodationImages { get; set; }
        public IDbSet<Image> Images { get; set; }
        public IDbSet<BookingPayment> BookingPayments { get; set; }
        public IDbSet<ParkCopy> ParkCopies { get; set; }
        public IDbSet<ParkList> ParkLists { get; set; }
        public IDbSet<Facility> Facilities { get; set; }
        public IDbSet<ParkFacility> ParkFacilities { get; set; }
        public IDbSet<MemberBenefit> MemberBenefits { get; set; }
        public IDbSet<MemberBenefitType> MemberBenefitTypes { get; set; }
        public IDbSet<SpecialOffer> SpecialOffers
        {
            get
            {
                
                var logger = new DataVersionCreator<SpecialOffer>();
                return new MongoDbSet<SpecialOffer,ParkDetail,int>(logger.GetAllByVersion(DataVersion), DataVersion);
               
            }
        }
        public IDbSet<Page> Pages { get; set; }
        public IDbSet<ATDWCategory> ATDWCategories { get; set; }
        public IDbSet<ATDWClassification> ATDWClassifications { get; set; }
        public IDbSet<ATDWExclusion> ATDWExclusions { get; set; }
        public IDbSet<Attraction> Attractions { get; set; }
        public IDbSet<GuestBook> GuestBooks { get; set; }
        public IDbSet<ParkFacebookPage> ParkFacebookPages { get; set; }
        public IDbSet<PrivateUserPark> PrivateUserParks { get; set; }
        public IDbSet<PrivateUser> PrivateUsers { get; set; }
        public IDbSet<PrivateRole> PrivateRoles { get; set; }
        public IDbSet<FacilityGroup> FacilityGroups { get; set; }
        public IDbSet<OptionGroup> OptionGroups { get; set; }
        public IDbSet<AwardType> AwardTypes { get; set; }
        public IDbSet<ParkAward> ParkAwards { get; set; }
        public IDbSet<EquipmentType> EquipmentTypes { get; set; }
        public IDbSet<ParkEquipmentType> ParkEquipmentTypes { get; set; }
        public IDbSet<Media> Media1 { get; set; }
        public IDbSet<Entertainment> Entertainments { get; set; }
        public IDbSet<EntertainmentType> EntertainmentTypes { get; set; }
        public IDbSet<LocalEvent> LocalEvents { get; set; }
        public IDbSet<ParkRedirect> ParkRedirects { get; set; }
        public IDbSet<Banner> Banners { get; set; }
        public IDbSet<BannerCampaign> BannerCampaigns { get; set; }
        public IDbSet<BannerStat> BannerStats { get; set; }
        public IDbSet<ParkWidget> ParkWidgets { get; set; }

        public void Update(Object entity)
        {
            throw new  NotImplementedException();
        }

        public new void SaveChanges()
        {
            throw new NotImplementedException();
        }

        #region Function Imports
        #endregion
    }


    public class MongoDbSet<T ,TP ,TID> : IDbSet<T> 
        where T : class , new()
        where TP : class , new()
    {
        private IEnumerable<T> _data;
        private int _version;
        private bool IsParentInPreview;
        private Func<TP, IEnumerable<T>> getChildren;
        private Func<T, TID> findId;


        public MongoDbSet(IEnumerable<T> data, int version, bool isParentInPreview = false, Func<TP, IEnumerable<T>> getChildren = null, Func<T, TID> getId = null)
        {
            _data = data;
            _version = version;
            IsParentInPreview = isParentInPreview;
            this.getChildren = getChildren;
            this.findId = getId;
        }

        public virtual T Find(params object[] keyValues)
        {
            if (IsParentInPreview)
            {
                // todo , try to find a way to put this logic in to a factory
                //currently park Detaiul is hard coded
                var logger = new DataVersionCreator<TP>();
                return logger.GetAllChildrenByVersion<T>(_version, getChildren).FirstOrDefault(t=> findId(t).Equals((TID) keyValues.First()));
            }
            else
            {
                var logger = new DataVersionCreator<T>();
                return logger.GetByIdAndVersion(keyValues[0].ToString(), _version);
            }
         
        }
         
        public T Add(T item)
        {
          //  _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
         //   _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
          //  _data.Add(item);
            return item;
        }

        //public void Detach(T item)
        //{
           
        //  //  _data.Remove(item);
        //}

        Type IQueryable.ElementType
        {
            get { return _data.AsQueryable().ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return _data.AsQueryable().Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _data.AsQueryable().Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public ObservableCollection<T> Local
        {
            get
            {
                return new ObservableCollection<T>(_data);
            }
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }
    }

}
