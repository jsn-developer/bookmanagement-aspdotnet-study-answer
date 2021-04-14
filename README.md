# bookmanagement-aspdotnet-study-answer  
bookmanagement-aspdotnet-studyの解答用プロジェクトとなります。  

## アクセス方法

https://localhost:5001/でアクセスするとログイン画面からスタートできます。  
アカウントは、https://localhost:5001/Userでアクセスすることによって確認することができます


## プロジェクトの開始方法

プロジェクトをVisual Studio Code（以降、VS Code）で開きます。

### libmanによるライブラリインストール

js/cssなどのWebフレームワークは、libmanを利用して管理しています。
以下のコマンドを実行することで、必要なライブラリをインターネットからインストールします。

```shell
libman restore
```

libmanがない場合には、以下のコマンドを実行することで利用可能になります。
（devcontainer利用の場合には不要）

```shell
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```

Linuxの場合、以下のコマンドを実行してパスを通すことを忘れずに。

```shell
export PATH="$PATH:/home/vscode/.dotnet/tools"
```

### MySQLの設定を変更  
「appsettings.json」ファイル内にある接続文字列を、それぞれの実行者の環境に合わせ変更してください    

### MySQLで「BookManagement」データベースを作成し、マイグレーションとアップデートを実行

### dotnetコマンドによる起動

```shell
dotnet run
```
