<center> #Wykonanie kwerendy która nic nie zwraca: </center>
```cs
sql = "insert into highscores (name, score) values ('And I', 9001)";
command = new SQLiteCommand(sql, m_dbConnection);
command.ExecuteNonQuery();
```

<center> #Odczyt danych z kwerendy: </center>
```cs
string sql = "select * from highscores order by score desc";
SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
SQLiteDataReader reader = command.ExecuteReader();
while (reader.Read())
       Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
```