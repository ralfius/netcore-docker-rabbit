# docker-compose based deployment

See [kubernetes deployment](../k8s) for k8s version

Sandbox application for .net core microservices in docker with rabbit queue

## To start application:
1. navigate to `~\netcore-docker-rabbit\CoreDockerRabbit\docker` with cmd
2. build and deploy all containers:
```
docker compose up --build --force-recreate -d
```
3. navigate to http://localhost:43001/ in browser
