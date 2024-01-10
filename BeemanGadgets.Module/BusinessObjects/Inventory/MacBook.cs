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
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static BeemanGadgets.Module.BusinessObjects.Enums;


namespace BeemanGadgets.Module.BusinessObjects.Inventory
{
    [DefaultClassOptions]
    [XafDisplayName("MacBooks")]
    [NavigationItem("Inventories")]
    [ImageName("Electronics_LaptopMac")]
    public class MacBook : InventoryBase
    {
        public MacBook(Session session) : base(session) { }

   
        RAMSize ram;
        ScreenSize size;
        StorageCapacity storage;
        MacBookCpuType cpu;
        MacBookYear year;
        bool isTouchBar;
        string serialNumber;
        MacBookModel model;
   

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public MacBookModel Model
        {
            get => model;
            set => SetPropertyValue(nameof(Model), ref model, value);
        }

        public RAMSize RAM
        {
            get => ram;
            set => SetPropertyValue(nameof(RAM), ref ram, value);
        }

        [XafDisplayName("S/N")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SerialNumber
        {
            get => serialNumber;
            set => SetPropertyValue(nameof(SerialNumber), ref serialNumber, value);
        }
        [CaptionsForBoolValues("Yes", "No")]
        [DevExpress.Xpo.DisplayName("TouchBar")]
        public bool IsTouchBar
        {
            get => isTouchBar;
            set => SetPropertyValue(nameof(IsTouchBar), ref isTouchBar, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public MacBookYear Year
        {
            get => year;
            set => SetPropertyValue(nameof(Year), ref year, value);
        }


        public MacBookCpuType CPU
        {
            get => cpu;
            set => SetPropertyValue(nameof(CPU), ref cpu, value);
        }

        public StorageCapacity Storage
        {
            get => storage;
            set => SetPropertyValue(nameof(Storage), ref storage, value);
        }

        public ScreenSize Size
        {
            get => size;
            set => SetPropertyValue(nameof(Size), ref size, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

    }


}