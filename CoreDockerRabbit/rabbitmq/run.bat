docker build -t web.rabbit .
docker run --name Web.Queue -d -p 5672:5672 -p 15672:15672 web.rabbit 
pause