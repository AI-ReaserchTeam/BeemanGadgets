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
    public partial class DistributeInventoriesController : ObjectViewController<ListView, InventoryBase>
    {
        private readonly SingleChoiceAction distributeItemAction;

        public DistributeInventoriesController()
        {
            InitializeComponent();

            distributeItemAction = new SingleChoiceAction(this, "Distribute", PredefinedCategory.Edit)
            {
                Caption = "Distribute",
                ImageName = "Actions_Send",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
                ItemType = SingleChoiceActionItemType.ItemIsOperation,
                TargetObjectsCriteria = "Status = 'Recieved'",
                ShowItemsOnClick = true
            };
            distributeItemAction.Execute += DistributeItemAction_Execute;
        }

        private void DistributeItemAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var objects = View.SelectedObjects.Cast<InventoryBase>().ToList();
            if (objects.Any())
                if (objects.Any())
                {
                    foreach (var item in objects)
                    {
                        item.DistributedBy = (SecuritySystem.CurrentUser as BeemanAppUser).Name;
                        item.Shop = e.SelectedChoiceActionItem.Data as BeemanAppUser;
                        item.Status = Status.Distributed;
                        item.DistributedDate = DateTime.Now;
                        item.CreatedAt = null;
                        item.Save();
                    }
                    ObjectSpace.CommitChanges();
                    string message = $"Items {objects.Count} Distributed to {(e.SelectedChoiceActionItem.Data as BeemanAppUser).Name} Successfully";
                    Application.ShowViewStrategy.ShowMessage(message, InformationType.Success);
                }

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            distributeItemAction.Items.Clear();

            if (SecuritySystem.CurrentUser is BeemanAppUser user)
            {
                distributeItemAction.Active.SetItemValue("Location", user.Location == UserLocation.Distributor);
            }

            var users = ObjectSpace.GetObjects<BeemanAppUser>(CriteriaOperator.Parse("Location = ?", UserLocation.Shop));

            if (users.Any())
            {
                foreach (var appuser in users)
                {
                    if (appuser.Oid != (SecuritySystem.CurrentUser as BeemanAppUser).Oid)
                    {
                        distributeItemAction.Items.Add(new ChoiceActionItem(appuser.Name, appuser));
                    }
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            distributeItemAction.Execute -= DistributeItemAction_Execute;
        }
    }
}
