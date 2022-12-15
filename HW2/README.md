# .NET Core、.NET Standard 與 .NET Framework 的差別
微軟命名的混亂人盡皆知，甚至有微軟改名部的戲稱存在。

## .NET Framework
就是為了在 Windows 上開發的框架。

## .NET Core
隨著跨平台的需求增長，.NET Core 也隨之而生，讓開發者不再侷限於 Windows，也可以在 Linux、macOS 或其他的作業系統上執行程式，尤其對於上雲、或是在 DevOps 流程中要打包程式的人來說更是非常方便。

## .NET Framework vs .NET Core
除了跨平台的優勢外，因為 .NET Core 比較新，無論在程式的簡潔性、效能等，也都表現得比 .NET Framework 要好。

對於 web 相關開發者來說，.NET Framework 的程式必須執行在 IIS 上，而 .NET Core 引進了輕量的 Kestrel server，打包起來更快更方便。

大部分情況下 .NET Core 可以被視作 .NET Framework 的替代品。微軟也已經宣布 .NET Framework 4.8 是最後一個版本。

## .NET Standard
雖然開發者都應該逐漸從 .NET Framework 遷移移到 .NET Core，然而總是會有過渡期。很不幸地，這兩個框架並不相容😢。

為了跨框架的開發，微軟從中找出了最基礎的 API，稱之為 .NET Standard，而不同的 .NET 框架也各自會實作這些 API。

如果你開發的套件有跨框架的需求，那就將其設定為 .NET Standard 吧！但反過來說，如果沒有跨框架的需求，優先使用 .NET Core 就好。

## .NET
隨著時代繼續演進，往後的世代應該只有 .NET Core。於是微軟改名部繼續發威，決定乾脆只稱 .NET 就好。.NET Core 3.1 的下一版，也就是 .NET 5 開始 (為避免混淆所以跳過 .NET 4)，就沒有 Core 的後綴了！

除此之外 .NET Standard 也在 .NET 5 之後宣布不再更新，未來的世界不再有 .NET Core、.NET Standard 或 .NET Framework，一律只有 .NET！

以及另一個新的框架 .NET MAUI 😂。