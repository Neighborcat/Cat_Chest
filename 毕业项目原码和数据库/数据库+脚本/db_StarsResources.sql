Create database db_StarsResources

--用户表
create table UserInfo(
	UserID int primary key identity(1,1),--用户ID
	RegistrationTime date default (getdate()) not null,--注册时间
	LoginName varchar(30) not null,--登录账户
	LoginPwd varchar(30) not null,--登录密码
	E_mail varchar(50) ,--电子邮箱
	UserName varchar(20) default ('萌新求带'),--用户名
	UserSex char(2) check(UserSex=0 or UserSex=1 or UserSex=2) default (0),--用户性别
	Userdescribe varchar(500) default ('懒得很~没有！'),--用户介绍
	integral int default (5) ,
	UserState int check (UserState=0 or UserState=1) default (0)--用户状态0、可用，1、封禁
)
select * from UserInfo
insert into UserInfo (LoginName,LoginPwd,E_mail,UserName,UserSex,Userdescribe) values ('2205579785','123123123','xcnw666@126.com','玄不救非，氪不改命','1','山有木兮木有枝，心悦君兮君不知')
insert into UserInfo(LoginName,LoginPwd,E_mail,integral) values --('10086','8888','China_Mobile@139.com'),
('BoyJim','123123','BoyJim@163.com','10'),
('LadyAilin','123123','LadyAilin@163.com','15'),
('OrdKala','123123','123123123@qq.com','20'),
('Yangon','123123','Yangon@139.com','30')
--评论表
Create table Comment(
	ComID int primary key identity(1,1),--评论ID
	UserID int Foreign key references UserInfo(UserID),--发布者
	Content varchar(200)--评论内容
)
alter table Comment add [Time] date default (getdate()) not null
--类别表
Create table Category(
	CategoryID int primary key identity(1,1),
	TabelName varchar(10) not null,--名称
)
select * from Category
insert into Category values('福利资源'),
('手机资源'),
('学习资源'),
('建站资源'),
('PC端资源')
Create table Lable(
	LableID int primary key identity(1,1),
	TabelName varchar(10) not null,--名称
	CategoryID int Foreign key references Category(CategoryID)--所属标签
)
alter table Lable drop column TabelName
alter table Lable add TabelName varchar(50) not null 
select * from Lable
insert into Lable(TabelName,CategoryID) values('开放下载',1),
('免费电影',1),
('安卓',2),
('苹果',2),
('破解软件',2),
('精品课程',3),
('赚钱教程',3),
('网站源码',4),
('Emolg',4),
('Emolg主题',4),
('Wordpress',4),
('Wordpress主题 ',4),
('Linux',5),
('Windows',5),
('破解软件',5),
('编程源码',5),
('游戏',5)

--资源表
Create table Resouces(
	ResoucesID int primary key identity(1,1),
	UserID int Foreign key references UserInfo(UserID),--发布者
	LinkID int Foreign key references Link(LinkID),--所属链接
	PictureID int Foreign key references Picture(PictureID),--所属图片
	CategoryID int Foreign key references Category(CategoryID),--所属类别
	LableID int Foreign key references Lable(LableID),--所属标签
	Releasetime date not null,
	Rname varchar(50) not null,--资源名称
	Rdescribe varchar(1000) not null,--资源描述
	Rdemand int,--下载需求
	Rstate int check (Rstate=0 or Rstate=1 or Rstate=2) default (0),--资源状态0、通过，1、审核中，2、未通过或者封禁
)
select*from Resouces
insert into Resouces(UserID,LinkID,PictureID,CategoryID,LableID,Releasetime,Rname,Rdescribe,Rdemand,Rstate) values
(1,1,1,1,38,getdate(),'变形金刚电影下载','应广大网友要求，收集变形金刚1-5部电影集合开放下载——中英双字，原声放送','0',0)
--资源链接表
Create table Link(
	LinkID int primary key identity(1,1),
	Ldescribe varchar(200) not null,--链接描述
	Linkline varchar(500) not null,--链接地址
	Lremarks varchar(100),--备注
)
select*from Link
insert into Link values('1-5部全系列。变形金刚1080P中英双字。合集！','https://pan.baidu.com/share/init?surl=rir-0ApnxkY84jm_-se0cw','提取码:j9fm')
--资源图片表
Create table Picture(
 PictureID int primary key identity(1,1),
 Picture_a varchar(200) ,--图一
 Picture_b varchar(200) ,--图二
 Picture_c varchar(200) ,--图三
 Picture_d varchar(200) ,--图四
 Picture_e varchar(200) --图五
)
select*from Picture 
insert into Picture values('64d99a3d5eee070089a92d6e554e6208.jpg','a86db145cdab3ab993b5a595f3164450.jpg','51cfd565c04f56db6c2e8d5dd89edf85.jpg','feefcba52a144e2e6987d445a4b31da9.jpg','7b39e76d30a843e91fc7f949c5862e31.jpg')


--管理员表
Create table Administrators(
	AdmID int primary key identity(1,1),--管理员ID
	LoginName varchar(30) not null,--登录账号
	LoginPwd varchar(30) not null,--登录密码
	Jurisdiction int check (Jurisdiction=0 or Jurisdiction=1 or Jurisdiction=2 or Jurisdiction=3) default (0)--权限0
)
insert into Administrators(LoginName,LoginPwd) values('Mr.smith','123123')
insert into Administrators(LoginName,LoginPwd,Jurisdiction) values('Mr.Sam','123123',1),
('LaoWang','123123',2),
('LILI','666666',3)
select * from Administrators
create table [Collection](--收藏表
CollectionID int primary key identity(1,1),

)
create table Recomment(
Recomment int primary key identity(1,1),

)
