# PartWire クラス設計初版

## 0. 目的

本書は、DDL 初版をもとに `PartWire` のクラス責務を整理するための初版設計である。

前提:

- UI: WPF
- パターン: MVVM
- アーキテクチャ: レイヤード構成
- ORM: EF Core

## 1. レイヤ責務

### 1.1 PartWire.Domain

役割:

- 業務ルールの中心
- 状態遷移
- 値の整合性保証
- 集約単位の操作

主な型:

- Entity
- Value Object
- Enum
- Domain Service
- Domain Exception

### 1.2 PartWire.Application

役割:

- ユースケース実行
- 入出力 DTO
- トランザクション境界
- 認可判定呼び出し
- 画面から見た操作単位の調停

主な型:

- Application Service
- Command / Query
- Result DTO
- Validation
- Interface

### 1.3 PartWire.Infrastructure

役割:

- EF Core 実装
- DB マッピング
- Repository / Query 実装
- 認証、通知、添付、時刻、採番など外部依存

主な型:

- DbContext
- EntityTypeConfiguration
- Repository 実装
- Query Service 実装
- Authentication Provider
- File Storage Service
- Numbering Service

### 1.4 PartWire.Desktop

役割:

- 画面表示
- 画面遷移
- ViewModel
- 入力状態管理
- ダイアログ

主な型:

- View
- ViewModel
- Navigation Service
- Dialog Service
- Converter

## 2. 集約候補

初期段階では次を主要集約として扱う。

### 2.1 Project Aggregate

中心:

- `Project`

配下:

- `ProjectItem`

責務:

- 案件作成
- 明細追加
- 重複部品防止
- 案件状態再計算

### 2.2 Quotation Aggregate

中心:

- `Quotation`

配下:

- `QuotationLine`
- `PurchaseApproval`

責務:

- 見積登録
- 改版管理
- 承認依頼状態管理
- 採用状態管理

### 2.3 Order Aggregate

中心:

- `Order`

配下:

- `OrderLine`

責務:

- 発注登録
- 発注キャンセル
- 残納数量管理

### 2.4 Delivery Aggregate

中心:

- `Delivery`

配下:

- `DeliveryLine`

責務:

- 納品書登録
- 納品確認
- 数量差異記録

### 2.5 Invoice Aggregate

中心:

- `Invoice`

配下:

- `InvoiceTarget`

責務:

- 請求登録
- 納品紐づけ
- 未請求数量更新

## 3. Domain の主要クラス案

### 3.1 共通

- `AuditableEntity`
- `AggregateRoot`
- `EntityId<T>`

### 3.2 マスタ系

- `ConstructionProject`
- `Department`
- `User`
- `ApprovalRank`
- `AmountRank`
- `BusinessPartner`
- `Manufacturer`
- `PurchasePart`
- `ManufacturedPart`
- `Material`
- `Shape`
- `ProcessingMethod`
- `Size`
- `SurfaceTreatment`
- `ApprovalRouteDefinition`
- `SystemSetting`

### 3.3 業務系

- `Project`
- `ProjectItem`
- `QuoteRequest`
- `Quotation`
- `QuotationLine`
- `PurchaseApproval`
- `Order`
- `OrderLine`
- `Delivery`
- `DeliveryLine`
- `Invoice`
- `InvoiceTarget`
- `DocumentForwarding`
- `AttachmentLink`
- `Notification`
- `NotificationRecipient`
- `PriceHistory`

### 3.4 Value Object 候補

- `ProjectNumber`
- `OrderNumber`
- `Money`
- `Quantity`
- `DateRange`
- `InvoiceTargetMonth`
- `UserIdentity`
- `AttachmentLocation`

### 3.5 Enum 候補

- `ItemType`
- `AuthType`
- `ApprovalStatus`
- `ProjectStatus`
- `OrderStatus`
- `DeliveryStatus`
- `InvoiceStatus`
- `DocumentType`
- `AttachmentTargetType`
- `RoleType`

## 4. Application の主要ユースケース案

### 4.1 認証

- `LoginUseCase`
- `ResolveCurrentUserUseCase`

### 4.2 案件

- `CreateProjectUseCase`
- `UpdateProjectUseCase`
- `AddProjectItemUseCase`
- `SearchProjectsUseCase`
- `GetProjectDetailUseCase`

### 4.3 見積

- `RegisterQuoteRequestUseCase`
- `RegisterQuotationUseCase`
- `ReviseQuotationUseCase`
- `SubmitQuotationApprovalUseCase`
- `ApproveQuotationUseCase`
- `RejectQuotationUseCase`

### 4.4 発注

- `CreateOrderUseCase`
- `CancelOrderUseCase`

### 4.5 納品

- `RegisterDeliveryUseCase`
- `ConfirmDeliveryUseCase`
- `SearchDeliveryConfirmTargetsUseCase`

### 4.6 請求

- `RegisterInvoiceUseCase`
- `SearchInvoiceTargetsUseCase`

### 4.7 回付、添付、通知

- `RegisterDocumentForwardingUseCase`
- `RegisterAttachmentLinkUseCase`
- `ListNotificationsUseCase`
- `MarkNotificationAsReadUseCase`

### 4.8 マスタ、CSV

- `ImportCsvUseCase`
- `ExportCsvUseCase`
- `ManageMasterUseCase`

## 5. Infrastructure の実装方針

### 5.1 DbContext

- `PartWireDbContext`

含める `DbSet`:

- マスタ系一式
- 業務系一式
- 監査ログ

### 5.2 Repository 方針

更新主体の集約のみ Repository を置く。

候補:

- `IProjectRepository`
- `IQuotationRepository`
- `IOrderRepository`
- `IDeliveryRepository`
- `IInvoiceRepository`

### 5.3 Query Service 方針

一覧、検索、集計は Query Service に分離する。

候補:

- `IProjectQueryService`
- `IDashboardQueryService`
- `IDeliveryQueryService`
- `IInvoiceQueryService`
- `IAggregationQueryService`

### 5.4 横断サービス

- `INumberSequenceService`
- `IClock`
- `IUserContext`
- `IAuthenticationService`
- `IAuthorizationService`
- `IAttachmentStorageService`
- `INotificationService`
- `IAuditLogWriter`

## 6. Desktop の ViewModel 初期案

### 6.1 Shell

- `ShellViewModel`
- `NavigationMenuViewModel`
- `StatusBarViewModel`

### 6.2 認証

- `LoginViewModel`

### 6.3 ダッシュボード

- `DashboardViewModel`

### 6.4 案件

- `ProjectListViewModel`
- `ProjectDetailViewModel`
- `ProjectEditViewModel`
- `ProjectItemEditViewModel`

### 6.5 見積、承認、発注

- `QuotationEditViewModel`
- `QuotationRevisionViewModel`
- `ApprovalInboxViewModel`
- `ApprovalDetailViewModel`
- `OrderEditViewModel`

### 6.6 納品、請求

- `DeliveryRegisterViewModel`
- `DeliveryConfirmationViewModel`
- `InvoiceRegisterViewModel`

### 6.7 共通、マスタ

- `MasterListViewModel`
- `CsvImportViewModel`
- `SettingsViewModel`
- `NotificationPanelViewModel`

## 7. 状態遷移ロジックの置き場所

次のロジックは Domain 側へ置く。

- 案件状態再計算
- 見積改版の可否判定
- 発注可否判定
- 納品確認完了判定
- 請求完了判定

次のロジックは Application 側へ置く。

- 画面入力の整形
- 複数集約にまたがる更新順序
- 承認依頼実行フロー
- CSV 取込フロー

## 8. 初期実装で避けたいこと

- ViewModel に業務判定を直接書くこと
- すべての一覧取得を Repository に寄せること
- EF Core Entity を画面にそのまま渡すこと
- 監査ログや採番を UI 層で呼ぶこと

## 9. 現時点の提案

初期実装では、厳密な DDD に寄せすぎず、次の方針で進めるのがよい。

- Domain は主要業務ルールに集中する
- Application はユースケース単位で整理する
- Infrastructure は EF Core と外部接続を閉じ込める
- Desktop は MVVM に徹し、画面責務に限定する
