# Coding Dojo

## BowlingGameを題材にしたKata

ボーリングゲームのスコア計算をおこなうロジックを作成する。
「初級」「中級」「上級」「超級」と徐々にSOLID原則を含む9ルールを適用していく。

## 仕様

| パターン | 説明 | スコア[点]
| ---- | ---- | ---- 
| Gutter game | 0*20 | 0
| All Ones | 1*20 | 20
| One Spare | 5,5,3,0*17 | 16
| One Strike | 10,3,4,0*16 | 24
| Perfect game | 10*12 | 300
| 10Frame Turkey | 0*18,10,10,10 | 30
| 10Frame Spare | 0*18,5,5,3 | 13

## 初級

- Coding Kataを20分以内にテスト駆動開発で実装できること
- マジックナンバー禁止
- 重複コード禁止

## 中級

- ①1つのメソッドでインデントは１階層まで(単一責務、抽象化レベル)
- ⑤名前を省略しない。命名は2つの単語まで(命名は難しいときはクラス分割のサイン)
- ⑥クラスは50行以内(すべてのエンティティを小さく保つ)

※制限時間は30分とする


## 上級

- ③すべてのプリミティブ型と文字列型はクラスでラップする(パラメータに意味を持たせる)
- ④1行につきドットは1つまで(デメテルの法則、再利用性の向上)
- ⑦1クラスのメンバ変数は2つまで(基本は1つに努める)
- ⑧ファーストクラスコレクションを使用する(コレクション変数を持つクラスのメンバはそれのみにする)

※制限時間はなし

## 超級

- ②else句を使わない(Strategyパターン、ポリモーフィズム)
- ⑨getter/setterプロパティの利用禁止(求めるな命じよ、データを持つクラスに仕事をさせる)