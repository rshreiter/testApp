using TestSolution.DataPersistence;

SessionFactory.Init("Host=localhost:5432;Username=postgres;Password=1234;Database=db");

var sess = SessionFactory.OpenSession();