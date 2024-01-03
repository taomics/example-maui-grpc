# .NET MAUI GRPC Example

このリポジトリは以下で作成したプロジェクトを元に作成しました。

```
$ dotnet new maui
$ dotnet new gitignore
```

実装方法については差分を見てください。

https://github.com/taomics/example-maui-grpc/compare/d91f5658772056726c4f8157a5399f84b321390f...master

## アプリの使い方

`Send` ボタンを押下すると GRPC サーバー https://example-go-grpc.gentlepond-fcbb8d44.japaneast.azurecontainerapps.io にリクエストを飛ばします。
最初の起動に時間がかかる場合があるので、最初のリクエストは30秒ほど待ってください。
