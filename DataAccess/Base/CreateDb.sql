DROP TABLE IF EXISTS Farm;
DROP TABLE IF EXISTS Alpaca;


CREATE TABLE [Farm] ( 
	Id			[int]				NOT NULL, 
	Name		[varchar]	(50)	NOT NULL, 
	Multiplier	[float]			NOT NULL,

	PRIMARY KEY (Id) 
);


CREATE TABLE [Alpaca] ( 
	Id			[int]				NOT NULL, 
	Name		[varchar]	(50)	NOT NULL, 
	Weight		[float]			NOT NULL,
	Color		[varchar]	(50)	NULL,
	FarmId		[int]				NOT NULL,

	PRIMARY KEY (Id) 
);
