# 什麼是 Restful API？
一種哲學 (無誤)。

更確切的說法，REST 是一種 API 的設計風格，大致有幾點特性：
1. 是建立於 HTTP 之上。
2. 把 API 分成名詞和動詞，利用一致的介面如 URI、HTTP method、HTTP status code 等，來提供簡單但具自我描述性的 API：
   - 名詞：又稱資源，透過 URI 來表示。
   - 動詞：即對資源的操作，通常透過 HTTP method 來表示。
   - 例如：`GET http://www.store.com/product/123` 可表示要取得代號為 123 的商品。
3. 無狀態，亦即服務端不會儲存客戶端的 session 資訊。

正如哲學是沒有硬性規範的，REST 也是，所以時常會有你的 REST 不是我的 REST 的情況。

因為 HTTP method 很少，世界上也不是只有 CRUD，例如"登入"，有時候 REST 把事情過於簡化了，反而造成設計上的侷限。也有時候，REST 的思想跟 HTTP 的設計相悖，例如多條件的搜尋如果達到 URL 的上限，只能把 GET 改用 POST，這時就會跟原本用來新增的 POST API 造成混淆。

API 設計真的好難...。
 


   