# PartWire EF Core 初期方針

## 0. 目的

本書は、`PartWire` における EF Core の初期採用方針を整理する。

## 1. 前提

- DB は SQLite と SQL Server の両対応を目指す
- 初期スキーマは SQL Server 基準で整理する
- 実行時のプロバイダ切り替えは設定で行う

## 2. 基本方針

- EF Core を更新系の標準手段とする
- 一覧や重い集計は Query Service に分離する
- スキーマ管理は Migration を基本とする
- `DbContext` は 1 つから開始する

## 3. DbContext 方針

初期名:

- `PartWireDbContext`

責務:

- エンティティの永続化
- 参照整合性定義
- 共通列の設定
- 楽観ロック列の設定

含めないもの:

- 画面専用 DTO
- 複雑な一覧検索 SQL の責務

## 4. Entity 設定方針

- Fluent API を基本とする
- `IEntityTypeConfiguration<T>` をテーブルごとに分ける
- Data Annotation は最小限にする

理由:

- SQL Server と SQLite 差分を設定クラス側で吸収しやすい
- Entity を業務モデルとして読みやすく保ちやすい

## 5. Migration 方針

- 初期 Migration は SQL Server 基準で作成する
- SQLite 実行時はプロバイダ差分を吸収できる範囲で同一モデルを利用する
- SQLite から SQL Server への移行は別ツールとする

## 6. 型方針

- 金額: `decimal(18,2)`
- 数量: `decimal(18,4)`
- 日時: `datetime2` 相当
- 日付: `date`
- 真偽値: `bit` / SQLite では整数変換
- 列挙値: 文字列保存を基本とする

## 7. 共通カラム方針

主要テーブルには次を持たせる。

- `created_at`
- `updated_at`
- `created_by`
- `updated_by`
- `row_version` または更新時刻ベースの競合管理列
- `is_active` または無効化フラグ

補足:

- DBML では未反映の共通列があるため、実装前に DDL と同期する

## 8. Repository と Query の切り分け

### 8.1 Repository

対象:

- 集約更新
- 単票取得

### 8.2 Query

対象:

- 一覧検索
- ダッシュボード集計
- 工事番号別集計
- 納品確認待ち一覧

## 9. 初期作業

1. `PartWireDbContext` を作る
2. 主要 EntityTypeConfiguration を配置する
3. 初期 Migration を作る
4. SQLite / SQL Server の接続切替を設定化する
5. サンプルデータ投入方針を決める
