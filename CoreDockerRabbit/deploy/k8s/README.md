# kubernetes-based deployment
See [docker deployment](../docker) for docker-compose version

**To develop locally with minikube - suggest https://minikube.sigs.k8s.io/docs/start/**

## Steps to run with minikube:
1. run minikube
```
minikube start
```
3. in new terminal (keep terminal running)
```
minikube dashboard
```
3. if minikube is configured for docker containers on Windows run the following in new terminal: 
```
@FOR /f "tokens=*" %i IN ('minikube -p minikube docker-env') DO @%i
```
otherwise follow https://minikube.sigs.k8s.io/docs/handbook/pushing/#1-pushing-directly-to-the-in-cluster-docker-daemon-docker-env or https://stackoverflow.com/questions/42564058/how-to-use-local-docker-images-with-minikube
4. using the same CMD navigate to ~\CoreKuberRabbit
5. build docker images (if there is an error pulling rabbit or postgre image - try caching it https://minikube.sigs.k8s.io/docs/handbook/pushing/#2-push-images-using-cache-command):
```
docker build -f ./rabbitmq/Dockerfile -t web.rabbit .
docker build -f ./postgresql/Dockerfile -t web.postgre .
docker build -f ./Web.Api/Dockerfile -t web.api .
docker build -f ./Web.Ui/Dockerfile -t web.ui .
```
6. usin the same CMD deploy the full application:
```
kubectl apply -f k8s
```
7. to open service in default browser
```
minikube service web-ui
```
or
```
minikube tunnel
```
see https://minikube.sigs.k8s.io/docs/handbook/accessing/
get outbound port for service using
```
kubectl get services web-ui
```
navigate http://localhost:#EXTERNAL_PORT

## Useful commands:
1. to see endpoints
```
kubectl get services
```
2. to see the pods (containers)
```
kubectl get pods
```
3. to see logs of specific pod 
```
kubectl logs $POD_NAME
```
4. to execute comman within pod
```
kubectl exec $POD_NAME
```
5. to expose deployment (set of pods)
```
kubectl expose deployment $DEPLOYMENT_NAME --type=LoadBalancer --name=$SERVICE_NAME
```
6. get details about service
```
kubectl describe services/$SERVICE_NAME
```
7. troubleshoot connectivity between services and pods
https://medium.com/kubernetes-tutorials/kubernetes-dns-for-services-and-pods-664804211501
8. use minikube with local docker images: https://minikube.sigs.k8s.io/docs/handbook/pushing/#1-pushing-directly-to-the-in-cluster-docker-daemon-docker-env
9. more details about pushing images from local docker to minikube docker daemon (see https://minikube.sigs.k8s.io/docs/handbook/pushing/#2-push-images-using-cache-command)
10. more on https://kubernetes.io/docs/reference/kubectl/cheatsheet/ and https://kubernetes.io/docs/tutorials/hello-minikube/
