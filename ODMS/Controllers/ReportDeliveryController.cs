using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ODMS.Models;

namespace ODMS.Controllers
{
    [SessionExpire]
    public class ReportDeliveryController : Controller
    {
    public ODMSEntities Db = new ODMSEntities();
     Supporting sp = new Supporting();



     public ActionResult RptOutletWiseDelivery()
        {
            return View("OutletWiseDelivery/RptOutletWiseDelivery");
        }

        [HttpPost]
        public ActionResult RptOutletWiseDeliveryFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate, int reportType)
        {
            string dbids = sp.Dbids(rsMid, asMid, cEid, id);
            string skulist = null;
            if (skuIds != null)
            {

                skulist = string.Join(",", skuIds);
            }
            ReportViewer reportViewer = new ReportViewer
            {
                Visible = true,
                
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };

            List<RPT_Delivery_OutletWiseSKUWiseDelivery_Result> delivery = Db
                .RPT_Delivery_OutletWiseSKUWiseDelivery(startDate, endDate,dbids,skulist).ToList();

            ReportParameter rp2 = null;

            if (reportType == 1)  //Summery
            {
                reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Delivery\RPT_Delivery_OutletWiseDeliverySummery.rdlc");
                rp2 = new ReportParameter("ReportNameParameter", "Outlet Wise Delivery [102] Summary");
            }
            else if (reportType == 2) //Details
            {
                reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Delivery\RPT_Delivery_OutletWiseDeliveryDetails.rdlc");
                rp2 = new ReportParameter("ReportNameParameter", "Outlet SKU Wise Delivery [102]");
            }
            ReportParameter rp1 = new ReportParameter("DateParameter", startDate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));
            ReportDataSource rdc = new ReportDataSource("OutletDelivery", delivery);


            reportViewer.LocalReport.SetParameters(new[] { rp1, rp2 });

            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;

            return PartialView("OutletWiseDelivery/RptOutletWiseDeliveryFilter");
        }

        //202
        public ActionResult RptPsrWiseSkuWiseDelivery()
        {
            return View("PsrWiseSkuWiseDelivery/RptPsrWiseSkuWiseDelivery");
        }

        [HttpPost]

        public ActionResult RptPsrWiseSkuWiseDeliveryFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate, int reportType)
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
            List<RPT_Delivery_PSRWiseSKUWiseDelivery_Result> psrskudelivery = Db.RPT_Delivery_PSRWiseSKUWiseDelivery(startDate, endDate,dbids,skulist).ToList();
          
                if (reportType == 1)  //Summery
                {
                    reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Delivery\RPT_Delivery_PsrWiseSkuWiseDeliverySummary.rdlc");
                    rp2 = new ReportParameter("ReportNameParameter", "PSR SKU Wise Delivery [202] Summary");
                }
                else if (reportType == 2) //Details
                {
                    reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Delivery\RPT_Delivery_PsrWiseSkuWiseDeliveryDetails.rdlc");
                   rp2 = new ReportParameter("ReportNameParameter", "PSR SKU Wise Delivery [202] Details");
                }


                var rdc = new ReportDataSource("PSRDELIVERY", psrskudelivery);
          

            ReportParameter rp1 = new ReportParameter("DateParameter", startDate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));
          

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

        public ActionResult RptDeliveryKpiFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id,  DateTime startDate, DateTime endDate, int reportType, int parformerType)
        {
            string dbids = sp.Dbids(rsMid, asMid, cEid, id);
            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };
            ReportDataSource rdc = null;
            if (parformerType == 1) // by DB
            {
                if (reportType == 1) //DB Summery
                {
                    List<RPT_Delivery_DBPerformanceKPISummary_Result> dbkpi = Db
                        .RPT_Delivery_DBPerformanceKPISummary(startDate, endDate, dbids).ToList();

                    reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Delivery\RPT_KPIDBSummary.rdlc");
                    rdc = new ReportDataSource("DBKPI", dbkpi);

                }
                else if (reportType == 2) //DB BY day
                {
                  
                }
            }
            else
            if (parformerType == 2) // by PSR
            {
                if (reportType == 1) //PSR Summery
                {
                    List<RPT_Delivery_PSRPerformanceKPISummary_Result> psrkpisummary = Db
                        .RPT_Delivery_PSRPerformanceKPISummary(startDate, endDate, dbids).ToList();

                    reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Delivery\RPT_KPIPSRSummary.rdlc");
                    rdc = new ReportDataSource("PSRKPISUMMARY", psrkpisummary);

                }
                else if (reportType == 2) //BY Day
                {
                  
                }
            }
           

            ReportParameter rp1 = new ReportParameter("DateParameter", startDate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));
           

            reportViewer.LocalReport.SetParameters(new[] { rp1 });

            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;
            return PartialView("Kpi/RptDeliveryKpiFilter");


        }

        //204
        public ActionResult RptBuyer()
        {
            return View("RptBuyer/RptBuyer");
        }

        [HttpPost]

        public ActionResult RptBuyerFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate, int reportType, int parformerType)
        {

            string dbids = sp.Dbids(rsMid, asMid, cEid, id);
            string skulist = null;
            if (skuIds != null)
            {

                skulist = string.Join(",", skuIds);
            }





            if (parformerType == 1) // by DB
            {
                if (reportType == 1) //DB Summery
                {
                    List<RPT_Delivery_BuyerByDBSummary_Result> dbBuyer = Db
                        .RPT_Delivery_BuyerByDBSummary(startDate, endDate, dbids, skulist).ToList();
                    ViewBag.startDate = startDate;
                    ViewBag.endDate = endDate;
                    ViewBag.skulist = skulist;

                    return PartialView("RptBuyer/RPTBuyerByDbSummary", dbBuyer);
                }
                else if (reportType == 2) // DB Details
                {
                    ReportViewer reportViewer = new ReportViewer
                    {
                        ProcessingMode = ProcessingMode.Local,
                        SizeToReportContent = true,
                        Width = Unit.Percentage(100),
                        Height = Unit.Pixel(600)

                    };
                 
                    List<RPT_Delivery_BuyerByDBDetails_Result> psrskudelivery = Db
                        .RPT_Delivery_BuyerByDBDetails(startDate, endDate, dbids, skulist).ToList();



                    reportViewer.LocalReport.ReportPath =
                        Server.MapPath(@"~\Reports\Delivery\RPT_Delivery_BuyerByDBDetails.rdlc");

                    var rdc = new ReportDataSource("SKUBuyer", psrskudelivery);


                    ReportParameter rp1 = new ReportParameter("DateParameter",
                        startDate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));


                    reportViewer.LocalReport.SetParameters(new[] {rp1});

                    reportViewer.LocalReport.DataSources.Add(rdc);

                    reportViewer.LocalReport.Refresh();
                    reportViewer.Visible = true;

                    ViewBag.ReportViewer = reportViewer;
                    return PartialView("RptBuyer/RPTBuyerByDbDetails");


                }
            }
            else if (parformerType == 2) //By PSR
            {
                if (reportType == 1) //PSR Summery
                {
                    List<RPT_Delivery_BuyerByPSRSummary_Result> psrbuyerSummary = Db
                        .RPT_Delivery_BuyerByPSRSummary(startDate, endDate, dbids, skulist).ToList();

                    ViewBag.startDate = startDate;
                    ViewBag.endDate = endDate;
                    ViewBag.skulist = skulist;

                    return PartialView("RptBuyer/RPTBuyerBypsrSummary", psrbuyerSummary);
                }
                else if (reportType == 2) //PSR Details
                {
                    ReportViewer reportViewer = new ReportViewer
                    {
                        ProcessingMode = ProcessingMode.Local,
                        SizeToReportContent = true,
                        Width = Unit.Percentage(100),
                        Height = Unit.Pixel(600)

                    };

                    List<RPT_Delivery_BuyerByPSRDetails_Result> psrskudelivery = Db
                        .RPT_Delivery_BuyerByPSRDetails(startDate, endDate, dbids, skulist).ToList();



                    reportViewer.LocalReport.ReportPath =
                        Server.MapPath(@"~\Reports\Delivery\RPT_Delivery_BuyerByPSRDetails.rdlc");

                    var rdc = new ReportDataSource("SKUBuyer", psrskudelivery);


                    ReportParameter rp1 = new ReportParameter("DateParameter",
                        startDate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));


                    reportViewer.LocalReport.SetParameters(new[] { rp1 });

                    reportViewer.LocalReport.DataSources.Add(rdc);

                    reportViewer.LocalReport.Refresh();
                    reportViewer.Visible = true;

                    ViewBag.ReportViewer = reportViewer;
                    return PartialView("RptBuyer/RPTBuyerBypsrDetails");

                }
            }


            return PartialView("RptBuyer/RptBuyerFilter");
        }

        public ActionResult DbwiseBuyerdetails(int id, DateTime startdate, DateTime endDate, String skuid)
        {


            var outletList = Db.RPT_Delivery_BuyerByDBsOutletList(startdate, endDate, id.ToString(), skuid);
         
                var gv = new GridView {DataSource = outletList.ToList()};

                gv.DataBind();
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=" + "DBBuyerOutletList" + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.Charset = "";
                StringWriter objStringWriter = new StringWriter();
                HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                gv.RenderControl(objHtmlTextWriter);
                Response.Output.Write(objStringWriter.ToString());

                Response.Flush();
                Response.End();

            return null;

        }
        public string DbwiseNonBuyerdetails(int id, DateTime startdate, DateTime endDate, String skuid)
        {
            var outletList = Db.RPT_Delivery_NonBuyerByDBsOutletList(startdate, endDate, id.ToString(), skuid);

            var gv = new GridView { DataSource = outletList.ToList() };

            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + "DBNonBuyerOutletList" + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());

            Response.Flush();
            Response.End();

            return null;
        }

        public string PsrWiseBuyerDetails(int id, DateTime startdate, DateTime endDate, String skuid)
        {
            var outletList = Db.RPT_Delivery_BuyerByPSRsOutletList(startdate, endDate, id.ToString(), skuid);

            var gv = new GridView { DataSource = outletList.ToList() };

            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + "PSRNonBuyerOutletList" + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());

            Response.Flush();
            Response.End();

            return null;
        }
        public string PsrWiseNonBuyerdetails(int id, DateTime startdate, DateTime endDate, String skuid)
        {
            var outletList = Db.RPT_Delivery_NonBuyerByPSRsOutletList(startdate, endDate, id.ToString(), skuid);

            var gv = new GridView { DataSource = outletList.ToList() };

            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + "PSRNonBuyerOutletList" + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());

            Response.Flush();
            Response.End();

            return null;
        }
    }
    }
