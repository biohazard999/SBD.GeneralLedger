﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    public class BankRuleActionInPopupController : ViewController
    {
        public BankRuleActionInPopupController()
        {
            //Dennis: Refer to the http://documentation.devexpress.com/#Xaf/CustomDocument2815 help article to see how to reorder Actions within the PopupActions container.

            TargetObjectType = typeof(BankImportRule);
            SetUpActions();
        }

        private void SetUpActions()
        {
            var actionInPopupAddAssetAccount = new SimpleAction(this, "addAssetAccount", PredefinedCategory.PopupActions);
            actionInPopupAddAssetAccount.Execute += ActionAddAsset_Execute;

            var actionInPopupAddLiabilityAccount = new SimpleAction(this, "addLiabilityAccount", PredefinedCategory.PopupActions);
            actionInPopupAddLiabilityAccount.Execute += ActionAddLiability_Execute;

            var actionInPopupAddExpenseAccount = new SimpleAction(this, "addExpenseAccount", PredefinedCategory.PopupActions);
            actionInPopupAddExpenseAccount.Execute += ActionAddExpense_Execute;

            var actionInPopupAddIncomeAccount = new SimpleAction(this, "addIncomeAccount", PredefinedCategory.PopupActions);
             
            actionInPopupAddIncomeAccount.Execute += ActionAddIncome_Execute;

            var actionInPopupAddCogsAccount = new SimpleAction(this, "addCogsAccount", PredefinedCategory.PopupActions);
            actionInPopupAddCogsAccount.Execute += ActionAddCogs_Execute;

            //var actionSaveAndApply = new SimpleAction(this, "saveAndApply", PredefinedCategory.PopupActions);
            //actionSaveAndApply.Execute += ActionSaveAndApply_Execute;

        }
        void ActionAddExpense_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Expense );
        }
        void ActionAddIncome_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Income );
        }

        void ActionAddAsset_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Asset );
        }
        void ActionAddLiability_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Liability );
        }
        void ActionAddCogs_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.CostOfSales);
        }

        //void ActionSaveAndApply_Execute(object sender, SimpleActionExecuteEventArgs e)
        //{
        //    BankRuleFunctions.SaveAndApplyRule(e.CurrentObject as BankImportRule);
        //    View.Close();
        //}
    }
}