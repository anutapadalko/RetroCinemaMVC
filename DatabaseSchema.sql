CREATE TABLE Movies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    ReleaseYear INT,
    Director NVARCHAR(100),
    DurationMinutes INT,
    PosterUrl NVARCHAR(500),
    IsDeleted BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);

CREATE TABLE Genres (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE MovieGenres (
    MovieId INT FOREIGN KEY REFERENCES Movies(Id),
    GenreId INT FOREIGN KEY REFERENCES Genres(Id),
    PRIMARY KEY (MovieId, GenreId)
);

CREATE TABLE MovieAuditLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT FOREIGN KEY REFERENCES Movies(Id),
    ChangeType NVARCHAR(20),
    ColumnName NVARCHAR(50),
    OldValue NVARCHAR(MAX),
    NewValue NVARCHAR(MAX),
    ChangedByUser NVARCHAR(100),
    ChangedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Halls (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Capacity INT NOT NULL
);

CREATE TABLE Sessions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT FOREIGN KEY REFERENCES Movies(Id),
    HallId INT FOREIGN KEY REFERENCES Halls(Id),
    StartTime DATETIME NOT NULL,
    BasePrice DECIMAL(18,2) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME NULL
);

CREATE TABLE Tickets (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SessionId INT FOREIGN KEY REFERENCES Sessions(Id),
    UserId NVARCHAR(450),
    RowNumber INT,
    SeatNumber INT,
    PricePaid DECIMAL(18,2),
    IsActive BIT DEFAULT 1,
    PurchaseDate DATETIME DEFAULT GETDATE()
);