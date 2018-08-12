USE [ODMS]
GO
/****** Object:  StoredProcedure [dbo].[RPT_Delivery_BuyerByDBSummary]    Script Date: 12-Aug-2018 9:28:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RPT_Delivery_BuyerByDBSummary]
	@Start_Date Datetime,
	@End_Date Datetime,
	@dbids Varchar(MAX),
	@skuids Varchar(MAX)
AS
BEGIN
	SELECT A.DB_Id, A.DB_Name, A.CEAREA_id, A.CEAREA_Name, A.AREA_id, A.AREA_Name, A.REGION_id, A.REGION_Name, COUNT(A.OutletId) AS TotalOutlet, ISNULL(B.BuyerOutlet,0) As BuyerOutlet,(COUNT(A.OutletId)-ISNULL(B.BuyerOutlet,0))As NonBuyer
FROM     tbld_db_psr_outlet_zone_view AS A LEFT OUTER JOIN
                      (SELECT t2.Distributorid, COUNT(DISTINCT t1.outlet_id) AS BuyerOutlet
                       FROM      tblr_OutletWiseBuyer AS t1 INNER JOIN
                                         tbld_Outlet AS t2 ON t1.outlet_id = t2.OutletId
                       WHERE   (t1.sku_id IN (select Value FROM dbo.FunctionStringtoIntlist(@skuids,','))) AND (t1.BatchDate BETWEEN @Start_Date AND @End_Date) AND (t2.IsActive = 1)
                       GROUP BY t2.Distributorid) AS B ON A.DB_Id = B.Distributorid
WHERE  (A.DB_Id IN (select Value FROM dbo.FunctionStringtoIntlist(@dbids,',')))
GROUP BY A.DB_Id, A.DB_Name, A.CEAREA_id, A.CEAREA_Name, A.AREA_id, A.AREA_Name, A.REGION_id, A.REGION_Name, B.BuyerOutlet
Order by A.REGION_id,A.AREA_id,A.CEAREA_id,A.DB_Id

END
