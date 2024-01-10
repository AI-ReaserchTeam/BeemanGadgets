using BeemanGadgets.Module.BusinessObjects;
using BeemanGadgets.Module.BusinessObjects.Inventory;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Blazor.Components;
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
    public partial class RecieveInventoryController : ObjectViewController<ListView, InventoryBase>
    {

        private readonly SimpleAction recieveInventoryAction;

        public RecieveInventoryController()
        {
            InitializeComponent();

            recieveInventoryAction = new SimpleAction(this, "RecieveInventory", PredefinedCategory.Edit)
            {
                Caption = "Recieve",
                ImageName = "NextComment",
                TargetObjectType = typeof(InventoryBase),
                TargetObjectsCriteria =  "Status = 'Moved'",

            };

            recieveInventoryAction.Execute += RecieveInventoryAction;
        }
        protected override void OnActivated()
        {
            base.OnActivated();


            if (SecuritySystem.CurrentUser is BeemanAppUser user)
            {
                recieveInventoryAction.Active.SetItemValue("Location", user.Location == UserLocation.Distributor);
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

            var movedObjects = View.ObjectSpace.GetObjects<InventoryBase>(CriteriaOperator.Parse("Status = ?", Status.Moved)).ToList();

            if (movedObjects.Any())
            {
                foreach (var item in movedObjects)
                {
                    item.Status = Status.Recieved;
                    item.Location = Location.Distributor;
                    item.WareHouse = null;
                }
                ObjectSpace.CommitChanges();
                message = $"Items {movedObjects.Count} Received Successfully";
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
            var ItemsReadyToBeRecieved = View.ObjectSpace.GetObjects<T>(CriteriaOperator.Parse("Status = ?", Status.Moved));
            bool condition = ItemsReadyToBeRecieved.Any() && (SecuritySystem.CurrentUser is BeemanAppUser user) && (user.Location == UserLocation.Distributor);

            if (condition)
            { 
                recieveInventoryAction.Enabled.SetItemValue("Location", ItemsReadyToBeRecieved.Any());
                Application.ShowViewStrategy.ShowMessage($"There are {ItemsReadyToBeRecieved.Count} {Item} ready to be receiveded", InformationType.Info);
            }

        }


    }
}
