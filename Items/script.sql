USE [master]
GO
/****** Object:  Database [LastChance]    Script Date: 4/22/2020 11:08:48 PM ******/
CREATE DATABASE [LastChance]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LastChance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\LastChance.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LastChance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\LastChance_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LastChance] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LastChance].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LastChance] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LastChance] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LastChance] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LastChance] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LastChance] SET ARITHABORT OFF 
GO
ALTER DATABASE [LastChance] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LastChance] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LastChance] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LastChance] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LastChance] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LastChance] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LastChance] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LastChance] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LastChance] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LastChance] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LastChance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LastChance] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LastChance] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LastChance] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LastChance] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LastChance] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LastChance] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LastChance] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LastChance] SET  MULTI_USER 
GO
ALTER DATABASE [LastChance] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LastChance] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LastChance] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LastChance] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LastChance] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LastChance] SET QUERY_STORE = OFF
GO
USE [LastChance]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditLogs]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EntityId] [nvarchar](63) NULL,
	[CommandType] [nvarchar](255) NULL,
	[CommandText] [nvarchar](max) NOT NULL,
	[Recorded] [datetime2](7) NOT NULL,
	[Role] [nvarchar](max) NULL,
	[User] [nvarchar](max) NULL,
 CONSTRAINT [PK_AuditLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BacklogItems]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BacklogItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sprint] [int] NULL,
	[ItemsAdded] [int] NOT NULL,
	[StoryPointsAdded] [int] NULL,
	[ItemsDone] [int] NOT NULL,
	[StoryPointsDone] [int] NULL,
	[ProjectId] [int] NOT NULL,
	[YearWeek] [int] NOT NULL,
 CONSTRAINT [PK_BacklogItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DivisionProjectStatuses]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DivisionProjectStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YearWeek] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[StatusColor] [nvarchar](max) NOT NULL,
	[ProjectStatus] [nvarchar](max) NULL,
	[Actions] [nvarchar](max) NULL,
 CONSTRAINT [PK_DivisionProjectStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoDReports]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoDReports](
	[ProjectId] [int] NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[YearWeek] [int] NOT NULL,
	[MetricId] [int] NOT NULL,
	[LinkToReport] [nvarchar](max) NULL,
	[ReportFileName] [nvarchar](max) NULL,
 CONSTRAINT [PK_DoDReports] PRIMARY KEY CLUSTERED 
(
	[MetricId] ASC,
	[ProjectId] ASC,
	[YearWeek] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Metrics]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Metrics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ValueType] [nvarchar](max) NOT NULL,
	[Unit] [nvarchar](max) NULL,
	[Tool] [nvarchar](max) NOT NULL,
	[SelectValues] [nvarchar](max) NULL,
	[Order] [int] NOT NULL,
	[ToolOrder] [int] NOT NULL,
 CONSTRAINT [PK_Metrics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MetricStatuses]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetricStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_MetricStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectAccesses]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAccesses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Email] [nvarchar](450) NOT NULL,
	[Role] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ProjectAccesses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DeliveryResponsibleName] [nvarchar](450) NULL,
	[Division] [nvarchar](max) NOT NULL,
	[JiraLink] [nvarchar](max) NULL,
	[KeyAccountManager] [nvarchar](max) NOT NULL,
	[ProjectEndDate] [datetime2](7) NULL,
	[ProjectStartDate] [datetime2](7) NOT NULL,
	[SourceCodeLink] [nvarchar](max) NULL,
	[PlatformVersion] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[Code] [nvarchar](450) NOT NULL,
	[PhrRequired] [bit] NOT NULL,
	[ProjectStateTypeId] [int] NOT NULL,
	[PhrRequiredFrom] [datetime2](7) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[DmrRequired] [bit] NOT NULL,
	[DmrRequiredFrom] [datetime2](7) NULL,
	[DmrRequiredTo] [datetime2](7) NULL,
	[DodRequired] [bit] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectStateTypes]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectStateTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[State] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProjectStateTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QualityReports]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QualityReports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CriticalBugs] [int] NOT NULL,
	[MajorBugs] [int] NOT NULL,
	[MinorBugs] [int] NOT NULL,
	[DoneBugs] [int] NOT NULL,
	[ReOpenBugs] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[YearWeek] [int] NOT NULL,
 CONSTRAINT [PK_QualityReports] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusColor] [nvarchar](max) NOT NULL,
	[ProjectStatus] [nvarchar](max) NULL,
	[ProjectId] [int] NOT NULL,
	[YearWeek] [int] NOT NULL,
	[RetrospectiveFeedBack] [nvarchar](max) NULL,
	[Milestone] [nvarchar](max) NULL,
	[MilestoneDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Thresholds]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Thresholds](
	[MetricStatusId] [int] NOT NULL,
	[MetricId] [int] NOT NULL,
	[UpperBound] [decimal](18, 2) NULL,
	[LowerBound] [decimal](18, 2) NULL,
	[IsRange] [bit] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[LowerBoundOperator] [nvarchar](max) NULL,
	[UpperBoundOperator] [nvarchar](max) NULL,
 CONSTRAINT [PK_Thresholds] PRIMARY KEY CLUSTERED 
(
	[MetricId] ASC,
	[MetricStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeeklyReportStatuses]    Script Date: 4/22/2020 11:08:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeeklyReportStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[YearWeek] [int] NOT NULL,
	[IsDeadlineMissed] [bit] NULL,
 CONSTRAINT [PK_WeeklyReportStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191129062211_InitDatabase', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191202042322_AddPropertiesToProject', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191202042751_MarkNameAsUniqueInProject', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191204035109_AddStatus_BacklogItems_QualityReports_AdditionalInfoEntities', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191204035733_AddIndexForWeekYearIdInWeeklyReportTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191204040132_AddProjectFKToBacklogItemQualityReportStatusAndAdditionalInfo', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191204040507_ChangeIndexFromIdToProjectIdInWeeklyReportTables', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191206082709_RemoveUnusedIndex', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191211085722_RemoveRemainingPropertiesInbacklogItems', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191211085854_MakeSprintNullable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191211090747_MakeStoryPointNullable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191211093140_AddYearWeekToWeeklyReportEntityAsGenerated', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191211095531_RemoveYearWeekToWeeklyReportEntityAsGenerated', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191211105259_AddUniqueIndexForYearWeekAndProjectId', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191212080551_RemoveRemainingBugs', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191212094846_AddRetrospectiveFeedBack_MakeProjectStatusOptional', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191212100018_AddInitialItemsAndStoryPointsAndPlatformVersion', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191219074430_AddIssueAndManyToManyRelationshipWithAdditionalInfo', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191219085041_AddStatusToAdditionalInfoIssues', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191219102513_RemoveRedundantProperties_AddIndexYearWeekToAdditionalInfo', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191223035640_AddOpenedYearWeekToIssue', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20191224084122_RemoveYearAndWeekFieldInAllTableInDB', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200103091645_AddAuditLogsTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200113075229_AddUserAndRoleToAuditLog', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117032150_AddDivisionProjectStatusTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117034110_AddResourceAvailabilityTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117034429_AddResourceSoonAvailableTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117035310_AddDivisionResourceUpdateTableAndRenameTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117035947_AddDivisionResourceNeedTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117041102_AddDivisionConcernTableAndUpdate', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117065157_AddActionToDivisionProjectStatus', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200117074935_RenameDivisionTables', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200120082937_RemoveResourceName', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200120091216_RemoveUniqueIndexForYearWeekAndResourceEmail', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200122024502_RemoveUnneccessaryIndexAndRenameTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200205070226_AddNoteToProject', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200205071522_AddMileStones', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200205072536_MileStonesChangeDateCreatedToDateUpdated', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200205080051_AddMileStonesDateAndProjectIdUniqueIndex', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200218122327_WeeklyReportStatus', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200228055906_AddCodeToProject', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200228062307_PopulateProjectsCode', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200228062817_MarkProjectCodeAsUnique', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200306091247_RemoveRequiredPropsInProject', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200306091738_AddPhrRequiredAndProjectStateToProjects', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200306094946_AddProjectStateType', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200306105945_SeedProjectsDataForDMR', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200313083127_AddPHRRequireFrom', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200315191039_AddMilestoneToStatus', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200317075724_BlankMigrationToPopulatePHRRequiredFrom', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200317080525_BlankMigration2ToPopulatePHRRequiredFrom', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200318013640_CreatedDateToProject', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200318024725_ClosedDateToProject', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200318061602_RemoveClosedDate_AddDMRRequired', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200318064118_BlankMigrationForInputtingData', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200319023137_AddProjectAccess', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200319023657_ProjectAccessRequiredEmailAndRole', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200319115405_AddIndexProjectIdAndDeliveryResponsibleName', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200320012527_AddDoDRelatedTables', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200320012936_BlankMigrationForSeedingData', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200320021447_AddDoDTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200320023319_RenameFKDoDReport', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200323043215_ChangeMetricStatusThresholdsToProjectMetrics', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200323044215_BlankMigrationsForSeedingData', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200323055309_MakeDoDReportValueRequired', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200323071512_MarkYearWeek_ProjectId_ProjectMetricIdUnique', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200323161007_ChangeProjectMetricToThreshold_RemoveKeyInDoD', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200323161848_AlterDoDReport', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200323163006_BlankMigrationForSeedingThresholdData', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200324072016_AddSelectValuesToMetric', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200325093334_AddDodRequiredField', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200326013530_AddLinkToReport_DoDReport', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200327034110_OrderToMetric_OperatorToThresholds', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200327034602_BlankMigrationSeedingData3', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200327072021_AddToolOrder', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200401105313_AddReportFileNameToDoD', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200405051925_RemoveUnsuedTables', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200405091005_RemoveUnsuedTable', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200414064742_DDDMigration', N'3.1.2')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200414160238_RemoveAdditionalInfo', N'3.1.2')
SET IDENTITY_INSERT [dbo].[BacklogItems] ON 

INSERT [dbo].[BacklogItems] ([Id], [Sprint], [ItemsAdded], [StoryPointsAdded], [ItemsDone], [StoryPointsDone], [ProjectId], [YearWeek]) VALUES (1, 0, 12, NULL, 0, 0, 62, 202000)
INSERT [dbo].[BacklogItems] ([Id], [Sprint], [ItemsAdded], [StoryPointsAdded], [ItemsDone], [StoryPointsDone], [ProjectId], [YearWeek]) VALUES (2, NULL, 0, 0, 0, 0, 62, 202014)
INSERT [dbo].[BacklogItems] ([Id], [Sprint], [ItemsAdded], [StoryPointsAdded], [ItemsDone], [StoryPointsDone], [ProjectId], [YearWeek]) VALUES (3, NULL, 1, 0, 0, 0, 62, 202013)
INSERT [dbo].[BacklogItems] ([Id], [Sprint], [ItemsAdded], [StoryPointsAdded], [ItemsDone], [StoryPointsDone], [ProjectId], [YearWeek]) VALUES (4, NULL, 5, 4, 2, 2, 62, 202012)
INSERT [dbo].[BacklogItems] ([Id], [Sprint], [ItemsAdded], [StoryPointsAdded], [ItemsDone], [StoryPointsDone], [ProjectId], [YearWeek]) VALUES (9, NULL, 1, 1, 0, 0, 62, 202011)
INSERT [dbo].[BacklogItems] ([Id], [Sprint], [ItemsAdded], [StoryPointsAdded], [ItemsDone], [StoryPointsDone], [ProjectId], [YearWeek]) VALUES (10, NULL, 1, 2, 2, 1, 62, 202015)
SET IDENTITY_INSERT [dbo].[BacklogItems] OFF
SET IDENTITY_INSERT [dbo].[DivisionProjectStatuses] ON 

INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (1, 202016, 26, N'RED', N'', NULL)
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (2, 202016, 27, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (3, 202016, 28, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (4, 202016, 29, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (5, 202016, 30, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (6, 202016, 31, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (7, 202016, 32, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (8, 202016, 33, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (9, 202016, 53, N'RED', N'', N'')
INSERT [dbo].[DivisionProjectStatuses] ([Id], [YearWeek], [ProjectId], [StatusColor], [ProjectStatus], [Actions]) VALUES (10, 202016, 54, N'RED', N'', N'')
SET IDENTITY_INSERT [dbo].[DivisionProjectStatuses] OFF
SET IDENTITY_INSERT [dbo].[Metrics] ON 

INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (1, N'Score', N'Number', NULL, N'Dareboost', NULL, 1, 1)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (2, N'Speed Index', N'Number', N'ms', N'Dareboost', NULL, 2, 1)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (3, N'Improvement', N'Number', NULL, N'Dareboost', NULL, 3, 1)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (4, N'Start render', N'Number', NULL, N'Dareboost', NULL, 4, 1)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (5, N'First byte', N'Number', N'ms', N'Dareboost', NULL, 5, 1)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (6, N'Performance', N'Number', N'%', N'Speed curve', NULL, 1, 2)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (7, N'Best Practice', N'Number', N'%', N'Speed curve', NULL, 2, 2)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (8, N'SEO', N'Number', N'%', N'Speed curve', NULL, 3, 2)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (9, N'Accessibility WCAG 2.1 AA', N'Number', N'%', N'Speed curve', NULL, 4, 2)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (10, N'40x, 50x', N'Text', NULL, N'Screaming Frog', N'Yes;No', 1, 3)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (11, N'Image with no alt text', N'Text', NULL, N'Screaming Frog', N'Yes;No', 2, 3)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (12, N'Fully loaded', N'Select', N's', N'Dareboost', N'Yes;No;Maybe', 6, 1)
INSERT [dbo].[Metrics] ([Id], [Name], [ValueType], [Unit], [Tool], [SelectValues], [Order], [ToolOrder]) VALUES (13, N'PWA', N'Text', N'%', N'Speed curve', NULL, 5, 2)
SET IDENTITY_INSERT [dbo].[Metrics] OFF
SET IDENTITY_INSERT [dbo].[MetricStatuses] ON 

INSERT [dbo].[MetricStatuses] ([Id], [Name]) VALUES (1, N'GREEN')
INSERT [dbo].[MetricStatuses] ([Id], [Name]) VALUES (2, N'YELLOW')
INSERT [dbo].[MetricStatuses] ([Id], [Name]) VALUES (3, N'RED')
SET IDENTITY_INSERT [dbo].[MetricStatuses] OFF
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (2, N'Maria Nila 24/7', NULL, N'AMS 24/7', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-06-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00Y', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-06-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (3, N'PAF maintenance', NULL, N'Baldur', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00M', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (4, N'Vi i Villa', NULL, N'Baldur', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2014-01-02T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00L', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2014-01-02T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (5, N'Maria Nila T&M', NULL, N'Baldur', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-06-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00K', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-06-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (6, N'Ahum', NULL, N'HCMC', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-10-17T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'005', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-10-17T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (7, N'Litium', NULL, N'HCMC', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2018-01-02T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00A', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-01-02T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (8, N'Nymans', NULL, N'HCMC', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-06-10T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00B', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-06-10T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (9, N'Nobia MRT', NULL, N'Odin', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00Q', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (10, N'BN Intranet', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01S', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (11, N'BNP - Management', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01T', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (12, N'BNP BE and Borssnack', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01U', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (13, N'BNP DnDi WordPress', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01V', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (14, N'NIS-FH intergration', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2016-11-07T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01L', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2016-11-07T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (15, N'Financial Hub', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01M', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (16, N'Service Plus', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01Z', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-10-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (17, N'CGI Driftsportal ', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-09-26T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01O', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-09-26T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (18, N'CGI New Osloskolen Web Portal', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-09-16T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01P', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-09-16T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (19, N'CGI Oslo School Mobile App', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-09-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01Q', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-09-03T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (20, N'CGI Technical Account Manager', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-08-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01R', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-08-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (21, N'Dialing.se', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-11-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01X', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-11-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (22, N'Transvoice - Corporate Web Design', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-06-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'021', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-06-03T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (23, N'Transvoice -  Public Site Development', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-11-04T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01K', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-11-04T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (24, N'Transvoice - Maintenance', NULL, N'Thor', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-10-07T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'017', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-10-07T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (25, N'Multisoft', NULL, N'Tyr', NULL, N'fredrik.spennare@niteco.se', NULL, CAST(N'2019-03-18T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'013', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-03-18T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (26, N'Electrolux AEG Morocco Template Update', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-12-10T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01C', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-10T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (27, N'Electrolux South Africa', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-12-19T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01D', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-19T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (28, N'Electrolux APAC Website Improvement', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-12-23T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01E', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-23T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (29, N'Eluctrolux Isarel Website Launching and Content', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-12-26T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01F', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-26T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (30, N'Electrolux Arabia Website Launching and Content Entry', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2020-04-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01G', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-04-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (31, N'Electrolux Zanussi Isarel Website Launching and Content Entry', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2020-01-31T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01H', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-01-31T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (32, N'Electrolux AEG Isarel Website Launching and Content Entry', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2020-03-02T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01I', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-03-02T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (33, N'Electrolux AMAD APAC', NULL, N'Frey', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'016', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (34, N'Electrolux Marketing Automation', NULL, N'Marketing', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-01-21T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'018', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-01-21T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (35, N'Electrolux Content Publishing', NULL, N'Marketing', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01A', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (36, N'Electrolux GA-GTM', NULL, N'Marketing', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01B', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (37, N'Blue Grass - Postrack App', NULL, N'Thor', NULL, N'khanh.do@niteco.se', NULL, CAST(N'2020-02-06T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'020', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-06T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (38, N'Grant Thornton 24/7', NULL, N'AMS 24/7', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2019-12-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00Z', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (39, N'Lexmod 24/7', NULL, N'AMS 24/7', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2019-10-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00X', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-10-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (40, N'Thinkmax 24/7', NULL, N'AMS 24/7', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2018-10-17T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00U', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-10-17T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (41, N'Delonghi T&M', NULL, N'Baldur', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2020-01-09T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00D', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-01-09T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (42, N'Delonghi content- Braun', NULL, N'Baldur', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00F', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (43, N'Delonghi content- Kenwood', NULL, N'Baldur', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00G', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (44, N'Delonghi content- SAP', NULL, N'Baldur', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2020-02-05T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00H', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-05T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (45, N'Delonghi content- Italy', NULL, N'Baldur', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00I', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (46, N'Grant Thornton', NULL, N'Baldur', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2019-12-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'024', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (47, N'High T&M', NULL, N'Baldur', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2017-07-14T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00J', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2017-07-14T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (48, N'Maginus MRT', NULL, N'Odin', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2019-12-22T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'023', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-22T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (49, N'Restockit - Design', NULL, N'Tyr', NULL, N'mark.welland@niteco.se', NULL, CAST(N'2020-02-06T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'026', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-06T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (50, N'Altius 24/7', NULL, N'AMS 24/7', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2018-12-26T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00S', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2018-12-26T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (51, N'Heineken 24/7', NULL, N'AMS 24/7', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2019-11-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00V', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-11-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (52, N'MLA Monthly Report', NULL, N'AMS 24/7', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'00W', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (53, N'Electrolux AMAD AU', NULL, N'Frey', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'02C', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (54, N'Vintec Contractor Agreement', NULL, N'Frey', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2019-12-30T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'01J', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-12-30T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (55, N'Adairs BAU', NULL, N'HCMC', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'002', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (56, N'Adairs AWS Beanstalk', NULL, N'HCMC', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'003', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-02-03T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (57, N'Adairs First Class', NULL, N'HCMC', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2020-01-02T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'004', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2020-01-02T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (58, N'Altius', NULL, N'HCMC', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2019-09-09T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'006', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-09-09T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (59, N'Heineken MRT', NULL, N'HCMC', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2019-09-02T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'007', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-09-02T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (60, N'MLA Monthly BAU', NULL, N'HCMC', NULL, N'paul.tannock@niteco.se', NULL, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'008', 0, 2, NULL, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (61, N'MLA Monthly BAU IT', N'PM3', N'HCMC', NULL, N'KAM1', NULL, CAST(N'2019-10-21T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'009', 0, 2, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-10-21T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (62, N'Oxford Shop', N'PM2', N'HCMC', NULL, N'KAM2', NULL, CAST(N'2019-01-11T00:00:00.0000000' AS DateTime2), NULL, NULL, N'', N'00C', 1, 2, CAST(N'2020-04-06T02:39:17.3853917' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1, CAST(N'2019-01-11T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (66, N'Test', N'PM8', N'AMS 24/7', NULL, N'KAM3', NULL, CAST(N'2020-04-16T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'123', 1, 1, CAST(N'2020-04-11T23:56:24.1173444' AS DateTime2), CAST(N'2020-04-08T18:44:16.8748153' AS DateTime2), 1, CAST(N'2019-01-11T00:00:00.0000000' AS DateTime2), NULL, 1)
INSERT [dbo].[Projects] ([Id], [Name], [DeliveryResponsibleName], [Division], [JiraLink], [KeyAccountManager], [ProjectEndDate], [ProjectStartDate], [SourceCodeLink], [PlatformVersion], [Note], [Code], [PhrRequired], [ProjectStateTypeId], [PhrRequiredFrom], [CreatedDate], [DmrRequired], [DmrRequiredFrom], [DmrRequiredTo], [DodRequired]) VALUES (71, N'Test2', N'PM1', N'AMS 24/7', NULL, N'KAM1', CAST(N'2020-04-15T00:00:00.0000000' AS DateTime2), CAST(N'2020-04-16T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, N'DFG', 1, 1, CAST(N'2020-04-11T13:44:37.0447120' AS DateTime2), CAST(N'2020-04-11T13:44:37.0447132' AS DateTime2), 1, CAST(N'2020-04-11T00:00:00.0000000' AS DateTime2), NULL, 1)
SET IDENTITY_INSERT [dbo].[Projects] OFF
SET IDENTITY_INSERT [dbo].[ProjectStateTypes] ON 

INSERT [dbo].[ProjectStateTypes] ([Id], [State]) VALUES (1, N'Preparing')
INSERT [dbo].[ProjectStateTypes] ([Id], [State]) VALUES (2, N'Active')
INSERT [dbo].[ProjectStateTypes] ([Id], [State]) VALUES (3, N'Closed')
SET IDENTITY_INSERT [dbo].[ProjectStateTypes] OFF
SET IDENTITY_INSERT [dbo].[QualityReports] ON 

INSERT [dbo].[QualityReports] ([Id], [CriticalBugs], [MajorBugs], [MinorBugs], [DoneBugs], [ReOpenBugs], [ProjectId], [YearWeek]) VALUES (1, 0, 0, 0, 0, 0, 62, 202014)
INSERT [dbo].[QualityReports] ([Id], [CriticalBugs], [MajorBugs], [MinorBugs], [DoneBugs], [ReOpenBugs], [ProjectId], [YearWeek]) VALUES (2, 2, 10, 1, 0, 0, 62, 202013)
INSERT [dbo].[QualityReports] ([Id], [CriticalBugs], [MajorBugs], [MinorBugs], [DoneBugs], [ReOpenBugs], [ProjectId], [YearWeek]) VALUES (3, 2, 5, 7, 2, 3, 62, 202012)
INSERT [dbo].[QualityReports] ([Id], [CriticalBugs], [MajorBugs], [MinorBugs], [DoneBugs], [ReOpenBugs], [ProjectId], [YearWeek]) VALUES (8, 1, 1, 0, 0, 0, 62, 202011)
INSERT [dbo].[QualityReports] ([Id], [CriticalBugs], [MajorBugs], [MinorBugs], [DoneBugs], [ReOpenBugs], [ProjectId], [YearWeek]) VALUES (9, 1, 1, 1, 2, 1, 62, 202015)
SET IDENTITY_INSERT [dbo].[QualityReports] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([Id], [StatusColor], [ProjectStatus], [ProjectId], [YearWeek], [RetrospectiveFeedBack], [Milestone], [MilestoneDate]) VALUES (1, N'GREEN', N'', 62, 202014, N'', N'', NULL)
INSERT [dbo].[Statuses] ([Id], [StatusColor], [ProjectStatus], [ProjectId], [YearWeek], [RetrospectiveFeedBack], [Milestone], [MilestoneDate]) VALUES (4, N'GREEN', N'', 62, 202012, N'', N'<p>12</p>
', CAST(N'2020-03-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Statuses] ([Id], [StatusColor], [ProjectStatus], [ProjectId], [YearWeek], [RetrospectiveFeedBack], [Milestone], [MilestoneDate]) VALUES (15, N'RED', N'<p>12</p>
', 62, 202013, N'<p>12</p>
', N'<p>12</p>
', CAST(N'2020-03-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Statuses] ([Id], [StatusColor], [ProjectStatus], [ProjectId], [YearWeek], [RetrospectiveFeedBack], [Milestone], [MilestoneDate]) VALUES (16, N'YELLOW', N'<p>12</p>
', 62, 202011, N'<p>12</p>
', N'<p>12</p>
', CAST(N'2020-03-27T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Statuses] ([Id], [StatusColor], [ProjectStatus], [ProjectId], [YearWeek], [RetrospectiveFeedBack], [Milestone], [MilestoneDate]) VALUES (17, N'RED', N'<p>12</p>
', 62, 202015, N'<p>12</p>
', N'<p>12</p>
', CAST(N'2020-04-09T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Statuses] OFF
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 1, CAST(100.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 1, CAST(89.00 AS Decimal(18, 2)), CAST(60.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 1, CAST(59.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 2, CAST(999.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 2, CAST(4999.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 2, CAST(1000000000.00 AS Decimal(18, 2)), CAST(5000.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 3, CAST(10.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 3, CAST(15.00 AS Decimal(18, 2)), CAST(11.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 3, CAST(1000000000.00 AS Decimal(18, 2)), CAST(16.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 4, CAST(1.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 4, CAST(1.20 AS Decimal(18, 2)), CAST(1.10 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 4, CAST(1000000000.00 AS Decimal(18, 2)), CAST(1.30 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 5, CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 5, CAST(200.00 AS Decimal(18, 2)), CAST(101.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 5, CAST(1000000000.00 AS Decimal(18, 2)), CAST(201.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 6, CAST(100.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 6, CAST(79.00 AS Decimal(18, 2)), CAST(70.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 6, CAST(69.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 7, CAST(100.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 7, CAST(79.00 AS Decimal(18, 2)), CAST(70.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 7, CAST(69.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 8, CAST(100.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 8, CAST(79.00 AS Decimal(18, 2)), CAST(60.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 8, CAST(59.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 9, CAST(100.00 AS Decimal(18, 2)), CAST(80.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 9, CAST(79.00 AS Decimal(18, 2)), CAST(60.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 9, CAST(59.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 1, NULL, N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 10, NULL, NULL, 0, N'NO', NULL, NULL)
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 10, NULL, NULL, 0, N'YES', NULL, NULL)
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 11, NULL, NULL, 0, N'NO', NULL, NULL)
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 11, NULL, NULL, 0, N'YES', NULL, NULL)
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (1, 12, NULL, NULL, 0, N'Yes', NULL, NULL)
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (2, 12, CAST(2.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), 0, N'Maybe', N'<=', N'<=')
INSERT [dbo].[Thresholds] ([MetricStatusId], [MetricId], [UpperBound], [LowerBound], [IsRange], [Value], [LowerBoundOperator], [UpperBoundOperator]) VALUES (3, 12, NULL, NULL, 0, N'No', NULL, NULL)
/****** Object:  Index [IX_BacklogItems_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_BacklogItems_ProjectId] ON [dbo].[BacklogItems]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BacklogItems_YearWeek_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_BacklogItems_YearWeek_ProjectId] ON [dbo].[BacklogItems]
(
	[YearWeek] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DivisionProjectStatuses_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_DivisionProjectStatuses_ProjectId] ON [dbo].[DivisionProjectStatuses]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DivisionProjectStatuses_YearWeek_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_DivisionProjectStatuses_YearWeek_ProjectId] ON [dbo].[DivisionProjectStatuses]
(
	[YearWeek] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DoDReports_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_DoDReports_ProjectId] ON [dbo].[DoDReports]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProjectAccesses_ProjectId_Role_Email]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProjectAccesses_ProjectId_Role_Email] ON [dbo].[ProjectAccesses]
(
	[ProjectId] ASC,
	[Role] ASC,
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Projects_Code]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Projects_Code] ON [dbo].[Projects]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Projects_Id_DeliveryResponsibleName]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Projects_Id_DeliveryResponsibleName] ON [dbo].[Projects]
(
	[Id] ASC,
	[DeliveryResponsibleName] ASC
)
WHERE ([DeliveryResponsibleName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Projects_ProjectStateTypeId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_Projects_ProjectStateTypeId] ON [dbo].[Projects]
(
	[ProjectStateTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QualityReports_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_QualityReports_ProjectId] ON [dbo].[QualityReports]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QualityReports_YearWeek_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_QualityReports_YearWeek_ProjectId] ON [dbo].[QualityReports]
(
	[YearWeek] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Statuses_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_Statuses_ProjectId] ON [dbo].[Statuses]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Statuses_YearWeek_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Statuses_YearWeek_ProjectId] ON [dbo].[Statuses]
(
	[YearWeek] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Thresholds_MetricStatusId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_Thresholds_MetricStatusId] ON [dbo].[Thresholds]
(
	[MetricStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WeeklyReportStatuses_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE NONCLUSTERED INDEX [IX_WeeklyReportStatuses_ProjectId] ON [dbo].[WeeklyReportStatuses]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_WeeklyReportStatuses_YearWeek_ProjectId]    Script Date: 4/22/2020 11:08:49 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_WeeklyReportStatuses_YearWeek_ProjectId] ON [dbo].[WeeklyReportStatuses]
(
	[YearWeek] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BacklogItems] ADD  DEFAULT ((0)) FOR [YearWeek]
GO
ALTER TABLE [dbo].[DoDReports] ADD  DEFAULT ((0)) FOR [MetricId]
GO
ALTER TABLE [dbo].[Metrics] ADD  DEFAULT ((0)) FOR [Order]
GO
ALTER TABLE [dbo].[Metrics] ADD  DEFAULT ((0)) FOR [ToolOrder]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (N'') FOR [Division]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (N'') FOR [KeyAccountManager]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [ProjectStartDate]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ((1)) FOR [PhrRequired]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ((2)) FOR [ProjectStateTypeId]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ('2020-01-01T00:00:00.0000000') FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (CONVERT([bit],(1))) FOR [DmrRequired]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (CONVERT([bit],(0))) FOR [DodRequired]
GO
ALTER TABLE [dbo].[QualityReports] ADD  DEFAULT ((0)) FOR [YearWeek]
GO
ALTER TABLE [dbo].[Statuses] ADD  DEFAULT ((0)) FOR [YearWeek]
GO
ALTER TABLE [dbo].[BacklogItems]  WITH CHECK ADD  CONSTRAINT [FK_BacklogItems_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BacklogItems] CHECK CONSTRAINT [FK_BacklogItems_Projects_ProjectId]
GO
ALTER TABLE [dbo].[DivisionProjectStatuses]  WITH CHECK ADD  CONSTRAINT [FK_DivisionProjectStatuses_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DivisionProjectStatuses] CHECK CONSTRAINT [FK_DivisionProjectStatuses_Projects_ProjectId]
GO
ALTER TABLE [dbo].[DoDReports]  WITH CHECK ADD  CONSTRAINT [FK_DoDReports_Metrics_MetricId] FOREIGN KEY([MetricId])
REFERENCES [dbo].[Metrics] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DoDReports] CHECK CONSTRAINT [FK_DoDReports_Metrics_MetricId]
GO
ALTER TABLE [dbo].[DoDReports]  WITH CHECK ADD  CONSTRAINT [FK_DoDReports_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DoDReports] CHECK CONSTRAINT [FK_DoDReports_Projects_ProjectId]
GO
ALTER TABLE [dbo].[ProjectAccesses]  WITH CHECK ADD  CONSTRAINT [FK_ProjectAccesses_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectAccesses] CHECK CONSTRAINT [FK_ProjectAccesses_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectStateTypes_ProjectStateTypeId] FOREIGN KEY([ProjectStateTypeId])
REFERENCES [dbo].[ProjectStateTypes] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_ProjectStateTypes_ProjectStateTypeId]
GO
ALTER TABLE [dbo].[QualityReports]  WITH CHECK ADD  CONSTRAINT [FK_QualityReports_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QualityReports] CHECK CONSTRAINT [FK_QualityReports_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Statuses]  WITH CHECK ADD  CONSTRAINT [FK_Statuses_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Statuses] CHECK CONSTRAINT [FK_Statuses_Projects_ProjectId]
GO
ALTER TABLE [dbo].[Thresholds]  WITH CHECK ADD  CONSTRAINT [FK_Thresholds_Metrics_MetricId] FOREIGN KEY([MetricId])
REFERENCES [dbo].[Metrics] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Thresholds] CHECK CONSTRAINT [FK_Thresholds_Metrics_MetricId]
GO
ALTER TABLE [dbo].[Thresholds]  WITH CHECK ADD  CONSTRAINT [FK_Thresholds_MetricStatuses_MetricStatusId] FOREIGN KEY([MetricStatusId])
REFERENCES [dbo].[MetricStatuses] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Thresholds] CHECK CONSTRAINT [FK_Thresholds_MetricStatuses_MetricStatusId]
GO
ALTER TABLE [dbo].[WeeklyReportStatuses]  WITH CHECK ADD  CONSTRAINT [FK_WeeklyReportStatuses_Projects_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WeeklyReportStatuses] CHECK CONSTRAINT [FK_WeeklyReportStatuses_Projects_ProjectId]
GO
USE [master]
GO
ALTER DATABASE [LastChance] SET  READ_WRITE 
GO
