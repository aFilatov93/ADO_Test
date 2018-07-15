CREATE TABLE [dbo].[albums] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (MAX) NOT NULL,
    [Year]        INT           NULL,
    [Artist_Id]   INT           NULL,
    [Duration]    VARCHAR (MAX)   NULL,
    [TracksCount] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Artist_Id]) REFERENCES [dbo].[artists] ([Id])
);

CREATE TABLE [dbo].[artists] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       VARCHAR (MAX) NOT NULL,
    [Country_Id] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Country_Id]) REFERENCES [dbo].[countries] ([Id])
);

CREATE TABLE [dbo].[countries] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[genres] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[tracks] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (MAX) NOT NULL,
    [Album_Id]    INT           NULL,
    [Artist_Id]   INT           NULL,
    [Genre_Id]    INT           NULL,
    [Duration]    VARCHAR (MAX)   NOT NULL,
    [TrackNumber] INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Album_Id]) REFERENCES [dbo].[albums] ([Id]),
    FOREIGN KEY ([Artist_Id]) REFERENCES [dbo].[artists] ([Id]),
    FOREIGN KEY ([Genre_Id]) REFERENCES [dbo].[genres] ([Id])
);

