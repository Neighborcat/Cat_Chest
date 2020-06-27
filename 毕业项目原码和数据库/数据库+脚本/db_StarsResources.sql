Create database db_StarsResources

--�û���
create table UserInfo(
	UserID int primary key identity(1,1),--�û�ID
	RegistrationTime date default (getdate()) not null,--ע��ʱ��
	LoginName varchar(30) not null,--��¼�˻�
	LoginPwd varchar(30) not null,--��¼����
	E_mail varchar(50) ,--��������
	UserName varchar(20) default ('�������'),--�û���
	UserSex char(2) check(UserSex=0 or UserSex=1 or UserSex=2) default (0),--�û��Ա�
	Userdescribe varchar(500) default ('���ú�~û�У�'),--�û�����
	integral int default (5) ,
	UserState int check (UserState=0 or UserState=1) default (0)--�û�״̬0�����ã�1�����
)
select * from UserInfo
insert into UserInfo (LoginName,LoginPwd,E_mail,UserName,UserSex,Userdescribe) values ('2205579785','123123123','xcnw666@126.com','�����ȷǣ�봲�����','1','ɽ��ľ��ľ��֦�����þ������֪')
insert into UserInfo(LoginName,LoginPwd,E_mail,integral) values --('10086','8888','China_Mobile@139.com'),
('BoyJim','123123','BoyJim@163.com','10'),
('LadyAilin','123123','LadyAilin@163.com','15'),
('OrdKala','123123','123123123@qq.com','20'),
('Yangon','123123','Yangon@139.com','30')
--���۱�
Create table Comment(
	ComID int primary key identity(1,1),--����ID
	UserID int Foreign key references UserInfo(UserID),--������
	Content varchar(200)--��������
)
alter table Comment add [Time] date default (getdate()) not null
--����
Create table Category(
	CategoryID int primary key identity(1,1),
	TabelName varchar(10) not null,--����
)
select * from Category
insert into Category values('������Դ'),
('�ֻ���Դ'),
('ѧϰ��Դ'),
('��վ��Դ'),
('PC����Դ')
Create table Lable(
	LableID int primary key identity(1,1),
	TabelName varchar(10) not null,--����
	CategoryID int Foreign key references Category(CategoryID)--������ǩ
)
alter table Lable drop column TabelName
alter table Lable add TabelName varchar(50) not null 
select * from Lable
insert into Lable(TabelName,CategoryID) values('��������',1),
('��ѵ�Ӱ',1),
('��׿',2),
('ƻ��',2),
('�ƽ����',2),
('��Ʒ�γ�',3),
('׬Ǯ�̳�',3),
('��վԴ��',4),
('Emolg',4),
('Emolg����',4),
('Wordpress',4),
('Wordpress���� ',4),
('Linux',5),
('Windows',5),
('�ƽ����',5),
('���Դ��',5),
('��Ϸ',5)

--��Դ��
Create table Resouces(
	ResoucesID int primary key identity(1,1),
	UserID int Foreign key references UserInfo(UserID),--������
	LinkID int Foreign key references Link(LinkID),--��������
	PictureID int Foreign key references Picture(PictureID),--����ͼƬ
	CategoryID int Foreign key references Category(CategoryID),--�������
	LableID int Foreign key references Lable(LableID),--������ǩ
	Releasetime date not null,
	Rname varchar(50) not null,--��Դ����
	Rdescribe varchar(1000) not null,--��Դ����
	Rdemand int,--��������
	Rstate int check (Rstate=0 or Rstate=1 or Rstate=2) default (0),--��Դ״̬0��ͨ����1������У�2��δͨ�����߷��
)
select*from Resouces
insert into Resouces(UserID,LinkID,PictureID,CategoryID,LableID,Releasetime,Rname,Rdescribe,Rdemand,Rstate) values
(1,1,1,1,38,getdate(),'���ν�յ�Ӱ����','Ӧ�������Ҫ���ռ����ν��1-5����Ӱ���Ͽ������ء�����Ӣ˫�֣�ԭ������','0',0)
--��Դ���ӱ�
Create table Link(
	LinkID int primary key identity(1,1),
	Ldescribe varchar(200) not null,--��������
	Linkline varchar(500) not null,--���ӵ�ַ
	Lremarks varchar(100),--��ע
)
select*from Link
insert into Link values('1-5��ȫϵ�С����ν��1080P��Ӣ˫�֡��ϼ���','https://pan.baidu.com/share/init?surl=rir-0ApnxkY84jm_-se0cw','��ȡ��:j9fm')
--��ԴͼƬ��
Create table Picture(
 PictureID int primary key identity(1,1),
 Picture_a varchar(200) ,--ͼһ
 Picture_b varchar(200) ,--ͼ��
 Picture_c varchar(200) ,--ͼ��
 Picture_d varchar(200) ,--ͼ��
 Picture_e varchar(200) --ͼ��
)
select*from Picture 
insert into Picture values('64d99a3d5eee070089a92d6e554e6208.jpg','a86db145cdab3ab993b5a595f3164450.jpg','51cfd565c04f56db6c2e8d5dd89edf85.jpg','feefcba52a144e2e6987d445a4b31da9.jpg','7b39e76d30a843e91fc7f949c5862e31.jpg')


--����Ա��
Create table Administrators(
	AdmID int primary key identity(1,1),--����ԱID
	LoginName varchar(30) not null,--��¼�˺�
	LoginPwd varchar(30) not null,--��¼����
	Jurisdiction int check (Jurisdiction=0 or Jurisdiction=1 or Jurisdiction=2 or Jurisdiction=3) default (0)--Ȩ��0
)
insert into Administrators(LoginName,LoginPwd) values('Mr.smith','123123')
insert into Administrators(LoginName,LoginPwd,Jurisdiction) values('Mr.Sam','123123',1),
('LaoWang','123123',2),
('LILI','666666',3)
select * from Administrators
create table [Collection](--�ղر�
CollectionID int primary key identity(1,1),

)
create table Recomment(
Recomment int primary key identity(1,1),

)
