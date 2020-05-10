DROP TABLE IF EXISTS [UsersFantasyTeam];
DROP TABLE IF EXISTS [UsersInTournament];
DROP TABLE IF EXISTS [TeamsInTournament];
DROP TABLE IF EXISTS [UsersFantasyTeamInTournament];
DROP TABLE IF EXISTS [FantasyTournament];
DROP TABLE IF EXISTS [UsersIcon];
DROP TABLE IF EXISTS [User];
DROP TABLE IF EXISTS [Player];
DROP TABLE IF EXISTS [Team];
DROP TABLE IF EXISTS [Icon];

Create table [User](
	Id integer IDENTITY(1,1),
	Username varchar(255) unique not null,
	[Password] varchar(255) not null,
	FirstName varchar (255) default '-',
	SecondName varchar (255) default '-',
	TeamName varchar(255) default 'MyTeam',
	Icon varchar(255) default 'default',
	Coins int not null default 100,
	Admin bit not null default 0,
	TFA bit not null default 0,
	PRIMARY KEY(Id)
)

Create table [Player](
	Id varchar(255) not null,
	TeamId varchar(255) not null,
	[Role] varchar(255) not null,
	FirstName varchar (255) not null,
	SecondName varchar (255) not null,
	Username varchar (255) unique not null,
	[Image] varchar (255) not null,
	Logo varchar (255) not null,
	PRIMARY KEY(Id)
)

Create table [Team](
	Id varchar(255) not null,
	Image varchar(255) not null,
	Name varchar(255) not null,
	Slug varchar(255) not null,
	HomeLeague  varchar(255) not null,
	PRIMARY KEY(Id)
)

Create table [Icon](
	Id integer IDENTITY(1,1),
	Name varchar(255) not null,
	Price varchar(255) not null,
	PRIMARY KEY(Id)
)

Create table [UsersIcon](
	Id integer IDENTITY(1,1),
	IconId int FOREIGN KEY REFERENCES [Icon](Id) not null,
	UserId int FOREIGN KEY REFERENCES [User](Id) not null,
	PRIMARY KEY(Id)
)

Create table [UsersFantasyTeam](
	Id integer IDENTITY(1,1),
	[Role] varchar(255) not null,
	RolePriority int not null,
	PlayerId varchar(255) FOREIGN KEY REFERENCES [Player](Id) not null,
	UserId int FOREIGN KEY REFERENCES [User](Id) not null,
	PRIMARY KEY(Id)
)

Create table [FantasyTournament](
	Id integer IDENTITY(1,1),
	[Name] varchar(255) not null,
	StartTime date not null,
	EndTime date not null,
	PRIMARY KEY(Id)
)

Create table [UsersInTournament](
	Id integer IDENTITY(1,1),
	Points integer not null default 0,
	TournamentId int FOREIGN KEY REFERENCES [FantasyTournament](Id) not null,
	UserId int FOREIGN KEY REFERENCES [User](Id) not null,
	PRIMARY KEY(Id)
)

Create table [TeamsInTournament](
	Id integer IDENTITY(1,1),
	TeamId varchar(255)  FOREIGN KEY REFERENCES [Team](Id) not null,
	TournamentId int FOREIGN KEY REFERENCES [FantasyTournament](Id) not null,
	PRIMARY KEY(Id)
)

Create table [UsersFantasyTeamInTournament](
	Id integer IDENTITY(1,1),
	PlayerId varchar(255) FOREIGN KEY REFERENCES [Player](Id) not null,
	UserId int FOREIGN KEY REFERENCES [User](Id) not null,
	TournamentId int FOREIGN KEY REFERENCES [FantasyTournament](Id) not null,
	PRIMARY KEY(Id)
)
