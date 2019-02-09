﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using SBD.GL.Module.BusinessObjects.Accounts;
using SBD.GL.Module.BusinessObjects.Transactions;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    public class BankImportLine : IObjectSpaceLink
    {
        [Key]
        public int Id { get; set; }

        public virtual BankImport  BankImport { get; set; }
        [Browsable(false)]
       
        public int? TranHeader_Id { get; set; }
        [VisibleInListView(true)]
        [ForeignKey("TranHeader_Id")]
        public virtual TranHeader MatchingHeader { get; set; }
        [VisibleInListView(true)]
        public string MatchError {
            get
            {
                try
                {
                    if (MatchingHeader == null)
                    {
                        return "";
                    }
                    if (MatchingHeader.LinkedAccount?.Id != BankImport.Account?.Id)
                    {
                        return "Matching Header Account has changed";
                    }

                    if (MatchingHeader.Transactions.Count > 1)
                    {
                        return "Multiple disbursements have been made";
                    }
                    if (MatchingHeader.Transactions.Count == 0)
                    {
                        return "No disbursements have been made";
                    }

                    return MatchingHeader.Transactions.FirstOrDefault().Account.Id != Account.Id ? "Disbursement account has been changed" : "";

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        [Browsable(false)]
        public string Ref1 { get; set; }
        [Browsable(false)]
        public string Ref2 { get; set; }
        [Browsable(false)]
        public string Ref3 { get; set; }
  
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
        public virtual Account Account { get; set; }
        public string Note { get; set; }
        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }
    }
}