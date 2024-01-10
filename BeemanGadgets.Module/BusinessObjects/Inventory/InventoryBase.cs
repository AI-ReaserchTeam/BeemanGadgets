using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DisplayNameAttribute = DevExpress.Xpo.DisplayNameAttribute;

namespace BeemanGadgets.Module.BusinessObjects.Inventory
{
    //[DefaultClassOptions]
    [ListViewFilter("Sold", "Status = 'Sold'", true, Index = 4)]
    [ListViewFilter("InStock", "Status = 'InStock'", true, Index = 1)]
    [ListViewFilter("All", "", true, Index = 0)]
    public class InventoryBase : BaseObject
    {
        DateTime distributedDate;
        DateTime soldDate;
        DateTime movedDate;
        string soldby;
        string distributedBy;
        string createdAt;
        Location location;
        BeemanAppUser distributor;
        BeemanAppUser shop;
        Status status;
        DateTime createdOn;
        BeemanAppUser wareHouse;

        public InventoryBase(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            createdOn = DateTime.Now;
            wareHouse = GetCurrentUser();
            Location = Location.Warehouse;
            Status = Status.InStock;
            //createdAt = WareHouse.Name;
        }
        protected override void OnSaved()
        {
            base.OnSaved();
            //refresh the view
            //Session.Reload(this);
        }


        [ExplicitLoading]
        [ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit), "false")]
        public BeemanAppUser WareHouse
        {
            get => wareHouse;
            set => SetPropertyValue(nameof(WareHouse), ref wareHouse, value);
        }

        [ExplicitLoading]
        public BeemanAppUser Shop
        {
            get => shop;
            set => SetPropertyValue(nameof(Shop), ref shop, value);
        }

        [ExplicitLoading]
        public BeemanAppUser Distributor
        {
            get => distributor;
            set => SetPropertyValue(nameof(Distributor), ref distributor, value);
        }


        [VisibleInDetailView(false)]
        [ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit), "False")]
        [ModelDefault(nameof(IModelCommonMemberViewItem.DisplayFormat), "G")]
        public DateTime CreatedOn
        {
            get => createdOn;
            set => SetPropertyValue(nameof(CreatedOn), ref createdOn, value);
        }

        [VisibleInListView(true)]
        public Status Status
        {
            get => status;
            set => SetPropertyValue(nameof(Status), ref status, value);
        }

        [VisibleInListView(true)]
        public Location Location
        {
            get => location;
            set => SetPropertyValue(nameof(Location), ref location, value);
        }

        public BeemanAppUser GetCurrentUser()
        {
            return Session.GetObjectByKey<BeemanAppUser>(Session.ServiceProvider.GetRequiredService<ISecurityStrategyBase>().UserId);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CreatedAt
        {
            get => createdAt;
            set => SetPropertyValue(nameof(CreatedAt), ref createdAt, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string DistributedBy
        {
            get => distributedBy;
            set => SetPropertyValue(nameof(DistributedBy), ref distributedBy, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Soldby
        {
            get => soldby;
            set => SetPropertyValue(nameof(Soldby), ref soldby, value);
        }

        [ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit), "False")]
        public DateTime MovedDate
        {
            get => movedDate;
            set => SetPropertyValue(nameof(MovedDate), ref movedDate, value);
        }
        [ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit), "False")]
        public DateTime DistributedDate
        {
            get => distributedDate;
            set => SetPropertyValue(nameof(DistributedDate), ref distributedDate, value);
        }
        [ModelDefault(nameof(IModelCommonMemberViewItem.AllowEdit), "False")]
        public DateTime SoldDate
        {
            get => soldDate;
            set => SetPropertyValue(nameof(SoldDate), ref soldDate, value);
        }

    }

    public enum Status { New, Moved, Recieved, Distributed, InStock, Sold }


    public enum Location { Warehouse, Distributor, Shop }
}