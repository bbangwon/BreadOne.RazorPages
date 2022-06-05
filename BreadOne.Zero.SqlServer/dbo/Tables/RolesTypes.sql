CREATE TABLE [dbo].[RolesTypes] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [Active] BIT            NOT NULL,
    CONSTRAINT [PK_RolesTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

