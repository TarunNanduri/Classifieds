CREATE TABLE [dbo].[AdType] (
    [Id]   INT           NOT NULL,
    [Name] VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Location] (
    [Id]   INT           NOT NULL,
    [Name] VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Category] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NOT NULL,
    [Icon]        INT           NOT NULL,
    [Description] VARCHAR (255) NOT NULL,
    [CreatedBy]   INT           NOT NULL,
    [ModifiedBy]  INT           NULL,
    [CreatedOn]   DATE          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Users] (
    [Id]         INT           NOT NULL,
    [Name]       VARCHAR (255) NOT NULL,
    [MailId]     VARCHAR (255) NOT NULL,
    [ContactNo]  VARCHAR (10)  NOT NULL,
    [LocationId] INT           NOT NULL,
    [Photo]      VARCHAR (MAX) NULL,
    [UserRole]   VARCHAR (5)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]),
    CHECK ([UserRole]='Admin' OR [UserRole]='User')
);
CREATE TABLE [dbo].[Advertisement] (
    [Id]         INT  IDENTITY (1, 1) NOT NULL,
    [UserId]     INT  NOT NULL,
    [AdTypeId]   INT  NOT NULL,
    [CategoryId] INT  NOT NULL,
    [PostedOn]   DATE NOT NULL,
    [ExpiryDays] INT  NOT NULL,
    [OfferCount] INT  NULL,
    [ViewCount]  INT  NULL,
    [Deleted]    BIT  NULL,
    [Expired]    BIT  NULL,
    [Reported]   BIT  NULL,
    [StatusId]   INT  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AdTypeId]) REFERENCES [dbo].[AdType] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);
CREATE TABLE [dbo].[Attribute] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [AdvertisementId] INT            NOT NULL,
    [Value]           NVARCHAR (MAX) NOT NULL,
    [Name]            VARCHAR (20)   NOT NULL,
    [Values]          VARCHAR (255)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AdvertisementId]) REFERENCES [dbo].[Advertisement] ([Id])
);
CREATE TABLE [dbo].[AttributeDetail] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [CategoryId]  INT          NULL,
    [Name]        VARCHAR (20) NOT NULL,
    [Type]        VARCHAR (20) NOT NULL,
    [IsMandatory] BIT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CHECK ([Type]='Boolean' OR [Type]='Number' OR [Type]='Text')
);
CREATE TABLE [dbo].[ChatBox] (
    [Id]              INT           NOT NULL,
    [UserId]          INT           NOT NULL,
    [AdvertisementId] INT           NOT NULL,
    [Msg]             VARCHAR (255) NOT NULL,
    [TimeStamp]       DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AdvertisementId]) REFERENCES [dbo].[Advertisement] ([Id])
);
CREATE TABLE [dbo].[Comment] (
    [AdvertisementId] INT           NULL,
    [Id]              INT           NOT NULL,
    [CommentedBy]     INT           NOT NULL,
    [Content]         VARCHAR (255) NOT NULL,
    [TimeStamp]       DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AdvertisementId]) REFERENCES [dbo].[Advertisement] ([Id])
);
CREATE TABLE [dbo].[Image] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [AdvertisementId] INT           NULL,
    [Img]             VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AdvertisementId]) REFERENCES [dbo].[Advertisement] ([Id])
);
CREATE TABLE [dbo].[Inbox] (
    [Id]      INT NOT NULL,
    [Offers]  INT NULL,
    [Reports] INT NULL,
    [Expired] INT NULL,
    [Deleted] INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Offer] (
    [Id]              INT           NOT NULL,
    [AdvertisementId] INT           NULL,
    [Price]           INT           NOT NULL,
    [Msg]             VARCHAR (255) NULL,
    [UserId]          INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AdvertisementId]) REFERENCES [dbo].[Advertisement] ([Id])
);
CREATE TABLE [dbo].[Report] (
    [Id]              INT           NOT NULL,
    [AdvertisementId] INT           NULL,
    [CategoryId]      INT           NOT NULL,
    [Description]     VARCHAR (255) NULL,
    [IsVerified]      BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AdvertisementId]) REFERENCES [dbo].[Advertisement] ([Id]),
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id])
);
CREATE TABLE [dbo].[Status] (
    [Id]              INT           NULL,
    [AdvertisementId] INT           NULL,
    [Msg]             VARCHAR (255) NOT NULL,
    FOREIGN KEY ([AdvertisementId]) REFERENCES [dbo].[Advertisement] ([Id])
);