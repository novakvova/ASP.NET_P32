@echo off

echo Changing directory api...
cd "WebApiTransfer"

echo Building Docker image api...
docker build -t karapus-api .

echo Docker login...
docker login

echo Tagging Docker image api...
docker tag karapus-api:latest novakvova/karapus-api:latest

echo Pushing Docker image api to repository...
docker push novakvova/karapus-api:latest

echo Done ---api---!
pause

