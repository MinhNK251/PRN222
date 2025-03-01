USE master
GO

CREATE DATABASE OilPaintingArt2024DB
GO

USE OilPaintingArt2024DB
GO

CREATE TABLE SystemAccount(
  AccountID int primary key,
  AccountPassword nvarchar(40) not null,
  AccountFullName nvarchar(80) not null,
  AccountEmail nvarchar(80) unique, 
  Role int
)
GO

INSERT INTO SystemAccount VALUES(1 ,N'@33', N'OilPaintingArt Systen Administrator', 'admin@OilPaintingArtStore.com.au', 1);
INSERT INTO SystemAccount VALUES(2 ,N'@33', N'OilPaintingArt Systen Staff', 'staff@OilPaintingArtStore.com.au', 2);
INSERT INTO SystemAccount VALUES(3 ,N'@33', N'OilPaintingArt Systen Manager', 'manager@OilPaintingArtStore.com.au', 3);
INSERT INTO SystemAccount VALUES(4 ,N'@33', N'OilPaintingArt Systen Customer', 'customer@OilPaintingArtStore.com.au', 4);
GO


CREATE TABLE SupplierCompany(
  SupplierId nvarchar(30) primary key,
  CompanyName nvarchar(100) not null,
  CompanyTypeDescription nvarchar(250) not null, 
  IsActive int
)
GO
INSERT INTO SupplierCompany VALUES(N'SPC001', N'Blick Art Materials', N'A well-known supplier of art materials, including oil paints and brushes', 1)
GO
INSERT INTO SupplierCompany VALUES(N'SPC002', N'Jerrys Artarama', N'A UK-based company specializing in high-quality, pigmented oils for artists', 1)
GO
INSERT INTO SupplierCompany VALUES(N'SPC003', N'Utrecht Art Supplies', N'A popular online retailer that offers a wide range of art supplies, including oil paints and brushes', 1)
GO
INSERT INTO SupplierCompany VALUES(N'SPC004', N'Cheap Joes Art Stuff', N'A UK-based company that specializes in natural bristle oil paints and brushes for artists', 1)
GO
INSERT INTO SupplierCompany VALUES(N'SPC005', N'ArtTutor', N'Indian Oil Corporation Limited (established in 1948)', 1)
GO



CREATE TABLE OilPaintingArt (
 OilPaintingArtId int primary key,
 ArtTitle nvarchar(100) not null,
 OilPaintingArtLocation nvarchar(240),
 OilPaintingArtStyle nvarchar(50),
 Artist nvarchar(80),
 NotablFeatures nvarchar(250),
 PriceOfOilPaintingArt decimal,
 StoreQuantity int, 
 CreatedDate Datetime,
 SupplierId nvarchar(30) references SupplierCompany(SupplierId) on delete cascade on update cascade
)
GO

INSERT INTO OilPaintingArt VALUES(1, N'The Starry Night', N'The Museum of Modern Art, New York', N'Post-Impressionism', N'Vincent van Gogh', N'Whirling and swirling sky, cypress tree, and a small village', 1149.5, 50, CAST(N'2024-02-01' AS DateTime), 'SPC001')
GO
INSERT INTO OilPaintingArt VALUES(2, N'Mona Lisa', N'The Louvre, Paris', N'Renaissance', N'Leonardo da Vinci', N'Enigmatic smile, sfumato technique, and captivating gaze', 2149.5, 10, CAST(N'2024-02-01' AS DateTime), 'SPC001')
GO
INSERT INTO OilPaintingArt VALUES(3, N'The Scream', N'National Gallery, Oslo', N'Expressionism', N'Edvard Munch', N'Vivid colors, distorted figure, and emotional intensity', 149.5, 40, CAST(N'2024-02-01' AS DateTime), 'SPC001')
GO
INSERT INTO OilPaintingArt VALUES(4, N'Guernica', N'Museo Reina Sofia, Madrid', N'Cubism', N'Pablo Picasso', N'Distressed figures, black and white palette, and powerful anti-war symbolism', 1049.5, 20, CAST(N'2024-02-01' AS DateTime), 'SPC001')
GO
INSERT INTO OilPaintingArt VALUES(5, N'Girl with a Pearl Earring', N'Mauritshuis, The Hague', N'Baroque', N'Johannes Vermeer', N'Mysterious subject, luminous pearl, and exquisite use of light and shadow', 1999.5, 80, CAST(N'2024-02-01' AS DateTime), 'SPC002')
GO
INSERT INTO OilPaintingArt VALUES(6, N'The Persistence of Memory', N'Museum of Modern Art, New York', N'Surrealism', N'Salvador Dali', N'Melting clocks, dreamlike landscape, and eerie atmosphere', 899.5, 30, CAST(N'2024-02-01' AS DateTime), 'SPC003')
GO
INSERT INTO OilPaintingArt VALUES(7, N'The Birth of Venus', N'Uffizi Gallery, Florence', N'Early Renaissance', N'Sandro Botticelli', N'Venus emerging from a seashell, idealized figures, and delicate colors', 799.5, 70, CAST(N'2024-02-01' AS DateTime), 'SPC004')
GO
INSERT INTO OilPaintingArt VALUES(8, N'The Night Watch', N'Rijksmuseum, Amsterdam', N'Baroque', N'Rembrandt van Rijn', N'Group portrait, dramatic use of light, and intricate details', 1049.5, 25, CAST(N'2024-02-01' AS DateTime), 'SPC001')
GO

