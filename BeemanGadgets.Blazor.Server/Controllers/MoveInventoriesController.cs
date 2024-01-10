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
    public partial class MoveInventoriesController : ObjectViewController<ListView, InventoryBase>
    {
        private readonly SingleChoiceAction moveItemAction;

        public MoveInventoriesController()
        {
            InitializeComponent();

            TargetObjectType = typeof(InventoryBase);

            moveItemAction = new SingleChoiceAction(this, "Move", PredefinedCategory.Edit)
            {
                Caption = "Move",
                ImageName = "PreviousComment",
                SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects,
                ItemType = SingleChoiceActionItemType.ItemIsOperation,
                TargetObjectsCriteria = "Status = 'InStock'",
                ShowItemsOnClick = true
            };
            moveItemAction.Execute += MoveAction_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            moveItemAction.Items.Clear();

            if (SecuritySystem.CurrentUser is BeemanAppUser user)
            {
                moveItemAction.Active.SetItemValue("Location", user.Location == UserLocation.Warehouse);
            }


            var users = ObjectSpace.GetObjects<BeemanAppUser>(new BinaryOperator("Location", UserLocation.Warehouse) || new BinaryOperator("Location", UserLocation.Distributor));

            if (moveItemAction != null)
            {
                foreach (var appuser in users)
                {
                    if (appuser.Oid != (SecuritySystem.CurrentUser as BeemanAppUser).Oid)
                    {
                        moveItemAction.Items.Add(new ChoiceActionItem(appuser.UserName, appuser));
                    }
                }
            }

            View.Refresh();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            moveItemAction.Execute -= MoveAction_Execute;
        }

        private void MoveAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var objects = View.SelectedObjects.Cast<InventoryBase>().ToList();
            var message = string.Empty;

            if (objects != null)
            {
                if (e.SelectedChoiceActionItem.Data is BeemanAppUser selectedUser)
                {

                    foreach (var item in objects)
                    {
                        if (item.Distributor == null)
                        {
                            item.Distributor = selectedUser;
                            item.Location = Location.Distributor;
                            item.MovedDate = DateTime.Now;
                            item.Status = Status.Moved;
                        }
                        else
                        {
                            message = string.Format("You have already moved {0} item(s) to {1}", objects.Count, selectedUser.Name);
                            Application.ShowViewStrategy.ShowMessage(message, InformationType.Warning);
                            return;

                        }

                    }

                    message = string.Format("You have successfully moved {0} item(s) to {1}", objects.Count, selectedUser.Name);

                }

                ObjectSpace.CommitChanges();
                View.Refresh();
                Application.ShowViewStrategy.ShowMessage(message, InformationType.Success);

            }
        }
    }

}

