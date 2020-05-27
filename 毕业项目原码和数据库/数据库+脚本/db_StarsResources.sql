Create database db_StarsResources

--�û���
create table UserInfo(
	UserID int primary key identity(1,1),
	RegistrationTime date default (getdate()) not null,
	IntegralID int Foreign key references Integral(IntID) not null,
	RecordID int Foreign key references Record(RecID),
	ResoucesID int Foreign key references Resouces(ResID),
	LoginName varchar(30) not null,
	LoginPwd varchar(30) not null,
	E_mail varchar(50) ,
	UserName varchar(20),
	UserState int check (UserState=0 or UserState=1) default (0)
)
--���۱�

Create table Comment(
	ComID int primary key identity(1,1),
	CuserID int not null,
	CresID int not null,
	Content varchar(200)
)
--���ֱ�
Create table Integral(
	IntID int primary key identity(1,1),
	IuserID int not null,
	Integrals int default (1)
)
--����
Create table Category(
	CatID int primary key identity(1,1),
	Ccategory varchar(10) not null,
	Clabel varchar(10) not null,
	--CID int Foreign key references Category(ResID),
	CrsoucesID int Foreign key references Resouces(ResID),
	CrecordID int Foreign key references Record(RecID)
)
--drop table Category
--��¼��
Create table Record(
	RecID int primary key identity(1,1),
	RuserID int not null,
	LinkID int Foreign key references Link(LinID),
	Releasetime date default (getdate()) not null,
	Rname varchar(50) not null,
	Rpicture varchar(200),
	Rdescribe varchar(1000) not null,
	Rstatement varchar(200),
	Rdemand int,
	Rstate int check (Rstate=0 or Rstate=1) default (0),
)
--��Դ��
Create table Resouces(
	ResID int primary key identity(1,1),
	RuserID int not null,
	LinkID int Foreign key references Link(LinID),
	Releasetime date not null,
	Rname varchar(50) not null,
	Rpicture varchar(200),
	Rdescribe varchar(1000) not null,
	Rstatement varchar(200),
	Rdemand int,
	Rstate int check (Rstate=0 or Rstate=1) default (0),
)
--Alter table Resouces add column ComID int
--��Դ����
Create table Link(
	LinID int primary key identity(1,1),
	Ldescribe varchar(200) not null,
	Linkline varchar(500) not null,
	Lremarks varchar(100),
	RecID int,
	ResID int
)
--����Ա��
Create table Administrators(
	AdmID int primary key identity(1,1),
	LoginName varchar(30) not null,
	LoginPwd varchar(30) not null,
	Jurisdiction int check (Jurisdiction=0 or Jurisdiction=1 or Jurisdiction=2 or Jurisdiction=3) default (0)
)