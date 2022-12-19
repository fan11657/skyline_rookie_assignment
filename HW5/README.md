# 透過 Docker 打包服務

1. 打包服務
   - 通用
   ```
   $docker build -t restfulapidemo src
   $docker run -d -p 5000:5000 restfulapidemo
   ```
   - Windows
   ```
   run.bat
   ```
   - Linux
   ```
   ./run.sh
   ```

2. 使用服務：<http://localhost:5000/swagger>