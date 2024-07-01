using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement.Migrations.PersonAccountTransactionData
{
    /// <inheritdoc />
    public partial class SQLProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                USE [PersonAccountTransactions]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllAccounts]    Script Date: 2024/07/01 07:17:14 ******/
DROP PROCEDURE IF Exists [dbo].[sp_GetAllAccounts]
GO

-- =============================================
-- Author:		Romel Singh
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllAccounts]
@pcode As int = 0
AS
BEGIN
		SELECT [code]
			  ,[person_code]
			  ,[account_number]
			  ,[outstanding_balance]
		  FROM [dbo].[Accounts]
		  WHere [person_code] = 
		  CASE 
		  WHEN @pcode <> 0 then @pcode
		  ELSE code end
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllPersons]    Script Date: 2024/07/01 07:18:08 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetAllPersons]
GO

-- =============================================
-- Author:		Romel Singh
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllPersons]
@code As int = 0
AS
BEGIN
		SELECT [code]
			  ,[name]
			  ,[surname]
			  ,[id_number]
		  FROM [dbo].[Persons]
		  WHere code = 
		  CASE 
		  WHEN @code <> 0 then @code
		  ELSE code end
END
GO


/****** Object:  StoredProcedure [dbo].[sp_GetAllTransactions]    Script Date: 2024/07/01 07:19:05 ******/
DROP PROCEDURE  IF EXISTS [dbo].[sp_GetAllTransactions]
GO



-- =============================================
-- Author:		Romel Singh
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetAllTransactions]
@acode int = 0 
AS
BEGIN
		SELECT [code]
			  ,[account_code]
			  ,[transaction_date]
			  ,[capture_date]
			  ,[amount]
			  ,[description]
		  FROM [dbo].[Transactions]
		  WHere [account_code] = 
		  CASE 
		  WHEN @acode <> 0 then @acode
		  ELSE code end 
END

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
