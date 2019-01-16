﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SBD.GL.Module.BusinessObjects.Imports
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class BankImportController : ViewController
    {
        public BankImportController()
        {
            InitializeComponent();
            TargetObjectType = typeof(BankImport);

            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void actApplyRules_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            

            var bankImport = e.CurrentObject as BankImport;

            HandyFunctions.RunBankRules(bankImport,View.ObjectSpace);
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

  
      

        private void actClearAccounts_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var import = View.CurrentObject as BankImport;
            foreach (var line in import.Lines)
            {
                line.Account = null;
                View.ObjectSpace.ModifiedObjects.Add(line);
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void actPost_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var bankImport = e.CurrentObject as BankImport;

            foreach (var line in bankImport.Lines.Where(x => x.MatchingHeader == null && x.Account != null))
            {
                var header = View.ObjectSpace.CreateObject<TranHeader>();
                header.Date = line.Date;
                header.LinkedAccount = View.ObjectSpace.GetObject(bankImport.Account);
                var tran = View.ObjectSpace.CreateObject<Transaction>();
                tran.Account = View.ObjectSpace.GetObject(line.Account);
                tran.HiddenAccount = header.LinkedAccount;
                tran.Amount = line.Amount;
                tran.TranHeader = header;
                tran.Memo = line.Note;
                header.Transactions.Add(tran);
                var lineobj = View.ObjectSpace.GetObject(line);
                lineobj.MatchingHeader = header;
                View.ObjectSpace.ModifiedObjects.Add(lineobj);
                line.MatchingHeader = header;
                View.ObjectSpace.ModifiedObjects.Add(header);
                View.ObjectSpace.ModifiedObjects.Add(tran);
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
    }
}
