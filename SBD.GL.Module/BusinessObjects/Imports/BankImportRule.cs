﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using SBD.GL.Module.Annotations;
using SBD.GL.Module.BusinessObjects.Accounts;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    [NavigationItem("02 Imports")]
    [XafDisplayName("Bank Import Rules")]
    [ImageName("BO_WorkflowDefinition")]
    public class BankImportRule: BasicBo, IObjectSpaceLink, INotifyPropertyChanged 
    {
        [Key]
        public int Id { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
        [Required]
        public virtual Account FromAccount { get; set; }

        private Account _toAccount;

        [Required]
      //  [ImmediatePostData]
        public virtual Account ToAccount
        {
            get => _toAccount;
            set
            {
                _toAccount = value;
                OnPropertyChanged();
            }

        }

        public string RuleName { get; set; }
        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }


        [Browsable(false)]
        [RuleFromBoolProperty("RuleNameOk", DefaultContexts.Save, "Enter a Rule Name.")]
        public bool RuleNameOk => RuleName.Length > 0;

        [NotMapped]
        public bool ApplyOnAdd { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override void OnSaving()
        {
            if (ApplyOnAdd)
            {
                
            }

            base.OnSaving();
        }
    }
}