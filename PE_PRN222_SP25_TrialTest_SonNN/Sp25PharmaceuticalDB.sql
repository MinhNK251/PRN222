USE master
GO

CREATE DATABASE Sp25PharmaceuticalDB 
GO

USE Sp25PharmaceuticalDB 
GO

CREATE TABLE StoreAccount (
  StoreAccountID int primary key,
  StoreAccountPassword nvarchar(90) not null,
  EmailAddress nvarchar(90) unique, 
  StoreAccountDescription nvarchar(140) not null,
  Role int
)
GO

INSERT INTO StoreAccount VALUES(551 ,N'@4','admin@PharmaceuticalStore.com', N'Store Admin', 1);
INSERT INTO StoreAccount VALUES(553 ,N'@4','manager@PharmaceuticalStore.com', N'Manager', 2);
INSERT INTO StoreAccount VALUES(552 ,N'@4','staff@PharmaceuticalStore.com', N'Staff', 3);
INSERT INTO StoreAccount VALUES(554 ,N'@4','member1@PharmaceuticalStore.com', N'Member', 4);
GO

/*
Manufacturer Name: The official name of the company.
Address: Physical address of the main headquarters.
Contact Information: Phone number, email, and website for customer inquiries.
Year Established: The year the company was founded.
Country of Origin: Country where the manufacturer is based.

*/
CREATE TABLE Manufacturer (
  ManufacturerID nvarchar(20) primary key,
  ManufacturerName nvarchar(100) not null,
  ShortDescription nvarchar(400),
  YearEstablished int,
  ContactInformation nvarchar(120) not null, 
  CountryofOrigin nvarchar(250) not null
)
GO

INSERT INTO Manufacturer VALUES(N'MM001231', N'Bayer', N'Bayer is a global pharmaceutical and life sciences company known for its research-based products in healthcare and agriculture.', 1863,  N'+49 214 30 1 (Germany) / www.bayer.com', N'Germany')
GO
INSERT INTO Manufacturer VALUES(N'MM001232', N'Motrin (Johnson & Johnson)', N'Motrin is a brand of ibuprofen manufactured by Johnson & Johnson, known for pain relief and anti-inflammatory properties.', 1886,  N'1-800-526-7736 (USA) / www.jnj.com', N'USA')
GO
INSERT INTO Manufacturer VALUES(N'MM001233', N'Glumetza (Santarus, Inc.)', N'Glumetza is a prescription medication used to manage blood sugar levels in adults with type 2 diabetes.', 2004,  N'1-866-726-8287 (USA) / www.santarus.com', N'USA')
GO
INSERT INTO Manufacturer VALUES(N'MM001234', N'Prilosec (AstraZeneca)', N'Prilosec is a proton pump inhibitor used to treat gastroesophageal reflux disease (GERD) and other related conditions.', 1999,  N'+44 (0)20 7604 7000 (UK) / www.astrazeneca.com', N'United Kingdom')
GO
INSERT INTO Manufacturer VALUES(N'MM001235', N'Merck & Co.', N'Merck & Co. is a global healthcare company that develops prescription medicines, vaccines, biologic therapies, and animal health products.', 1891,  N'1-800-444-2080 (USA) / www.merck.com', N'USA')
GO
INSERT INTO Manufacturer VALUES(N'MM001236', N'Pfizer', N'Pfizer is a leading global biopharmaceutical company known for its innovative medicines, vaccines, and consumer healthcare products.', 1849,  N'1-212-733-2323 (USA) / www.pfizer.com', N'USA')
GO
INSERT INTO Manufacturer VALUES(N'MM001237', N'Claritin (Bayer)', N'Claritin is an over-the-counter antihistamine used to relieve allergy symptoms, produced by Bayer.', 1993,  N'+49 214 30 1 (Germany) / www.bayer.com', N'Germany')
GO
INSERT INTO Manufacturer VALUES(N'MM001238', N'Zyrtec (Johnson & Johnson)', N'Zyrtec is an antihistamine used to treat allergy symptoms, marketed by Johnson & Johnson.', 1995,  N'1-800-526-7736 (USA) / www.jnj.com', N'USA')
GO


/*
Drug Name: The name of the pharmaceutical (both generic and brand names).
ActiveIngredients: The main chemical components responsible for the drug’s effects.
Indications: Conditions or diseases the drug is used to treat.
Dosage Form: Form in which the drug is administered (e.g., tablet, capsule, injection, cream).
Expiration Date: Shelf life or expiration date of the pharmaceutical.
Warnings and Precautions: Important safety information for healthcare providers and patients.
Manufacturer: The company that produces the drug.

*/


CREATE TABLE MedicineInformation (
  MedicineID nvarchar(30) PRIMARY KEY,
  MedicineName nvarchar(160) not null,
  ActiveIngredients nvarchar(200) not null, 
  ExpirationDate nvarchar(120),
  DosageForm nvarchar(400) not null, 
  WarningsAndPrecautions nvarchar(400) not null, 
  ManufacturerID nvarchar(20) FOREIGN KEY references Manufacturer(ManufacturerID) on delete cascade on update cascade
)
GO


INSERT INTO MedicineInformation VALUES(N'MI000110', N'Aspirin', N'Acetylsalicylic Acid', N'3 years from manufacturing date', N'Tablet', N'May cause stomach bleeding; not for use in children with viral infections.', N'MM001231')
GO
INSERT INTO MedicineInformation VALUES(N'MI000111', N'Ibuprofen', N'Ibuprofen', N'3 years from manufacturing date', N'Tablet', N'Use with caution in patients with kidney disease; may cause gastrointestinal issues.', N'MM001232')
GO
INSERT INTO MedicineInformation VALUES(N'MI000112', N'Metformin', N'Metformin Hydrochloride', N'2 years from manufacturing date', N'Tablet', N'Risk of lactic acidosis; monitor renal function.', N'MM001233')
GO
INSERT INTO MedicineInformation VALUES(N'MI000113', N'Omeprazole', N'Omeprazole', N'3 years from manufacturing date', N'Capsule', N'May cause kidney problems; long-term use may increase risk of bone fractures.', N'MM001234')
GO
INSERT INTO MedicineInformation VALUES(N'MI000114', N'Simvastatin', N'Simvastatin', N'2 years from manufacturing date', N'Tablet', N'Monitor liver function; avoid grapefruit juice.', N'MM001235')
GO
INSERT INTO MedicineInformation VALUES(N'MI000115', N'Amoxicillin', N'Amoxicillin Trihydrate', N'2 years from manufacturing date', N'Capsule', N'May cause allergic reactions; use caution in patients with penicillin allergy.', N'MM001236')
GO
INSERT INTO MedicineInformation VALUES(N'MI000116', N'Loratadine', N'Loratadine', N'2 years from manufacturing date', N'Tablet', N'Not recommended for patients with severe liver issues; may cause drowsiness.', N'MM001237')
GO
INSERT INTO MedicineInformation VALUES(N'MI000117', N'Cetirizine', N'Cetirizine Hydrochloride', N'2 years from manufacturing date', N'Tablet', N'May cause drowsiness; use caution when driving or operating machinery.', N'MM001238')
GO
