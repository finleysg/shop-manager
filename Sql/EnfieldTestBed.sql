USE EnfieldMasterTestBed
GO

-- CLEAR ALL TABLES
delete from Labor
delete from Service
delete from InvoiceHistory
delete from Invoice
delete from Account
delete from Contact
delete from Employee
delete from aspnet_SchemaVersions
delete from aspnet_Membership
delete from aspnet_UsersInRoles
delete from aspnet_Users
delete from aspnet_Roles
delete from aspnet_Applications
delete from LoginAttemptLog
delete from ApprovedIp
delete from ErrorLog
delete from EmployeeLog
delete from AccountTypeService
delete from AccountTypeLabor
delete from AccountType
delete from ContactType
delete from InvoiceType
delete from LaborType
delete from Location
delete from ServiceType
GO

-- INSERTS
-- LOOKUP DATA
SET IDENTITY_INSERT [AccountType] ON
INSERT [AccountType] ([AccountTypeId], [Description], [TaxRate], [IsActive], [ModifyUser], [ModifyDate]) VALUES (1, N'DEALER', CAST(0.0000 AS Decimal(6, 4)), 1, N'system', CAST(0x00009BE10077E3B6 AS DateTime))
INSERT [AccountType] ([AccountTypeId], [Description], [TaxRate], [IsActive], [ModifyUser], [ModifyDate]) VALUES (2, N'PRIVATE', CAST(0.0925 AS Decimal(6, 4)), 1, N'system', CAST(0x00009BE10077E3B6 AS DateTime))
SET IDENTITY_INSERT [AccountType] OFF

SET IDENTITY_INSERT [ServiceType] ON
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (3, N'COMPLETE DETAIL')
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (10, N'NEW CAR DETAIL')
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (11, N'PREDELIVERY')
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (17, N'WASH ONLY')
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (29, N'PIN STRIPE')
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (41, N'FLEET DETAIL')
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (43, N'BED LINER')
INSERT [ServiceType] ([ServiceTypeId], [Description]) VALUES (44, N'VENT VISOR REMOVAL')
SET IDENTITY_INSERT [ServiceType] OFF

SET IDENTITY_INSERT [Location] ON
INSERT [Location] ([LocationId], [LocationName]) VALUES (1, N'MAIN SHOP')
INSERT [Location] ([LocationId], [LocationName]) VALUES (2, N'DOBBS')
INSERT [Location] ([LocationId], [LocationName]) VALUES (3, N'DOBBS WC')
SET IDENTITY_INSERT [Location] OFF

SET IDENTITY_INSERT [LaborType] ON
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (1, N'BUFF BAY')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (3, N'HAND WASH')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (4, N'INTERIORS')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (5, N'NEW CAR')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (7, N'NEW CAR-W/B')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (8, N'PREDELIVERY')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (14, N'WET BAY')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (23, N'NEW CAR-INT')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (36, N'DOOR EDGE GUARD')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (37, N'PIN STRIPE')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (38, N'DECAL REMOVAL')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (39, N'FLEET DETAIL')
INSERT [LaborType] ([LaborTypeId], [Description]) VALUES (40, N'BED LINER')
SET IDENTITY_INSERT [LaborType] OFF

SET IDENTITY_INSERT [InvoiceType] ON
INSERT [InvoiceType] ([InvoiceTypeId], [Description]) VALUES (1, N'REGULAR')
INSERT [InvoiceType] ([InvoiceTypeId], [Description]) VALUES (2, N'PREDELIVERY')
SET IDENTITY_INSERT [InvoiceType] OFF

SET IDENTITY_INSERT [ContactType] ON
INSERT [ContactType] ([ContactTypeId], [Description]) VALUES (1, N'HOME PHONE')
INSERT [ContactType] ([ContactTypeId], [Description]) VALUES (2, N'BUSINESS PHONE')
INSERT [ContactType] ([ContactTypeId], [Description]) VALUES (3, N'CELL PHONE')
INSERT [ContactType] ([ContactTypeId], [Description]) VALUES (4, N'FAX')
INSERT [ContactType] ([ContactTypeId], [Description]) VALUES (5, N'EMAIL')
SET IDENTITY_INSERT [ContactType] OFF

SET IDENTITY_INSERT [AccountTypeService] ON
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (1, 1, 3, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (15, 1, 17, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (31, 2, 17, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (43, 1, 29, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (44, 1, 11, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (45, 1, 10, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (48, 1, 41, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (55, 1, 43, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
INSERT [AccountTypeService] ([AccountTypeServiceId], [AccountTypeId], [ServiceTypeId], [DefaultRate], [DefaultEstimatedTime], [IsActive]) VALUES (56, 1, 44, CAST(0.0000 AS Decimal(12, 4)), 0, 1)
SET IDENTITY_INSERT [AccountTypeService] OFF

SET IDENTITY_INSERT [AccountTypeLabor] ON
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (1, 1, 1, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (3, 1, 4, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (5, 1, 14, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (27, 15, 3, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (28, 31, 3, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (40, 43, 37, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (41, 44, 8, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (42, 45, 23, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (43, 45, 7, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (44, 1, 38, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (45, 1, 36, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (49, 1, 39, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (50, 1, 5, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (51, 48, 39, CAST(0.0000 AS Decimal(9, 4)), N'F')
INSERT [AccountTypeLabor] ([AccountTypeLaborId], [AccountTypeServiceId], [LaborTypeId], [DefaultRate], [DefaultRateType]) VALUES (52, 55, 40, CAST(0.0000 AS Decimal(9, 4)), N'F')
SET IDENTITY_INSERT [AccountTypeLabor] OFF

-- USER DATA
INSERT [aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) VALUES (N'/', N'/', N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', NULL)

INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'ccfcb8b8-5f47-4b83-b1af-176a21230405', N'BRIAN', N'brian', NULL, 0, CAST(0x00009FBB00D90729 AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'bf3940f9-99c2-4145-8d7a-0c7144aa5f9c', N'BRUCE', N'bruce', NULL, 0, CAST(0x00009FBC004B898A AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'1099071e-ad8f-4e2b-b85d-fec2c162e13c', N'CEDRIC', N'cedric', NULL, 0, CAST(0x00009FBB00ED499E AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'9c355e58-9a65-41d4-9ac4-e0e448facd44', N'DANIEL', N'daniel', NULL, 0, CAST(0x00009FB900EF0C46 AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'3641dd78-5c00-46dc-8998-704cba9875aa', N'DENIS E', N'denis e', NULL, 0, CAST(0x00009FBB00B79BAB AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'd588a0e5-e14f-4269-a2c1-0d1658ffb34d', N'JAMES', N'james', NULL, 0, CAST(0x00009FBB00DAF318 AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'd6969a2c-acc9-491d-a8c2-d39a4481fe48', N'JONATHAN', N'jonathan', NULL, 0, CAST(0x00009FBB00DB22E2 AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'125b6a55-1acc-439f-a5b6-b473bc6322b0', N'JOSH', N'josh', NULL, 0, CAST(0x00009FBB00DB511C AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'676739af-e11d-419d-83f4-7b81bee99aa7', N'MATT', N'matt', NULL, 0, CAST(0x00009FBB00DBB8CC AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'1da0baeb-93ae-4b8c-817b-a21b7d9c03ea', N'MICHELLE', N'michelle', NULL, 0, CAST(0x00009FBB01570E8C AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'2db273f1-76c5-453a-b4a4-11391c2a16fe', N'NIKI', N'niki', NULL, 0, CAST(0x00009FBB00EE34EF AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'0d103103-d7c7-49f0-8eae-4c3827570a7d', N'RYAN E', N'ryan e', NULL, 0, CAST(0x00009FBC0022FD76 AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'3bb61bde-f274-4edc-80c2-ab2db50ad890', N'STUART', N'stuart', NULL, 0, CAST(0x00009FB50160AB5E AS DateTime))
INSERT [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'c0a78667-2116-49f7-969f-1c96d1fefd1d', N'TY', N'ty', NULL, 0, CAST(0x00009FBB00F27E0D AS DateTime))

INSERT [aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'0266b3d7-999c-4f27-b122-f79dc3edaa33', N'Administrator', N'administrator', NULL)
INSERT [aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'd2583e71-a574-4579-a2e6-ee2e2589fe03', N'Employee', N'employee', NULL)
INSERT [aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'9678647e-fb72-423e-93e1-1ff4537f4af3', N'Manager', N'manager', NULL)

INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'd588a0e5-e14f-4269-a2c1-0d1658ffb34d', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'2db273f1-76c5-453a-b4a4-11391c2a16fe', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'ccfcb8b8-5f47-4b83-b1af-176a21230405', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'c0a78667-2116-49f7-969f-1c96d1fefd1d', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'3641dd78-5c00-46dc-8998-704cba9875aa', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'676739af-e11d-419d-83f4-7b81bee99aa7', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'1da0baeb-93ae-4b8c-817b-a21b7d9c03ea', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'125b6a55-1acc-439f-a5b6-b473bc6322b0', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'd6969a2c-acc9-491d-a8c2-d39a4481fe48', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'9c355e58-9a65-41d4-9ac4-e0e448facd44', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'1099071e-ad8f-4e2b-b85d-fec2c162e13c', N'd2583e71-a574-4579-a2e6-ee2e2589fe03')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'bf3940f9-99c2-4145-8d7a-0c7144aa5f9c', N'0266b3d7-999c-4f27-b122-f79dc3edaa33')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'0d103103-d7c7-49f0-8eae-4c3827570a7d', N'0266b3d7-999c-4f27-b122-f79dc3edaa33')
INSERT [aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'3bb61bde-f274-4edc-80c2-ab2db50ad890', N'0266b3d7-999c-4f27-b122-f79dc3edaa33')

INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'ccfcb8b8-5f47-4b83-b1af-176a21230405', N'MJQuwmPJOOui0X5cwUjuT+gu2Vo=', 1, N'VleM+p5OcgDm8IeqLsDfRg==', NULL, N'brian@enfieldsdetail.com', N'brian@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009F600104A438 AS DateTime), CAST(0x00009FBB00D90729 AS DateTime), CAST(0x00009F8000E16AA8 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'bf3940f9-99c2-4145-8d7a-0c7144aa5f9c', N'+phqNp7WClwUmck2bZpVzHrb3L0=', 1, N'f/snCfNhotrJ0xh7EF5RCw==', NULL, N'bruce@enfieldsdetail.com', N'bruce@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009BDB000C38E8 AS DateTime), CAST(0x00009FBC004B898A AS DateTime), CAST(0x00009D2601446579 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'1099071e-ad8f-4e2b-b85d-fec2c162e13c', N'Gu2g8NhVyRGjYKH/m3HlQIsbHIU=', 1, N'bQk5A50+UrkdcN8KwbCb2g==', NULL, N'cedric@enfieldsdetail.com', N'cedric@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009F600104E128 AS DateTime), CAST(0x00009FBB00ED499E AS DateTime), CAST(0x00009F60010507F6 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'9c355e58-9a65-41d4-9ac4-e0e448facd44', N'kqXlkhCPaKrJFsoFYiPP4wQNwgI=', 1, N'MswrzDMFdP4dArgjgc9tgA==', NULL, N'daniel@enfieldsdetail.com', N'daniel@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009F600104C3DC AS DateTime), CAST(0x00009FB900EF0C46 AS DateTime), CAST(0x00009F600105269C AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'3641dd78-5c00-46dc-8998-704cba9875aa', N'/Q2+RDuU+wjAE2L4aBD6rB5zLMo=', 1, N'QpkFflubZDfUlyMV+eVZLg==', NULL, N'denis_e@enfieldsdetail.com', N'denis_e@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009C6E0181134C AS DateTime), CAST(0x00009FBB00B79BAB AS DateTime), CAST(0x00009EBF00E66C3A AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'3bb61bde-f274-4edc-80c2-ab2db50ad890', N'QBvJBolkzTsyTFUJqPndsIkk7Tc=', 1, N'8IXeiZb5HwktVOooNWs7Lg==', NULL, N'finleysg@gmail.com', N'finleysg@gmail.com', NULL, NULL, 1, 0, CAST(0x00009B940163107C AS DateTime), CAST(0x00009FB50160AB5E AS DateTime), CAST(0x00009BDA012F86BF AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'd588a0e5-e14f-4269-a2c1-0d1658ffb34d', N'fyk5Cj4k3y/ByM4aRvbWNBTLCQY=', 1, N'JwMRIFXzTNUeCnMVZqMXbw==', NULL, N'james@enfieldsdetail.com', N'james@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009F4701053204 AS DateTime), CAST(0x00009FBB00DAF318 AS DateTime), CAST(0x00009F4701053204 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'd6969a2c-acc9-491d-a8c2-d39a4481fe48', N'CCL6Y7KVVgpA3y/+8nLzwaCyvRo=', 1, N'90TTvk/LaXCpKydc9oc+9g==', NULL, N'jonathan@enfieldsdetail.com', N'jonathan@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009C6E0181134C AS DateTime), CAST(0x00009FBB00DB22E2 AS DateTime), CAST(0x00009D250112A7EA AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'125b6a55-1acc-439f-a5b6-b473bc6322b0', N'08sa1YD6HKstmQCqrojse90PBzs=', 1, N'YD9ACbMlYtM3PF4Ji1Ni6w==', NULL, N'josh@enfieldsdetail.com', N'josh@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009C6E0181134C AS DateTime), CAST(0x00009FBB00DB511C AS DateTime), CAST(0x00009D250112C862 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'676739af-e11d-419d-83f4-7b81bee99aa7', N'ehuxlospGgmjxv8QIbzsPv7QqjA=', 1, N'2+8V0z7mv2tyubsrce1yhA==', NULL, N'matt@enfieldsdetail.com', N'matt@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009DF40135DFBC AS DateTime), CAST(0x00009FBB00DBB8CC AS DateTime), CAST(0x00009DF40135E8C7 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'1da0baeb-93ae-4b8c-817b-a21b7d9c03ea', N'a+wONi75wRqLWBTkDPEExan1/W4=', 1, N'xQ7O30iFSSyB4ZVuK9Ee8A==', NULL, N'michelle@enfieldsdetail.com', N'michelle@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009C6E0181134C AS DateTime), CAST(0x00009FBB01570E8C AS DateTime), CAST(0x00009EA2010F1642 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'2db273f1-76c5-453a-b4a4-11391c2a16fe', N'Yd2HX4YiwaQ+up94UAhaGFUiRWQ=', 1, N'Q/EEqjR4YHlIf8hcPwLPjg==', NULL, N'niki@enfieldsdetail.com', N'niki@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009C6E0181134C AS DateTime), CAST(0x00009FBB00EE34EF AS DateTime), CAST(0x00009FB7010B6395 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'0d103103-d7c7-49f0-8eae-4c3827570a7d', N'TjppcNpfTz+oAGGDVM65tbndJpk=', 1, N'8IXeiZb5HwktVOooNWs7Lg==', NULL, N'ryane@enfieldsdetail.com', N'ryane@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009FB3013F64C2 AS DateTime), CAST(0x00009FBC0022FD76 AS DateTime), CAST(0x00009FB401561F26 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'4d54ee3b-bf70-4090-b712-a0574ea4b9bc', N'c0a78667-2116-49f7-969f-1c96d1fefd1d', N'c/Vo8ZN5LfYS05I2mD1Mi/SIzH4=', 1, N'4glpgpypBkQMZAPBZBfuDg==', NULL, N'ty@enfieldsdetail.com', N'ty@enfieldsdetail.com', NULL, NULL, 1, 0, CAST(0x00009D3C00C5E554 AS DateTime), CAST(0x00009FBB00F27E0D AS DateTime), CAST(0x00009FBA011124D6 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)

INSERT [aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'common', N'1', 1)
INSERT [aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'health monitoring', N'1', 1)
INSERT [aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'membership', N'1', 1)
INSERT [aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'personalization', N'1', 1)
INSERT [aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'profile', N'1', 1)
INSERT [aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'role manager', N'1', 1)

SET IDENTITY_INSERT [Employee] ON
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (10, N'3641dd78-5c00-46dc-8998-704cba9875aa', N'DENIS E', NULL, NULL, NULL, CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'stuart', CAST(0x00009D7600000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (25, N'0d103103-d7c7-49f0-8eae-4c3827570a7d', N'RYAN E', NULL, NULL, NULL, CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'stuart', CAST(0x00009FB300000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (54, N'3bb61bde-f274-4edc-80c2-ab2db50ad890', N'STUART', NULL, NULL, CAST(0x0000806800000000 AS DateTime), CAST(0.000 AS Decimal(4, 3)), 1, NULL, N'Stuart', CAST(0x00009BE100000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (55, N'bf3940f9-99c2-4145-8d7a-0c7144aa5f9c', N'BRUCE', NULL, NULL, CAST(0x0000806800000000 AS DateTime), CAST(0.000 AS Decimal(4, 3)), 1, NULL, N'bruce', CAST(0x00009D2500000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (57, N'2db273f1-76c5-453a-b4a4-11391c2a16fe', N'NIKI', NULL, NULL, CAST(0x0000925F00000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'bruce', CAST(0x00009D2500000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (67, N'1da0baeb-93ae-4b8c-817b-a21b7d9c03ea', N'MICHELLE', NULL, NULL, CAST(0x0000944200000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'bruce', CAST(0x00009D2500000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (95, N'125b6a55-1acc-439f-a5b6-b473bc6322b0', N'JOSH', NULL, NULL, CAST(0x00009BB400000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'bruce', CAST(0x00009D5100000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (100, N'00000000-0000-0000-0000-000000000000', N'-NONE-', NULL, NULL, CAST(0x00009B8400000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'system', CAST(0x00009BE10077E368 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (102, N'd6969a2c-acc9-491d-a8c2-d39a4481fe48', N'JONATHAN', NULL, NULL, CAST(0x00009BFA00000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'bruce', CAST(0x00009D2500000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (104, N'c0a78667-2116-49f7-969f-1c96d1fefd1d', N'TY', NULL, NULL, CAST(0x00009FBA00000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'bruce', CAST(0x00009FBA00000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (107, N'676739af-e11d-419d-83f4-7b81bee99aa7', N'MATT', NULL, NULL, CAST(0x00009DF400000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'stuart', CAST(0x00009DF400000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (110, N'd588a0e5-e14f-4269-a2c1-0d1658ffb34d', N'JAMES', NULL, NULL, CAST(0x00009F4700000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'stuart', CAST(0x00009F4700000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (113, N'ccfcb8b8-5f47-4b83-b1af-176a21230405', N'BRIAN', NULL, NULL, CAST(0x00009F6000000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'stuart', CAST(0x00009F6000000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (114, N'9c355e58-9a65-41d4-9ac4-e0e448facd44', N'DANIEL', NULL, NULL, CAST(0x00009F6000000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'stuart', CAST(0x00009F6000000000 AS DateTime))
INSERT [Employee] ([EmployeeId], [UserId], [Name], [FirstName], [LastName], [StartDate], [Rate], [IsEmployed], [Notes], [ModifyUser], [ModifyDate]) VALUES (115, N'1099071e-ad8f-4e2b-b85d-fec2c162e13c', N'CEDRIC', NULL, NULL, CAST(0x00009F6000000000 AS DateTime), CAST(1.000 AS Decimal(4, 3)), 1, NULL, N'stuart', CAST(0x00009F6000000000 AS DateTime))
SET IDENTITY_INSERT [Employee] OFF

-- ACCOUNT DATA
SET IDENTITY_INSERT [Contact] ON
INSERT [Contact] ([ContactId], [ContactTypeId], [AccountId], [LastName], [FirstName], [ContactDetail], [DoNotify]) VALUES (2248, 2, 574, N'RUTLAND', N'JAMIE', N'9013825555', 0)
INSERT [Contact] ([ContactId], [ContactTypeId], [AccountId], [LastName], [FirstName], [ContactDetail], [DoNotify]) VALUES (2253, 2, 699, N'GUY', N'BILL', N'901-377-9504', 0)
INSERT [Contact] ([ContactId], [ContactTypeId], [AccountId], [LastName], [FirstName], [ContactDetail], [DoNotify]) VALUES (2412, 2, 2741, NULL, NULL, N'9013626364', 0)
INSERT [Contact] ([ContactId], [ContactTypeId], [AccountId], [LastName], [FirstName], [ContactDetail], [DoNotify]) VALUES (2551, 4, 574, N'RUTLAND', N'JAMIE', N'9014614320', 0)
INSERT [Contact] ([ContactId], [ContactTypeId], [AccountId], [LastName], [FirstName], [ContactDetail], [DoNotify]) VALUES (2693, 5, 574, N'RUTLAND', N'JAMIE', N'9013825555', 0)
SET IDENTITY_INSERT [Contact] OFF

SET IDENTITY_INSERT [Account] ON
INSERT [Account] ([AccountId], [AccountTypeId], [AccountNumber], [AccountName], [AddressLine1], [AddressLine2], [City], [StateCode], [PostalCode], [Notes], [ModifyUser], [ModifyDate]) VALUES (574, 1, N'01-00574', N'DOBBS FORD AT WOLFCHASE', N'7925 STAGE', NULL, N'MEMPHIS', N'TN', N'38133', NULL, N'system', CAST(0x00009CC20108CD33 AS DateTime))
INSERT [Account] ([AccountId], [AccountTypeId], [AccountNumber], [AccountName], [AddressLine1], [AddressLine2], [City], [StateCode], [PostalCode], [Notes], [ModifyUser], [ModifyDate]) VALUES (699, 1, N'01-00699', N'BILL GUY', N'CITY AUTO SALES', NULL, NULL, NULL, NULL, NULL, N'system', CAST(0x00009CC20108CD33 AS DateTime))
INSERT [Account] ([AccountId], [AccountTypeId], [AccountNumber], [AccountName], [AddressLine1], [AddressLine2], [City], [StateCode], [PostalCode], [Notes], [ModifyUser], [ModifyDate]) VALUES (2741, 1, N'01-02741', N'DOBBS FORD AT MT. MORIAH', NULL, NULL, NULL, NULL, NULL, NULL, N'system', CAST(0x00009CC20108CD33 AS DateTime))
SET IDENTITY_INSERT [Account] OFF

-- INVOICE DATA
SET IDENTITY_INSERT [Invoice] ON
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358500, 2, 1, 2741, NULL, CAST(0x00009FAC010559DC AS DateTime), CAST(0x00009FB0009DFCEC AS DateTime), N'2011', N'GMC', N'SIERRA', N'BLUE', NULL, N'BZ112576', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'bruce', CAST(0x00009FB400A88568 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358525, 2, 1, 2741, NULL, CAST(0x00009FAD00DAA0C0 AS DateTime), CAST(0x00009FAD00DC8E1C AS DateTime), N'2012', N'FORD', N'FUSION', N'BLUE', NULL, N'CR167495', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'bruce', CAST(0x00009FB400A89BAC AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358550, 2, 1, 2741, NULL, CAST(0x00009FB0009997EC AS DateTime), CAST(0x00009FB0009AB834 AS DateTime), N'2011', N'FORD', N'RANGER', N'WHITE', NULL, N'BPB16133', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'bruce', CAST(0x00009FB400924BF4 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358575, 2, 1, 2741, NULL, CAST(0x00009FB100DA0160 AS DateTime), CAST(0x00009FB100DA7C6C AS DateTime), N'2012', N'FORD', N'F-150', N'GRAY', NULL, N'BFD01689', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'bruce', CAST(0x00009FB4009244EC AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358600, 2, 1, 2741, NULL, CAST(0x00009FB200C39DBC AS DateTime), CAST(0x00009FB200C3C33C AS DateTime), N'2011', N'FORD', N'RANGER', N'WHITE', NULL, N'BPB16131', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'bruce', CAST(0x00009FB400923DE4 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358625, 2, 1, 2741, NULL, CAST(0x00009FB201037CE8 AS DateTime), CAST(0x00009FB20103A4C0 AS DateTime), N'2011', N'FORD', N'RANGER', N'WHITE', NULL, N'BPB16147', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'bruce', CAST(0x00009FB4009235B0 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358650, 2, 1, 2741, NULL, CAST(0x00009FB300ACAB20 AS DateTime), CAST(0x00009FB300B11020 AS DateTime), N'2012', N'FORD', N'FOCUS', N'BLUE', NULL, N'CL292918', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'bruce', CAST(0x00009FB400922674 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358675, 2, 1, 2741, NULL, CAST(0x00009FB400C9B580 AS DateTime), CAST(0x00009FB400CA9EB4 AS DateTime), N'2012', N'FORD', N'ESCAPE', N'BLACK', NULL, N'CKB62254', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'danny g', CAST(0x00009FB400C9B580 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358700, 3, 1, 574, NULL, CAST(0x00009FB500E72E08 AS DateTime), CAST(0x00009FB700AC9D10 AS DateTime), N'2012', N'FORD', N'FUSION', N'GRAY', NULL, N'CR228767', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'ryan e', CAST(0x00009FB500E72E08 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358725, 2, 1, 2741, NULL, CAST(0x00009FB600C78558 AS DateTime), CAST(0x00009FB600C7AAD8 AS DateTime), N'2012', N'FORD', N'TAURUS', N'WHITE', NULL, N'CG129635', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'danny g', CAST(0x00009FB600C78558 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358750, 2, 1, 2741, NULL, CAST(0x00009FB700B6EFA4 AS DateTime), CAST(0x00009FB800C31248 AS DateTime), N'2012', N'FORD', N'ESCAPE', N'BLUE', NULL, N'CKB26781', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'danny g', CAST(0x00009FB700B6EFA4 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358775, 3, 1, 574, NULL, CAST(0x00009FB800A8A50C AS DateTime), CAST(0x00009FB800C8C2EC AS DateTime), N'2012', N'FORD', N'FUSION', N'GRAY', NULL, N'CKB26781', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'ryan e', CAST(0x00009FB800A8A50C AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358800, 2, 1, 2741, NULL, CAST(0x00009FB800F66684 AS DateTime), CAST(0x00009FB900CFF8A0 AS DateTime), N'2010', N'JEEP', N'LIBERTY', N'RED', NULL, N'AW103430', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'danny g', CAST(0x00009FB800F66684 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358825, 1, 1, 699, NULL, CAST(0x00009FB900A42068 AS DateTime), CAST(0x00009FBB009A1B2C AS DateTime), N'2006', N'JEEP', N'LIBERTY', N'KHAKI', NULL, N'6W189463', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'chris m', CAST(0x00009FB900A42068 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358850, 3, 1, 574, NULL, CAST(0x00009FB90125F520 AS DateTime), CAST(0x00009FBA00BE57BC AS DateTime), N'2012', N'FORD', N'EDGE', N'WHITE', NULL, N'CBA48296', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'ryan e', CAST(0x00009FB90125F520 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358875, 2, 1, 2741, NULL, CAST(0x00009FBA00C492A8 AS DateTime), CAST(0x00009FBA00C514BC AS DateTime), N'2012', N'FORD', N'FIESTA', N'SILVER', NULL, N'CM152678', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'danny g', CAST(0x00009FBA00C492A8 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358900, 3, 1, 574, NULL, CAST(0x00009FBA012A7640 AS DateTime), CAST(0x00009FBB008DC624 AS DateTime), N'2012', N'LINCOLN ', N'MKX', N'CRYSTAL', NULL, N'CBL10564', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'ryan e', CAST(0x00009FBA012A7640 AS DateTime))
INSERT [Invoice] ([InvoiceId], [LocationId], [InvoiceTypeId], [AccountId], [ServiceOrderId], [ReceiveDate], [CompleteDate], [Year], [Make], [Model], [Color], [VIN], [StockNumber], [PurchaseOrderNumber], [WorkOrderNumber], [IsComplete], [IsPaid], [TaxRate], [ModifyUser], [ModifyDate]) VALUES (358925, 3, 1, 2741, NULL, CAST(0x00009FBB00E1E484 AS DateTime), CAST(0x00009FBB011B28AC AS DateTime), N'2012', N'FORD', N'F150', N'WHITE', NULL, N'CKD02215', NULL, NULL, 1, 0, CAST(0.0000 AS Decimal(5, 4)), N'ryan e', CAST(0x00009FBB00E1E484 AS DateTime))
SET IDENTITY_INSERT [Invoice] OFF

SET IDENTITY_INSERT [Service] ON
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57513, 358500, 3, CAST(165.0000 AS Decimal(12, 4)), 0, CAST(0x00009FAC00000000 AS DateTime), N'danny g', CAST(0x00009FB0009DEDB0 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57541, 358525, 17, CAST(35.0000 AS Decimal(12, 4)), 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00DAA0C0 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57550, 358500, 44, CAST(25.0000 AS Decimal(12, 4)), 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00F65ACC AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57579, 358550, 41, CAST(40.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB000000000 AS DateTime), N'danny g', CAST(0x00009FB0009997EC AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57611, 358575, 11, CAST(0.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB100000000 AS DateTime), N'danny g', CAST(0x00009FB100DA0160 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57612, 358575, 43, CAST(15.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB100000000 AS DateTime), N'danny g', CAST(0x00009FB100DA0160 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57640, 358600, 41, CAST(40.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB200000000 AS DateTime), N'danny g', CAST(0x00009FB200C39DBC AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57668, 358625, 41, CAST(40.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB200000000 AS DateTime), N'danny g', CAST(0x00009FB201037CE8 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57696, 358650, 29, CAST(35.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB300000000 AS DateTime), N'danny g', CAST(0x00009FB300B0BCEC AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57697, 358650, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB300000000 AS DateTime), N'danny g', CAST(0x00009FB300B0C64C AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57729, 358675, 29, CAST(35.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB400000000 AS DateTime), N'danny g', CAST(0x00009FB400C9B580 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57730, 358675, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB400000000 AS DateTime), N'danny g', CAST(0x00009FB400C9B580 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57764, 358700, 11, CAST(0.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB500000000 AS DateTime), N'ryan e', CAST(0x00009FB500E90AFC AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57796, 358725, 29, CAST(35.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB600000000 AS DateTime), N'danny g', CAST(0x00009FB600C78558 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57797, 358725, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB600000000 AS DateTime), N'danny g', CAST(0x00009FB600C78558 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57823, 358750, 3, CAST(85.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB700000000 AS DateTime), N'danny g', CAST(0x00009FB700B74080 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57848, 358775, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB800000000 AS DateTime), N'ryan e', CAST(0x00009FB800A8A50C AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57883, 358800, 3, CAST(165.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB800000000 AS DateTime), N'danny g', CAST(0x00009FB900A6FCD4 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57912, 358825, 3, CAST(225.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB900000000 AS DateTime), N'chris m', CAST(0x00009FBB009A1424 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57944, 358850, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FB900000000 AS DateTime), N'ryan e', CAST(0x00009FB90125F520 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57972, 358875, 29, CAST(35.0000 AS Decimal(12, 4)), 0, CAST(0x00009FBA00000000 AS DateTime), N'danny g', CAST(0x00009FBA00C492A8 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (57973, 358875, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FBA00000000 AS DateTime), N'danny g', CAST(0x00009FBA00C492A8 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (58001, 358900, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FBA00000000 AS DateTime), N'ryan e', CAST(0x00009FBA012A7640 AS DateTime))
INSERT [Service] ([ServiceId], [InvoiceId], [ServiceTypeId], [Rate], [EstimatedTime], [ServiceDate], [ModifyUser], [ModifyDate]) VALUES (58030, 358925, 10, CAST(125.0000 AS Decimal(12, 4)), 0, CAST(0x00009FBB00000000 AS DateTime), N'ryan e', CAST(0x00009FBB00E1E484 AS DateTime))
SET IDENTITY_INSERT [Service] OFF

SET IDENTITY_INSERT [Labor] ON
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195467, 358500, 115, 14, 10.0000, 10.0000, 0, 0, CAST(0x00009FAC00000000 AS DateTime), N'danny g', CAST(0x00009FAC01058538 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195517, 358525, 95, 3, 12.0000, 12.0000, 0, 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00DAC190 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195551, 358500, 102, 1, 24.0000, 12.0000, 0, 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00F8055C AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195552, 358500, 107, 4, 22.0000, 11.0000, 0, 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00F81114 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195553, 358500, 102, 4, 22.0000, 11.0000, 0, 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00F808E0 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195554, 358500, 107, 1, 24.0000, 12.0000, 0, 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00F80D90 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195555, 358500, 107, 38, 6.0000, 3.0000, 0, 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00F837C0 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195556, 358500, 102, 38, 6.0000, 3.0000, 0, 0, CAST(0x00009FAD00000000 AS DateTime), N'danny g', CAST(0x00009FAD00F83C70 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195617, 358550, 115, 39, 11.0000, 11.0000, 0, 0, CAST(0x00009FB000000000 AS DateTime), N'danny g', CAST(0x00009FB000999EF4 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195708, 358575, 95, 8, 11.0000, 11.0000, 0, 0, CAST(0x00009FB100000000 AS DateTime), N'danny g', CAST(0x00009FB100DA5110 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195709, 358575, 95, 40, 5.0000, 5.0000, 0, 0, CAST(0x00009FB100000000 AS DateTime), N'danny g', CAST(0x00009FB100DA4C60 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195750, 358600, 115, 39, 11.0000, 11.0000, 0, 0, CAST(0x00009FB200000000 AS DateTime), N'danny g', CAST(0x00009FB200C3A398 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195788, 358625, 102, 39, 11.0000, 11.0000, 0, 0, CAST(0x00009FB200000000 AS DateTime), N'danny g', CAST(0x00009FB2010388A0 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195832, 358650, 102, 37, 5.0000, 5.0000, 0, 0, CAST(0x00009FB300000000 AS DateTime), N'danny g', CAST(0x00009FB300B0F400 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195833, 358650, 102, 23, 20.0000, 20.0000, 0, 0, CAST(0x00009FB300000000 AS DateTime), N'danny g', CAST(0x00009FB300B0EF50 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195900, 358675, 115, 37, 5.0000, 5.0000, 0, 0, CAST(0x00009FB400000000 AS DateTime), N'danny g', CAST(0x00009FB400CA98D8 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195901, 358675, 115, 23, 20.0000, 20.0000, 0, 0, CAST(0x00009FB400000000 AS DateTime), N'danny g', CAST(0x00009FB400CA9428 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195991, 358725, 110, 37, 5.0000, 5.0000, 0, 0, CAST(0x00009FB600000000 AS DateTime), N'danny g', CAST(0x00009FB600C7A178 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (195992, 358725, 110, 23, 20.0000, 20.0000, 0, 0, CAST(0x00009FB600000000 AS DateTime), N'danny g', CAST(0x00009FB600C7A4FC AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196037, 358750, 95, 1, 7.5000, 7.0000, 0, 0, CAST(0x00009FB700000000 AS DateTime), N'danny g', CAST(0x00009FB700B763A8 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196038, 358750, 95, 4, 15.0000, 15.0000, 0, 0, CAST(0x00009FB700000000 AS DateTime), N'danny g', CAST(0x00009FB700B71FB0 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196039, 358750, 115, 14, 8.0000, 8.0000, 0, 0, CAST(0x00009FB700000000 AS DateTime), N'danny g', CAST(0x00009FB700B7258C AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196087, 358775, 100, 23, 20.0000, 20.0000, 0, 0, CAST(0x00009FB800000000 AS DateTime), N'ryan e', CAST(0x00009FB800A8A50C AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196140, 358800, 115, 14, 10.0000, 10.0000, 0, 0, CAST(0x00009FB800000000 AS DateTime), N'danny g', CAST(0x00009FB800F69C6C AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196191, 358825, 10, 14, 9.6000, 9.6000, 0, 0, CAST(0x00009FB900000000 AS DateTime), N'chris m', CAST(0x00009FB900A431FC AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196194, 358800, 114, 4, 28.0000, 28.0000, 0, 0, CAST(0x00009FB900000000 AS DateTime), N'danny g', CAST(0x00009FB900A6C368 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196195, 358800, 114, 1, 26.0000, 24.0000, 0, 0, CAST(0x00009FB900000000 AS DateTime), N'danny g', CAST(0x00009FB900A6E7BC AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196244, 358850, 25, 23, 20.0000, 10.0000, 0, 0, CAST(0x00009FBA00000000 AS DateTime), N'ryan e', CAST(0x00009FBA00A66F08 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196265, 358850, 57, 7, 20.0000, 15.0000, 0, 0, CAST(0x00009FBA00000000 AS DateTime), N'ryan e', CAST(0x00009FBA00A67AC0 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196283, 358875, 115, 37, 5.0000, 5.0000, 0, 0, CAST(0x00009FBA00000000 AS DateTime), N'danny g', CAST(0x00009FBA00C499B0 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196284, 358875, 115, 23, 20.0000, 20.0000, 0, 0, CAST(0x00009FBA00000000 AS DateTime), N'danny g', CAST(0x00009FBA00C49F8C AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196312, 358825, 113, 4, 50.0000, 25.0000, 0, 0, CAST(0x00009FBA00000000 AS DateTime), N'chris m', CAST(0x00009FBA00EEE4A4 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196313, 358825, 67, 4, 50.0000, 25.0000, 0, 0, CAST(0x00009FBA00000000 AS DateTime), N'chris m', CAST(0x00009FBA00EEEA80 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196349, 358900, 67, 23, 25.0000, 10.0000, 0, 0, CAST(0x00009FBB00000000 AS DateTime), N'ryan e', CAST(0x00009FBB008A16C8 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196350, 358900, 115, 23, 25.0000, 15.0000, 0, 0, CAST(0x00009FBB00000000 AS DateTime), N'ryan e', CAST(0x00009FBB008A97B0 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196357, 358825, 104, 1, 22.0000, 22.0000, 0, 0, CAST(0x00009FBB00000000 AS DateTime), N'chris m', CAST(0x00009FBB008FC190 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196402, 358925, 57, 23, 20.0000, 10.0000, 0, 0, CAST(0x00009FBB00000000 AS DateTime), N'ryan e', CAST(0x00009FBB00F88FA4 AS DateTime))
INSERT [Labor] ([LaborId], [InvoiceId], [EmployeeId], [LaborTypeId], [EstimatedRate], [ActualRate], [EstimatedTime], [ActualTime], [LaborDate], [ModifyUser], [ModifyDate]) VALUES (196413, 358925, 25, 23, 20.0000, 10.0000, 0, 0, CAST(0x00009FBB00000000 AS DateTime), N'ryan e', CAST(0x00009FBB00F8A390 AS DateTime))
SET IDENTITY_INSERT [Labor] OFF

SET IDENTITY_INSERT [InvoiceHistory] ON
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (16880, 358525, N'Invoice completed', N'danny g', CAST(0x00009FAD00DC8E1C AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (16910, 358550, N'Invoice completed', N'danny g', CAST(0x00009FB0009AB834 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (16914, 358500, N'Invoice completed', N'danny g', CAST(0x00009FB0009DFCEC AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (16940, 358575, N'Invoice completed', N'danny g', CAST(0x00009FB100DA7C6C AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (16970, 358600, N'Invoice completed', N'danny g', CAST(0x00009FB200C3C33C AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (16999, 358625, N'Invoice completed', N'danny g', CAST(0x00009FB20103A4C0 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17026, 358650, N'Invoice completed', N'danny g', CAST(0x00009FB300ACD2F8 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17028, 358650, N'Invoice recalled', N'danny g', CAST(0x00009FB300AE8B98 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17029, 358650, N'Invoice completed', N'danny g', CAST(0x00009FB300B009A0 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17030, 358650, N'Invoice recalled', N'danny g', CAST(0x00009FB300B04EC4 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17031, 358650, N'Invoice completed', N'danny g', CAST(0x00009FB300B11020 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17050, 358675, N'Invoice completed', N'danny g', CAST(0x00009FB400CA9EB4 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17099, 358725, N'Invoice completed', N'danny g', CAST(0x00009FB600C7AAD8 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17120, 358700, N'Invoice completed', N'ryan e', CAST(0x00009FB700AC9D10 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17151, 358750, N'demo clean 4 lot', N'danny g', CAST(0x00009FB700B77D70 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17181, 358750, N'Invoice completed', N'danny g', CAST(0x00009FB800C31248 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17183, 358775, N'Invoice completed', N'ryan e', CAST(0x00009FB800C8C2EC AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17260, 358800, N'Invoice completed', N'danny g', CAST(0x00009FB900CFF8A0 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17297, 358850, N'Invoice completed', N'ryan e', CAST(0x00009FBA00BE57BC AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17305, 358875, N'Invoice completed', N'danny g', CAST(0x00009FBA00C514BC AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17336, 358900, N'Invoice completed', N'ryan e', CAST(0x00009FBB008DC624 AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17340, 358825, N'Invoice completed', N'chris m', CAST(0x00009FBB009A1B2C AS DateTime))
INSERT [InvoiceHistory] ([InvoiceHistoryId], [InvoiceId], [Note], [ModifyUser], [ModifyDate]) VALUES (17364, 358925, N'Invoice completed', N'ryan e', CAST(0x00009FBB011B28AC AS DateTime))
SET IDENTITY_INSERT [InvoiceHistory] OFF

-- LOG DATA
SET IDENTITY_INSERT [ErrorLog] ON
INSERT [ErrorLog] ([ErrorLogId], [InvoiceId], [ExceptionType], [Message], [StackTrace], [ModifyUser], [ModifyDate]) VALUES (984, NULL, N'System.ArgumentNullException', N'Value cannot be null.
Parameter name: You must specify a password.', N'   at EnfieldWeb.Controllers.ShopController.SignIn(Int32 employeeId, String password) in C:\Projects\EnfieldWeb\EnfieldWeb\Controllers\ShopController.cs:line 467
   at lambda_method(ExecutionScope , ControllerBase , Object[] )
   at System.Web.Mvc.ActionMethodDispatcher.Execute(ControllerBase controller, Object[] parameters)
   at System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<InvokeActionMethodWithFilters>b__7()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodFilter(IActionFilter filter, ActionExecutingContext preContext, Func`1 continuation)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<>c__DisplayClassc.<InvokeActionMethodWithFilters>b__9()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodWithFilters(ControllerContext controllerContext, IList`1 filters, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeAction(ControllerContext controllerContext, String actionName)', N'chris m', CAST(0x00009FBA00AB6300 AS DateTime))
INSERT [ErrorLog] ([ErrorLogId], [InvoiceId], [ExceptionType], [Message], [StackTrace], [ModifyUser], [ModifyDate]) VALUES (985, NULL, N'System.ArgumentException', N'The parameters dictionary contains a null entry for parameter ''id'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.ActionResult NewInvoice(Int32)'' in ''EnfieldWeb.Controllers.ShopController''. To make a parameter optional its type should be either a reference type or a Nullable type.
Parameter name: parameters', N'   at System.Web.Mvc.ReflectedActionDescriptor.ExtractParameterFromDictionary(ParameterInfo parameterInfo, IDictionary`2 parameters, MethodInfo methodInfo)
   at System.Web.Mvc.ReflectedActionDescriptor.<>c__DisplayClass1.<Execute>b__0(ParameterInfo parameterInfo)
   at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
   at System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<InvokeActionMethodWithFilters>b__7()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodFilter(IActionFilter filter, ActionExecutingContext preContext, Func`1 continuation)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<>c__DisplayClassc.<InvokeActionMethodWithFilters>b__9()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodWithFilters(ControllerContext controllerContext, IList`1 filters, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeAction(ControllerContext controllerContext, String actionName)', N'danny g', CAST(0x00009FBA00B98B9C AS DateTime))
INSERT [ErrorLog] ([ErrorLogId], [InvoiceId], [ExceptionType], [Message], [StackTrace], [ModifyUser], [ModifyDate]) VALUES (988, NULL, N'System.ArgumentException', N'The parameters dictionary contains a null entry for parameter ''id'' of non-nullable type ''System.Int32'' for method ''System.Web.Mvc.ActionResult NewInvoice(Int32)'' in ''EnfieldWeb.Controllers.ShopController''. To make a parameter optional its type should be either a reference type or a Nullable type.
Parameter name: parameters', N'   at System.Web.Mvc.ReflectedActionDescriptor.ExtractParameterFromDictionary(ParameterInfo parameterInfo, IDictionary`2 parameters, MethodInfo methodInfo)
   at System.Web.Mvc.ReflectedActionDescriptor.<>c__DisplayClass1.<Execute>b__0(ParameterInfo parameterInfo)
   at System.Linq.Enumerable.WhereSelectArrayIterator`2.MoveNext()
   at System.Linq.Buffer`1..ctor(IEnumerable`1 source)
   at System.Linq.Enumerable.ToArray[TSource](IEnumerable`1 source)
   at System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<InvokeActionMethodWithFilters>b__7()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodFilter(IActionFilter filter, ActionExecutingContext preContext, Func`1 continuation)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<>c__DisplayClassc.<InvokeActionMethodWithFilters>b__9()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodWithFilters(ControllerContext controllerContext, IList`1 filters, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeAction(ControllerContext controllerContext, String actionName)', N'danny g', CAST(0x00009FBB00939EA0 AS DateTime))
INSERT [ErrorLog] ([ErrorLogId], [InvoiceId], [ExceptionType], [Message], [StackTrace], [ModifyUser], [ModifyDate]) VALUES (989, NULL, N'Castle.ActiveRecord.Framework.ActiveRecordException', N'Could not perform Save for Invoice', N'   at Castle.ActiveRecord.ActiveRecordBase.InternalSave(Object instance, Boolean flush) in c:\Projects\CastleTrunk\ActiveRecord\Castle.ActiveRecord\Framework\ActiveRecordBase.cs:line 594
   at Castle.ActiveRecord.ActiveRecordBase.SaveAndFlush(Object instance) in c:\Projects\CastleTrunk\ActiveRecord\Castle.ActiveRecord\Framework\ActiveRecordBase.cs:line 517
   at Castle.ActiveRecord.ActiveRecordBase.SaveAndFlush() in c:\Projects\CastleTrunk\ActiveRecord\Castle.ActiveRecord\Framework\ActiveRecordBase.cs:line 1429
   at EnfieldWeb.Models.DetailRepository.SaveInvoice(Invoice invoice) in C:\Projects\EnfieldWeb\EnfieldWeb\Models\DetailRepository.cs:line 398
   at EnfieldWeb.Controllers.ShopController.DeleteLabor(Int32 invoiceId, Int32 laborId) in C:\Projects\EnfieldWeb\EnfieldWeb\Controllers\ShopController.cs:line 381
   at lambda_method(ExecutionScope , ControllerBase , Object[] )
   at System.Web.Mvc.ActionMethodDispatcher.Execute(ControllerBase controller, Object[] parameters)
   at System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<InvokeActionMethodWithFilters>b__7()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodFilter(IActionFilter filter, ActionExecutingContext preContext, Func`1 continuation)
   at System.Web.Mvc.ControllerActionInvoker.<>c__DisplayClassa.<>c__DisplayClassc.<InvokeActionMethodWithFilters>b__9()
   at System.Web.Mvc.ControllerActionInvoker.InvokeActionMethodWithFilters(ControllerContext controllerContext, IList`1 filters, ActionDescriptor actionDescriptor, IDictionary`2 parameters)
   at System.Web.Mvc.ControllerActionInvoker.InvokeAction(ControllerContext controllerContext, String actionName)', N'danny g', CAST(0x00009FBB00E75964 AS DateTime))
SET IDENTITY_INSERT [ErrorLog] OFF

SET IDENTITY_INSERT [EmployeeLog] ON
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6631, 10, CAST(0x00009FAC005D58E0 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6634, 113, CAST(0x00009FAC0074C688 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6636, 110, CAST(0x00009FAC0085D040 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6637, 115, CAST(0x00009FAC0085EFE4 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6657, 110, CAST(0x00009FAD00B20764 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6658, 10, CAST(0x00009FAE00778F08 AS DateTime), CAST(0x00009FAE00D051B0 AS DateTime))
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6659, 115, CAST(0x00009FAE00D06A4C AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6661, 110, CAST(0x00009FAE00D09800 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6663, 57, CAST(0x00009FB000734754 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6666, 113, CAST(0x00009FB0007D1A2C AS DateTime), CAST(0x00009FB001011DE0 AS DateTime))
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6689, 113, CAST(0x00009FB2005E68C0 AS DateTime), CAST(0x00009FB201070DCC AS DateTime))
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6690, 10, CAST(0x00009FB2005EE3CC AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6692, 110, CAST(0x00009FB20087FBB8 AS DateTime), CAST(0x00009FB200E1D9F8 AS DateTime))
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6693, 115, CAST(0x00009FB2008811FC AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6720, 113, CAST(0x00009FB40070DA3C AS DateTime), CAST(0x00009FB400F46FC8 AS DateTime))
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6721, 57, CAST(0x00009FB40075E220 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6724, 110, CAST(0x00009FB400C7C6F8 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6725, 115, CAST(0x00009FB400C7EB4C AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6726, 107, CAST(0x00009FB400C803E8 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6727, 95, CAST(0x00009FB400C81B58 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6745, 102, CAST(0x00009FB7009724F8 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6746, 95, CAST(0x00009FB70097368C AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6747, 107, CAST(0x00009FB70097575C AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6749, 57, CAST(0x00009FB700A8B7CC AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6750, 10, CAST(0x00009FB8004DE4A0 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6751, 113, CAST(0x00009FB80061C038 AS DateTime), CAST(0x00009FB800805B60 AS DateTime))
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6754, 113, CAST(0x00009FB800806844 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6758, 57, CAST(0x00009FB800A42518 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6759, 115, CAST(0x00009FB800BD3198 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6774, 114, CAST(0x00009FB9008C2AD0 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6775, 67, CAST(0x00009FB900931908 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6778, 57, CAST(0x00009FB9009EE9A4 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6780, 10, CAST(0x00009FBA0051BAA8 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6804, 25, CAST(0x00009FBB008B0704 AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6805, 57, CAST(0x00009FBB008B545C AS DateTime), NULL)
INSERT [EmployeeLog] ([EmployeeLogId], [EmployeeId], [SignInDate], [SignOutDate]) VALUES (6807, 104, CAST(0x00009FBB008F9D3C AS DateTime), NULL)
SET IDENTITY_INSERT [EmployeeLog] OFF

SET IDENTITY_INSERT [LoginAttemptLog] ON
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5365, CAST(0x00009FAC005D44F4 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5366, CAST(0x00009FAC0071BB3C AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5367, CAST(0x00009FAC00748038 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5389, CAST(0x00009FAC00DAC9C4 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5405, CAST(0x00009FAD0083E53C AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5407, CAST(0x00009FAD00861A14 AS DateTime), 1, 1, N'75.64.19.31', N'bruce', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5408, CAST(0x00009FAD0094E210 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5409, CAST(0x00009FAD00A4DF6C AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5605, CAST(0x00009FB400C67578 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5606, CAST(0x00009FB400C78EB8 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5607, CAST(0x00009FB400C79944 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5608, CAST(0x00009FB400D47D44 AS DateTime), 1, 1, N'75.64.19.31', N'bruce', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5609, CAST(0x00009FB400D5492C AS DateTime), 1, 1, N'75.64.19.31', N'bruce', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5610, CAST(0x00009FB400D6E804 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5611, CAST(0x00009FB400D700A0 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5612, CAST(0x00009FB400D8BCC4 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5613, CAST(0x00009FB400DFF3A4 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5614, CAST(0x00009FB400E02608 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5621, CAST(0x00009FB400F57AF8 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5622, CAST(0x00009FB400F742D4 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5623, CAST(0x00009FB400F7C998 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5624, CAST(0x00009FB400F7E5B8 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5626, CAST(0x00009FB40108720C AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5627, CAST(0x00009FB4010F3038 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5629, CAST(0x00009FB500BB8F3C AS DateTime), 0, 1, N'166.249.193.29', N'danny g', N'Invalid IP')
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5631, CAST(0x00009FB500C00CD8 AS DateTime), 1, 1, N'174.53.197.208', N'stuart', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5633, CAST(0x00009FB500DACD48 AS DateTime), 1, 1, N'166.249.193.29', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5634, CAST(0x00009FB500DD0220 AS DateTime), 1, 1, N'166.249.193.29', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5669, CAST(0x00009FB7009981A8 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5670, CAST(0x00009FB70099C924 AS DateTime), 1, 1, N'166.249.199.51', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5799, CAST(0x00009FB900A28E74 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5800, CAST(0x00009FB900A629E4 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5802, CAST(0x00009FB900B14158 AS DateTime), 1, 1, N'166.249.193.248', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5803, CAST(0x00009FB900B14BE4 AS DateTime), 1, 1, N'166.249.193.248', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5804, CAST(0x00009FB900B24580 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5806, CAST(0x00009FB900BACB88 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5807, CAST(0x00009FB900BAD740 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5808, CAST(0x00009FB900C26988 AS DateTime), 1, 1, N'166.249.195.53', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5809, CAST(0x00009FB900C26D0C AS DateTime), 1, 1, N'166.249.195.53', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5811, CAST(0x00009FB900C27B1C AS DateTime), 1, 1, N'166.249.195.53', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5813, CAST(0x00009FB900C88E30 AS DateTime), 0, 1, N'68.157.238.173', N'danny8765', N'Invalid credentials')
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5814, CAST(0x00009FB900C8AF00 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5816, CAST(0x00009FB900CF2484 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5818, CAST(0x00009FB900D23DE0 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5820, CAST(0x00009FB900D70424 AS DateTime), 1, 1, N'166.249.199.196', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5821, CAST(0x00009FB900D707A8 AS DateTime), 1, 1, N'166.249.199.196', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5822, CAST(0x00009FB900D70B2C AS DateTime), 1, 1, N'166.249.199.196', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5823, CAST(0x00009FB900E49C9C AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5824, CAST(0x00009FB900E4AE30 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5827, CAST(0x00009FB900E7C408 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5829, CAST(0x00009FB900F4CB30 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5832, CAST(0x00009FB901028A54 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5833, CAST(0x00009FB90102D2FC AS DateTime), 1, 1, N'75.64.19.31', N'bruce', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5834, CAST(0x00009FB90103DF58 AS DateTime), 1, 1, N'166.249.193.248', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5835, CAST(0x00009FB90103E408 AS DateTime), 1, 1, N'166.249.193.248', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5836, CAST(0x00009FB901046040 AS DateTime), 1, 1, N'75.64.19.31', N'bruce', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5837, CAST(0x00009FB90104C9B8 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5880, CAST(0x00009FBA00CBDEA0 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5881, CAST(0x00009FBA00CEB530 AS DateTime), 1, 1, N'75.64.19.31', N'bruce', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5882, CAST(0x00009FBA00D44AE0 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5883, CAST(0x00009FBA00D5B628 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5922, CAST(0x00009FBB0054A3F8 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5923, CAST(0x00009FBB0075FE40 AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5924, CAST(0x00009FBB0075FF6C AS DateTime), 1, 1, N'75.64.19.31', N'chris m', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5925, CAST(0x00009FBB0077AC54 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5938, CAST(0x00009FBB0093C678 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5977, CAST(0x00009FBB01083198 AS DateTime), 1, 1, N'68.157.238.173', N'danny g', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5978, CAST(0x00009FBB0116E224 AS DateTime), 1, 1, N'166.249.200.9', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5979, CAST(0x00009FBB0116F3B8 AS DateTime), 1, 1, N'166.249.200.9', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5980, CAST(0x00009FBB011716E0 AS DateTime), 1, 1, N'166.249.200.9', N'ryan e', NULL)
INSERT [LoginAttemptLog] ([LoginAttemptId], [LoginDate], [ResultFlag], [LocationId], [IpAddress], [UserName], [Reason]) VALUES (5991, CAST(0x00009FBB01742A9C AS DateTime), 1, 1, N'99.109.166.191', N'bruce', NULL)
SET IDENTITY_INSERT [LoginAttemptLog] OFF

SET IDENTITY_INSERT [ApprovedIp] ON
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (1, 1, N'Administrator', N'*')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (2, 1, N'Employee', N'127.0.0.2')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (3, 1, N'Manager', N'127.0.0.1')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (4, 1, N'Dealer', N'*')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (5, 1, N'Manager', N'75.64.19.31')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (6, 1, N'Employee', N'75.64.19.31')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (7, 2, N'Employee', N'68.18.199.130')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (8, 2, N'Manager', N'68.18.199.130')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (9, 2, N'Administrator', N'*')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (34, 2, N'Employee', N'68.157.238.173')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (35, 2, N'Manager', N'68.157.238.173')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (36, 2, N'Employee', N'166.249.193.29')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (37, 2, N'Manager', N'166.249.193.29')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (38, 2, N'Employee', N'74.177.31.33')
INSERT [ApprovedIp] ([ApprovedIpId], [LocationId], [RoleName], [IpAddress]) VALUES (39, 2, N'Manager', N'74.177.31.33')
SET IDENTITY_INSERT [ApprovedIp] OFF

GO