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
    [XafDisplayName("Computers")]
    [NavigationItem("Inventories")]
    [ImageName("Electronics_LaptopWindows")]
    public class PC : InventoryBase
    {

        StorageCapacity storage;
        CPUGeneration generation;
        string serialNumber;
        PCBrand brand;
        GPUType gputype;
        RAMSize ram;
        CPUType processor;
        string modelName;

        public PC(Session session) : base(session) { }

        [XafDisplayName("Model")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ModelName
        {
            get => modelName;
            set => SetPropertyValue(nameof(ModelName), ref modelName, value);
        }

        public CPUType Processor
        {
            get => processor;
            set => SetPropertyValue(nameof(Processor), ref processor, value);
        }

        public CPUGeneration Generation
        {
            get => generation;
            set => SetPropertyValue(nameof(Generation), ref generation, value);
        }

        public RAMSize RAM
        {
            get => ram;
            set => SetPropertyValue(nameof(RAM), ref ram, value);
        }

        public GPUType GPUType
        {
            get => gputype;
            set => SetPropertyValue(nameof(GPUType), ref gputype, value);
        }

        public PCBrand Brand
        {
            get => brand;
            set => SetPropertyValue(nameof(Brand), ref brand, value);
        }

        [XafDisplayName("S/N")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string SerialNumber
        {
            get => serialNumber;
            set => SetPropertyValue(nameof(SerialNumber), ref serialNumber, value);
        }

        public StorageCapacity Storage
        {
            get => storage;
            set => SetPropertyValue(nameof(Storage), ref storage, value);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
    }
}