using Microsoft.EntityFrameworkCore.Migrations;

namespace UnionMall.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpEntityChanges_AbpEntityChangeSets_EntityChangeSetId",
                table: "AbpEntityChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId",
                table: "AbpEntityPropertyChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpFeatures_AbpEditions_EditionId",
                table: "AbpFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpPermissions_AbpRoles_RoleId",
                table: "AbpPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpPermissions_AbpUsers_UserId",
                table: "AbpPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpRoleClaims_AbpRoles_RoleId",
                table: "AbpRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpRoles_AbpUsers_CreatorUserId",
                table: "AbpRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpRoles_AbpUsers_DeleterUserId",
                table: "AbpRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpRoles_AbpUsers_LastModifierUserId",
                table: "AbpRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpSettings_AbpUsers_UserId",
                table: "AbpSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpTenants_AbpUsers_CreatorUserId",
                table: "AbpTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpTenants_AbpUsers_DeleterUserId",
                table: "AbpTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpTenants_AbpEditions_EditionId",
                table: "AbpTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpTenants_AbpUsers_LastModifierUserId",
                table: "AbpTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUserClaims_AbpUsers_UserId",
                table: "AbpUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUserLogins_AbpUsers_UserId",
                table: "AbpUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUserRoles_AbpUsers_UserId",
                table: "AbpUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AbpUsers_CreatorUserId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AbpUsers_DeleterUserId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AbpUsers_LastModifierUserId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AbpUserTokens_AbpUsers_UserId",
                table: "AbpUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserTokens",
                table: "AbpUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUsers",
                table: "AbpUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserRoles",
                table: "AbpUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserOrganizationUnits",
                table: "AbpUserOrganizationUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserNotifications",
                table: "AbpUserNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserLogins",
                table: "AbpUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserLoginAttempts",
                table: "AbpUserLoginAttempts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserClaims",
                table: "AbpUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpUserAccounts",
                table: "AbpUserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpTenants",
                table: "AbpTenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpTenantNotifications",
                table: "AbpTenantNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpSettings",
                table: "AbpSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpRoles",
                table: "AbpRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpRoleClaims",
                table: "AbpRoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpPermissions",
                table: "AbpPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpOrganizationUnits",
                table: "AbpOrganizationUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpNotificationSubscriptions",
                table: "AbpNotificationSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpNotifications",
                table: "AbpNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpLanguageTexts",
                table: "AbpLanguageTexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpLanguages",
                table: "AbpLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpFeatures",
                table: "AbpFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEntityPropertyChanges",
                table: "AbpEntityPropertyChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEntityChangeSets",
                table: "AbpEntityChangeSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEntityChanges",
                table: "AbpEntityChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpEditions",
                table: "AbpEditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpBackgroundJobs",
                table: "AbpBackgroundJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpAuditLogs",
                table: "AbpAuditLogs");

            migrationBuilder.RenameTable(
                name: "AbpUserTokens",
                newName: "TUserTokens");

            migrationBuilder.RenameTable(
                name: "AbpUsers",
                newName: "TUsers");

            migrationBuilder.RenameTable(
                name: "AbpUserRoles",
                newName: "TUserRoles");

            migrationBuilder.RenameTable(
                name: "AbpUserOrganizationUnits",
                newName: "TUserOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "AbpUserNotifications",
                newName: "TUserNotifications");

            migrationBuilder.RenameTable(
                name: "AbpUserLogins",
                newName: "TUserLogins");

            migrationBuilder.RenameTable(
                name: "AbpUserLoginAttempts",
                newName: "TUserLoginAttempts");

            migrationBuilder.RenameTable(
                name: "AbpUserClaims",
                newName: "TUserClaims");

            migrationBuilder.RenameTable(
                name: "AbpUserAccounts",
                newName: "TUserAccounts");

            migrationBuilder.RenameTable(
                name: "AbpTenants",
                newName: "TTenants");

            migrationBuilder.RenameTable(
                name: "AbpTenantNotifications",
                newName: "TTenantNotifications");

            migrationBuilder.RenameTable(
                name: "AbpSettings",
                newName: "TSettings");

            migrationBuilder.RenameTable(
                name: "AbpRoles",
                newName: "TRoles");

            migrationBuilder.RenameTable(
                name: "AbpRoleClaims",
                newName: "TRoleClaims");

            migrationBuilder.RenameTable(
                name: "AbpPermissions",
                newName: "TPermissions");

            migrationBuilder.RenameTable(
                name: "AbpOrganizationUnits",
                newName: "TOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "AbpNotificationSubscriptions",
                newName: "TNotificationSubscriptions");

            migrationBuilder.RenameTable(
                name: "AbpNotifications",
                newName: "TNotifications");

            migrationBuilder.RenameTable(
                name: "AbpLanguageTexts",
                newName: "TLanguageTexts");

            migrationBuilder.RenameTable(
                name: "AbpLanguages",
                newName: "TLanguages");

            migrationBuilder.RenameTable(
                name: "AbpFeatures",
                newName: "TFeatures");

            migrationBuilder.RenameTable(
                name: "AbpEntityPropertyChanges",
                newName: "TEntityPropertyChanges");

            migrationBuilder.RenameTable(
                name: "AbpEntityChangeSets",
                newName: "TEntityChangeSets");

            migrationBuilder.RenameTable(
                name: "AbpEntityChanges",
                newName: "TEntityChanges");

            migrationBuilder.RenameTable(
                name: "AbpEditions",
                newName: "TEditions");

            migrationBuilder.RenameTable(
                name: "AbpBackgroundJobs",
                newName: "TBackgroundJobs");

            migrationBuilder.RenameTable(
                name: "AbpAuditLogs",
                newName: "TAuditLogs");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserTokens_TenantId_UserId",
                table: "TUserTokens",
                newName: "IX_TUserTokens_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserTokens_UserId",
                table: "TUserTokens",
                newName: "IX_TUserTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUsers_TenantId_NormalizedUserName",
                table: "TUsers",
                newName: "IX_TUsers_TenantId_NormalizedUserName");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUsers_TenantId_NormalizedEmailAddress",
                table: "TUsers",
                newName: "IX_TUsers_TenantId_NormalizedEmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUsers_LastModifierUserId",
                table: "TUsers",
                newName: "IX_TUsers_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUsers_DeleterUserId",
                table: "TUsers",
                newName: "IX_TUsers_DeleterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUsers_CreatorUserId",
                table: "TUsers",
                newName: "IX_TUsers_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserRoles_TenantId_UserId",
                table: "TUserRoles",
                newName: "IX_TUserRoles_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserRoles_TenantId_RoleId",
                table: "TUserRoles",
                newName: "IX_TUserRoles_TenantId_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserRoles_UserId",
                table: "TUserRoles",
                newName: "IX_TUserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserOrganizationUnits_TenantId_UserId",
                table: "TUserOrganizationUnits",
                newName: "IX_TUserOrganizationUnits_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserOrganizationUnits_TenantId_OrganizationUnitId",
                table: "TUserOrganizationUnits",
                newName: "IX_TUserOrganizationUnits_TenantId_OrganizationUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserNotifications_UserId_State_CreationTime",
                table: "TUserNotifications",
                newName: "IX_TUserNotifications_UserId_State_CreationTime");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserLogins_TenantId_LoginProvider_ProviderKey",
                table: "TUserLogins",
                newName: "IX_TUserLogins_TenantId_LoginProvider_ProviderKey");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserLogins_TenantId_UserId",
                table: "TUserLogins",
                newName: "IX_TUserLogins_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserLogins_UserId",
                table: "TUserLogins",
                newName: "IX_TUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserLoginAttempts_TenancyName_UserNameOrEmailAddress_Result",
                table: "TUserLoginAttempts",
                newName: "IX_TUserLoginAttempts_TenancyName_UserNameOrEmailAddress_Result");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserLoginAttempts_UserId_TenantId",
                table: "TUserLoginAttempts",
                newName: "IX_TUserLoginAttempts_UserId_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserClaims_TenantId_ClaimType",
                table: "TUserClaims",
                newName: "IX_TUserClaims_TenantId_ClaimType");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserClaims_UserId",
                table: "TUserClaims",
                newName: "IX_TUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserAccounts_TenantId_UserName",
                table: "TUserAccounts",
                newName: "IX_TUserAccounts_TenantId_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserAccounts_TenantId_UserId",
                table: "TUserAccounts",
                newName: "IX_TUserAccounts_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserAccounts_TenantId_EmailAddress",
                table: "TUserAccounts",
                newName: "IX_TUserAccounts_TenantId_EmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserAccounts_UserName",
                table: "TUserAccounts",
                newName: "IX_TUserAccounts_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_AbpUserAccounts_EmailAddress",
                table: "TUserAccounts",
                newName: "IX_TUserAccounts_EmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTenants_TenancyName",
                table: "TTenants",
                newName: "IX_TTenants_TenancyName");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTenants_LastModifierUserId",
                table: "TTenants",
                newName: "IX_TTenants_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTenants_EditionId",
                table: "TTenants",
                newName: "IX_TTenants_EditionId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTenants_DeleterUserId",
                table: "TTenants",
                newName: "IX_TTenants_DeleterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTenants_CreatorUserId",
                table: "TTenants",
                newName: "IX_TTenants_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpTenantNotifications_TenantId",
                table: "TTenantNotifications",
                newName: "IX_TTenantNotifications_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpSettings_TenantId_Name",
                table: "TSettings",
                newName: "IX_TSettings_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AbpSettings_UserId",
                table: "TSettings",
                newName: "IX_TSettings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpRoles_TenantId_NormalizedName",
                table: "TRoles",
                newName: "IX_TRoles_TenantId_NormalizedName");

            migrationBuilder.RenameIndex(
                name: "IX_AbpRoles_LastModifierUserId",
                table: "TRoles",
                newName: "IX_TRoles_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpRoles_DeleterUserId",
                table: "TRoles",
                newName: "IX_TRoles_DeleterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpRoles_CreatorUserId",
                table: "TRoles",
                newName: "IX_TRoles_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpRoleClaims_TenantId_ClaimType",
                table: "TRoleClaims",
                newName: "IX_TRoleClaims_TenantId_ClaimType");

            migrationBuilder.RenameIndex(
                name: "IX_AbpRoleClaims_RoleId",
                table: "TRoleClaims",
                newName: "IX_TRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpPermissions_UserId",
                table: "TPermissions",
                newName: "IX_TPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpPermissions_RoleId",
                table: "TPermissions",
                newName: "IX_TPermissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpPermissions_TenantId_Name",
                table: "TPermissions",
                newName: "IX_TPermissions_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AbpOrganizationUnits_TenantId_Code",
                table: "TOrganizationUnits",
                newName: "IX_TOrganizationUnits_TenantId_Code");

            migrationBuilder.RenameIndex(
                name: "IX_AbpOrganizationUnits_ParentId",
                table: "TOrganizationUnits",
                newName: "IX_TOrganizationUnits_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpNotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId",
                table: "TNotificationSubscriptions",
                newName: "IX_TNotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpNotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId",
                table: "TNotificationSubscriptions",
                newName: "IX_TNotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpLanguageTexts_TenantId_Source_LanguageName_Key",
                table: "TLanguageTexts",
                newName: "IX_TLanguageTexts_TenantId_Source_LanguageName_Key");

            migrationBuilder.RenameIndex(
                name: "IX_AbpLanguages_TenantId_Name",
                table: "TLanguages",
                newName: "IX_TLanguages_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AbpFeatures_TenantId_Name",
                table: "TFeatures",
                newName: "IX_TFeatures_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AbpFeatures_EditionId_Name",
                table: "TFeatures",
                newName: "IX_TFeatures_EditionId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityPropertyChanges_EntityChangeId",
                table: "TEntityPropertyChanges",
                newName: "IX_TEntityPropertyChanges_EntityChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityChangeSets_TenantId_UserId",
                table: "TEntityChangeSets",
                newName: "IX_TEntityChangeSets_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityChangeSets_TenantId_Reason",
                table: "TEntityChangeSets",
                newName: "IX_TEntityChangeSets_TenantId_Reason");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityChangeSets_TenantId_CreationTime",
                table: "TEntityChangeSets",
                newName: "IX_TEntityChangeSets_TenantId_CreationTime");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityChanges_EntityTypeFullName_EntityId",
                table: "TEntityChanges",
                newName: "IX_TEntityChanges_EntityTypeFullName_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpEntityChanges_EntityChangeSetId",
                table: "TEntityChanges",
                newName: "IX_TEntityChanges_EntityChangeSetId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "TBackgroundJobs",
                newName: "IX_TBackgroundJobs_IsAbandoned_NextTryTime");

            migrationBuilder.RenameIndex(
                name: "IX_AbpAuditLogs_TenantId_UserId",
                table: "TAuditLogs",
                newName: "IX_TAuditLogs_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AbpAuditLogs_TenantId_ExecutionTime",
                table: "TAuditLogs",
                newName: "IX_TAuditLogs_TenantId_ExecutionTime");

            migrationBuilder.RenameIndex(
                name: "IX_AbpAuditLogs_TenantId_ExecutionDuration",
                table: "TAuditLogs",
                newName: "IX_TAuditLogs_TenantId_ExecutionDuration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserTokens",
                table: "TUserTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUsers",
                table: "TUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserRoles",
                table: "TUserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserOrganizationUnits",
                table: "TUserOrganizationUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserNotifications",
                table: "TUserNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserLogins",
                table: "TUserLogins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserLoginAttempts",
                table: "TUserLoginAttempts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserClaims",
                table: "TUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TUserAccounts",
                table: "TUserAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TTenants",
                table: "TTenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TTenantNotifications",
                table: "TTenantNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TSettings",
                table: "TSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TRoles",
                table: "TRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TRoleClaims",
                table: "TRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TPermissions",
                table: "TPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TOrganizationUnits",
                table: "TOrganizationUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TNotificationSubscriptions",
                table: "TNotificationSubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TNotifications",
                table: "TNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TLanguageTexts",
                table: "TLanguageTexts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TLanguages",
                table: "TLanguages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TFeatures",
                table: "TFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEntityPropertyChanges",
                table: "TEntityPropertyChanges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEntityChangeSets",
                table: "TEntityChangeSets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEntityChanges",
                table: "TEntityChanges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TEditions",
                table: "TEditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBackgroundJobs",
                table: "TBackgroundJobs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAuditLogs",
                table: "TAuditLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TEntityChanges_TEntityChangeSets_EntityChangeSetId",
                table: "TEntityChanges",
                column: "EntityChangeSetId",
                principalTable: "TEntityChangeSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TEntityPropertyChanges_TEntityChanges_EntityChangeId",
                table: "TEntityPropertyChanges",
                column: "EntityChangeId",
                principalTable: "TEntityChanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TFeatures_TEditions_EditionId",
                table: "TFeatures",
                column: "EditionId",
                principalTable: "TEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TOrganizationUnits_TOrganizationUnits_ParentId",
                table: "TOrganizationUnits",
                column: "ParentId",
                principalTable: "TOrganizationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TPermissions_TRoles_RoleId",
                table: "TPermissions",
                column: "RoleId",
                principalTable: "TRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TPermissions_TUsers_UserId",
                table: "TPermissions",
                column: "UserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TRoleClaims_TRoles_RoleId",
                table: "TRoleClaims",
                column: "RoleId",
                principalTable: "TRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TRoles_TUsers_CreatorUserId",
                table: "TRoles",
                column: "CreatorUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TRoles_TUsers_DeleterUserId",
                table: "TRoles",
                column: "DeleterUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TRoles_TUsers_LastModifierUserId",
                table: "TRoles",
                column: "LastModifierUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TSettings_TUsers_UserId",
                table: "TSettings",
                column: "UserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TTenants_TUsers_CreatorUserId",
                table: "TTenants",
                column: "CreatorUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TTenants_TUsers_DeleterUserId",
                table: "TTenants",
                column: "DeleterUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TTenants_TEditions_EditionId",
                table: "TTenants",
                column: "EditionId",
                principalTable: "TEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TTenants_TUsers_LastModifierUserId",
                table: "TTenants",
                column: "LastModifierUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TUserClaims_TUsers_UserId",
                table: "TUserClaims",
                column: "UserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TUserLogins_TUsers_UserId",
                table: "TUserLogins",
                column: "UserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TUserRoles_TUsers_UserId",
                table: "TUserRoles",
                column: "UserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TUsers_TUsers_CreatorUserId",
                table: "TUsers",
                column: "CreatorUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TUsers_TUsers_DeleterUserId",
                table: "TUsers",
                column: "DeleterUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TUsers_TUsers_LastModifierUserId",
                table: "TUsers",
                column: "LastModifierUserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TUserTokens_TUsers_UserId",
                table: "TUserTokens",
                column: "UserId",
                principalTable: "TUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TEntityChanges_TEntityChangeSets_EntityChangeSetId",
                table: "TEntityChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_TEntityPropertyChanges_TEntityChanges_EntityChangeId",
                table: "TEntityPropertyChanges");

            migrationBuilder.DropForeignKey(
                name: "FK_TFeatures_TEditions_EditionId",
                table: "TFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_TOrganizationUnits_TOrganizationUnits_ParentId",
                table: "TOrganizationUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_TPermissions_TRoles_RoleId",
                table: "TPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TPermissions_TUsers_UserId",
                table: "TPermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_TRoleClaims_TRoles_RoleId",
                table: "TRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_TRoles_TUsers_CreatorUserId",
                table: "TRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TRoles_TUsers_DeleterUserId",
                table: "TRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TRoles_TUsers_LastModifierUserId",
                table: "TRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TSettings_TUsers_UserId",
                table: "TSettings");

            migrationBuilder.DropForeignKey(
                name: "FK_TTenants_TUsers_CreatorUserId",
                table: "TTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_TTenants_TUsers_DeleterUserId",
                table: "TTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_TTenants_TEditions_EditionId",
                table: "TTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_TTenants_TUsers_LastModifierUserId",
                table: "TTenants");

            migrationBuilder.DropForeignKey(
                name: "FK_TUserClaims_TUsers_UserId",
                table: "TUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_TUserLogins_TUsers_UserId",
                table: "TUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_TUserRoles_TUsers_UserId",
                table: "TUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_TUsers_TUsers_CreatorUserId",
                table: "TUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TUsers_TUsers_DeleterUserId",
                table: "TUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TUsers_TUsers_LastModifierUserId",
                table: "TUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_TUserTokens_TUsers_UserId",
                table: "TUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserTokens",
                table: "TUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUsers",
                table: "TUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserRoles",
                table: "TUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserOrganizationUnits",
                table: "TUserOrganizationUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserNotifications",
                table: "TUserNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserLogins",
                table: "TUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserLoginAttempts",
                table: "TUserLoginAttempts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserClaims",
                table: "TUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TUserAccounts",
                table: "TUserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TTenants",
                table: "TTenants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TTenantNotifications",
                table: "TTenantNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TSettings",
                table: "TSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TRoles",
                table: "TRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TRoleClaims",
                table: "TRoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TPermissions",
                table: "TPermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TOrganizationUnits",
                table: "TOrganizationUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TNotificationSubscriptions",
                table: "TNotificationSubscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TNotifications",
                table: "TNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TLanguageTexts",
                table: "TLanguageTexts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TLanguages",
                table: "TLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TFeatures",
                table: "TFeatures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEntityPropertyChanges",
                table: "TEntityPropertyChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEntityChangeSets",
                table: "TEntityChangeSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEntityChanges",
                table: "TEntityChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TEditions",
                table: "TEditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBackgroundJobs",
                table: "TBackgroundJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAuditLogs",
                table: "TAuditLogs");

            migrationBuilder.RenameTable(
                name: "TUserTokens",
                newName: "AbpUserTokens");

            migrationBuilder.RenameTable(
                name: "TUsers",
                newName: "AbpUsers");

            migrationBuilder.RenameTable(
                name: "TUserRoles",
                newName: "AbpUserRoles");

            migrationBuilder.RenameTable(
                name: "TUserOrganizationUnits",
                newName: "AbpUserOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "TUserNotifications",
                newName: "AbpUserNotifications");

            migrationBuilder.RenameTable(
                name: "TUserLogins",
                newName: "AbpUserLogins");

            migrationBuilder.RenameTable(
                name: "TUserLoginAttempts",
                newName: "AbpUserLoginAttempts");

            migrationBuilder.RenameTable(
                name: "TUserClaims",
                newName: "AbpUserClaims");

            migrationBuilder.RenameTable(
                name: "TUserAccounts",
                newName: "AbpUserAccounts");

            migrationBuilder.RenameTable(
                name: "TTenants",
                newName: "AbpTenants");

            migrationBuilder.RenameTable(
                name: "TTenantNotifications",
                newName: "AbpTenantNotifications");

            migrationBuilder.RenameTable(
                name: "TSettings",
                newName: "AbpSettings");

            migrationBuilder.RenameTable(
                name: "TRoles",
                newName: "AbpRoles");

            migrationBuilder.RenameTable(
                name: "TRoleClaims",
                newName: "AbpRoleClaims");

            migrationBuilder.RenameTable(
                name: "TPermissions",
                newName: "AbpPermissions");

            migrationBuilder.RenameTable(
                name: "TOrganizationUnits",
                newName: "AbpOrganizationUnits");

            migrationBuilder.RenameTable(
                name: "TNotificationSubscriptions",
                newName: "AbpNotificationSubscriptions");

            migrationBuilder.RenameTable(
                name: "TNotifications",
                newName: "AbpNotifications");

            migrationBuilder.RenameTable(
                name: "TLanguageTexts",
                newName: "AbpLanguageTexts");

            migrationBuilder.RenameTable(
                name: "TLanguages",
                newName: "AbpLanguages");

            migrationBuilder.RenameTable(
                name: "TFeatures",
                newName: "AbpFeatures");

            migrationBuilder.RenameTable(
                name: "TEntityPropertyChanges",
                newName: "AbpEntityPropertyChanges");

            migrationBuilder.RenameTable(
                name: "TEntityChangeSets",
                newName: "AbpEntityChangeSets");

            migrationBuilder.RenameTable(
                name: "TEntityChanges",
                newName: "AbpEntityChanges");

            migrationBuilder.RenameTable(
                name: "TEditions",
                newName: "AbpEditions");

            migrationBuilder.RenameTable(
                name: "TBackgroundJobs",
                newName: "AbpBackgroundJobs");

            migrationBuilder.RenameTable(
                name: "TAuditLogs",
                newName: "AbpAuditLogs");

            migrationBuilder.RenameIndex(
                name: "IX_TUserTokens_TenantId_UserId",
                table: "AbpUserTokens",
                newName: "IX_AbpUserTokens_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserTokens_UserId",
                table: "AbpUserTokens",
                newName: "IX_AbpUserTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUsers_TenantId_NormalizedUserName",
                table: "AbpUsers",
                newName: "IX_AbpUsers_TenantId_NormalizedUserName");

            migrationBuilder.RenameIndex(
                name: "IX_TUsers_TenantId_NormalizedEmailAddress",
                table: "AbpUsers",
                newName: "IX_AbpUsers_TenantId_NormalizedEmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_TUsers_LastModifierUserId",
                table: "AbpUsers",
                newName: "IX_AbpUsers_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUsers_DeleterUserId",
                table: "AbpUsers",
                newName: "IX_AbpUsers_DeleterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUsers_CreatorUserId",
                table: "AbpUsers",
                newName: "IX_AbpUsers_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserRoles_TenantId_UserId",
                table: "AbpUserRoles",
                newName: "IX_AbpUserRoles_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserRoles_TenantId_RoleId",
                table: "AbpUserRoles",
                newName: "IX_AbpUserRoles_TenantId_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserRoles_UserId",
                table: "AbpUserRoles",
                newName: "IX_AbpUserRoles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserOrganizationUnits_TenantId_UserId",
                table: "AbpUserOrganizationUnits",
                newName: "IX_AbpUserOrganizationUnits_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserOrganizationUnits_TenantId_OrganizationUnitId",
                table: "AbpUserOrganizationUnits",
                newName: "IX_AbpUserOrganizationUnits_TenantId_OrganizationUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserNotifications_UserId_State_CreationTime",
                table: "AbpUserNotifications",
                newName: "IX_AbpUserNotifications_UserId_State_CreationTime");

            migrationBuilder.RenameIndex(
                name: "IX_TUserLogins_TenantId_LoginProvider_ProviderKey",
                table: "AbpUserLogins",
                newName: "IX_AbpUserLogins_TenantId_LoginProvider_ProviderKey");

            migrationBuilder.RenameIndex(
                name: "IX_TUserLogins_TenantId_UserId",
                table: "AbpUserLogins",
                newName: "IX_AbpUserLogins_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserLogins_UserId",
                table: "AbpUserLogins",
                newName: "IX_AbpUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserLoginAttempts_TenancyName_UserNameOrEmailAddress_Result",
                table: "AbpUserLoginAttempts",
                newName: "IX_AbpUserLoginAttempts_TenancyName_UserNameOrEmailAddress_Result");

            migrationBuilder.RenameIndex(
                name: "IX_TUserLoginAttempts_UserId_TenantId",
                table: "AbpUserLoginAttempts",
                newName: "IX_AbpUserLoginAttempts_UserId_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserClaims_TenantId_ClaimType",
                table: "AbpUserClaims",
                newName: "IX_AbpUserClaims_TenantId_ClaimType");

            migrationBuilder.RenameIndex(
                name: "IX_TUserClaims_UserId",
                table: "AbpUserClaims",
                newName: "IX_AbpUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserAccounts_TenantId_UserName",
                table: "AbpUserAccounts",
                newName: "IX_AbpUserAccounts_TenantId_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_TUserAccounts_TenantId_UserId",
                table: "AbpUserAccounts",
                newName: "IX_AbpUserAccounts_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TUserAccounts_TenantId_EmailAddress",
                table: "AbpUserAccounts",
                newName: "IX_AbpUserAccounts_TenantId_EmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_TUserAccounts_UserName",
                table: "AbpUserAccounts",
                newName: "IX_AbpUserAccounts_UserName");

            migrationBuilder.RenameIndex(
                name: "IX_TUserAccounts_EmailAddress",
                table: "AbpUserAccounts",
                newName: "IX_AbpUserAccounts_EmailAddress");

            migrationBuilder.RenameIndex(
                name: "IX_TTenants_TenancyName",
                table: "AbpTenants",
                newName: "IX_AbpTenants_TenancyName");

            migrationBuilder.RenameIndex(
                name: "IX_TTenants_LastModifierUserId",
                table: "AbpTenants",
                newName: "IX_AbpTenants_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TTenants_EditionId",
                table: "AbpTenants",
                newName: "IX_AbpTenants_EditionId");

            migrationBuilder.RenameIndex(
                name: "IX_TTenants_DeleterUserId",
                table: "AbpTenants",
                newName: "IX_AbpTenants_DeleterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TTenants_CreatorUserId",
                table: "AbpTenants",
                newName: "IX_AbpTenants_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TTenantNotifications_TenantId",
                table: "AbpTenantNotifications",
                newName: "IX_AbpTenantNotifications_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_TSettings_TenantId_Name",
                table: "AbpSettings",
                newName: "IX_AbpSettings_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_TSettings_UserId",
                table: "AbpSettings",
                newName: "IX_AbpSettings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TRoles_TenantId_NormalizedName",
                table: "AbpRoles",
                newName: "IX_AbpRoles_TenantId_NormalizedName");

            migrationBuilder.RenameIndex(
                name: "IX_TRoles_LastModifierUserId",
                table: "AbpRoles",
                newName: "IX_AbpRoles_LastModifierUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TRoles_DeleterUserId",
                table: "AbpRoles",
                newName: "IX_AbpRoles_DeleterUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TRoles_CreatorUserId",
                table: "AbpRoles",
                newName: "IX_AbpRoles_CreatorUserId");

            migrationBuilder.RenameIndex(
                name: "IX_TRoleClaims_TenantId_ClaimType",
                table: "AbpRoleClaims",
                newName: "IX_AbpRoleClaims_TenantId_ClaimType");

            migrationBuilder.RenameIndex(
                name: "IX_TRoleClaims_RoleId",
                table: "AbpRoleClaims",
                newName: "IX_AbpRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TPermissions_UserId",
                table: "AbpPermissions",
                newName: "IX_AbpPermissions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TPermissions_RoleId",
                table: "AbpPermissions",
                newName: "IX_AbpPermissions_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_TPermissions_TenantId_Name",
                table: "AbpPermissions",
                newName: "IX_AbpPermissions_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_TOrganizationUnits_TenantId_Code",
                table: "AbpOrganizationUnits",
                newName: "IX_AbpOrganizationUnits_TenantId_Code");

            migrationBuilder.RenameIndex(
                name: "IX_TOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits",
                newName: "IX_AbpOrganizationUnits_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_TNotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId",
                table: "AbpNotificationSubscriptions",
                newName: "IX_AbpNotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TNotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId",
                table: "AbpNotificationSubscriptions",
                newName: "IX_AbpNotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TLanguageTexts_TenantId_Source_LanguageName_Key",
                table: "AbpLanguageTexts",
                newName: "IX_AbpLanguageTexts_TenantId_Source_LanguageName_Key");

            migrationBuilder.RenameIndex(
                name: "IX_TLanguages_TenantId_Name",
                table: "AbpLanguages",
                newName: "IX_AbpLanguages_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_TFeatures_TenantId_Name",
                table: "AbpFeatures",
                newName: "IX_AbpFeatures_TenantId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_TFeatures_EditionId_Name",
                table: "AbpFeatures",
                newName: "IX_AbpFeatures_EditionId_Name");

            migrationBuilder.RenameIndex(
                name: "IX_TEntityPropertyChanges_EntityChangeId",
                table: "AbpEntityPropertyChanges",
                newName: "IX_AbpEntityPropertyChanges_EntityChangeId");

            migrationBuilder.RenameIndex(
                name: "IX_TEntityChangeSets_TenantId_UserId",
                table: "AbpEntityChangeSets",
                newName: "IX_AbpEntityChangeSets_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TEntityChangeSets_TenantId_Reason",
                table: "AbpEntityChangeSets",
                newName: "IX_AbpEntityChangeSets_TenantId_Reason");

            migrationBuilder.RenameIndex(
                name: "IX_TEntityChangeSets_TenantId_CreationTime",
                table: "AbpEntityChangeSets",
                newName: "IX_AbpEntityChangeSets_TenantId_CreationTime");

            migrationBuilder.RenameIndex(
                name: "IX_TEntityChanges_EntityTypeFullName_EntityId",
                table: "AbpEntityChanges",
                newName: "IX_AbpEntityChanges_EntityTypeFullName_EntityId");

            migrationBuilder.RenameIndex(
                name: "IX_TEntityChanges_EntityChangeSetId",
                table: "AbpEntityChanges",
                newName: "IX_AbpEntityChanges_EntityChangeSetId");

            migrationBuilder.RenameIndex(
                name: "IX_TBackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                newName: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime");

            migrationBuilder.RenameIndex(
                name: "IX_TAuditLogs_TenantId_UserId",
                table: "AbpAuditLogs",
                newName: "IX_AbpAuditLogs_TenantId_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TAuditLogs_TenantId_ExecutionTime",
                table: "AbpAuditLogs",
                newName: "IX_AbpAuditLogs_TenantId_ExecutionTime");

            migrationBuilder.RenameIndex(
                name: "IX_TAuditLogs_TenantId_ExecutionDuration",
                table: "AbpAuditLogs",
                newName: "IX_AbpAuditLogs_TenantId_ExecutionDuration");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserTokens",
                table: "AbpUserTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUsers",
                table: "AbpUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserRoles",
                table: "AbpUserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserOrganizationUnits",
                table: "AbpUserOrganizationUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserNotifications",
                table: "AbpUserNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserLogins",
                table: "AbpUserLogins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserLoginAttempts",
                table: "AbpUserLoginAttempts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserClaims",
                table: "AbpUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpUserAccounts",
                table: "AbpUserAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpTenants",
                table: "AbpTenants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpTenantNotifications",
                table: "AbpTenantNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpSettings",
                table: "AbpSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpRoles",
                table: "AbpRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpRoleClaims",
                table: "AbpRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpPermissions",
                table: "AbpPermissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpOrganizationUnits",
                table: "AbpOrganizationUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpNotificationSubscriptions",
                table: "AbpNotificationSubscriptions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpNotifications",
                table: "AbpNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpLanguageTexts",
                table: "AbpLanguageTexts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpLanguages",
                table: "AbpLanguages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpFeatures",
                table: "AbpFeatures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEntityPropertyChanges",
                table: "AbpEntityPropertyChanges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEntityChangeSets",
                table: "AbpEntityChangeSets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEntityChanges",
                table: "AbpEntityChanges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpEditions",
                table: "AbpEditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpBackgroundJobs",
                table: "AbpBackgroundJobs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpAuditLogs",
                table: "AbpAuditLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpEntityChanges_AbpEntityChangeSets_EntityChangeSetId",
                table: "AbpEntityChanges",
                column: "EntityChangeSetId",
                principalTable: "AbpEntityChangeSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId",
                table: "AbpEntityPropertyChanges",
                column: "EntityChangeId",
                principalTable: "AbpEntityChanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpFeatures_AbpEditions_EditionId",
                table: "AbpFeatures",
                column: "EditionId",
                principalTable: "AbpEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId",
                table: "AbpOrganizationUnits",
                column: "ParentId",
                principalTable: "AbpOrganizationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpPermissions_AbpRoles_RoleId",
                table: "AbpPermissions",
                column: "RoleId",
                principalTable: "AbpRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpPermissions_AbpUsers_UserId",
                table: "AbpPermissions",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpRoleClaims_AbpRoles_RoleId",
                table: "AbpRoleClaims",
                column: "RoleId",
                principalTable: "AbpRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpRoles_AbpUsers_CreatorUserId",
                table: "AbpRoles",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpRoles_AbpUsers_DeleterUserId",
                table: "AbpRoles",
                column: "DeleterUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpRoles_AbpUsers_LastModifierUserId",
                table: "AbpRoles",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpSettings_AbpUsers_UserId",
                table: "AbpSettings",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTenants_AbpUsers_CreatorUserId",
                table: "AbpTenants",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTenants_AbpUsers_DeleterUserId",
                table: "AbpTenants",
                column: "DeleterUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTenants_AbpEditions_EditionId",
                table: "AbpTenants",
                column: "EditionId",
                principalTable: "AbpEditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpTenants_AbpUsers_LastModifierUserId",
                table: "AbpTenants",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUserClaims_AbpUsers_UserId",
                table: "AbpUserClaims",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUserLogins_AbpUsers_UserId",
                table: "AbpUserLogins",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUserRoles_AbpUsers_UserId",
                table: "AbpUserRoles",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AbpUsers_CreatorUserId",
                table: "AbpUsers",
                column: "CreatorUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AbpUsers_DeleterUserId",
                table: "AbpUsers",
                column: "DeleterUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AbpUsers_LastModifierUserId",
                table: "AbpUsers",
                column: "LastModifierUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUserTokens_AbpUsers_UserId",
                table: "AbpUserTokens",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
