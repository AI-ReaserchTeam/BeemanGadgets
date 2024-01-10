using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
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
    
    public class BeemanRole : PermissionPolicyRoleBase, IPermissionPolicyRoleWithUsers
    { 
        public BeemanRole(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        [Association("AppUsers-BeemanRoles")]
        public XPCollection<BeemanAppUser> AppUsers
        {
            get
            {
                return GetCollection<BeemanAppUser>(nameof(AppUsers));
            }
        }
        IEnumerable<IPermissionPolicyUser> IPermissionPolicyRoleWithUsers.Users
        {
            get { return AppUsers.OfType<IPermissionPolicyUser>(); }
        }
    }
}