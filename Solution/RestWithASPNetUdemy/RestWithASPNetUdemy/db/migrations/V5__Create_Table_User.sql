/****** Object:  Table [dbo].[person]    Script Date: 20/04/2022 14:24:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](50) NOT NULL,
	[password] [varchar](130) NOT NULL,
	[full_name] [varchar](120) NOT NULL,
	[refresh_token] [varchar](500) NOT NULL,
	[refresh_token_expiry_time] datetime
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO