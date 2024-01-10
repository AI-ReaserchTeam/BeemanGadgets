using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeemanGadgets.Module.BusinessObjects
{
    public class Enums
    {
        public enum GPUType
        {
            [Description("Integrated")]
            Integrated,
            [Description("Dedicated")]
            Dedicated,
            [Description("Integrated and Dedicated")]
            IntegratedAndDedicated, NONE
        }

        public enum StorageCapacity
        {
            [XafDisplayName("4 GB")]
            Size_4GB = 4,
            [XafDisplayName("8 GB")]
            Size_8GB = 8,
            [XafDisplayName("16 GB")]
            Size_16GB = 16,
            [XafDisplayName("32 GB")]
            Size_32GB = 32,
            [XafDisplayName("64 GB")]
            Size_64GB = 64,
            [XafDisplayName("128 GB")]
            Size_128GB = 128,
            [XafDisplayName("256 GB")]
            Size_256GB = 256,
            [XafDisplayName("320 GB")]
            Size_320GB = 320,
            [XafDisplayName("500 GB")]
            Size_500GB = 500,
            [XafDisplayName("512 GB")]
            Size_512GB = 512,
            [XafDisplayName("1 TB")]
            Size_1TB = 1024,
            [XafDisplayName("2 TB")]
            Size_2TB = 2048,
            [XafDisplayName("4 TB")]
            Size_4TB = 4096
        }
        public enum CPU
        {
            [XafDisplayName("M Chip")]
            MChip,
            [XafDisplayName("Intel")]
            Intel,
            [XafDisplayName("AMD")]
            AMD,
            [XafDisplayName("ARM")]
            ARM
        }

        public enum RAMType
        {
            DDR,
            DDR2,
            DDR3,
            DDR4,
            DDR5,
            DDR6
        }

        public enum InterfaceType
        {
            [Description("SATA")]
            SATA,
            [Description("NVMe")]
            NVMe,
            [Description("USB")]
            USB,
            UNKNOWN,
        }

        public enum PCBrand
        {
            [XafDisplayName("ACER")]
            Acer,
            [XafDisplayName("ALIENWARE")]
            Alienware,
            [XafDisplayName("APPLE")]
            Apple,
            [XafDisplayName("ASUS")]
            Asus,
            [XafDisplayName("CHEMUSA")]
            ChemUSA,
            [XafDisplayName("COMPAQ")]
            CPTechnologies,
            [XafDisplayName("CRAY")]
            Cray,
            [XafDisplayName("CYBERPOWERPC")]
            CyberPowerPC,
            [XafDisplayName("DELL")]
            Dell,
            [XafDisplayName("HP")]
            HP,
            [XafDisplayName("HUAWEI")]
            Huawei,
            [XafDisplayName("IBM")]
            IBM,
            [XafDisplayName("LENOVO")]
            Lenovo,
            [XafDisplayName("LG")]
            LG,
            [XafDisplayName("MEDION")]
            Medion,
            [XafDisplayName("MICROSOFT")]
            Microsoft,
            [XafDisplayName("MSI")]
            MSI,
            [XafDisplayName("NEC")]
            NEC,
            [XafDisplayName("PACKARD BELL")]
            PackardBell,
            [XafDisplayName("RAZER")]
            Razer,
            [XafDisplayName("SAMSUNG")]
            Samsung,
            [XafDisplayName("SHUTTLE")]
            Shuttle,
            [XafDisplayName("SONY")]
            Sony,
            [XafDisplayName("TOSHIBA")]
            Toshiba,
            [XafDisplayName("ZOTAC")]
            Zotac
        }


        public enum CPUType
        {
            [XafDisplayName("Intel Core i3")]
            Core_i3,
            [XafDisplayName("Intel Core i5")]
            Core_i5,
            [XafDisplayName("Intel Core i7")]
            Core_i7,
            [XafDisplayName("Intel Core i9")]
            Core_i9,
            [XafDisplayName("Intel Xeon")]
            Xeon,
            [XafDisplayName("Intel Atom")]
            Atom,
            [XafDisplayName("Intel Celeron")]
            Celeron,
            [XafDisplayName("Intel Pentium")]
            Pentium,
            [XafDisplayName("AMD Ryzen 3")]
            Ryzen3,
            [XafDisplayName("AMD Ryzen 5")]
            Ryzen5,
            [XafDisplayName("AMD Ryzen 7")]
            Ryzen7,
            [XafDisplayName("AMD Ryzen 9")]
            Ryzen9,
            [XafDisplayName("AMD Threadripper")]
            Threadripper,
            [XafDisplayName("AMD Athlon")]
            Athlon,
            [XafDisplayName("AMD A-Series")]
            A_Series,
            [XafDisplayName("AMD FX")]
            FX
        }

        public enum CPUGeneration
        {
            [XafDisplayName("1st Gen")]
            FirstGen,
            [XafDisplayName("2nd Gen")]
            SecondGen,
            [XafDisplayName("3rd Gen")]
            ThirdGen,
            [XafDisplayName("4th Gen")]
            FourthGen,
            [XafDisplayName("5th Gen")]
            FifthGen,
            [XafDisplayName("6th Gen")]
            SixthGen,
            [XafDisplayName("7th Gen")]
            SeventhGen,
            [XafDisplayName("8th Gen")]
            EighthGen,
            [XafDisplayName("9th Gen")]
            NinthGen,
            [XafDisplayName("10th Gen")]
            TenthGen,
            [XafDisplayName("11th Gen")]
            EleventhGen,
            [XafDisplayName("12th Gen")]
            TwelfthGen
        }
        public enum SSDType
        {
            [XafDisplayName("2.5 inch")]
            Type_25inch,
            [XafDisplayName("M.2")]
            Type_M2,
            [XafDisplayName("mSATA")]
            Type_mSATA,
            [XafDisplayName("M.2 NVMe")]
            Type_M2_NVMe,
            [XafDisplayName("M.2 SATA")]
            Type_M2_SATA,
            [XafDisplayName("M.2 PCIe")]
            Type_M2_PCIe,

        }

        public enum MacBookModel
        {
            [XafDisplayName("MacBook Air")]
            MacBookAir,
            [XafDisplayName("MacBook Pro")]
            MacBookPro,
            [XafDisplayName("MacBook")]
            MacBook,
            [XafDisplayName("MacBook Mini")]
            MacBookMini
        }

        public enum MacBookCpuType
        {
            [XafDisplayName("Intel Core i3")]
            IntelCorei3,
            [XafDisplayName("Intel Core i5")]
            IntelCorei5,
            [XafDisplayName("Intel Core i7")]
            IntelCorei7,
            [XafDisplayName("Intel Core i9")]
            IntelCorei9,
            [XafDisplayName("AppleSilicon M1")]
            AppleSiliconM1,
            [XafDisplayName("AppleSilicon M1 Pro")]
            AppleSiliconM1Pro,
            [XafDisplayName("AppleSilicon M1 Max")]
            AppleSiliconM1Max,
            [XafDisplayName("AppleSilicon M1 Ultra")]
            AppleSiliconM1Ultra,
            [XafDisplayName("AppleSilicon M2")]
            AppleSiliconM2,
        }

        public enum ScreenSize
        {
            [XafDisplayName("13 Inch")]
            ThirteenInch,
            [XafDisplayName("15 Inch")]
            FourteenInch,
            [XafDisplayName("16 Inch")]
            SixteenInch
        }
        public enum MacBookColor
        {
            [XafDisplayName("SILVER")]
            Silver,
            [XafDisplayName("SPACEGRAY")]
            SpaceGray,
            [XafDisplayName("GOLD")]
            Gold
        }
        public enum MacBookYear
        {
            [XafDisplayName("2010")]
            Year2010,
            [XafDisplayName("2011")]
            Year2011,
            [XafDisplayName("2012")]
            Year2012,
            [XafDisplayName("2013")]
            Year2013,
            [XafDisplayName("2014")]
            Year2014,
            [XafDisplayName("2015")]
            Year2015,
            [XafDisplayName("2016")]
            Year2016,
            [XafDisplayName("2017")]
            Year2017,
            [XafDisplayName("2018")]
            Year2018,
            [XafDisplayName("2019")]
            Year2019,
            [XafDisplayName("2020")]
            Year2020,
            [XafDisplayName("2021")]
            Year2021,
            [XafDisplayName("2022")]
            Year2022,
            [XafDisplayName("2023")]
            Year2023
        }

        public enum IPhoneModel
        {
            [XafDisplayName("iPhone SE")]
            iPhoneSE,
            [XafDisplayName("iPhone 6")]
            iPhone6,
            [XafDisplayName("iPhone 6+")]
            iPhone6Plus,
            [XafDisplayName("iPhone 6S")]
            iPhone6S,
            [XafDisplayName("iPhone 6S+")]
            iPhone6SPlus,
            [XafDisplayName("iPhone 7")]
            iPhone7,
            [XafDisplayName("iPhone 7+")]
            iPhone7Plus,
            [XafDisplayName("iPhone 8")]
            iPhone8,
            [XafDisplayName("iPhone 8+")]
            iPhone8Plus,
            [XafDisplayName("iPhone X")]
            iPhoneX,
            [XafDisplayName("iPhone XR")]
            iPhoneXR,
            [XafDisplayName("iPhone XS")]
            iPhoneXS,
            [XafDisplayName("iPhone XS Max")]
            iPhoneXSMax,
            [XafDisplayName("iPhone 11")]
            iPhone11,
            [XafDisplayName("iPhone 11 Pro")]
            iPhone11Pro,
            [XafDisplayName("iPhone 11 Pro Max")]
            iPhone11ProMax,
            [XafDisplayName("iPhone SE 2")]
            iPhoneSE2,
            [XafDisplayName("iPhone 12 Mini")]
            iPhone12Mini,
            [XafDisplayName("iPhone 12")]
            iPhone12,
            [XafDisplayName("iPhone 12 Pro")]
            iPhone12Pro,
            [XafDisplayName("iPhone 12 Pro Max")]
            iPhone12ProMax,
            [XafDisplayName("iPhone 13 Mini")]
            iPhone13Mini,
            [XafDisplayName("iPhone 13")]
            iPhone13,
            [XafDisplayName("iPhone 13 Pro")]
            iPhone13Pro,
            [XafDisplayName("iPhone 13 Pro Max")]
            iPhone13ProMax,
            [XafDisplayName("iPhone 14")]
            iPhone14,
            [XafDisplayName("iPhone 14 Pro")]
            iPhone14Pro,
            [XafDisplayName("iPhone 14 Pro Max")]
            iPhone14ProMax,
            [XafDisplayName("iPhone 15")]
            iPhone15,
            [XafDisplayName("iPhone 15 Pro")]
            iPhone15Pro,
            [XafDisplayName("iPhone 15 Pro Max")]
            iPhone15ProMax
        }

        public enum iPhoneColor
        {
            [XafDisplayName("SILVER")]
            Silver,
            [XafDisplayName("SPACEGRAY")]
            SpaceGray,
            [XafDisplayName("GOLD")]
            Gold,
            [XafDisplayName("ROSEGOLD")]
            RoseGold,
            [XafDisplayName("MIDNIGHTGREEN")]
            MidnightGreen,
            [XafDisplayName("BLACK")]
            PacificBlue,
            [XafDisplayName("RED")]
            ProductRed
        }

        public enum RAMSize
        {

            [XafDisplayName("1 GB")]
            Size_1GB,
            [XafDisplayName("2 GB")]
            Size_2GB,
            [XafDisplayName("4 GB")]
            Size_4GB,
            [XafDisplayName("8 GB")]
            Size_8GB,
            [XafDisplayName("16 GB")]
            Size_16GB,
            [XafDisplayName("32 GB")]
            Size_32GB,
            [XafDisplayName("64 GB")]
            Size_64GB
        }


    }
}
