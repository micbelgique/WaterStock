IF EXISTS(SELECT * FROM sys.objects WHERE name = 'AppGroupAppUser' AND type = 'U')
   DROP TABLE AppGroupAppUser
   
IF EXISTS(SELECT * FROM sys.objects WHERE name = 'AppUserStorePoint' AND type = 'U')
   DROP TABLE AppUserStorePoint

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'Consumption' AND type = 'U')
   DROP TABLE Consumption

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'Alert' AND type = 'U')
   DROP TABLE Alert

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'Badge' AND type = 'U')
   DROP TABLE Badge

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'StorePoint' AND type = 'U')
   DROP TABLE StorePoint

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'AppUser' AND type = 'U')
   DROP TABLE AppUser

IF EXISTS(SELECT * FROM sys.objects WHERE name = 'AppGroup' AND type = 'U')
   DROP TABLE AppGroup


CREATE TABLE AppUser
(
	AppUserID INT NOT NULL IDENTITY(1,1),
    Login VARCHAR(256),
	Password VARCHAR(256),
	Email VARCHAR(256),
	IsEnabled BIT NOT NULL DEFAULT(0),
	Role CHAR(1) NOT NULL DEFAULT('U') CHECK (Role IN ('U', 'A', 'C')),		-- User - Administrator - Checker
	Lastname VARCHAR(256) NOT NULL,
	Firstname VARCHAR(256),
	CONSTRAINT PK_AppUser_AppUserID PRIMARY KEY (AppUserID)
)

CREATE TABLE AppGroup
(
	AppGroupID INT NOT NULL IDENTITY(1,1),
	Name VARCHAR(256) NOT NULL,
	CONSTRAINT PK_AppGroup_AppGroupID PRIMARY KEY (AppGroupID),
	CONSTRAINT UX_AppGroup_Name UNIQUE (Name)
)

CREATE TABLE AppGroupAppUser
(
	AppGroupID INT NOT NULL,
	AppUserID INT NOT NULL,
	CONSTRAINT PK_AppGroupAppUser_IDs PRIMARY KEY (AppGroupID, AppUserID),
	CONSTRAINT FK_AppGroupAppUser_AppGroupID FOREIGN KEY (AppGroupID) REFERENCES AppGroup(AppGroupID),
	CONSTRAINT FK_AppGroupAppUser_AppUserID  FOREIGN KEY (AppUserID)  REFERENCES AppUser(AppUserID)
)

CREATE TABLE StorePoint
(
	StorePointID INT NOT NULL IDENTITY(1,1),
	Name VARCHAR(256) NOT NULL,
	Reference VARCHAR(256) NOT NULL,
	Quantity FLOAT NOT NULL DEFAULT(0) CHECK (Quantity >= 0),
	Location VARCHAR(256),
	CONSTRAINT PK_StorePoint_StorePointID PRIMARY KEY (StorePointID),
	CONSTRAINT UX_StorePoint_Reference UNIQUE (Reference)
)

--CREATE TABLE AppUserStorePoint
--(
--	AppUserID INT NOT NULL,
--	StorePointID INT NOT NULL,
--	CONSTRAINT PK_AppUserStorePoint_IDs PRIMARY KEY (AppUserID, StorePointID),
--	CONSTRAINT FK_AppUserStorePoint_AppUserID FOREIGN KEY (AppUserID) REFERENCES AppUser(AppUserID),
--	CONSTRAINT FK_AppUserStorePoint_StorePointID FOREIGN KEY (StorePointID) REFERENCES StorePoint(StorePointID)
--)

CREATE TABLE Consumption
(
	ConsumptionID INT NOT NULL IDENTITY(1,1),
	RecordedDate DATETIME NOT NULL DEFAULT(GETDATE()),
	StorePointID INT NOT NULL,
	AppUserID INT,
	QuantityAdded FLOAT NOT NULL DEFAULT(0),
	CONSTRAINT PK_Consumption_ConsumptionID PRIMARY KEY (ConsumptionID),
	CONSTRAINT FK_Consumption_Consumptiont_StorePointID FOREIGN KEY (StorePointID) REFERENCES StorePoint(StorePointID),
	CONSTRAINT FK_Consumption_AppUserID FOREIGN KEY (AppUserID) REFERENCES AppUser(AppUserID)
)

CREATE TABLE Badge
(
	BadgeID INT NOT NULL IDENTITY(1,1),
	AppUserID INT NOT NULL,
	Reference VARCHAR(256) NOT NULL,
	CONSTRAINT PK_Badge_BadgeID PRIMARY KEY (BadgeID),
	CONSTRAINT UX_Badge_Reference UNIQUE (Reference),
	CONSTRAINT FK_Badge_AppUserID FOREIGN KEY (AppUserID) REFERENCES AppUser(AppUserID),
)

CREATE TABLE Alert
(
	AlertID INT NOT NULL IDENTITY(1,1),
	AppUserID INT NOT NULL,
	StorePointID INT NOT NULL,
	QuantityGreatherThan FLOAT NOT NULL DEFAULT (0),
	SentDate DATETIME,
	CONSTRAINT PK_Alert_AlerttID PRIMARY KEY (AlertID),
	CONSTRAINT FK_Alert_AppUserID FOREIGN KEY (AppUserID) REFERENCES AppUser(AppUserID),
	CONSTRAINT FK_Alert_StorePointID FOREIGN KEY (StorePointID) REFERENCES StorePoint(StorePointID),
)
