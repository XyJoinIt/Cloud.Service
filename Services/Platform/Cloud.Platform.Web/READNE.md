命名：Cloud.项目名.服务类型

迁移：
dotnet ef migrations add AddBlogCreatedTimestamp

dotnet ef database update

启动consul： consul agent -dev

启动：
--ip:ip地址
--port：端口
--agree：协议
--weight：权重


Platform:
dotnet run --urls="https://*:7250" --ip="localhost" --port=7250 --agree="https" --weight="1"
dotnet run --urls="https://*:7251" --ip="localhost" --port=7251 --agree="https" --weight="2"
dotnet run --urls="https://*:7252" --ip="localhost" --port=7252 --agree="https" --weight="5"
dotnet run --urls="https://localhost:7250" -e port=7250 agree="https" weight="1"
网关：
dotnet run --urls="https://localhost:7100" 