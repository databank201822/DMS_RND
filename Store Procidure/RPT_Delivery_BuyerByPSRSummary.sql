USE [ODMS]
GO
/****** Object:  StoredProcedure [dbo].[RPT_Delivery_BuyerByPSRSummary]    Script Date: 12-Aug-2018 9:25:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RPT_Delivery_BuyerByPSRSummary]
	@Start_Date Datetime,
	@End_Date Datetime,
	@dbids Varchar(MAX),
	@skuids Varchar(MAX)
AS
BEGIN
SELECT A.DB_Id, A.DB_Name, A.CEAREA_id, A.CEAREA_Name, A.AREA_id, A.AREA_Name, A.REGION_id, A.REGION_Name,A.PSR_id,A.Name As PSR, COUNT(A.OutletId) AS TotalOutlet, ISNULL(B.BuyerOutlet,0) As BuyerOutlet,(COUNT(A.OutletId)-ISNULL(B.BuyerOutlet,0))As NonBuyer
FROM     tbld_db_psr_outlet_zone_view As A
Left join (SELECT t2.PSR_id, COUNT(DISTINCT t1.outlet_id) AS BuyerOutlet
FROM     tblr_OutletWiseBuyer AS t1 INNER JOIN
                  tbld_db_psr_outlet_zone_view As t2 ON t1.outlet_id = t2.OutletId
WHERE  (t1.sku_id IN (select Value FROM dbo.FunctionStringtoIntlist(@skuids,','))) AND (t1.BatchDate BETWEEN @Start_Date AND @End_Date)
GROUP BY t2.PSR_id)As B On A.PSR_id=B.PSR_id
WHERE  (A.DB_Id IN (select Value FROM dbo.FunctionStringtoIntlist(@dbids,',')))
GROUP BY A.PSR_id,A.Name ,A.DB_Id, A.DB_Name, A.CEAREA_id, A.CEAREA_Name, A.AREA_id, A.AREA_Name, A.REGION_id, A.REGION_Name,B.BuyerOutlet
Order by A.REGION_id,A.AREA_id,A.CEAREA_id,A.DB_Id,A.PSR_id

END
