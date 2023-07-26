
-- Pagination by Store Procedure 
Create proc SP_products_category
@Pageindex int ,
@Pagesize int ,
@TotalRecords int output
as 
begin
 SELECT @TotalRecords = COUNT(*)
FROM products as p
INNER JOIN categories as c
ON p.categoryid = c.categoryid 


-- Fetched the page data 

select p.productid,p.productname,c.categoryid,c.categoryname
FROM products as p
INNER JOIN categories as c
ON p.categoryid = c.categoryid 
order by p.productid asc Offset @Pagesize*(@Pageindex-1) Rows Fetch next @Pagesize rows only
Select Count(1) as totalcount from products

end

