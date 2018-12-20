CREATE TABLE [dbo].[UserInfo] (
    [UserId]     NVARCHAR (128) NOT NULL,
    [UserStatus] INT            NULL,
    --PRIMARY KEY CLUSTERED ([UserId] ASC)
	CONSTRAINT [PK_dbo.UserInfo] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_dbo.UserInfo_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserInfo]([UserId] ASC);

