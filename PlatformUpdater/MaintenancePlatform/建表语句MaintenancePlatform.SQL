/***************************/
/*                         */
/* MaintenancePlatform.SQL */
/*                         */
/***************************/

/* Disable Foreign Keys */
pragma foreign_keys = off;
/* Begin Transaction */
begin transaction;

/* Database [ZNCPlatform] */
pragma auto_vacuum=0;
pragma encoding='UTF-8';
pragma page_size=4096;

/* Drop table [main].[AcquisitionBaseData] */
drop table if exists [main].[AcquisitionBaseData];

/* Table structure [main].[AcquisitionBaseData] */
CREATE TABLE [main].[AcquisitionBaseData] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(50), 
  [Value] NVARCHAR(20), 
  [Confidence] FLOAT, 
  [Remark] NVARCHAR(200));

/* Data [main].[AcquisitionBaseData] */


/* Drop table [main].[AcquisitionSetting] */
drop table if exists [main].[AcquisitionSetting];

/* Table structure [main].[AcquisitionSetting] */
CREATE TABLE [main].[AcquisitionSetting] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [AcquisitionName] NVARCHAR(50), 
  [AcquisitionTypeID] INTEGER, 
  [Col1] NVARCHAR(200), 
  [Col2] NVARCHAR(200), 
  [Col3] NVARCHAR(200), 
  [Col4] NVARCHAR(200), 
  [Col5] NVARCHAR(200), 
  [Remark] NVARCHAR(200));

/* Data [main].[AcquisitionSetting] */


/* Drop table [main].[AlarmHistory] */
drop table if exists [main].[AlarmHistory];

/* Table structure [main].[AlarmHistory] */
CREATE TABLE [main].[AlarmHistory] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(100), 
  [PushTime] TIMESTAMP, 
  [PushContent] NVARCHAR(200), 
  [ModuleID] INTEGER, 
  [PushTypeID] INTEGER, 
  [PushRuleID] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[AlarmHistory] */


/* Drop table [main].[Department] */
drop table if exists [main].[Department];

/* Table structure [main].[Department] */
CREATE TABLE [main].[Department] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(20), 
  [UplevelCode] INTEGER, 
  [UplevelName] NVARCHAR(20), 
  [InnerCode] NVARCHAR(50));

/* Data [main].[Department] */


/* Drop table [main].[Dictionary] */
drop table if exists [main].[Dictionary];

/* Table structure [main].[Dictionary] */
CREATE TABLE [main].[Dictionary] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(100), 
  [EnableStatus] INTEGER, 
  [Remark] NVARCHAR(200), 
  [InnerCode] NVARCHAR(200), 
  [type] NVARCHAR(200), 
  [UplevelCode] INTEGER, 
  [UplevelName] NVARCHAR(100));

/* Data [main].[Dictionary] */


/* Drop table [main].[Equipment] */
drop table if exists [main].[Equipment];

/* Table structure [main].[Equipment] */
CREATE TABLE [main].[Equipment] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [FactoryCode] NVARCHAR(100), 
  [BelongDepartID] INTEGER, 
  [RepairDepartID] INTEGER, 
  [UseDepartID] INTEGER, 
  [Usufruct] NVARCHAR(100), 
  [Sort] INTEGER, 
  [EnableStatus] INTEGER, 
  [Name] NVARCHAR(100), 
  [EquipTypeID] INTEGER, 
  [Specification] NVARCHAR(20), 
  [Model] NVARCHAR(50), 
  [Manufacturer] NVARCHAR(50), 
  [ExFactoryNo] NVARCHAR(50), 
  [ManufactNo] NVARCHAR(50), 
  [ManufactDate] DATE, 
  [Weight] NVARCHAR(10), 
  [InstallCapacity] NVARCHAR(10), 
  [AssetNumber] NVARCHAR(20), 
  [PurchaseDate] DATE, 
  [InstallDate] DATE, 
  [WorkingDate] DATE, 
  [ToAssetDate] DATE, 
  [Age] INTEGER, 
  [Location] NVARCHAR(50), 
  [UseProperty] NVARCHAR(50), 
  [AssetStatus] NVARCHAR(20), 
  [ManageType] NVARCHAR(20), 
  [EstimateLifeMonth] FLOAT, 
  [Remark] NVARCHAR(200));

/* Data [main].[Equipment] */


/* Drop table [main].[EquipmentAlarmRecord] */
drop table if exists [main].[EquipmentAlarmRecord];

/* Table structure [main].[EquipmentAlarmRecord] */
CREATE TABLE [main].[EquipmentAlarmRecord] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(50), 
  [EquipID] INTEGER, 
  [AlarmTypeID] INTEGER, 
  [AlarmTime] TIMESTAMP, 
  [HandledStatus] NVARCHAR(20), 
  [HandledMan] NVARCHAR(20), 
  [Remark] NVARCHAR(200));

/* Data [main].[EquipmentAlarmRecord] */


/* Drop table [main].[EquipmentCard] */
drop table if exists [main].[EquipmentCard];

/* Table structure [main].[EquipmentCard] */
CREATE TABLE [main].[EquipmentCard] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [CardName] NVARCHAR(50), 
  [EquipID] INTEGER, 
  [EquipStatus] NVARCHAR(20), 
  [Enable] NVARCHAR(20), 
  [EnableAlarm] NVARCHAR(20), 
  [MonitoringLevelID] INTEGER, 
  [RebootTime] TIMESTAMP, 
  [LastOverhaulDate] TIMESTAMP, 
  [EquipHealthID] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[EquipmentCard] */


/* Drop table [main].[EquipmentHealth] */
drop table if exists [main].[EquipmentHealth];

/* Table structure [main].[EquipmentHealth] */
CREATE TABLE [main].[EquipmentHealth] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [EquipID] INTEGER, 
  [HealthName] NVARCHAR(20), 
  [Content] NVARCHAR(500), 
  [EnableStatus] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[EquipmentHealth] */


/* Drop table [main].[EquipmentLedger] */
drop table if exists [main].[EquipmentLedger];

/* Table structure [main].[EquipmentLedger] */
CREATE TABLE [main].[EquipmentLedger] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [EquipID] INTEGER, 
  [PurchaseFee] FLOAT, 
  [Freight] FLOAT, 
  [InstallationFee] FLOAT, 
  [AssetOriginWorth] FLOAT, 
  [AssetNetWorth] FLOAT, 
  [AccumulatedDepreciation] FLOAT, 
  [DepreciationMethod] NVARCHAR(20), 
  [DepreciationLife] FLOAT, 
  [Fundsource] NVARCHAR(20), 
  [AssetType] NVARCHAR(20));

/* Data [main].[EquipmentLedger] */


/* Drop table [main].[EquipmentLog] */
drop table if exists [main].[EquipmentLog];

/* Table structure [main].[EquipmentLog] */
CREATE TABLE [main].[EquipmentLog] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(20), 
  [EquipID] INTEGER, 
  [EquipName] NVARCHAR(100), 
  [LogTypeID] INTEGER, 
  [EquipTypeID] INTEGER, 
  [DepartID] INTEGER, 
  [LogInfo] NVARCHAR(1000), 
  [LogURL] NVARCHAR(200), 
  [Remark] NVARCHAR(200));

/* Data [main].[EquipmentLog] */


/* Drop table [main].[EquipmentMaterial] */
drop table if exists [main].[EquipmentMaterial];

/* Table structure [main].[EquipmentMaterial] */
CREATE TABLE [main].[EquipmentMaterial] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [EquipID] INTEGER, 
  [MaterialName1] NVARCHAR(50), 
  [MaterialNum1] INTEGER, 
  [MaterialAddr1] NVARCHAR(200), 
  [MaterialName2] NVARCHAR(50), 
  [MaterialNum2] INTEGER, 
  [MaterialAddr2] NVARCHAR(200), 
  [MaterialName3] NVARCHAR(50), 
  [MaterialNum3] INTEGER, 
  [MaterialAddr3] NVARCHAR(200), 
  [MaterialName4] NVARCHAR(50), 
  [MaterialNum4] INTEGER, 
  [MaterialAddr4] NVARCHAR(200), 
  [MaterialName5] NVARCHAR(50), 
  [MaterialNum5] INTEGER, 
  [MaterialAddr5] NVARCHAR(200), 
  [RelatedDocuments] NVARCHAR(200), 
  [EquipPicture] NVARCHAR(4000), 
  [Enclosure] NVARCHAR(200), 
  [Remark] NVARCHAR(200));

/* Data [main].[EquipmentMaterial] */


/* Drop table [main].[EquipmentRepairRecord] */
drop table if exists [main].[EquipmentRepairRecord];

/* Table structure [main].[EquipmentRepairRecord] */
CREATE TABLE [main].[EquipmentRepairRecord] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(20), 
  [EquipID] INTEGER, 
  [EquipName] NVARCHAR(100), 
  [BillCode] NVARCHAR(50), 
  [ReimburseDepartID] INTEGER, 
  [ReimburseDepartName] NVARCHAR(20), 
  [FactoryCode] NVARCHAR(100), 
  [ErrorDicID] INTEGER, 
  [ErrorAppearance] NVARCHAR(500), 
  [ErrorDescribe] NVARCHAR(500), 
  [RepairMan] NVARCHAR(20), 
  [RepairTypeID] INTEGER, 
  [RepairFee] FLOAT, 
  [RepairHours] FLOAT, 
  [RepairStartTime] TIMESTAMP, 
  [RepairEndTime] TIMESTAMP, 
  [RepairTerm] FLOAT, 
  [EquipStatusID] INTEGER, 
  [Recorder] NVARCHAR(20), 
  [RecordTime] TIMESTAMP, 
  [RepairStatus] NVARCHAR(20), 
  [Remark] NVARCHAR(200));

/* Data [main].[EquipmentRepairRecord] */


/* Drop table [main].[ErrorDictionary] */
drop table if exists [main].[ErrorDictionary];

/* Table structure [main].[ErrorDictionary] */
CREATE TABLE [main].[ErrorDictionary] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [ErrorName] NVARCHAR(100), 
  [ErrorDescribe] NVARCHAR(1000), 
  [Solution] NVARCHAR(1000), 
  [UplevelCode] INTEGER, 
  [UplevelName] NVARCHAR(100), 
  [Remark] NVARCHAR(20));

/* Data [main].[ErrorDictionary] */


/* Drop table [main].[Jurisdiction] */
drop table if exists [main].[Jurisdiction];

/* Table structure [main].[Jurisdiction] */
CREATE TABLE [main].[Jurisdiction] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(20), 
  [Value] NVARCHAR(200), 
  [Remark] NVARCHAR(200));

/* Data [main].[Jurisdiction] */


/* Drop table [main].[PushRule] */
drop table if exists [main].[PushRule];

/* Table structure [main].[PushRule] */
CREATE TABLE [main].[PushRule] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [RuleName] NVARCHAR(50), 
  [ModuleID] INTEGER, 
  [PushTypeID] INTEGER, 
  [Format] NVARCHAR(100), 
  [EnableStatus] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[PushRule] */


/* Drop table [main].[Role] */
drop table if exists [main].[Role];

/* Table structure [main].[Role] */
CREATE TABLE [main].[Role] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(20), 
  [JurisdictionID] INTEGER, 
  [MenuPermission] NVARCHAR(200), 
  [ModulePermission] NVARCHAR(200), 
  [SourcePermission] NVARCHAR(200), 
  [CRUDPermission] NVARCHAR(200), 
  [Remark] NVARCHAR(200));

/* Data [main].[Role] */


/* Drop table [main].[SystemLog] */
drop table if exists [main].[SystemLog];

/* Table structure [main].[SystemLog] */
CREATE TABLE [main].[SystemLog] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(50), 
  [LogTypeID] INTEGER, 
  [ModuleID] INTEGER, 
  [InfoContent] NVARCHAR(1000), 
  [RecordTime] TIMESTAMP, 
  [CauseReason] NVARCHAR(200), 
  [Remark] NVARCHAR(200));

/* Data [main].[SystemLog] */


/* Drop table [main].[SystemModule] */
drop table if exists [main].[SystemModule];

/* Table structure [main].[SystemModule] */
CREATE TABLE [main].[SystemModule] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(20), 
  [UplevelCode] INTEGER, 
  [UplevelName] NVARCHAR(20), 
  [Sort] INTEGER, 
  [URL] NVARCHAR(100), 
  [EnableStatus] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[SystemModule] */


/* Drop table [main].[SystemService] */
drop table if exists [main].[SystemService];

/* Table structure [main].[SystemService] */
CREATE TABLE [main].[SystemService] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [ServiceName] NVARCHAR(50), 
  [ServiceTypeID] INTEGER, 
  [ServiceURL] NVARCHAR(200), 
  [Describtion] NVARCHAR(200), 
  [EnableStatus] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[SystemService] */


/* Drop table [main].[SystemSetting] */
drop table if exists [main].[SystemSetting];

/* Table structure [main].[SystemSetting] */
CREATE TABLE [main].[SystemSetting] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [Name] NVARCHAR(50), 
  [Value] NVARCHAR(100), 
  [EnableStatus] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[SystemSetting] */


/* Drop table [main].[UploadSetting] */
drop table if exists [main].[UploadSetting];

/* Table structure [main].[UploadSetting] */
CREATE TABLE [main].[UploadSetting] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [RuleName] NVARCHAR(50), 
  [Format] NVARCHAR(50), 
  [DataTranferTypeID] NVARCHAR(20), 
  [EnableStatus] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[UploadSetting] */


/* Drop table [main].[User] */
drop table if exists [main].[User];

/* Table structure [main].[User] */
CREATE TABLE [main].[User] (
  [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
  [Code] INTEGER, 
  [DepartmentID] INTEGER, 
  [WorkNum] NVARCHAR(30), 
  [Name] NVARCHAR(20), 
  [Phone] NVARCHAR(20), 
  [Job] NVARCHAR(20), 
  [RoleID] INTEGER, 
  [Remark] NVARCHAR(200));

/* Data [main].[User] */


/* Commit Transaction */
commit transaction;

/* Enable Foreign Keys */
pragma foreign_keys = on;
