using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BeemanGadgets.Module.BusinessObjects
{
    [DefaultClassOptions]
    [ImageName("BO_User")]
    [DefaultProperty(nameof(Name))]
    [ObjectCaptionFormat("{0:Name}")]
    public class BeemanAppUser : BaseObject, ISecurityUser, IAuthenticationStandardUser, IAuthenticationActiveDirectoryUser, ISecurityUserWithRoles, IPermissionPolicyUser, ICanInitialize, ISecurityUserWithLoginInfo
    {
        public BeemanAppUser(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            changePasswordOnFirstLogon = true;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (Session.IsNewObject(this))
            {
               switch (Location)
                {
                    case UserLocation.Admin:
                        BeemanRole adminRole = AssignRole("Administrators");
                        BeemanRoles.Add(adminRole);
                        break;
                    case UserLocation.Warehouse:
                        BeemanRole warehouseRole = AssignRole("WarehouseRole");
                        BeemanRoles.Add(AssignRole("DefaultRole"));
                        BeemanRoles.Add(warehouseRole);
                        break;
                    case UserLocation.Shop:
                        BeemanRole shopRole = AssignRole("ShopRole");
                        BeemanRoles.Add(AssignRole("DefaultRole"));
                        BeemanRoles.Add(shopRole);
                        break;
                    case UserLocation.Distributor:
                        BeemanRole distributorRole = AssignRole("DistributorRole");
                        BeemanRoles.Add(AssignRole("DefaultRole"));
                        BeemanRoles.Add(distributorRole);
                        break;
                    default:
                        break;
                }

            }
        }

        #region ISecurityUser Members
        private bool isActive = true;
        [CaptionsForBoolValues("Active", "Inactive")]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        public bool IsActive
        {
            get { return isActive; }
            set { SetPropertyValue(nameof(IsActive), ref isActive, value); }
        }
        private string userName = String.Empty;
        [RuleRequiredField("AppUser UserNameRequired", DefaultContexts.Save)]
        [RuleRegularExpression("AppUser UserNameRegEx", DefaultContexts.Save, @"^[a-zA-Z0-9_.+-]+@beemangadgets.com",  messageTemplate:"Please provide a username with '@beemangadgets.com'") ]
        [RuleUniqueValue("EmployeeUserNameIsUnique", DefaultContexts.Save,
            "The login with the entered user name was already registered within the system.")]
        public string UserName
        {
            get { return userName; }
            set { SetPropertyValue(nameof(UserName), ref userName, value); }
        }
        #endregion
        #region IAuthenticationStandardUser Members
        private bool changePasswordOnFirstLogon;
        [VisibleInListView(false)]
        [ImagesForBoolValues("Action_Grant", "Action_Deny")]
        [CaptionsForBoolValues("Yes", "No")]
        public bool ChangePasswordOnFirstLogon
        {
            get { return changePasswordOnFirstLogon; }
            set
            {
                SetPropertyValue(nameof(ChangePasswordOnFirstLogon), ref changePasswordOnFirstLogon, value);
            }
        }
        private string storedPassword;
        [Browsable(false), Size(SizeAttribute.Unlimited), Persistent, SecurityBrowsable]
        protected string StoredPassword
        {
            get { return storedPassword; }
            set { storedPassword = value; }
        }
        public bool ComparePassword(string password)
        {
            return PasswordCryptographer.VerifyHashedPasswordDelegate(this.storedPassword, password);
        }
        public void SetPassword(string password)
        {
            this.storedPassword = PasswordCryptographer.HashPasswordDelegate(password);
            OnChanged(nameof(StoredPassword));
        }
        #endregion
        #region ISecurityUserWithRoles Members
        IList<ISecurityRole> ISecurityUserWithRoles.Roles
        {
            get
            {
                IList<ISecurityRole> result = new List<ISecurityRole>();
                foreach (BeemanRole role in BeemanRoles)
                {
                    result.Add(role);
                }
                return result;
            }
        }
        [Association("AppUsers-BeemanRoles")]
        //[RuleRequiredField("EmployeeRoleIsRequired", DefaultContexts.Save,
        //    TargetCriteria = "IsActive",
        //    CustomMessageTemplate = "An active BeemanUser must have at least one role assigned")]
        public XPCollection<BeemanRole> BeemanRoles
        {
            get
            {
                return GetCollection<BeemanRole>(nameof(BeemanRoles));
            }
        }
        #endregion
        #region IPermissionPolicyUser Members
        IEnumerable<IPermissionPolicyRole> IPermissionPolicyUser.Roles
        {
            get { return BeemanRoles.OfType<IPermissionPolicyRole>(); }
        }
        #endregion
        #region ICanInitialize Members
        void ICanInitialize.Initialize(IObjectSpace objectSpace, SecurityStrategyComplex security)
        {
            BeemanRole newUserRole = (BeemanRole)objectSpace.FirstOrDefault<BeemanRole>(role => role.Name == security.NewUserRoleName);
            if (newUserRole == null)
            {
                newUserRole = objectSpace.CreateObject<BeemanRole>();
                newUserRole.Name = security.NewUserRoleName;
                newUserRole.IsAdministrative = true;
                newUserRole.AppUsers.Add(this);
            }
        }
        #endregion
        #region ISecurityUserWithLoginInfo Members


        [Browsable(false)]
        [DevExpress.Xpo.Aggregated, Association("User-LoginInfo")]
        public XPCollection<BeemanLoginInfo> LoginInfo
        {
            get { return GetCollection<BeemanLoginInfo>(nameof(LoginInfo)); }
        }

        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
        {
            BeemanLoginInfo result = new BeemanLoginInfo(Session);
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }
        #endregion



        UserLocation? location;
        string phone;
        string name;


        [RuleRequiredField]
        [DevExpress.Xpo.DisplayName("WareHouse|Shop")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        [RuleRequiredField]
        [ModelDefault("EditMask", "000 0000 0000")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Phone
        {
            get => phone;
            set => SetPropertyValue(nameof(Phone), ref phone, value);
        }

        [RuleRequiredField]
        public UserLocation? Location
        {
            get => location;
            set => SetPropertyValue(nameof(Location), ref location, value);
        }

        public BeemanRole AssignRole(string role)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("Name=?", role);
            BeemanRole beemanrole =  Session.FindObject<BeemanRole>(criteria);
            return beemanrole;
        }


    }

    public enum UserLocation
    {
        Admin,
        Warehouse,
        Shop,
        Distributor
    }
}