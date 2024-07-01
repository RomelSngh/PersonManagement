using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement.Migrations.PersonAccountTransactionData
{
    /// <inheritdoc />
    public partial class CreateIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.Sql(@"

            /****** Object:  Index [NonClusteredIndex-20240701-065128]    Script Date: 2024/07/01 06:51:59 ******/
            DROP INDEX IF EXISTS  [NonClusteredIndex-20240701-065128] ON [dbo].[Accounts]
            GO

            SET ANSI_PADDING ON
            GO

            /****** Object:  Index [NonClusteredIndex-20240701-065128]    Script Date: 2024/07/01 06:52:00 ******/
            CREATE NONCLUSTERED INDEX [NonClusteredIndex-20240701-065128] ON [dbo].[Accounts]
            (
	            [account_number] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            GO


            /****** Object:  Index [NonClusteredIndex-20240701-065238]    Script Date: 2024/07/01 06:53:03 ******/
            DROP INDEX IF EXISTS [NonClusteredIndex-20240701-065238] ON [dbo].[Persons]
            GO

            SET ANSI_PADDING ON
            GO

            /****** Object:  Index [NonClusteredIndex-20240701-065238]    Script Date: 2024/07/01 06:53:03 ******/
            CREATE NONCLUSTERED INDEX [NonClusteredIndex-20240701-065238] ON [dbo].[Persons]
            (
	            [name] ASC,
	            [surname] ASC,
	            [id_number] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            GO



            /****** Object:  Index [NonClusteredIndex-20240701-065332]    Script Date: 2024/07/01 06:54:28 ******/
            DROP INDEX IF EXISTS  [NonClusteredIndex-20240701-065332] ON [dbo].[Transactions]
            GO

            /****** Object:  Index [NonClusteredIndex-20240701-065332]    Script Date: 2024/07/01 06:54:28 ******/
            CREATE NONCLUSTERED INDEX [NonClusteredIndex-20240701-065332] ON [dbo].[Transactions]
            (
	            [account_code] ASC,
	            [transaction_date] ASC,
	            [capture_date] ASC
            )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
            GO

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

            /****** Object:  Index [NonClusteredIndex-20240701-065128]    Script Date: 2024/07/01 06:51:59 ******/
            DROP INDEX IF EXISTS  [NonClusteredIndex-20240701-065128] ON [dbo].[Accounts]
           
            /****** Object:  Index [NonClusteredIndex-20240701-065238]    Script Date: 2024/07/01 06:53:03 ******/
            DROP INDEX IF EXISTS [NonClusteredIndex-20240701-065238] ON [dbo].[Persons]

            /****** Object:  Index [NonClusteredIndex-20240701-065332]    Script Date: 2024/07/01 06:54:28 ******/
            DROP INDEX IF EXISTS  [NonClusteredIndex-20240701-065332] ON [dbo].[Transactions]

            ");
        }
    }
}
