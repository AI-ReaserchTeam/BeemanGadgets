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
    [XafDisplayName("Hard Disks")]
    [NavigationItem("Inventories")]
    [ImageName("Electronics_DesktopMac")]
    public class HardDisk : InventoryBase
    {
        public HardDisk(Session session) : base(session) { }

        InterfaceType interfacetype;
        StorageCapacity capacity;


        public StorageCapacity Capacity
        {
            get => capacity;
            set => SetPropertyValue(nameof(Capacity), ref capacity, value);
        }


        public InterfaceType Interface
        {
            get => interfacetype;
            set => SetPropertyValue(nameof(Interface), ref interfacetype, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }


    }
}