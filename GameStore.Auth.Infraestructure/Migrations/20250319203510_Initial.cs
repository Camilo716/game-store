using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStore.Auth.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentRoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_AspNetRoles_ParentRoleId",
                        column: x => x.ParentRoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BanExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrivilegeRoles",
                columns: table => new
                {
                    PrivilegesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivilegeRoles", x => new { x.PrivilegesId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PrivilegeRoles_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrivilegeRoles_Privileges_PrivilegesId",
                        column: x => x.PrivilegesId,
                        principalTable: "Privileges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "ParentRoleId" },
                values: new object[] { "00000000-0000-0000-0000-000000000010", null, "Admin", "ADMIN", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BanExpirationDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-0000-0000-0000-000000000001", 0, null, "36f6981d-9045-453c-8a3b-92b0a732c104", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEDD11mhD6Uwd1+PYkxMJaQYpRKYVH7Gdcu5jucD3y6qH52KVwZjVrg4Phn5ySFH0zw==", null, false, "a0e73ef8-59bb-413e-85b6-2567feb30704", false, "admin" });

            migrationBuilder.InsertData(
                table: "Privileges",
                columns: new[] { "Id", "Key" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "AddGame" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "DeleteGame" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "ViewGames" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "UpdateGame" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "ViewRoles" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "AddRole" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "UpdateRole" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "DeleteRole" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "ViewUsers" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "AddUser" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "DeleteUser" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "UpdateUser" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "ViewGenres" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "AddGenre" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "DeleteGenre" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "UpdateGenre" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "ViewPlatforms" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "AddPlatform" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "DeletePlatform" },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "UpdatePlatform" },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "ViewPublishers" },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "AddPublisher" },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "DeletePublisher" },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "UpdatePublisher" },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "BanUser" },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "DeleteComment" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "ParentRoleId" },
                values: new object[] { "00000000-0000-0000-0000-000000000020", null, "Manager", "MANAGER", "00000000-0000-0000-0000-000000000010" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "00000000-0000-0000-0000-000000000010", "00000000-0000-0000-0000-000000000001" });

            migrationBuilder.InsertData(
                table: "PrivilegeRoles",
                columns: new[] { "PrivilegesId", "RolesId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000005"), "00000000-0000-0000-0000-000000000010" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "00000000-0000-0000-0000-000000000010" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "00000000-0000-0000-0000-000000000010" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "00000000-0000-0000-0000-000000000010" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "00000000-0000-0000-0000-000000000010" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "00000000-0000-0000-0000-000000000010" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "00000000-0000-0000-0000-000000000010" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "00000000-0000-0000-0000-000000000010" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "ParentRoleId" },
                values: new object[] { "00000000-0000-0000-0000-000000000030", null, "Moderator", "MODERATOR", "00000000-0000-0000-0000-000000000020" });

            migrationBuilder.InsertData(
                table: "PrivilegeRoles",
                columns: new[] { "PrivilegesId", "RolesId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "00000000-0000-0000-0000-000000000020" },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "00000000-0000-0000-0000-000000000020" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "ParentRoleId" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000040", null, "User", "USER", "00000000-0000-0000-0000-000000000030" },
                    { "00000000-0000-0000-0000-000000000050", null, "Guest", "GUEST", "00000000-0000-0000-0000-000000000040" }
                });

            migrationBuilder.InsertData(
                table: "PrivilegeRoles",
                columns: new[] { "PrivilegesId", "RolesId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000003"), "00000000-0000-0000-0000-000000000050" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ParentRoleId",
                table: "AspNetRoles",
                column: "ParentRoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PrivilegeRoles_RolesId",
                table: "PrivilegeRoles",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_Privileges_Key",
                table: "Privileges",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PrivilegeRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Privileges");
        }
    }
}
