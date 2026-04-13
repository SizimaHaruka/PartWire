# PartWire 設計決定ログ兼チェックリスト

## 1. 目的

本書は、`PartWire` の未決事項を一元管理し、設計上の決定事項とその根拠、残課題、関連文書への反映状況を継続的に記録するための一次ソースである。

運用ルール:

- 未決事項は本書で管理する
- 各論点は Markdown チェックボックスで管理する
- 論点チェックは方針確定時に更新する
- `反映済み` は関連文書への反映が完了した時点で更新する
- 既存の `requirements.md`、`basic-design.md`、`technical-requirements.md` は本書を反映した成果文書として扱う
- 文書反映は論点単位ではなく、領域単位でまとめて実施する

## 2. 記入ルール

各論点は以下の固定フォーマットで記録する。

- [ ] 論点名
  - 現在の候補/前提:
  - 回答:
  - 根拠:
  - 保留事項:
  - 反映先:
  - 反映済み: [ ] requirements [ ] basic-design [ ] technical-requirements

補足:

- `回答` は未決時は `未決` と記載し、確定後に結論へ更新する
- `根拠` は採用理由、運用上の前提、関連する要件を簡潔に記載する
- `保留事項` は後続で再検討が必要な派生論点のみを記載する
- `反映先` は反映対象となる文書名を列挙する
- 技術判断を含まない論点では `technical-requirements` を反映対象に含めない

## 3. 承認・発注

- [x] 承認段階数と承認単位
  - 現在の候補/前提: 承認は見積書単位を基本とし、単段階承認か多段階承認かは未決
  - 回答: 承認は見積書単位。承認段階は部署と金額をパラメータとした承認ルール設定が出来る機能を追加し、多段階承認可能にする。差し戻し時は、差し戻し者へ何もせずとも再度承認依頼をかけられるものとする。
  - 根拠: 承認状態、権限、申請画面、承認ルート定義の構造に直結する
  - 保留事項: 差戻し時の再申請ルール
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 発注単位と一部明細発注可否
  - 現在の候補/前提: 発注は見積書単位が基本案だが、一部明細のみ先行発注を許すかは未決
  - 回答: 見積単位以外の注文をせず、先行発注を許さない。
  - 根拠: 発注明細、残納管理、案件状態、画面導線に影響する
  - 保留事項: 追加発注時の履歴表現
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 見積改版時の旧版承認と旧版発注の失効ルール
  - 現在の候補/前提: 旧版はキャンセル扱いとする方向だが、承認失効の範囲と発注済データの扱いは未決
  - 回答: 改版は納品前まで許可する。承認は見積書単位のため旧版では失効し、新版で再承認する。発注後に改版する場合は旧版の納品予定日を無効化し、新しい見積書を登録する。旧版に紐づくデータは履歴として残すが、現行進行対象からは外す。納品後は改版ではなく別見積または追加発注で処理する。
  - 根拠: 改版時の整合性、履歴保持、価格履歴反映ルールに影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 案件個別の承認ルート差し替え範囲
  - 現在の候補/前提: 部署と金額ランクで承認ルートを決めるが、個別上書きの許容範囲は未決
  - 回答: 個別上書きの許容はしない。
  - 根拠: 統制と運用柔軟性のバランスを決める論点であり、監査要件にも影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 金額ランク境界と承認者ランク適用ルール
  - 現在の候補/前提: 部署と金額ランクに応じて承認ルートを決定する方針だが、境界値とランク適用規則は未決
  - 回答: 承認ルール設定にてユーザー設定する。変更時は以降の案件について適用
  - 根拠: 承認ルート定義、利用者マスタ、権限制御に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 発注番号の採番方式
  - 現在の候補/前提: 発注番号を持つ前提だが、採番単位、文字種、年度リセット有無は未決
  - 回答: プレフィックス”O”+10桁の数値、年度リセットなし通番
  - 根拠: 外部連携、帳票、検索性に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 発注キャンセル記録の粒度
  - 現在の候補/前提: キャンセル日と理由は保持する案があるが、発注全体か明細単位かは未決
  - 回答: 見積単位でのキャンセルとする。発注キャンセル全般について通常画面では非表示とし、履歴としてのみ参照可能にする。案件自体を却下する場合も同様に、途中の納品確認や納品書が存在しても有効案件としては表示しない。
  - 根拠: 改版対応、履歴追跡、残納数量の整合性に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

## 4. 納品・請求

- [x] 納品確認の利用デバイス前提
  - 現在の候補/前提: PC 前提か、タブレット運用も想定するかが未決
  - 回答: PC前提。タブレット運用は発展でWEB版を検討する。
  - 根拠: 画面設計、入力方式、レイアウト優先順位に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] バーコード/QR 活用有無
  - 現在の候補/前提: 納品確認でコード読み取りを使うかは未決
  - 回答: モジュール設計を行い、オプションとして設計する。
  - 根拠: ハードウェア要件と画面導線に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 納品確認の処理単位
  - 現在の候補/前提: 納品書ごとに 1 件ずつ確認する方向だが、案件横断の一括確認可否は未決
  - 回答: 一括確認はしない。
  - 根拠: 操作性と監査性の両方に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 請求書登録対象の判定ルール
  - 現在の候補/前提: 取引先マスタに請求書管理要否を持つ案があるが、手動補正の範囲は未決
  - 回答: 取引先マスタの `請求書管理要否` が有効な取引先のみを請求書登録対象とする。
  - 根拠: 請求登録対象、一覧抽出、マスタ設計に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 請求完了判定の基準
  - 現在の候補/前提: 見積書単位で完了判定する方向だが、数量基準か金額基準かは未決
  - 回答: 数量基準だが、見積額に対して金額が異なる場合は通知が必要。
  - 根拠: 状態遷移、集計、請求残管理に影響する
  - 保留事項: 端数や値引き発生時の扱いを別途詳細化する
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 1 請求書に紐づける納品範囲
  - 現在の候補/前提: 複数納品明細の集約可否が未決
  - 回答: 1 請求書に紐づけられる納品は、同一見積書に属する納品のみとする。
  - 根拠: 請求対象紐づけテーブル、画面入力補助、回付運用に影響する
  - 保留事項: 月またぎ分納時の画面表示方法
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

## 5. 案件・明細・採番

- [x] 案件主軸か明細主軸か
  - 現在の候補/前提: 案件中心で時系列管理する案があるが、実際の画面・状態設計でどちらを主軸にするかは未決
  - 回答: 案件主軸だが、承認画面や納品確認画面は特化画面としたい。
  - 根拠: 一覧設計、状態遷移、集計、操作導線に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 同一案件内の同一部品重複の扱い
  - 現在の候補/前提: 運用上は重複行登録しない前提だが、システム制約にするかは未決
  - 回答: 案件明細では同一案件内の同一部品重複を禁止するシステム制約とする。見積以降は、同一見積内でも数量違いや価格違いがあり得るため重複を許可する。
  - 根拠: 明細入力の自由度と集計整合性のバランスに関わる
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 案件番号の表示形式
  - 現在の候補/前提: 短い一意識別子を自動採番する方針だが、桁数、文字種、年度リセット有無は未決
  - 回答: Q+10桁
  - 根拠: 利用者入力性と将来変更影響を抑えるため
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 状態自動判定と手動補正範囲
  - 現在の候補/前提: 明細進捗を積み上げて状態を表す案があるが、手動補正の可否は未決
  - 回答: 手動補正は行わない。`見積依頼中` と `見積取得中` は分けず `見積取得中` に統合する。`納品進行中` と `納品確認進行中` は分ける。`完了` は、発注対象の全明細で必要な納品確認が完了し、請求書管理要否が有効な取引先については請求完了し、回付対象書類がすべて回付済みになった時点とする。分納中は全数量の納品確認が終わるまで完了にしない。`回付済` は回付対象書類がすべて回付された時点で遷移する。`中止` は案件を進めなくなった場合の救済状態として、手動却下に加え、発注対象の見積がすべてキャンセルまたは失効した場合、または一部発注後でも残りを進めないと判断した場合に遷移可能とする。
  - 根拠: 運用負荷、誤判定時の救済、監査要件に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

## 6. 取引先・製作部品

- [x] 製作部品拡張属性の方式
  - 現在の候補/前提: 汎用属性方式か固定カラム追加方式かは未決
  - 回答: 性能維持のため、固定カラム追加方式
  - 根拠: 将来拡張性、検索性、CSV 取込性に直結する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 加工メーカー候補絞り込みの厳密さ
  - 現在の候補/前提: 補助的絞り込みとする方針だが、条件不一致候補の表示ルールは未決
  - 回答: 条件不一致は何も表示しない
  - 根拠: 候補提示の使い勝手とマスタ保守負荷に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 取引先の請求・役割例外設定
  - 現在の候補/前提: 1 取引先に複数役割を持てるが、個別案件での例外設定可否は未決
  - 回答: 個別案件では例外なし
  - 根拠: 見積先候補と請求対象判定の柔軟性に関わる
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

## 7. 権限・認証

- [x] 閲覧範囲の基本単位
  - 現在の候補/前提: 自案件のみか、部署内まで広げるかが未決
  - 回答: ロールにて設定可能とする。
  - 根拠: 役割設計、一覧表示、検索結果制御に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 金額閲覧権限の制御粒度
  - 現在の候補/前提: 役割で制御する案があるが、画面単位か項目単位かは未決
  - 回答: 金額閲覧権限機能は設けない。
  - 根拠: 権限制御方式と UI 設計に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 権限判定の主ソース
  - 現在の候補/前提: 独自ユーザー情報か AD グループ参照か、どちらを主にするかは未決
  - 回答: 通常時は独自ユーザー情報と AD グループ参照を併用して権限判定する。AD グループ参照が失敗した場合は、独自ユーザー情報のみで継続運用する。
  - 根拠: 権限変更運用、障害時運用、認証切替時の継続性に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 認証方式切替時の利用者ひも付けキー
  - 現在の候補/前提: ログイン ID または AD 連携キーを利用する案があるが、正式な識別子は未決
  - 回答: ADのUPNが連携キー
  - 根拠: 利用者継続性、監査ログの一貫性に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

## 8. 添付・CSV・監査・DB基盤

- [x] 添付ファイル機能の初期対象範囲
  - 現在の候補/前提: 見積書、納品書、請求書の添付を想定するが、v1 でどこまで実装するかは未決
  - 回答: すべて実装
  - 根拠: スコープと保存先設計に影響する
  - 保留事項: リンク切れ検知方式を別途詳細化する
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 添付保存先と制約
  - 現在の候補/前提: 標準は共有フォルダ案だが、SharePoint 等を対象に含めるか、容量上限を設けるかは未決
  - 回答: SharePoint と共有フォルダを対象に含める。容量上限を設け、上限を超えた場合は通知し、添付追加のみ停止する。
  - 根拠: 技術構成、運用負荷、障害時運用に影響する
  - 保留事項: 保存先切替時の既存リンク移行
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] CSV 取込の更新ポリシー
  - 現在の候補/前提: 新規、更新、スキップを選択可能とする案があるが、上書きや無効化まで含むかは未決
  - 回答: UserKeyを設けて上書き及び無効化可能とする。取り込みはv1に含める
  - 根拠: データ移行運用、エラー処理、監査に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] CSV 取込エラー時のロールバック粒度
  - 現在の候補/前提: ロールバック範囲は未決
  - 回答: 全件ロールバック
  - 根拠: 利用者体験とデータ整合性のバランスに関わる
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 監査ログの保持粒度と保持期間
  - 現在の候補/前提: 主要テーブルの CRUD を対象とする案があるが、保持期間と詳細粒度は未決
  - 回答: 監査ログは標準粒度とし、変更前後の主要項目を記録する。保持期間は標準 5 年とし、設定で変更可能にする。
  - 根拠: 容量、性能、監査性に影響する
  - 保留事項: 閲覧権限と運用手順書への反映
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 楽観ロック方式
  - 現在の候補/前提: 更新トークンまたは更新日時を持つ案があるが、正式方式は未決
  - 回答: 更新日時方式とする。ミリ秒単位での衝突は許容
  - 根拠: SQLite と SQL Server の差分吸収と実装方針に影響する
  - 保留事項: 競合解消 UI の標準動作
  - 反映先: basic-design, technical-requirements
  - 反映済み: [ ] requirements [x] basic-design [x] technical-requirements

- [x] SQLite から SQL Server への移行方式
  - 現在の候補/前提: 移行可能とする方針だが、マイグレーション方式と責務分界は未決
  - 回答: 専用移行ツール方式とする。アプリ本体とは分離した管理者向けの移行ツールまたはコマンドで、SQLite から SQL Server へデータ移行を行う。
  - 根拠: 初期導入計画と将来運用に直結する
  - 保留事項: 添付ファイルリンクの移行
  - 反映先: basic-design, technical-requirements
  - 反映済み: [ ] requirements [x] basic-design [x] technical-requirements

## 9. 通知・ダッシュボード

- [x] 通知機能の v1 対象可否
  - 現在の候補/前提: 見積回答遅延、納期超過、未回付の通知要否は未決
  - 回答: 見積回答遅延、納期超過、未回付は通知必要
  - 根拠: スコープ、画面設計、運用期待値に影響する
  - 保留事項: 通知条件の設定化
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 通知チャネル
  - 現在の候補/前提: メール通知かアプリ内通知かは未決
  - 回答: メール通知とアプリ内通知の両方を提供する。
  - 根拠: 技術構成と利用者導線に影響する
  - 保留事項: 通知履歴の保持要否
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] ダッシュボード指標の固定化範囲
  - 現在の候補/前提: 未見積回答、承認待ち、納期超過などの候補があるが、固定表示か利用者別かは未決
  - 回答: 案件のどこかに名前がある利用者に表示、その他の案件が見たい場合、未承認一覧検索や未終了案件一覧検索などできるように。
  - 根拠: トップ画面設計と権限制御に影響する
  - 保留事項: 担当者別の優先表示ルール
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

## 10. 継続論点

- [ ] 差戻し時の再申請 UI と入力保持ルール
  - 現在の候補/前提: 差戻し後は同一見積書に対して再申請できる方針まで決定済み
  - 回答:
  - 根拠: 承認差戻し後の操作性と入力や添付の再利用範囲を実装前に固定したい
  - 保留事項:
  - 反映先: basic-design
  - 反映済み: [ ] requirements [ ] basic-design [ ] technical-requirements

- [x] 追加発注時の履歴表現
  - 現在の候補/前提: 発注は見積書単位固定だが、追加発注の見せ方は未整理
  - 回答: 1案件に対し複数見積もりがあるのと同様の見せ方とする。
  - 根拠: 発注一覧、履歴表示、集計の単位に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 請求金額の端数・値引きの扱い
  - 現在の候補/前提: 請求完了判定は数量基準、金額差異は通知対象まで決定済み
  - 回答: 値引き額登録、雑費、管理費、輸送費などが自由記述で登録可能とする。
  - 根拠: 完了判定と差異通知の閾値、画面表示ルールに影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 月またぎ分納時の請求画面表示ルール
  - 現在の候補/前提: 1請求書は同一見積書内の納品に限定する方針まで決定済み
  - 回答: 特別に画面を作りはせずに、案件に対する請求という形を守る。
  - 根拠: 請求書登録画面の一覧表示と選択導線に影響する
  - 保留事項: なし
  - 反映先: basic-design
  - 反映済み: [ ] requirements [x] basic-design [ ] technical-requirements

- [x] 添付リンク切れ検知方式
  - 現在の候補/前提: 添付は共有フォルダまたは SharePoint、容量超過時は添付追加のみ停止まで決定済み
  - 回答: 添付が表示される画面にてリンク切れがあれば表示する。
  - 根拠: 画面表示、定期チェック、運用アラートの要否に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 添付保存先切替時の既存リンク移行方式
  - 現在の候補/前提: 保存先は共有フォルダと SharePoint を対象にし、切替可能とする方針まで決定済み
  - 回答: 既存リンクは移行しない。
  - 根拠: 移行ツール、運用手順、障害復旧方針に影響する
  - 保留事項: なし
  - 反映先: basic-design, technical-requirements
  - 反映済み: [ ] requirements [x] basic-design [x] technical-requirements

- [x] 監査ログの閲覧権限と運用手順書反映
  - 現在の候補/前提: 監査ログの粒度と保持期間は決定済み
  - 回答: 監査ログの閲覧権限はシステム管理者のみとする。
  - 根拠: セキュリティ、監査性、運用手順に影響する
  - 保留事項: 運用手順書への記載粒度
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [x] technical-requirements

- [x] 楽観ロック競合時の UI 標準動作
  - 現在の候補/前提: 更新日時方式は決定済み
  - 回答: 競合ありと表示し画面更新し再度入力を促す。
  - 根拠: 利用者体験と再入力負荷に影響する
  - 保留事項: なし
  - 反映先: basic-design, technical-requirements
  - 反映済み: [ ] requirements [x] basic-design [x] technical-requirements

- [x] 移行ツールでの添付ファイルリンク移行方式
  - 現在の候補/前提: SQLite から SQL Server への移行は専用ツール方式まで決定済み
  - 回答: ファイルリンクは何も変えずにそのまま移行する。
  - 根拠: DB移行とファイル保存先移行の責務分界を明確にしたい
  - 保留事項: なし
  - 反映先: basic-design, technical-requirements
  - 反映済み: [ ] requirements [x] basic-design [x] technical-requirements

- [x] 通知条件の設定化
  - 現在の候補/前提: 見積回答遅延、納期超過、未回付は通知対象、メール通知とアプリ内通知の両方提供まで決定済み
  - 回答: 案件関連者全員に通知し、何を通知するかは項目ごとに設定できるようにする。
  - 根拠: 通知の閾値や ON/OFF の管理方法に影響する
  - 保留事項: なし
  - 反映先: requirements, basic-design, technical-requirements
  - 反映済み: [x] requirements [x] basic-design [ ] technical-requirements

- [x] 通知履歴と既読管理の保持ルール
  - 現在の候補/前提: ER に通知・通知宛先テーブルを追加済み
  - 回答: 標準保持期間1年で設定可能とする。
  - 根拠: 通知テーブルの保持期間、既読表示、再通知制御に影響する
  - 保留事項: なし
  - 反映先: basic-design, technical-requirements
  - 反映済み: [ ] requirements [x] basic-design [ ] technical-requirements

- [ ] 担当者別ダッシュボード優先表示ルール
  - 現在の候補/前提: 案件に名前がある利用者向け表示と横断一覧導線までは決定済み
  - 回答: 実装後調整する。
  - 根拠: ダッシュボードの並び順、件数集計、注意案件抽出に影響する
  - 保留事項: 優先順位ロジックは未決
  - 反映先: requirements, basic-design
  - 反映済み: [ ] requirements [ ] basic-design [ ] technical-requirements
