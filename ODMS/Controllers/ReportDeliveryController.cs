using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using ODMS.Models;
using ODMS.Models.ViewModel;

namespace ODMS.Controllers
{
    [SessionExpire]
    public class ReportDeliveryController : Controller
    {
        public ODMSBIEntities Dbbi = new ODMSBIEntities();
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

            List<RPT_Delivery_OutletWiseSKUWiseDelivery_Result> delivery = Dbbi
                .RPT_Delivery_OutletWiseSKUWiseDelivery(startDate, endDate, dbids, skulist).ToList();

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
            List<RPT_Delivery_PSRWiseSKUWiseDelivery_Result> psrskudelivery = Dbbi.RPT_Delivery_PSRWiseSKUWiseDelivery(startDate, endDate, dbids, skulist).ToList();

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

        public ActionResult RptDeliveryKpiFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, DateTime startDate, DateTime endDate, int reportType, int parformerType)
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
                    List<RPT_Delivery_DBPerformanceKPISummary_Result> dbkpi = Dbbi
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
                        List<RPT_Delivery_PSRPerformanceKPISummary_Result> psrkpisummary = Dbbi
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
                    List<RPT_Delivery_BuyerByDBSummary_Result> dbBuyer = Dbbi
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

                    List<RPT_Delivery_BuyerByDBDetails_Result> psrskudelivery = Dbbi
                        .RPT_Delivery_BuyerByDBDetails(startDate, endDate, dbids, skulist).ToList();



                    reportViewer.LocalReport.ReportPath =
                        Server.MapPath(@"~\Reports\Delivery\RPT_Delivery_BuyerByDBDetails.rdlc");

                    var rdc = new ReportDataSource("SKUBuyer", psrskudelivery);


                    ReportParameter rp1 = new ReportParameter("DateParameter",
                        startDate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));


                    reportViewer.LocalReport.SetParameters(new[] { rp1 });

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
                    List<RPT_Delivery_BuyerByPSRSummary_Result> psrbuyerSummary = Dbbi
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

                    List<RPT_Delivery_BuyerByPSRDetails_Result> psrskudelivery = Dbbi
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


            var outletList = Dbbi.RPT_Delivery_BuyerByDBsOutletList(startdate, endDate, id.ToString(), skuid);

            var gv = new GridView { DataSource = outletList.ToList() };

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
            var outletList = Dbbi.RPT_Delivery_NonBuyerByDBsOutletList(startdate, endDate, id.ToString(), skuid);

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
            var outletList = Dbbi.RPT_Delivery_BuyerByPSRsOutletList(startdate, endDate, id.ToString(), skuid);

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
            var outletList = Dbbi.RPT_Delivery_NonBuyerByPSRsOutletList(startdate, endDate, id.ToString(), skuid);

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



        public ActionResult TradePromotionSales()
        {
            return View("TradePromotionSales/TradePromotionSales");
        }


        [HttpPost]
        public ActionResult TradePromotionSalesFilter(int[] rsMid, int[] asMid, int[] cEid, int[] id, int[] promoIds, DateTime startDate, DateTime endDate)
        {
            string dbids = sp.Dbids(rsMid, asMid, cEid, id);

            string promoId = string.Join(",", promoIds);

            var data = Dbbi.RPT_TP_Summary(startDate, endDate, dbids, promoId);
            return PartialView("TradePromotionSales/TradePromotionSalesFilter", data.ToList());


        }

        [HttpPost]
        public ActionResult TpList(int[] rsMid, int[] asMid, int[] cEid, int[] id, DateTime startDate, DateTime endDate)
        {
            string dbids = sp.Dbids(rsMid, asMid, cEid, id);

            var list = Db.RPT_TP_List(startDate, endDate, dbids);

            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TradePromotionSalesDetails(int id, int promoid, DateTime startdate, DateTime endDate)
        {

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Pixel(600)

            };

            List<RPT_TP_OutletWiseDetails_Result> tpDetails = Dbbi
                .RPT_TP_OutletWiseDetails(startdate, endDate, id.ToString(), promoid.ToString()).ToList();

            reportViewer.LocalReport.ReportPath = Server.MapPath(@"~\Reports\Delivery\RPT_TPOutletWiseDetails.rdlc");
            ReportDataSource rdc = new ReportDataSource("TPOutlet", tpDetails);

            var tbp = Db.tblt_TradePromotion.FirstOrDefault(x=>x.id==promoid);

            ReportParameter rp1 = new ReportParameter("DateParameter", startdate.ToString("dd-MMM-yyy") + " TO " + endDate.ToString("dd-MMM-yyy"));


            if (tbp != null)
            {
                ReportParameter rp2 = new ReportParameter("TPNameParameter",tbp.name);


                reportViewer.LocalReport.SetParameters(new[] { rp1, rp2 });
            }

            reportViewer.LocalReport.DataSources.Add(rdc);

            reportViewer.LocalReport.Refresh();
            reportViewer.Visible = true;

            ViewBag.ReportViewer = reportViewer;


            var orderid = tpDetails.Select(x => x.Orderid).Distinct().ToList();

            ViewBag.Momonumber = string.Join(",", orderid);


            return View("TradePromotionSales/TradePromotionSalesDetails");
        }

        [HttpPost]
        public ActionResult BulkInvoice(string ids)
        {


            int numOfLineInInvoice = 8;
            List<InvoiceVm> invoiceList = new List<InvoiceVm>();

            var orderid = ids.Split(',').Select(Int32.Parse).ToList();

            var inv = Db.tblt_Order.Where(x => orderid.Contains(x.Orderid));
            foreach (var invitem in inv)
            {
                int id = invitem.Orderid;
                int dbid = invitem.db_id;

                InvoiceVm invoiceVm = new InvoiceVm();

                var invoiceLineItemSdata = (from a in Db.tblt_Order_line
                                            join b in Db.tbld_SKU on a.sku_id equals b.SKU_id
                                            where a.Orderid == id
                                            orderby a.id ascending
                                            select new InvoiceLineDetilsVm
                                            {
                                                SkuCode = b.SKUcode,
                                                SkuId = a.sku_id,
                                                SkuName = b.SKUName,
                                                BetchId = a.Betch_id,
                                                PackSize = a.Pack_size

                                            }).Distinct().ToList();
                var numberofmemo = (double)invoiceLineItemSdata.Count() / numOfLineInInvoice;
                if (numberofmemo <= 1)
                {
                    List<InvoiceLineDetilsVm> invoiceLineItem = new List<InvoiceLineDetilsVm>();
                    var outlet = Db.tbld_Outlet.SingleOrDefault(x => x.OutletId == invitem.outlet_id);
                    var dbHouse = Db.tbld_distribution_house.SingleOrDefault(x => x.DB_Id == invitem.db_id);
                    var psrInfo = Db.tbld_distribution_employee.SingleOrDefault(x => x.id == invitem.psr_id);

                    var totalDelivered = (int)(invitem.total_delivered ?? 0);
                    var salesOrderTypeId = invitem.sales_order_type_id ?? 0;
                    var manualDiscount = (int)(invitem.manual_discount ?? 0);

                    if (outlet != null)
                        if (dbHouse != null)
                            if (psrInfo != null)
                                invoiceVm = new InvoiceVm
                                {
                                    Orderid = id,
                                    SoId = invitem.so_id,
                                    RouteName = Db.tbld_distributor_Route
                                        .Where(x => x.RouteID == invitem.route_id)
                                        .Select(x => x.RouteName).SingleOrDefault(),
                                    OutletName = outlet.OutletName,
                                    OutletAddress = outlet.Address,
                                    ChallanNo = invitem.Challan_no,
                                    PlannedOrderDate = invitem.planned_order_date,
                                    DeliveryDate = invitem.delivery_date,
                                    DbName = dbHouse.DBName ?? "",
                                    DbAddress = dbHouse.OfficeAddress,
                                    DbMobile = dbHouse.OwnerMoble,
                                    PsrName = psrInfo.Name,
                                    PsrMobile = psrInfo.contact_no,
                                    TotalDelivered = totalDelivered,
                                    SalesOrderTypeId = salesOrderTypeId,
                                    ManualDiscount = manualDiscount
                                };



                    foreach (var lineItem in invoiceLineItemSdata)
                    {
                        int confirmQty = 0;
                        int freeQty = 0;

                        double unitSalePrice = (from a in Db.tbld_bundle_price_details
                                                join b in Db.tbld_distribution_house on a.bundle_price_id equals b.PriceBuandle_id
                                                where a.sku_id == lineItem.SkuId && a.batch_id == lineItem.BetchId && b.DB_Id == dbid
                                                select a.outlet_lifting_price).SingleOrDefault();


                        var orderQtysum = (from b in Db.tblt_Order_line
                                           where b.Orderid == id && b.sku_id == lineItem.SkuId &&
                                                 b.Betch_id == lineItem.BetchId && b.sku_order_type_id == 1
                                           select (int?)b.quantity_delivered).Sum();

                        var freeQtysum = (from b in Db.tblt_Order_line
                                          where b.Orderid == id && b.sku_id == lineItem.SkuId &&
                                                b.Betch_id == lineItem.BetchId && b.sku_order_type_id == 2
                                          select (int?)b.quantity_delivered).Sum();

                        if (orderQtysum != null)
                        {
                            confirmQty = (int)orderQtysum;
                        }

                        if (freeQtysum != null)
                        {
                            freeQty = (int)freeQtysum;
                        }
                        var totalSalePrice = Db.tblt_Order_line
                                                 .Where(b => b.Orderid == id && b.sku_id == lineItem.SkuId &&
                                                             b.Betch_id == lineItem.BetchId &&
                                                             b.sku_order_type_id == 1)
                                                 .Sum(x => (double?)(x.total_sale_price)) ?? 0;
                        var totalDiscountAmount = Db.tblt_Order_line
                                                      .Where(b => b.Orderid == id && b.sku_id == lineItem.SkuId &&
                                                                  b.Betch_id == lineItem.BetchId &&
                                                                  b.sku_order_type_id == 1)
                                                      .Sum(x => (double?)(x.total_discount_amount)) ?? 0;
                        var totalBilledAmount = Db.tblt_Order_line
                                                    .Where(b => b.Orderid == id && b.sku_id == lineItem.SkuId &&
                                                                b.Betch_id == lineItem.BetchId &&
                                                                b.sku_order_type_id == 1)
                                                    .Sum(x => (double?)(x.total_billed_amount)) ?? 0;

                        InvoiceLineDetilsVm invoiceLineDetilsVm = new InvoiceLineDetilsVm
                        {
                            SkuCode = lineItem.SkuCode,
                            SkuName = lineItem.SkuName,
                            BetchId = lineItem.BetchId,
                            PackSize = lineItem.PackSize,
                            UnitSalePrice = confirmQty == 0 ? 0 : unitSalePrice,
                            QuantityDeliveredCs = confirmQty / lineItem.PackSize,
                            QuantityDeliveredPs = confirmQty % lineItem.PackSize,
                            QuantityFree = freeQty,
                            TotalSalePrice = Math.Round(totalSalePrice),
                            TotalDiscountAmount = Math.Round(totalDiscountAmount),
                            TotalBilledAmount = Math.Round(totalBilledAmount)



                        };
                        invoiceLineItem.Add(invoiceLineDetilsVm);

                    }


                    invoiceVm.InvoiceLine = invoiceLineItem;
                    invoiceList.Add(invoiceVm);
                }
                else
                {
                    double numberofInvoice = (double)invoiceLineItemSdata.Count() / numOfLineInInvoice;

                    for (int j = 0; j <= numberofInvoice; j++)
                    {
                        List<InvoiceLineDetilsVm> invoiceLineItem = new List<InvoiceLineDetilsVm>();


                        var outlet = Db.tbld_Outlet.SingleOrDefault(x => x.OutletId == invitem.outlet_id);
                        var dbHouse =
                            Db.tbld_distribution_house.SingleOrDefault(x => x.DB_Id == invitem.db_id);
                        var psrInfo =
                            Db.tbld_distribution_employee.SingleOrDefault(x => x.id == invitem.psr_id);

                        var totalDelivered = (int)(invitem.total_delivered ?? 0);
                        var salesOrderTypeId = invitem.sales_order_type_id ?? 0;
                        var manualDiscount = (int)(invitem.manual_discount ?? 0);

                        if (outlet != null) { }
                        if (dbHouse != null) { }
                        if (psrInfo != null) { }
                        invoiceVm = new InvoiceVm
                        {
                            Orderid = id,
                            SoId = invitem.so_id,
                            RouteName = Db.tbld_distributor_Route
                                .Where(x => x.RouteID == invitem.route_id)
                                .Select(x => x.RouteName).SingleOrDefault(),
                            OutletName = outlet.OutletName,
                            OutletAddress = outlet.Address,
                            ChallanNo = invitem.Challan_no,
                            PlannedOrderDate = invitem.planned_order_date,
                            DeliveryDate = invitem.delivery_date,
                            DbName = dbHouse.DBName ?? "",
                            DbAddress = dbHouse.OfficeAddress,
                            DbMobile = dbHouse.OwnerMoble,
                            PsrName = psrInfo.Name,
                            PsrMobile = psrInfo.contact_no,
                            TotalDelivered = totalDelivered,
                            SalesOrderTypeId = salesOrderTypeId,
                            ManualDiscount = manualDiscount
                        };



                        foreach (var lineItem in invoiceLineItemSdata.Take(numOfLineInInvoice))
                        {




                            int confirmQty = 0;
                            int freeQty = 0;

                            double unitSalePrice = (from a in Db.tbld_bundle_price_details
                                                    join b in Db.tbld_distribution_house on a.bundle_price_id equals b
                                                        .PriceBuandle_id
                                                    where a.sku_id == lineItem.SkuId && a.batch_id == lineItem.BetchId &&
                                                          b.DB_Id == dbid
                                                    select a.outlet_lifting_price).SingleOrDefault();


                            var orderQtysum = (from b in Db.tblt_Order_line
                                               where b.Orderid == id && b.sku_id == lineItem.SkuId &&
                                                     b.Betch_id == lineItem.BetchId && b.sku_order_type_id == 1
                                               select (int?)b.quantity_delivered).Sum();

                            var freeQtysum = (from b in Db.tblt_Order_line
                                              where b.Orderid == id && b.sku_id == lineItem.SkuId &&
                                                    b.Betch_id == lineItem.BetchId && b.sku_order_type_id == 2
                                              select (int?)b.quantity_delivered).Sum();

                            if (orderQtysum != null)
                            {
                                confirmQty = (int)orderQtysum;
                            }

                            if (freeQtysum != null)
                            {
                                freeQty = (int)freeQtysum;
                            }
                            var totalSalePrice = Db.tblt_Order_line
                                                     .Where(b => b.Orderid == id &&
                                                                 b.sku_id == lineItem.SkuId &&
                                                                 b.Betch_id == lineItem.BetchId &&
                                                                 b.sku_order_type_id == 1)
                                                     .Sum(x => (double?)(x.total_sale_price)) ?? 0;
                            var totalDiscountAmount = Db.tblt_Order_line
                                                          .Where(b => b.Orderid == id &&
                                                                      b.sku_id == lineItem.SkuId &&
                                                                      b.Betch_id == lineItem.BetchId &&
                                                                      b.sku_order_type_id == 1)
                                                          .Sum(x => (double?)(x.total_discount_amount)) ?? 0;
                            var totalBilledAmount = Db.tblt_Order_line
                                                        .Where(b => b.Orderid == id &&
                                                                    b.sku_id == lineItem.SkuId &&
                                                                    b.Betch_id == lineItem.BetchId &&
                                                                    b.sku_order_type_id == 1)
                                                        .Sum(x => (double?)(x.total_billed_amount)) ?? 0;

                            InvoiceLineDetilsVm invoiceLineDetilsVm = new InvoiceLineDetilsVm
                            {
                                SkuCode = lineItem.SkuCode,
                                SkuName = lineItem.SkuName,
                                BetchId = lineItem.BetchId,
                                PackSize = lineItem.PackSize,
                                UnitSalePrice = confirmQty == 0 ? 0 : unitSalePrice,
                                QuantityDeliveredCs = confirmQty / lineItem.PackSize,
                                QuantityDeliveredPs = confirmQty % lineItem.PackSize,
                                QuantityFree = freeQty,
                                TotalSalePrice = totalSalePrice,
                                TotalDiscountAmount = totalDiscountAmount,
                                TotalBilledAmount = totalBilledAmount



                            };
                            invoiceLineItem.Add(invoiceLineDetilsVm);


                        }


                        invoiceVm.InvoiceLine = invoiceLineItem;

                        invoiceList.Add(invoiceVm);
                        if (invoiceLineItemSdata.Count() >= numOfLineInInvoice)
                        {
                            invoiceLineItemSdata.RemoveRange(0, numOfLineInInvoice);
                        }


                    }



                }
            }

            return PartialView("TradePromotionSales/BulkInvoice", invoiceList);

        }


    }
}
