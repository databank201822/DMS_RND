
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Alter PROCEDURE [dbo].[DailySchedule_tbld_sku_list]

AS
BEGIN

TRUNCATE TABLE [ODMSBI].[dbo].[tbld_sku_list];

INSERT INTO  [ODMSBI].[dbo].[tbld_sku_list]
           ([SKU_id]
           ,[SKUName]
           ,[SKUsl]
           ,[SKUcode]
           ,[SKUStatus]
           ,[SKUbrand_id]
           ,[Sub_brand]
           ,[Brand]
           ,[SKUcategoryid]
           ,[sku_category_name]
           ,[SKUtype_id]
           ,[SKUtype]
           ,[SKUVolume_ml]
           ,[SKUVolume_8oz]
           ,[SKUUnit]
           ,[Pack_Size])

SELECT TOP (100) PERCENT t1.SKU_id, t1.SKUName, t1.SKUsl, t1.SKUcode, t1.SKUStatus, t1.SKUbrand_id, t3.element_name AS Sub_brand, t5.element_name AS Brand, t1.SKUcategoryid, t4.sku_category_name, t1.SKUtype_id, t6.SKUtype, 
                  t1.SKUVolume_ml, t1.SKUVolume_8oz,t1.SKUUnit, t2.qty AS Pack_Size
FROM     dbo.tbld_SKU AS t1 LEFT OUTER JOIN
                  dbo.tbld_SKU_unit AS t2 ON t1.SKUUnit = t2.id LEFT OUTER JOIN
                  dbo.tbld_SKU_Brand AS t3 ON t1.SKUbrand_id = t3.id LEFT OUTER JOIN
                  dbo.tbld_sku_category AS t4 ON t1.SKUcategoryid = t4.Id LEFT OUTER JOIN
                  dbo.tbld_SKU_Brand AS t5 ON t3.parent_element_id = t5.id LEFT OUTER JOIN
                  dbo.tbld_SKUtype AS t6 ON t1.SKUtype_id = t6.SKUtypeId
END
GO
