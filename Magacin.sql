CREATE DATABASE Magacin
go

use Magacin
go

CREATE TABLE Kategorija
(
	KategorijaId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	NazivKategorije NVARCHAR(50)  NOT NULL,
	OpisKategorije NVARCHAR(100)  NOT NULL
)

CREATE TABLE Proizvod
(
ProizvodId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
KategorijaId INT NOT NULL FOREIGN KEY REFERENCES Kategorija(KategorijaId),
NazivProizvoda NVARCHAR(50)  NOT NULL,
Cena DECIMAL(12,3) NOT NULL,
KolicinaNaLageru INT NOT NULL,
)



INSERT INTO Kategorija VALUES('Laptopovi','Laptopovi razlicitih proizvodjaca')
INSERT INTO Kategorija VALUES('Stampaci','Stampaci razlicitih proizvodjaca')
INSERT INTO Kategorija VALUES('Tableti','Tablet racunari razlicitih proizvodjaca')

INSERT INTO Proizvod VALUES(1,'Laptop Dell Inspiron N4050',30999.25,39)
INSERT INTO Proizvod VALUES(1,'Laptop Asus X55U-SX009D',32990.12,17)
INSERT INTO Proizvod VALUES(1,'Acer Aspire laptop E5-521G-47DX',41989,25)

INSERT INTO Proizvod VALUES(2,'Stampač Laser A4 Lexmark E260',8549.1,13)
INSERT INTO Proizvod VALUES(2,'Canon laserski stampac LBP-6670DN',31989.1,18)
INSERT INTO Proizvod VALUES(2,'Canon stampač imageCLASS LBP6030W',13589.12,11)

INSERT INTO Proizvod VALUES(3,'Acer tablet B1-730HD 8GB',11999.3,12)
INSERT INTO Proizvod VALUES(3,'Asus tablet MeMO Pad 7 ME70C-1A003A',12999.9,14)
INSERT INTO Proizvod VALUES(3,'Goclever tablet ORION 70 L KB',5699.45,25)

