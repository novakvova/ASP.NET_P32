## Begin ASP.NET Core
```
dotnet --info
dotnet new mvc -o MyMvcApp
dotnet run

```


## RUN Ubuntu OS
```
dos2unix docker_karapus.sh
chmod +x docker_karapus.sh
./docker_karapus.sh
```

##nginx options /etc/nginx/sites-available/default
```
server {
    server_name   karapus.itstep.click *.karapus.itstep.click;
    client_max_body_size 200M;
    location / {
       proxy_pass         http://localhost:1984;
       proxy_http_version 1.1;
       proxy_set_header   Upgrade $http_upgrade;
       proxy_set_header   Connection keep-alive;
       proxy_set_header   Host $host;
       proxy_cache_bypass $http_upgrade;
       proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
       proxy_set_header   X-Forwarded-Proto $scheme;
    }

}
```

##nginx restart
```
systemctl status nginx
systemctl restart nginx
certbot
```