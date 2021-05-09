docker build -t web.postgre .
docker run --name Web.DB -dp 5433:5432 web.postgre 
pause