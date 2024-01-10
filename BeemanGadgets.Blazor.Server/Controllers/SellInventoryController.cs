using BeemanGadgets.Module.BusinessObjects;
using BeemanGadgets.Module.BusinessObjects.Inventory;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeemanGadgets.Blazor.Server.Controllers
{
    public partial class SellInventoryController : ObjectViewController<ListView, InventoryBase>
    {
        private readonly SimpleAction sellInventoryAction;
       
        public SellInventoryController()
        {
            InitializeComponent();
            sellInventoryAction = new SimpleAction(this, "SellInventory", PredefinedCategory.Edit)
            {
                Caption = "Sell",
                ImageName = "Actions_Send",
                TargetObjectType = typeof(InventoryBase),
                TargetObjectsCriteria = "Status = 'InStock'",
            };

            sellInventoryAction.Execute += SellInventoryAction;
            
        }

        private void SellInventoryAction(object sender, SimpleActionExecuteEventArgs e)
        {
            var objects = View.SelectedObjects.Cast<InventoryBase>().ToList();
            if (objects.Any())
            {
                foreach (var item in objects)
                {
                    item.Soldby = (SecuritySystem.CurrentUser as BeemanAppUser).Name;
                    item.Status = Status.Sold;
                    item.SoldDate = DateTime.Now;   
                    item.Save();
                }
                ObjectSpace.CommitChanges();
                string message = $"Items {objects.Count} Sold Successfully";
                Application.ShowViewStrategy.ShowMessage(message, InformationType.Success);
            }
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (SecuritySystem.CurrentUser is BeemanAppUser user)
            {
                sellInventoryAction.Active.SetItemValue("Location", user.Location == UserLocation.Shop);
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            sellInventoryAction.Execute -= SellInventoryAction;
        }
    }
}
