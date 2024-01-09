# CsprojLangVersionProcessor
 A Unity editor extension that automatically overrides the LangVersion property of csproj files and allows the latest C# version to work in the IDE.

[English README is here](README.md)

## 概要

Csproj LangVersion ProcessorはUnityの生成するcsprojファイルの`LangVersion`プロパティを上書きし、C#10.0 (11.0)相当の言語機能をIDEで動作させるためのエディタ拡張です。

現在Unityが公式にサポートするC#バージョンは依然として9.0ですが、Unity2022.2以降では内部で使用されるコンパイラのバージョンがアップデートされており、C#10.0の言語機能をコンパイルすることが可能になっています。また、Unity 2022.3.12f1以降ではC#11.0に相当する言語機能をがコンパイル可能になっています。

そのため、明示的にコンパイラオプションに`-langVersion:preview`を渡すことで、C#9.0より先のバージョンをUnityで動作させることが可能になります。

しかし生成されるcsprojファイルの`LangVersion`は9.0で固定されており、IDE上ではC#9.0以降の機能が使用できずコンパイルエラーが表示されます。Csproj LangVersion Processorは生成されるcsprojファイルの`LangVersion`を自動で書き換えることによって、この問題を解決します。

> [!WARNING]
> ランタイムの更新は行われていないため、全ての言語機能が使用できるわけではありません。また、公式が推奨する方法ではないため動作の保証はないことに留意してください。

## セットアップ

### 要件

* Unity 2022.2 以上 (Unity 2022.3.12f1以上を推奨)

### インストール

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力する

```
https://github.com/AnnulusGames/CsprojLangVersionProcessor.git?path=Assets/CsprojLangVersionProcessor
```

あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記

```json
{
    "dependencies": {
        "com.annulusgames.csproj-langversion-processor": "https://github.com/AnnulusGames/CsprojLangVersionProcessor.git?path=Assets/CsprojLangVersionProcessor"
    }
}
```

## 使用方法

ProjectSettings > Player > Other Settings > Script Compilationを開き、Additional Compiler Argumentsに以下を追加します。(preview以外のオプションを指定することも可能です。Unity 2022.3.12f1以降のバージョンでC#11.0相当の機能を使用する場合はpreviewを指定します。)

```
-langVersion:preview
```

<img src="https://github.com/AnnulusGames/CsprojLangVersionProcessor/blob/main/docs/images/img1.png" width="500">

次にProjectSettings > Editor > Csproj LangVersion Processorを開き、IDEで使用するLangVersionを指定します。

<img src="https://github.com/AnnulusGames/CsprojLangVersionProcessor/blob/main/docs/images/img2.png" width="500">

これでcsファイルの追加時に自動でcsprojが更新されますが、Edit > Preferences > External Tools > Regenerate project filesから手動で更新することも可能です。

<img src="https://github.com/AnnulusGames/CsprojLangVersionProcessor/blob/main/docs/images/img3.png" width="500">

## 制限事項

Csproj LangVersion ProcessorはLangVersionプロパティの上書きを手軽に行うためのライブラリであり、他のプロパティの書き換えや対象のアセンブリの指定などはサポートされていません。(プロジェクトに含まれる全てのcsprojファイルが対象になります。)

より多くの機能が必要な場合は[Cysharp/CsprojModifier](https://github.com/Cysharp/CsprojModifier)を使用することを推奨します。

## ライセンス

[MIT License](LICENSE)