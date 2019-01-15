﻿using System;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;

using DevExpress.DataAccess;
 
using DevExpress.XtraReports.Configuration;
 

namespace SBD.GL.Module.Reports.PandL
{
    public partial class PandLReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PandLReport()
        {
            InitializeComponent();
            BindToData();
        }
        private void BindToData()
        {
            if ((DateTime)Parameters[0].Value == DateTime.MinValue)
            {
                 var date = DateTime.Today;
                 var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                 Parameters[0].Value = firstDayOfMonth;
            }

            if ((DateTime) Parameters[1].Value == DateTime.MinValue)
            {
                Parameters[1].Value = DateTime.Today;
            }

            var fromDate = (DateTime) this.Parameters[0].Value;
            var toDate = (DateTime) this.Parameters[1].Value  ;
            var results = PandLReportData.PandL(fromDate, toDate);
            DataSource = results;
        }
    }
}
