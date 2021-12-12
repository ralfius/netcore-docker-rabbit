# netcore-kuber-rabbit
Sandbox for .NET Core + RabbitMQ application in k8s

To develop locally - suggest https://minikube.sigs.k8s.io/docs/start/

Steps to run:
1. minikube start
2. minikube dashboard (keep terminal running)
3. if minikube is configured for docker containers on Windows run the following in CMD: 
@FOR /f "tokens=*" %i IN ('minikube -p minikube docker-env') DO @%i
4. otherwise follow https://minikube.sigs.k8s.io/docs/handbook/pushing/#1-pushing-directly-to-the-in-cluster-docker-daemon-docker-env or https://stackoverflow.com/questions/42564058/how-to-use-local-docker-images-with-minikube
5. using the same CMD navigate to CoreKuberRabbit
6. build docker images
docker build -f ./rabbitmq/Dockerfile -t web.rabbit .
docker build -f ./postgresql/Dockerfile -t web.postgre .
docker build -f ./Web.Api/Dockerfile -t web.api .
docker build -f ./Web.Ui/Dockerfile -t web.ui .
7. usin the same CMD run the full application:
kubectl apply -f k8s
Right now web-api has no connection to web-rabbit. Most probably need to register DNS for web-rabbit as web-api is using "web.queue" as hostname
8. to open service in default browser
minikube service web-ui
or
minikube tunnel (see https://minikube.sigs.k8s.io/docs/handbook/accessing/)
get outbound port for service using
kubectl get services web-ui
navigate http://localhost:#EXTERNAL_PORT

Useful commands (see https://kubernetes.io/docs/reference/kubectl/cheatsheet/ and https://kubernetes.io/docs/tutorials/hello-minikube/):
1. to see endpoints
kubectl get services
2. to see the pods (containers)
kubectl get pods
3. to see logs of specific pod 
kubectl logs $POD_NAME
4. to execute comman within pod
kubectl exec $POD_NAME
5. to expose deployment (set of pods)
kubectl expose deployment $DEPLOYMENT_NAME --type=LoadBalancer --name=$SERVICE_NAME
6. get details about service
kubectl describe services/$SERVICE_NAME
7. troubleshoot connectivity between services and pods
https://medium.com/kubernetes-tutorials/kubernetes-dns-for-services-and-pods-664804211501
8. use minikube with local docker images: https://minikube.sigs.k8s.io/docs/handbook/pushing/#1-pushing-directly-to-the-in-cluster-docker-daemon-docker-env
9. more details about pushing images from local docker to minikube docker daemon (see https://minikube.sigs.k8s.io/docs/handbook/pushing/#2-push-images-using-cache-command)