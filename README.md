From the input CSV file, read the data and inserted into the db.
Declared a common connectionstring in appsettings.json to avoid repetition of creating connection strings.
Followed N-Layered architecture using Repository Pattern and Unit Of Work for database operations.
Controllers - with endpoints to read files from csv and implemented the api's for finding the top N products
