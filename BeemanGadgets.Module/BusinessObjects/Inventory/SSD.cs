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
    [NavigationItem("Inventories")]
    [ImageName("Electronics_DesktopMac")]
    public class SSD : InventoryBase
    {

        SSDType type;
        StorageCapacity capacity;


        public StorageCapacity Capacity
        {
            get => capacity;
            set => SetPropertyValue(nameof(Capacity), ref capacity, value);
        }


        public SSDType Type
        {
            get => type;
            set => SetPropertyValue(nameof(Type), ref type, value);
        }


        public SSD(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

    }
}