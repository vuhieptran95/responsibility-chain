USE [master]
GO
/****** Object:  Database [LastChanceIdentity]    Script Date: 5/22/2020 5:58:10 PM ******/
CREATE DATABASE [LastChanceIdentity]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LastChanceIdentity', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LastChanceIdentity.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LastChanceIdentity_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\LastChanceIdentity_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LastChanceIdentity] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LastChanceIdentity].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LastChanceIdentity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET ARITHABORT OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LastChanceIdentity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LastChanceIdentity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LastChanceIdentity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LastChanceIdentity] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [LastChanceIdentity] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET RECOVERY FULL 
GO
ALTER DATABASE [LastChanceIdentity] SET  MULTI_USER 
GO
ALTER DATABASE [LastChanceIdentity] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LastChanceIdentity] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LastChanceIdentity] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LastChanceIdentity] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LastChanceIdentity] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LastChanceIdentity', N'ON'
GO
ALTER DATABASE [LastChanceIdentity] SET QUERY_STORE = OFF
GO
USE [LastChanceIdentity]
GO
/****** Object:  User [NITECO\hiep.tran2]    Script Date: 5/22/2020 5:58:10 PM ******/
CREATE USER [NITECO\hiep.tran2] FOR LOGIN [NITECO\hiep.tran2] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NITECO\hiep.tran2]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [NITECO\hiep.tran2]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [NITECO\hiep.tran2]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [NITECO\hiep.tran2]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [NITECO\hiep.tran2]
GO
ALTER ROLE [db_datareader] ADD MEMBER [NITECO\hiep.tran2]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [NITECO\hiep.tran2]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/22/2020 5:58:10 PM ******/
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
/****** Object:  Table [dbo].[Clients]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [nvarchar](450) NOT NULL,
	[Secret] [nvarchar](max) NULL,
	[RedirectedUri] [nvarchar](max) NULL,
	[UserInteractionRequired] [bit] NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientScopes]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientScopes](
	[ClientId] [nvarchar](450) NOT NULL,
	[ScopeId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ClientScopes] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC,
	[ScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Policies]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Policies](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PolicyScopes]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PolicyScopes](
	[PolicyId] [nvarchar](450) NOT NULL,
	[ScopeId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_PolicyScopes] PRIMARY KEY CLUSTERED 
(
	[PolicyId] ASC,
	[ScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScopeProviders]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScopeProviders](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_ScopeProviders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scopes]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scopes](
	[Id] [nvarchar](450) NOT NULL,
	[ScopeProviderId] [nvarchar](450) NULL,
	[Resource] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
 CONSTRAINT [PK_Scopes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPolicies]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPolicies](
	[Username] [nvarchar](450) NOT NULL,
	[PolicyId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserPolicies] PRIMARY KEY CLUSTERED 
(
	[Username] ASC,
	[PolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/22/2020 5:58:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Username] [nvarchar](450) NOT NULL,
	[Role] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ClientScopes_ScopeId]    Script Date: 5/22/2020 5:58:10 PM ******/
CREATE NONCLUSTERED INDEX [IX_ClientScopes_ScopeId] ON [dbo].[ClientScopes]
(
	[ScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PolicyScopes_ScopeId]    Script Date: 5/22/2020 5:58:10 PM ******/
CREATE NONCLUSTERED INDEX [IX_PolicyScopes_ScopeId] ON [dbo].[PolicyScopes]
(
	[ScopeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Scopes_ScopeProviderId]    Script Date: 5/22/2020 5:58:10 PM ******/
CREATE NONCLUSTERED INDEX [IX_Scopes_ScopeProviderId] ON [dbo].[Scopes]
(
	[ScopeProviderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserPolicies_PolicyId]    Script Date: 5/22/2020 5:58:10 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserPolicies_PolicyId] ON [dbo].[UserPolicies]
(
	[PolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientScopes]  WITH CHECK ADD  CONSTRAINT [FK_ClientScopes_Clients_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClientScopes] CHECK CONSTRAINT [FK_ClientScopes_Clients_ClientId]
GO
ALTER TABLE [dbo].[ClientScopes]  WITH CHECK ADD  CONSTRAINT [FK_ClientScopes_Scopes_ScopeId] FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scopes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ClientScopes] CHECK CONSTRAINT [FK_ClientScopes_Scopes_ScopeId]
GO
ALTER TABLE [dbo].[PolicyScopes]  WITH CHECK ADD  CONSTRAINT [FK_PolicyScopes_Policies_PolicyId] FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PolicyScopes] CHECK CONSTRAINT [FK_PolicyScopes_Policies_PolicyId]
GO
ALTER TABLE [dbo].[PolicyScopes]  WITH CHECK ADD  CONSTRAINT [FK_PolicyScopes_Scopes_ScopeId] FOREIGN KEY([ScopeId])
REFERENCES [dbo].[Scopes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PolicyScopes] CHECK CONSTRAINT [FK_PolicyScopes_Scopes_ScopeId]
GO
ALTER TABLE [dbo].[Scopes]  WITH CHECK ADD  CONSTRAINT [FK_Scopes_ScopeProviders_ScopeProviderId] FOREIGN KEY([ScopeProviderId])
REFERENCES [dbo].[ScopeProviders] ([Id])
GO
ALTER TABLE [dbo].[Scopes] CHECK CONSTRAINT [FK_Scopes_ScopeProviders_ScopeProviderId]
GO
ALTER TABLE [dbo].[UserPolicies]  WITH CHECK ADD  CONSTRAINT [FK_UserPolicies_Policies_PolicyId] FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPolicies] CHECK CONSTRAINT [FK_UserPolicies_Policies_PolicyId]
GO
ALTER TABLE [dbo].[UserPolicies]  WITH CHECK ADD  CONSTRAINT [FK_UserPolicies_Users_Username] FOREIGN KEY([Username])
REFERENCES [dbo].[Users] ([Username])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserPolicies] CHECK CONSTRAINT [FK_UserPolicies_Users_Username]
GO
USE [master]
GO
ALTER DATABASE [LastChanceIdentity] SET  READ_WRITE 
GO
