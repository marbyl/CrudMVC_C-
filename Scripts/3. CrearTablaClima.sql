/****** Object:  Table [dbo].[Clima]    Script Date: 18/10/2022 1:13:45 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clima](
	[id] [int] identity NOT NULL,
	[temperaturaCentigrados] [int] NULL,
	[descripcion] [varchar](max) NULL,
	[idCiudad] [int] NULL,
	[fecha] [date] NULL,
	[hora] [time](7) NULL,
 CONSTRAINT [PK_Clima] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Ciudad] ([id], [nombre]) VALUES (1, N'Garagoa')
GO
INSERT [dbo].[Ciudad] ([id], [nombre]) VALUES (2, N'Bogotá')
GO
INSERT [dbo].[Ciudad] ([id], [nombre]) VALUES (3, N'Medellín')
GO
INSERT [dbo].[Ciudad] ([id], [nombre]) VALUES (4, N'Pasto')
GO
INSERT [dbo].[Ciudad] ([id], [nombre]) VALUES (5, N'Cali')
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (5, N'Lluvias y más lluvias', NULL, CAST(N'2020-12-12' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (15, N'Parcialmente nublado', 1, CAST(N'2022-10-18' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (20, N'Parcialmente nublado', 2, CAST(N'2022-10-19' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (20, N'Lluvias parciales', 2, CAST(N'2022-10-19' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (20, N'Lluvias torrenciales', 3, CAST(N'2022-11-19' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (20, N'Soleado', 3, CAST(N'2022-11-19' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (20, N'Soleado', 3, CAST(N'2022-11-19' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES (20, N'Soleado', 4, CAST(N'2022-11-19' AS Date), NULL)
GO
INSERT [dbo].[Clima] ([temperaturaCentigrados], [descripcion], [idCiudad], [fecha], [hora]) VALUES ( 20, N'Soleado', 4, CAST(N'2022-11-19' AS Date), NULL)
GO
ALTER TABLE [dbo].[Clima]  WITH CHECK ADD  CONSTRAINT [FK_Clima_Ciudad] FOREIGN KEY([idCiudad])
REFERENCES [dbo].[Ciudad] ([id])
GO
ALTER TABLE [dbo].[Clima] CHECK CONSTRAINT [FK_Clima_Ciudad]
GO
