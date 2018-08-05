using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ODMS.Models;

namespace ODMS.Controllers
{
    [SessionExpire]
    public class DeliveryReportController : Controller
    {
    public ODMSEntities Db = new ODMSEntities();
     Supporting sp = new Supporting();
        public ActionResult RptPsrWiseSkuWiseDelivery()
        {
            return View("PsrWiseSkuWiseDelivery/RptPsrWiseSkuWiseDelivery");
        }

        [HttpPost]

        public ActionResult RptPsrWiseSkuWiseDeliveryFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, DateTime startDate, DateTime endDate)
        {
            HashSet<int> dbids = sp.Alldbids(rsMid, asMid, cEid, id);

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };


            List<RPT_Delivery_PSRWiseSKUWiseDelivery_Result> psrskudelivery = Db.RPT_Delivery_PSRWiseSKUWiseDelivery(startDate, endDate).Where(x => dbids.Contains(x.DB_Id)).ToList();

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Delivery\RPT_PsrWiseSkuWiseDelivery.rdlc";

            ReportDataSource rdc = new ReportDataSource("PSRSKUDELIVERY", psrskudelivery);

            ReportParameter rp1 = new ReportParameter("DateParameter", startDate.ToString("dd-MMM-yyy") + " TO" + endDate.ToString("dd-MMM-yyy"));
            ReportParameter rp2 = new ReportParameter("ReportNameParameter", "PSR SKU Wise Delivery [202]");

            reportViewer.LocalReport.SetParameters(new[] { rp1, rp2 });

            reportViewer.LocalReport.DataSources.Add(rdc);
            
            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("PsrWiseSkuWiseDelivery/RptPsrWiseSkuWiseDeliveryFilter");
        }


        public ActionResult RptDeliveryKpi()
        {
            return View("kpi/RptDeliveryKpi");
        }

        [HttpPost]

        public ActionResult RptDeliveryKpiFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, DateTime startDate, DateTime endDate)
        {
            HashSet<int> dbids = sp.Alldbids(rsMid, asMid, cEid, id);

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };


            List<RPT_Delivery_PSRPerformanceKPISummary_Result> psrkpisummary = Db.RPT_Delivery_PSRPerformanceKPISummary(startDate, endDate).Where(x => dbids.Contains(x.DB_Id)).ToList();

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Delivery\RPT_PSRKPISummary.rdlc";

            ReportDataSource rdc = new ReportDataSource("PSRKPISUMMARY", psrkpisummary);

            ReportParameter rp1 = new ReportParameter("DateParameter", startDate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));
            ReportParameter rp2 = new ReportParameter("ReportNameParameter", "PSR Wise KPI [203]");

            reportViewer.LocalReport.SetParameters(new[] { rp1, rp2 });

            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("Kpi/RptDeliveryKpiFilter");
        }
    }
    }
