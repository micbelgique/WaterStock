SET IDENTITY_INSERT AppUser ON;

INSERT INTO AppUser (AppUserID, Login, Password, EMail, IsEnabled, Role, Firstname, Lastname) 
             VALUES (1, 'admin', 'admin', 'admin@comtosio.com', 1, 'A', 'Mister', 'Administrator');

INSERT INTO AppUser (AppUserID, Login, Password, EMail, IsEnabled, Role, Firstname, Lastname) 
             VALUES (2, 'drink1', 'drink1', 'drink1@comtosio.com', 1, 'U', 'Mrs', 'First Drink Girl');

INSERT INTO AppUser (AppUserID, Login, Password, EMail, IsEnabled, Role, Firstname, Lastname) 
             VALUES (3, 'drink2', 'drink2', 'drink2@comtosio.com', 1, 'U', 'Mrs', 'Second Drink Girl');

INSERT INTO AppUser (AppUserID, Login, Password, EMail, IsEnabled, Role, Firstname, Lastname) 
             VALUES (4, 'check', 'check', 'check@comtosio.com', 1, 'C', 'Mister', 'Checker Man');

SET IDENTITY_INSERT AppUser OFF;

-- *******************************************************************

SET IDENTITY_INSERT AppGroup ON;

INSERT INTO AppGroup (AppGroupID, Name) VALUES (1, 'Internal');
INSERT INTO AppGroup (AppGroupID, Name) VALUES (2, 'External');
INSERT INTO AppGroup (AppGroupID, Name) VALUES (3, 'Office');

INSERT INTO AppGroupAppUser (AppGroupID,AppUserID) VALUES (1, 1);
INSERT INTO AppGroupAppUser (AppGroupID,AppUserID) VALUES (1, 4);
INSERT INTO AppGroupAppUser (AppGroupID,AppUserID) VALUES (2, 2);
INSERT INTO AppGroupAppUser (AppGroupID,AppUserID) VALUES (2, 3);
INSERT INTO AppGroupAppUser (AppGroupID,AppUserID) VALUES (3, 1);

SET IDENTITY_INSERT AppGroup OFF;

-- *******************************************************************

SET IDENTITY_INSERT StorePoint ON;

INSERT INTO StorePoint (StorePointID, Name, Reference, Quantity, Location) 
                VALUES (1, 'Water 1', '123', 5.0, 'Room 1');

INSERT INTO StorePoint (StorePointID, Name, Reference, Quantity, Location) 
                VALUES (2, 'Water 2', '456', 4.7, 'Room 2');

SET IDENTITY_INSERT StorePoint OFF;

-- *******************************************************************

SET IDENTITY_INSERT Consumption ON;

INSERT INTO Consumption (ConsumptionID, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (1            , 1           , 2        , 0.5);

INSERT INTO Consumption (ConsumptionID, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (2            , 1           , 2        , 0.4);

INSERT INTO Consumption (ConsumptionID, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (3            , 2           , 2        , 0.3);

INSERT INTO Consumption (ConsumptionID, RecordedDate, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (4            , GETDATE() - 1, 2           , 2        , 0.3);

INSERT INTO Consumption (ConsumptionID, RecordedDate, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (5            , GETDATE() - 2, 2           , 2        , 2);

INSERT INTO Consumption (ConsumptionID, RecordedDate, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (6            , GETDATE() - 3, 2           , 2        , 1.8);

INSERT INTO Consumption (ConsumptionID, RecordedDate, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (7            , GETDATE() - 4, 2           , 2        , 2.3);
				 
INSERT INTO Consumption (ConsumptionID, RecordedDate, StorePointID, AppUserID, QuantityAdded) 
                 VALUES (8            , GETDATE() - 5, 2           , 2        , 3.1);

SET IDENTITY_INSERT Consumption OFF;

-- *******************************************************************

SET IDENTITY_INSERT Badge ON;

INSERT INTO Badge (BadgeID, AppUserID, Reference) VALUES (1, 1, '11111');
INSERT INTO Badge (BadgeID, AppUserID, Reference) VALUES (2, 2, '22222');
INSERT INTO Badge (BadgeID, AppUserID, Reference) VALUES (3, 3, '33333');
INSERT INTO Badge (BadgeID, AppUserID, Reference) VALUES (4, 4, '44444');

SET IDENTITY_INSERT Badge OFF;

-- *******************************************************************

SET IDENTITY_INSERT Alert ON;

INSERT INTO Alert (AlertID, AppUserID, StorePointID, QuantityGreatherThan, SentDate) 
           VALUES (1, 1, 1, 7, NULL);

SET IDENTITY_INSERT Alert OFF;

-- *******************************************************************