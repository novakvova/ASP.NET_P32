#!/bin/bash

set -e

server_up() {
    echo "Server up..."
    docker pull novakvova/karapus-api:latest
    docker stop karapus-api_container || true
    docker rm karapus-api_container || true
    docker run -d --restart=always -v /volumes/karapus-api/images:/app/images --name karapus-api_container -p 1984:8080 novakvova/karapus-api
}

start_containers() {
    echo "Containers start..."
    docker run -d --restart=always -v /volumes/karapus-api/images:/app/images --name karapus-api_container -p 1984:8080 novakvova/karapus-api
}

stop_containers() {
    echo "Containers stop..."
    docker stop karapus-api_container || true
    docker rm karapus-api_container || true
}

restart_containers() {
    echo "Containers restart..."
    docker stop karapus-api_container || true
    docker rm karapus-api_container || true
    docker run -d --restart=always -v /volumes/karapus-api/images:/app/images --name karapus-api_container -p 1984:8080 novakvova/karapus-api
}

echo "Choose action:"
echo "1. Server up"
echo "2. Containers start"
echo "3. Containers stop"
echo "4. Containers restart"
read -p "Enter action number: " action

case $action in
    1)
        server_up
        ;;
    2)
        start_containers
        ;;
    3)
        stop_containers
        ;;
    4)
        restart_containers
        ;;
    *)
        echo "Invalid action number!"
        exit 1
        ;;
esac
