﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createrealation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                    ALTER TABLE [seller].Inventories
                    ADD CONSTRAINT FK_Inventories_Products_ProductId
                    FOREIGN KEY (ProductId) REFERENCES [product].Products(Id);
                ");

            migrationBuilder.Sql(@"
                    ALTER TABLE [order].Items
                    ADD CONSTRAINT FK_Items_Inventories_InventoryId
                    FOREIGN KEY (InventoryId) REFERENCES [seller].Inventories(Id);
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
