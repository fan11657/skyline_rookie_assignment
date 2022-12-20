# gRPC

## gRPC 基本特色
- 是一種基於 HTTP/2 的 RPC 框架。
- 利用 Protocol Buffers 定義 API，格式類似 CSV，可以在傳輸時省去欄位名稱，大幅減少傳輸量及序列化/反序列化的成本。
- 跟一般的 HTTP 只能由客戶端發請求不同，gRPC 在連線建立後可以由服務端推送資料給客戶端。
- 因為比基於 json 的 API 快很多，在許多微服務需要互相溝通的內部系統中，可以大幅減少傳輸成本。

## gRPC 四種模式
1. Unary RPC
   - 跟一般的 API 一樣，一個請求、一個回傳。
2. Server streaming RPC
   - 一個請求，多個回傳。
   - 可以解決過去客戶端需要不斷的輪詢服務端來取得資料的情境。
   - 可以解決需要由服務端推送資料給客戶端地情境。
3. Client streaming RPC
   - 多個請求，一個回傳。
   - 當客戶端需要不斷地傳送資料給服務端的時候。
4. Bidirectional streaming RPC
   - 客戶端與服務端不斷地交換資料。
   - 在互動模式上滿像 WebSocket 的。
	