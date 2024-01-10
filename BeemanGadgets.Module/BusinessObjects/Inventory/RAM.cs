using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using static BeemanGadgets.Module.BusinessObjects.Enums;

namespace BeemanGadgets.Module.BusinessObjects.Inventory
{
    [DefaultClassOptions]
    [XafDisplayName("RAM")]
    [NavigationItem("Inventories")]
    [ImageName("Electronics_DesktopMac")]
    public class RAM : InventoryBase
    {
        public RAM(Session session) : base(session) { }

        StorageCapacity capacity;
        RAMType ramtype;
   


        public StorageCapacity Capacity
        {
            get => capacity;
            set => SetPropertyValue(nameof(Capacity), ref capacity, value);
        }

        public RAMType RAMType
        {
            get => ramtype;
            set => SetPropertyValue(nameof(RAMType), ref ramtype, value);
        }

       

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

   
    }
}