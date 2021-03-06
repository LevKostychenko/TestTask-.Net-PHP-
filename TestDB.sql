USE [Test]
GO
/****** Object:  Table [dbo].[TreeInfo]    Script Date: 4/18/2020 2:00:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreeInfo](
	[Tree] [varchar](30) NOT NULL,
	[TreeName] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Tree] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TreeNode]    Script Date: 4/18/2020 2:00:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreeNode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tree] [varchar](30) NULL,
	[ParentID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'1', N'Шторы')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'1.1', N'Двери')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'1.2', N'Пальмы')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'1.3', N'Окна')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'2', N'Цемент')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'2.1', N'Плодовые')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'2.1.1', N'Скатерть')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'2.1.2', N'Монеты')
INSERT [dbo].[TreeInfo] ([Tree], [TreeName]) VALUES (N'2.2', N'Краска')
SET IDENTITY_INSERT [dbo].[TreeNode] ON 

INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (1, N'1', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (2, N'1.1', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (3, N'1.3', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (4, N'1.2', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (5, N'2', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (6, N'2.2', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (7, N'2.1', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (8, N'2.1.2', NULL)
INSERT [dbo].[TreeNode] ([Id], [Tree], [ParentID]) VALUES (9, N'2.1.1', NULL)
SET IDENTITY_INSERT [dbo].[TreeNode] OFF
ALTER TABLE [dbo].[TreeNode]  WITH CHECK ADD FOREIGN KEY([Tree])
REFERENCES [dbo].[TreeInfo] ([Tree])
GO

DECLARE @Cursor CURSOR;
DECLARE @ParentID varchar;
BEGIN
    SET @Cursor = CURSOR FOR
    SELECT * FROM [dbo].[TreeNode]
	print @ParentID

    OPEN @Cursor 
    FETCH NEXT FROM @Cursor 
    INTO @ParentID

    WHILE @@FETCH_STATUS = 0
    BEGIN
      /*
         YOUR ALGORITHM GOES HERE   
      */
      FETCH NEXT FROM @Cursor 
      INTO @ParentID 
    END; 

    CLOSE @Cursor ;
    DEALLOCATE @Cursor;
END;

GO
DECLARE @RowCount int;
DECLARE @Value varchar(30);
DECLARE @i int;
SET @i = 0;
SELECT @RowCount = COUNT(*) FROM [dbo].[TreeNode];
WHILE @i <= @RowCount
    BEGIN
		SET @Value = CAST(LEFT((SELECT Tree FROM [dbo].[TreeNode] WHERE Id = @i), LEN(CAST((SELECT Tree FROM [dbo].[TreeNode] WHERE Id = @i) AS varchar))-1) AS varchar(30)) 			
		IF RIGHT(@Value, 1) = '.'
			SET @Value = LEFT(@Value, LEN(@Value)-1);
		ELSE
			SET @Value = @Value
		
		UPDATE [dbo].[TreeNode] 
		SET [dbo].[TreeNode].[ParentID] = 
			(SELECT Id FROM [dbo].[TreeNode] WHERE Tree LIKE @Value
			)
		WHERE Id = @i
		SET @i = @i+1
	END; 
GO

SELECT * FROM [dbo].[TreeNode] ORDER BY [dbo].[TreeNode].[Tree]
SELECT * FROM [dbo].[TreeInfo] ORDER BY [dbo].[TreeInfo].[Tree]
GO