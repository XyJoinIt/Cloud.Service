������Cloud.��Ŀ��.��������

Ǩ�ƣ�
dotnet ef migrations add AddBlogCreatedTimestamp

dotnet ef database update

����consul�� consul agent -dev

������
--ip:ip��ַ
--port���˿�
--agree��Э��
--weight��Ȩ��


Platform:
dotnet run --urls="https://*:7250" --ip="localhost" --port=7250 --agree="https" --weight="1"
dotnet run --urls="https://*:7251" --ip="localhost" --port=7251 --agree="https" --weight="2"
dotnet run --urls="https://*:7252" --ip="localhost" --port=7252 --agree="https" --weight="5"
dotnet run --urls="https://localhost:7250" -e port=7250 agree="https" weight="1"
���أ�
dotnet run --urls="https://localhost:7100" 