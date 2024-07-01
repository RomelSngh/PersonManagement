using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement.Migrations.PersonAccountTransactionData
{
    /// <inheritdoc />
    public partial class GetAllAccountsStoredProcUpate : Migration
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
                                              ,[statusId]  
		                                  FROM [dbo].[Accounts]
		                                  WHere [person_code] = 
		                                  CASE 
		                                  WHEN @pcode <> 0 then @pcode
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
