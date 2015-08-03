Connection to Azure

- WEB
   + URL: WaterStock2015.azurewebsites.net

- SQL
   + DB: SummerCamp2015WaterStock
   + Connection String: DefaultConnection
   + DB User: [LoginHere]
   + DB Password : [PasswordHere]

   + To execute in Azure...
     
     USE Master
     GO

     CREATE LOGIN SummerCamp WITH password='[PasswordHere]'
     GO
 
     CREATE USER SummerCamp FOR LOGIN SummerCamp WITH DEFAULT_SCHEMA=[dbo]
     GO

     USE SummerCamp2015WaterStock
     GO

     CREATE USER SummerCamp FOR LOGIN SummerCamp WITH DEFAULT_SCHEMA=[dbo]
     GO

     EXEC sp_addrolemember 'db_owner', '[LoginHere]';
     GO