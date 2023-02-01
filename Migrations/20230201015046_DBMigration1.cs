using Microsoft.EntityFrameworkCore.Migrations;

namespace acme_publishing_app.Migrations
{
    public partial class DBMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryCompany",
                columns: table => new
                {
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrintDistCompanyId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCompany", x => new { x.SubscriptionId, x.CountryId });
                });

            migrationBuilder.CreateTable(
                name: "PrintDistCompany",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintDistCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAddress",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAddress_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryAddress_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryOrder",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubscriptionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryAddressId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrintDistCompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_DeliveryAddress_DeliveryAddressId",
                        column: x => x.DeliveryAddressId,
                        principalTable: "DeliveryAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_PrintDistCompany_PrintDistCompanyId",
                        column: x => x.PrintDistCompanyId,
                        principalTable: "PrintDistCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "b00db9eb-7650-4878-b814-8a96d5a8220e", "Australia" },
                    { "f30e74cd-2494-4fc8-8eb3-8de05c4a821e", "New Zealand" },
                    { "070e30e7-488b-47e3-ad28-4379b9be6185", "United States" },
                    { "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", "United Kingdom" },
                    { "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", "South Africa" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { "c208fdd5-75d5-4cb9-b202-ed668b77222c", "Laura", "Simmons" },
                    { "cf994cb1-dc3c-4822-93f7-a7422a016ca9", "John", "Thomas" },
                    { "d5d4c8e5-c7c1-4e4a-8ba5-49765082f826", "David", "Rogers" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryCompany",
                columns: new[] { "CountryId", "SubscriptionId", "PrintDistCompanyId" },
                values: new object[,]
                {
                    { "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", "7caba613-eaa2-4c83-9d47-97b438169f95", "eeed9a4a-e29f-403c-9437-eb34f45948b4" },
                    { "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", "7a852c93-ca4f-4ab5-84ac-c8b70927506e", "eeed9a4a-e29f-403c-9437-eb34f45948b4" },
                    { "ef813fe9-8ef1-4809-8822-6c9eb5feefea", "50909b94-5134-4392-be80-13bcccd2086c", "e094d643-1e9b-4fe5-a051-e4e7a4753a48" },
                    { "f6095b43-57f1-49a7-9acf-ac3d145fef4f", "50909b94-5134-4392-be80-13bcccd2086c", "e094d643-1e9b-4fe5-a051-e4e7a4753a48" },
                    { "fd0e69f0-a1af-4091-b8a4-815a058d927a", "50909b94-5134-4392-be80-13bcccd2086c", "e094d643-1e9b-4fe5-a051-e4e7a4753a48" }
                });

            migrationBuilder.InsertData(
                table: "PrintDistCompany",
                columns: new[] { "Id", "ApiUrl", "Name" },
                values: new object[,]
                {
                    { "e094d643-1e9b-4fe5-a051-e4e7a4753a48", "http://printer1/api", "Printer Company 1" },
                    { "eeed9a4a-e29f-403c-9437-eb34f45948b4", "http://printer2/api", "Printer Company 2" }
                });

            migrationBuilder.InsertData(
                table: "Subscription",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { "7a852c93-ca4f-4ab5-84ac-c8b70927506e", "Magazine 2" },
                    { "50909b94-5134-4392-be80-13bcccd2086c", "Magazine 1" },
                    { "7caba613-eaa2-4c83-9d47-97b438169f95", "Magazine 3" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryAddress",
                columns: new[] { "Id", "CountryId", "CustomerId", "Description" },
                values: new object[,]
                {
                    { "ef813fe9-8ef1-4809-8822-6c9eb5feefea", "b00db9eb-7650-4878-b814-8a96d5a8220e", "c208fdd5-75d5-4cb9-b202-ed668b77222c", "Av 10" },
                    { "f6095b43-57f1-49a7-9acf-ac3d145fef4f", "f30e74cd-2494-4fc8-8eb3-8de05c4a821e", "c208fdd5-75d5-4cb9-b202-ed668b77222c", "Av 20" },
                    { "fd0e69f0-a1af-4091-b8a4-815a058d927a", "070e30e7-488b-47e3-ad28-4379b9be6185", "cf994cb1-dc3c-4822-93f7-a7422a016ca9", "Av 30" },
                    { "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", "cf994cb1-dc3c-4822-93f7-a7422a016ca9", "Av 40" },
                    { "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", "d5d4c8e5-c7c1-4e4a-8ba5-49765082f826", "Av 50" }
                });

            migrationBuilder.InsertData(
                table: "DeliveryOrder",
                columns: new[] { "Id", "DeliveryAddressId", "PrintDistCompanyId", "SubscriptionId" },
                values: new object[,]
                {
                    { "100db9eb-7650-4878-b814-8a96d5a82201", "ef813fe9-8ef1-4809-8822-6c9eb5feefea", "e094d643-1e9b-4fe5-a051-e4e7a4753a48", "50909b94-5134-4392-be80-13bcccd2086c" },
                    { "230e74cd-2494-4fc8-8eb3-8de05c4a8212", "f6095b43-57f1-49a7-9acf-ac3d145fef4f", "e094d643-1e9b-4fe5-a051-e4e7a4753a48", "50909b94-5134-4392-be80-13bcccd2086c" },
                    { "370e30e7-488b-47e3-ad28-4379b9be6183", "fd0e69f0-a1af-4091-b8a4-815a058d927a", "e094d643-1e9b-4fe5-a051-e4e7a4753a48", "50909b94-5134-4392-be80-13bcccd2086c" },
                    { "44ca9e4c-2f51-4bfb-8ee0-12efe0db1874", "74ca9e4c-2f51-4bfb-8ee0-12efe0db187f", "eeed9a4a-e29f-403c-9437-eb34f45948b4", "7a852c93-ca4f-4ab5-84ac-c8b70927506e" },
                    { "56d124a9-df5b-4ae8-848d-d15bb33bbd15", "d6d124a9-df5b-4ae8-848d-d15bb33bbd19", "eeed9a4a-e29f-403c-9437-eb34f45948b4", "7caba613-eaa2-4c83-9d47-97b438169f95" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddress_CountryId",
                table: "DeliveryAddress",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAddress_CustomerId",
                table: "DeliveryAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_DeliveryAddressId",
                table: "DeliveryOrder",
                column: "DeliveryAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_PrintDistCompanyId",
                table: "DeliveryOrder",
                column: "PrintDistCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_SubscriptionId",
                table: "DeliveryOrder",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryCompany");

            migrationBuilder.DropTable(
                name: "DeliveryOrder");

            migrationBuilder.DropTable(
                name: "DeliveryAddress");

            migrationBuilder.DropTable(
                name: "PrintDistCompany");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
