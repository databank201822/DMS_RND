USE [ODMS]
GO
/****** Object:  StoredProcedure [dbo].[RPT_Order_PSRWiseSKUWiseOrder]    Script Date: 14-Jul-18 2:39:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[RPT_Delivery_OutletWiseSKUWiseDelivery]
	@Start_Date Datetime,
	@End_Date Datetime
AS
BEGIN
	SELECT A.DB_Id, A.DB_Name, A.CEAREA_id, A.CEAREA_Name, A.AREA_id, A.AREA_Name, A.REGION_id, A.REGION_Name, B.id, B.BatchDate, B.OutletId, B.OutletCode, B.OutletName, B.Distributorid, B.HaveVisicooler, B.SKUId, B.SKUName, 
                  B.PackSize, B.UnitPrice, B.SKUVolume8oz, Sum(B.Delivered_Quentity) AS Delivered_Quentity, Sum(B.FreeDelivered_Quentity) As FreeDelivered_Quentity
FROM     tbld_db_zone_view AS A INNER JOIN
                  tblr_OutletWiseSKUWiseDelivery AS B ON A.DB_Id = B.Distributorid
				    where   B.BatchDate between @Start_Date AND @End_Date
				  Group by  A.DB_Id, A.DB_Name, A.CEAREA_id, A.CEAREA_Name, A.AREA_id, A.AREA_Name, A.REGION_id, A.REGION_Name, B.id, B.BatchDate, B.OutletId, B.OutletCode, B.OutletName, B.Distributorid, B.HaveVisicooler, B.SKUId, B.SKUName, 
                  B.PackSize, B.UnitPrice, B.SKUVolume8oz

				
END
