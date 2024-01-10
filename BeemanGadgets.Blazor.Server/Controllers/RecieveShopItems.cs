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
    public partial class RecieveShopItems : ObjectViewController<ListView, InventoryBase>
    {
        private SimpleAction recieveInventoryAction;

        public RecieveShopItems()
        {
            InitializeComponent();

            recieveInventoryAction = new SimpleAction(this, "RecieveInventories", PredefinedCategory.Edit)
            {
                Caption = "RecieveItems",
                ImageName = "NextComment",
                TargetObjectType = typeof(InventoryBase),
                TargetObjectsCriteria = "Status = 'Distributed'",

            };

            recieveInventoryAction.Execute += RecieveInventoryAction;
        }
        protected override void OnActivated()
        {
            base.OnActivated();


            if (SecuritySystem.CurrentUser is BeemanAppUser user)
            {
                recieveInventoryAction.Active.SetItemValue("Location", user.Location == UserLocation.Shop);
            }


            switch (View.Caption)
            {
                case "iPhones":
                    CheckIfItemsReadyToReceive<Iphone>("iPhone(s)");
                    break;
                case "MacBooks":
                    CheckIfItemsReadyToReceive<MacBook>("MacBook(s)");
                    break;

                case "SSD":
                    CheckIfItemsReadyToReceive<SSD>("SSD(s)");
                    break;

                case "Hard Disks":
                    CheckIfItemsReadyToReceive<HardDisk>("HardDrive(s)");
                    break;
                case "Computers":
                    CheckIfItemsReadyToReceive<PC>("Computer(s)");
                    break;
                case "RAM":
                    CheckIfItemsReadyToReceive<RAM>("RAM(s)");
                    break;

                default:
                    break;
            }


        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            recieveInventoryAction.Execute -= RecieveInventoryAction;
        }


        private void RecieveInventoryAction(object sender, SimpleActionExecuteEventArgs e)
        {
            string message;

            var distributedObjects = View.ObjectSpace.GetObjects<InventoryBase>(CriteriaOperator.Parse("Status = ?", Status.Distributed)).ToList();
            if (distributedObjects.Any())
            {
                foreach (var item in distributedObjects)
                {
                    item.Status = Status.InStock;
                    item.Location = Location.Shop;
                    item.Distributor = null;
                }
                ObjectSpace.CommitChanges();
                message = $"{distributedObjects.Count} Item(s) Received Successfully";
                Application.ShowViewStrategy.ShowMessage(message, InformationType.Success);
            }
            else
            {
                message = $"No Items to Receive";
                Application.ShowViewStrategy.ShowMessage(message, InformationType.Warning);
            }


        }


        private void CheckIfItemsReadyToReceive<T>(string Item) where T : InventoryBase
        {

            var ItemsReadyToBeRecieved = View.ObjectSpace.GetObjects<T>(CriteriaOperator.Parse("Status = ?", Status.Distributed));
            bool condition = ItemsReadyToBeRecieved.Any() && (SecuritySystem.CurrentUser is BeemanAppUser user) && (user.Location == UserLocation.Shop);

            if (condition)
            {
                recieveInventoryAction.Enabled.SetItemValue("Location", ItemsReadyToBeRecieved.Any());
                Application.ShowViewStrategy.ShowMessage($"There are {ItemsReadyToBeRecieved.Count} {Item} ready to be received", InformationType.Info);
            }

        }


    }
}

