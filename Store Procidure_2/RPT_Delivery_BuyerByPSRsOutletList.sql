USE [ODMS]
GO
/****** Object:  StoredProcedure [dbo].[RPT_Delivery_BuyerByPSRsOutletList]    Script Date: 13-Aug-2018 3:12:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RPT_Delivery_BuyerByPSRsOutletList]
	@Start_Date Datetime,
	@End_Date Datetime,
	@Psrids Varchar(MAX),
	@skuids Varchar(MAX)
AS
BEGIN
SELECT  A.REGION_Name, A.AREA_Name,A.CEAREA_Name, A.DB_Name,  A.DBCode,A.cluster,  A.Name As PSR_Name, A.PSR_Code,
                  A.RouteName,A.OutletCode, A.OutletName, A.Address,A.OwnerName, A.ContactNo, IIF(A.HaveVisicooler = 1,'Yes','No') AS HaveVisicooler, ISNULL(A.channel_name,'') As Channel, ISNULL(A.outlet_category_name,'') As Category, ISNULL(A.Outlet_grade,'') As Grade
FROM     tbld_db_psr_outlet_zone_view AS A 
where A.OutletId in (SELECT DISTINCT outlet_id AS OutletId
                       FROM      tblr_OutletWiseBuyer AS t2
                       WHERE   (BatchDate BETWEEN @Start_Date AND @End_Date) AND (sku_id IN
                                             (SELECT Value
                                              FROM      dbo.FunctionStringtoIntlist(@skuids, ','))))  AND  (A.PSR_id IN(SELECT Value FROM      dbo.FunctionStringtoIntlist(@Psrids, ','))) AND (A.IsActive = 1)
ORDER BY A.REGION_id, A.AREA_id, A.CEAREA_id, A.DB_Id,A.PSR_id,A.RouteID
END
