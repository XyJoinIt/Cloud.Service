dotnet ef migrations add AddBlogCreatedTimestamp
dotnet ef migrations add AddBlogCreatedTimestamp

dotnet ef database update

consul?? consul agent -dev

--ip
--port
--agree  
--weight


Platform:
dotnet run --urls="https://*:7250" --ip="localhost" --port=7250 --agree="https" --weight="1"
dotnet run --urls="https://*:7251" --ip="localhost" --port=7251 --agree="https" --weight="2"
dotnet run --urls="https://*:7252" --ip="localhost" --port=7252 --agree="https" --weight="5"
dotnet run --urls="https://localhost:7250" -e port=7250 agree="https" weight="1"
?????
dotnet run --urls="https://localhost:7100" 

docker build -t platform:1.0.0 .
docker run -t --name platform_api -p 8001:80 -e agree=http -e ip=10.0.12.6 -e port=8001 -e weight=1  platform:1.0.0