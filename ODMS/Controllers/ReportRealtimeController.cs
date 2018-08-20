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
    public class ReportRealtimeController : Controller
    {
        public ODMSEntities Db = new ODMSEntities();

        Supporting sp = new Supporting();

        public ActionResult ROrderVsDelivery()
        {
            return View("ROrderVsDelivery/ROrderVsDelivery");
        }

        [HttpPost]
        public ActionResult ROrderVsDeliveryFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate, int reportType, int parformerType)
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
                    List<RPT_Realtime_OrderVsdeliveredDBSummary_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredDBSummary(startDate, endDate, dbids, skulist)
                        .ToList();
                   

                    return PartialView("ROrderVsDelivery/OrderVsdeliveredDBSummary", orderVsDeleviry);
                }
                else if (reportType == 2) // DB Details
                {

                    List<RPT_Realtime_OrderVsdeliveredDBDetails_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredDBDetails(startDate, endDate, dbids, skulist)
                        .ToList();
                   

                    return PartialView("ROrderVsDelivery/OrderVsdeliveredDBDetails", orderVsDeleviry);


                }
            }
            else if (parformerType == 2) // by PSR
            {
                if (reportType == 1) //PSR Summery
                {
                    List<RPT_Realtime_OrderVsdeliveredSummary_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredSummary(startDate, endDate, dbids, skulist)
                        .ToList();


                    return PartialView("ROrderVsDelivery/RPSRROrderVsDeliveryFilter", orderVsDeleviry);
                }
                else if (reportType == 2) // PSR Details
                {

                    List<RPT_Realtime_OrderVsdeliveredDetails_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredDetails(startDate, endDate, dbids, skulist)
                        .ToList();


                    return PartialView("ROrderVsDelivery/OrderVsdeliveredDetails", orderVsDeleviry);

                }

            }

            return null;


        }


        public ActionResult OrderVsDelivery()
        {
            return View("OrderVsDelivery/ROrderVsDelivery");
        }

        [HttpPost]
        public ActionResult OrderVsDeliveryFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate, int reportType, int parformerType)
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
                    List<RPT_Realtime_OrderVsdeliveredDBSummary_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredDBSummary(startDate, endDate, dbids, skulist)
                        .ToList();


                    return PartialView("OrderVsDelivery/OrderVsdeliveredDBSummary", orderVsDeleviry);
                }
                else if (reportType == 2) // DB Details
                {

                    List<RPT_Realtime_OrderVsdeliveredDBDetails_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredDBDetails(startDate, endDate, dbids, skulist)
                        .ToList();


                    return PartialView("OrderVsDelivery/OrderVsdeliveredDBDetails", orderVsDeleviry);


                }
            }
            else if (parformerType == 2) // by PSR
            {
                if (reportType == 1) //PSR Summery
                {
                    List<RPT_Realtime_OrderVsdeliveredSummary_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredSummary(startDate, endDate, dbids, skulist)
                        .ToList();


                    return PartialView("OrderVsDelivery/RPSRROrderVsDeliveryFilter", orderVsDeleviry);
                }
                else if (reportType == 2) // PSR Details
                {

                    List<RPT_Realtime_OrderVsdeliveredDetails_Result> orderVsDeleviry = Db
                        .RPT_Realtime_OrderVsdeliveredDetails(startDate, endDate, dbids, skulist)
                        .ToList();


                    return PartialView("OrderVsDelivery/OrderVsdeliveredDetails", orderVsDeleviry);

                }

            }

            return null;


        }

        public ActionResult CurrentPsrWiseOrder()
        {
            return View("CurrentPsrWiseOrder/CurrentPsrWiseOrder");
        }

        [HttpPost]
        public ActionResult CurrentPsrWiseOrderFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] skuIds, DateTime startDate, DateTime endDate)
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

            List<RPT_Realtime_SKUWiseOrder_Result> skuOrder = Db.RPT_Realtime_SKUWiseOrder(startDate, endDate, dbids, skulist)
                .ToList();


            reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Realtime\RPT_Realtime_SKUWiseOrder.rdlc");


            ReportDataSource rdc = new ReportDataSource("SKUOrder", skuOrder);

            reportViewer.LocalReport.DataSources.Add(rdc);
            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;

            return PartialView("CurrentPsrWiseOrder/CurrentPsrWiseOrderFilter");
        }



        public ActionResult CurrentOutletwiseOrder()
        {
            return View("CurrentOutletwiseOrder/CurrentOutletwiseOrder");
        }


        [HttpPost]
        public ActionResult CurrentOutletwiseOrderFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id,DateTime startDate, DateTime endDate)
        {
            string dbids = sp.Dbids(rsMid, asMid, cEid, id);
            string skulist = null;
           

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };

            //List<RPT_Realtime_SKUWiseOrder> outletOrder = Db.RPT_Realtime_SKUWiseOrder(startDate, endDate, dbids, skulist)
            //    .ToList();


            //reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Order\RPT_OutletWiseOrder_Symmary.rdlc");


            //ReportDataSource rdc = new ReportDataSource("OutletOrder", outletOrder);

            //reportViewer.LocalReport.DataSources.Add(rdc);
            //reportViewer.LocalReport.Refresh();
            //reportViewer.Visible = true;

            //ViewBag.ReportViewer = reportViewer;

            return PartialView("CurrentPsrWiseOrder/CurrentPsrWiseOrderFilter");
        }
    }
}