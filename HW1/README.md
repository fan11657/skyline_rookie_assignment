# 什麼是 Git Submodule？
Submodule 的中文就是"子"模組，顧名思義，就是在管理repo之間的依賴關係。

當你有一個 repo A，需要使用到另一個 repo B，亦即：
   - `A -> B`

在沒有套件管理工具的情況下，最暴力的做法就是把 B clone 下來複製到 A 裡面，把 B 變成 A 的一部份。顯而易見，不論是上下游對 B 的變更，在這種做法下都難以進行合併。

而 submodule 就是為了解決這個問題的。

## 新增 Submodule
`git submodule add <remote submodule repo>`

例如：
```
$ cd repo_A
$ git submodule add https://github.com/fan11657/repo_B
```
執行指令後可以看見 B 的內容已經被 clone 進 A，並且在 A 的根目錄會多出一個 `.gitmodules` 檔，裡面會記載跟 submodule 有關的資訊。

## 更新 Submodule
更新分成兩種：
   1. 上游進行變更後，要同步到下游的 submodule 中。
   2. 下游的 submodule 進行變更後，要同步到上游中。

#### 1. 上游變更同步到下游
你可以把 submodule 當成一般的 repo，進入其目錄中 pull。

例如：
```
$ cd repo_A/repo_B
$ git pull
```
一旦數量一多，操作起來會很麻煩。可以改成在 main module 執行：

`git submodule update --remote --merge [<local submodule path>...]`

例如：
```
$ cd repo_A
$ git submodule update --remote --merge repo_B
```
注意，如果忘記加 `--merge`，你會帶著 remote master 的狀態切到一個游離的 branch 上。local master 上則保留更新前的狀態。

更新完 submodule 後記得在 main module 中 commit：
```
$ git add .
$ git commit -m "Update submodule B"
```

#### 2. 下游變更同步到上游
同樣地，你可以把 submodule 當成一般的 repo，進入其目錄中直接 commit + push。

例如：
```
$ cd repo_A/repo_B
$ git add .
$ git commit -m "Update"
$ git push
```
或者是連同 main module 一起更新。

例如：
```
$ cd repo_A/repo_B
$ git add .
$ git commit -m "Update"
$ cd ..
$ git add .
$ git commit -m "Update submodule B"
$ git push --recurse-submodules=on-demand
```
加上 `--recurse-submodules=on-demand` 的選項，會依序 push submodule 和 main module。如果你只想 push submodule，可以改成 `--recurse-submodules=only`。注意這邊**push的順序非常重要**，如果已經先 push 了 main module 上去，這個做法就沒有用了，你只能乖乖地進 submodule 的目錄中 push。

## 抓取有 Submodule 的 Repo
上面的內容主要在 main module 已經 clone 下來，且其他部分沒有變更的情況下。如果是第一次 clone，會發現 submodule 的目錄裡面是空的。這時要重新初始化並更新 submodule：

`git submodule update --init [--recursive]`

或是在 clone 的時候就順帶更新：

`git clone --recurse-submodules <remote main module repo>`

## 總結
個人經驗上，submodule 的管理很繁雜，如果有套件管理工具，就用套件管理吧。除了在靜態檔案上的管理，或是一些跨模組重構的需求，用到 submodule 算是某種程度上的抽象洩漏 (leaky abstaction)。
