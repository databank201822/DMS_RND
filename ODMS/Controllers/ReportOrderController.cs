using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ODMS.Models;

namespace ODMS.Controllers
{
    [SessionExpire]
    public class ReportOrderController : Controller
    {
        public ODMSEntities Db = new ODMSEntities();
        public ODMSBIEntities Dbbi = new ODMSBIEntities();
        Supporting sp = new Supporting();




        public ActionResult RptOutletWiseOrder()
        {
            return View("OutletWiseOrder/RptOutletWiseOrder");
        }

        [HttpPost]
        public ActionResult RptOutletWiseOrderFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate, int reportType)
        {
            string dbids = sp.Dbids(rsMid, asMid, cEid, id);
            string skulist = null;
            if (skuIds != null)
            {
              
                skulist =string.Join(",", skuIds); 
            }

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };

            List<RPT_Order_OutletWiseSKUWiseOrder_Result> outletOrder = Dbbi
                .RPT_Order_OutletWiseSKUWiseOrder(startDate, endDate, dbids, skulist)
                .ToList();

            if (reportType == 1)  //Summery
            {
                reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Order\RPT_OutletWiseOrder_Symmary.rdlc");
            }
            else if (reportType == 2) //Details
            {
                reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Order\RPT_OutletWiseOrder_Details.rdlc");
            }

            ReportDataSource rdc = new ReportDataSource("OutletOrder", outletOrder);

            reportViewer.LocalReport.DataSources.Add(rdc);
            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;

            return PartialView("OutletWiseOrder/RptOutletWiseOrderFilter");
        }

        public ActionResult RptPsrWiseSkuWiseOrder()
        {
            return View("PsrWiseSkuWiseOrder/RptPsrWiseSkuWiseOrder");
        }

        [HttpPost]

        public ActionResult RptOrderPsrWiseSkuWiseOrderFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate, int reportType)
        {

            string dbids = sp.Dbids(rsMid, asMid, cEid, id);
            string skulist = null;
            if (skuIds != null)
            {

                skulist = string.Join(",", skuIds);
            }

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };
          
            ReportParameter rp2 = null;
            List<RPT_Order_PSRWiseSKUWiseOrder_Result> psrskuorder = Dbbi.RPT_Order_PSRWiseSKUWiseOrder(startDate, endDate, dbids, skulist).ToList();
            if (reportType == 1)  //Summery
            {
                reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Order\RPT_Order_PsrWiseSkuWiseOrderSummary.rdlc");

             

                rp2 = new ReportParameter("ReportNameParameter", "PSR SKU Wise Order [102] Summary");
            }
            else if (reportType == 2) //Details
            {

                reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Order\RPT_Order_PsrWiseSkuWiseOrderDetails.rdlc");
                rp2 = new ReportParameter("ReportNameParameter", "PSR SKU Wise Order [102] Details");
            } 
              ReportDataSource rdc = new ReportDataSource("PSRSKUORDER", psrskuorder);

            ReportParameter rp1 = new ReportParameter("DateParameter", startDate.ToString("dd-MMM-yyy") + " TO" + endDate.ToString("dd-MMM-yyy"));


            reportViewer.LocalReport.SetParameters(new[] { rp1, rp2 });

            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("PsrWiseSkuWiseOrder/RptPsrWiseSkuWiseOrderFilter");
        }



    }
}