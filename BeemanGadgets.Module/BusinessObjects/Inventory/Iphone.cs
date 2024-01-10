using DevExpress.DashboardExport.Map;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static BeemanGadgets.Module.BusinessObjects.Enums;

namespace BeemanGadgets.Module.BusinessObjects.Inventory
{
    [DefaultClassOptions]
    [XafDisplayName("iPhones")]
    [NavigationItem("Inventories")]
    [ImageName("Electronics_PhoneIphone")]
    public class Iphone : InventoryBase
    {
        public Iphone(Session session) : base(session) { }

        bool isFaceID;
        StorageCapacity? storage;
        double? batteryPercentage;
        string imei;
        iPhoneColor? color;
        IPhoneModel? modelName;


        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public IPhoneModel? ModelName
        {
            get => modelName;
            set => SetPropertyValue(nameof(ModelName), ref modelName, value);
        }

        [RuleRequiredField]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public iPhoneColor? Color
        {
            get => color;
            set => SetPropertyValue(nameof(Color), ref color, value);
        }

        [RuleUniqueValue, RuleRequiredField]
        [ModelDefault("EditMask", "000 000000 000000 00")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string IMEI
        {
            get => imei;
            set => SetPropertyValue(nameof(IMEI), ref imei, value);
        }

        [XafDisplayName("Battery %")]
        [RuleRequiredField]
        [ModelDefault("DisplayFormat", "P0")]
        //[ModelDefault("EditMask", "P0")]
        public double? BatteryPercentage
        {
            get { return batteryPercentage; }
            set => SetPropertyValue(nameof(BatteryPercentage), ref batteryPercentage, value);
        }

        public StorageCapacity? Storage
        {
            get => storage;
            set => SetPropertyValue(nameof(Storage), ref storage, value);
        }

        [XafDisplayName("FaceID")]
        [CaptionsForBoolValues("Yes", "No")]
        public bool IsFaceID
        {
            get => isFaceID;
            set => SetPropertyValue(nameof(IsFaceID), ref isFaceID, value);
        }


    }
}