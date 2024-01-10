using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using BeemanGadgets.Module.BusinessObjects;
using BeemanGadgets.Module.BusinessObjects.Inventory;

namespace BeemanGadgets.Module.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater {
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
        base(objectSpace, currentDBVersion) {
    }
    public override void UpdateDatabaseAfterUpdateSchema() {
        base.UpdateDatabaseAfterUpdateSchema();
       
        CreateDefaultRole();
        CreateDistributorRole();
        CreateShopRole();
        CreateWarehouseRole();


        BeemanAppUser userAdmin = ObjectSpace.FirstOrDefault<BeemanAppUser>(u => u.UserName == "Francis");
        if(userAdmin == null) {
            userAdmin = ObjectSpace.CreateObject<BeemanAppUser>();
            userAdmin.UserName = "Francis";
            userAdmin.Location = UserLocation.Admin;
            userAdmin.ChangePasswordOnFirstLogon = true;
            // Set a password if the standard authentication type is used
            userAdmin.SetPassword("");

            // The UserLoginInfo object requires a user object Id (Oid).
            // Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
            ObjectSpace.CommitChanges(); //This line persists created object(s).
            ((ISecurityUserWithLoginInfo)userAdmin).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin));
        }
		// If a role with the Administrators name doesn't exist in the database, create this role
        BeemanRole adminRole = ObjectSpace.FirstOrDefault<BeemanRole>(r => r.Name == "Administrators");
        if(adminRole == null) {
            adminRole = ObjectSpace.CreateObject<BeemanRole>();
            adminRole.Name = "Administrators";
        }
        adminRole.IsAdministrative = true;
		userAdmin.BeemanRoles.Add(adminRole);
        ObjectSpace.CommitChanges();

    }
    public override void UpdateDatabaseBeforeUpdateSchema() {
        base.UpdateDatabaseBeforeUpdateSchema();
        //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        //}
    }
    private void CreateDefaultRole() {
        BeemanRole defaultRole = ObjectSpace.FirstOrDefault<BeemanRole>(role => role.Name == "DefaultRole");
        if(defaultRole == null) {
            defaultRole = ObjectSpace.CreateObject<BeemanRole>();
            defaultRole.Name = "DefaultRole";

			defaultRole.AddObjectPermissionFromLambda<BeemanAppUser>(SecurityOperations.Read, cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
			defaultRole.AddMemberPermissionFromLambda<BeemanAppUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
			defaultRole.AddMemberPermissionFromLambda<BeemanAppUser>(SecurityOperations.Write, "StoredPassword", cm => cm.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<BeemanRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
            defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
			defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
            defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
        }
        
        ObjectSpace.CommitChanges();
    }


    public void CreateShopRole()
    {
        BeemanRole shopRole = ObjectSpace.FirstOrDefault<BeemanRole>(role => role.Name == "ShopRole");

        if (shopRole == null)
        {

            shopRole = ObjectSpace.CreateObject<BeemanRole>();
            shopRole.Name = "ShopRole";


            shopRole.AddObjectPermissionFromLambda<BeemanAppUser>(SecurityOperations.Read, item => item.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            shopRole.AddObjectPermissionFromLambda<InventoryBase>(SecurityOperations.Read, item => item.Shop.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            shopRole.AddObjectPermissionFromLambda<Iphone>(SecurityOperations.Read, item => item.Shop.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            shopRole.AddObjectPermissionFromLambda<MacBook>(SecurityOperations.Read, item => item.Shop.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            shopRole.AddObjectPermissionFromLambda<PC>(SecurityOperations.Read, item => item.Shop.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            shopRole.AddObjectPermissionFromLambda<RAM>(SecurityOperations.Read, item => item.Shop.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            shopRole.AddObjectPermissionFromLambda<SSD>(SecurityOperations.Read, item => item.Shop.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
        }

        ObjectSpace.CommitChanges();
    }



    public void CreateWarehouseRole()
    {

        BeemanRole warehouseRole = ObjectSpace.FirstOrDefault<BeemanRole>(role => role.Name == "WarehouseRole");

        if (warehouseRole == null)
        {
            warehouseRole = ObjectSpace.CreateObject<BeemanRole>();
            warehouseRole.Name = "WarehouseRole";
            warehouseRole.AddObjectPermissionFromLambda<BeemanAppUser>(SecurityOperations.Read, user => user.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<InventoryBase>(SecurityOperations.ReadWriteAccess, item => item.WareHouse.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<Iphone>(SecurityOperations.ReadWriteAccess, item => item.WareHouse.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<MacBook>(SecurityOperations.ReadWriteAccess, item => item.WareHouse.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<PC>(SecurityOperations.ReadWriteAccess, item => item.WareHouse.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<RAM>(SecurityOperations.ReadWriteAccess, item => item.WareHouse.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<SSD>(SecurityOperations.ReadWriteAccess, item => item.WareHouse.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);

        }

        ObjectSpace.CommitChanges();

    }

    public void CreateDistributorRole()
    {

        BeemanRole warehouseRole = ObjectSpace.FirstOrDefault<BeemanRole>(role => role.Name == "DistributorRole");

        if (warehouseRole == null)
        {
            warehouseRole = ObjectSpace.CreateObject<BeemanRole>();
            warehouseRole.Name = "DistributorRole";

            warehouseRole.AddObjectPermissionFromLambda<BeemanAppUser>(SecurityOperations.Read, user => user.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<InventoryBase>(SecurityOperations.Read, item => item.Distributor.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<Iphone>(SecurityOperations.Read, item => item.Distributor.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<MacBook>(SecurityOperations.Read, item => item.Distributor.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<PC>(SecurityOperations.Read, item => item.Distributor.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<RAM>(SecurityOperations.Read, item => item.Distributor.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);
            warehouseRole.AddObjectPermissionFromLambda<SSD>(SecurityOperations.Read, item => item.Distributor.Oid == (Guid)CurrentUserIdOperator.CurrentUserId(), SecurityPermissionState.Allow);

        }

        ObjectSpace.CommitChanges();
    }


}
