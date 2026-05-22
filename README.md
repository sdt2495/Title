#　概要　落ちる地面に気を付けて何秒でクリアできるか！！
# 操作方法
  　WASD　spaceキー
   #工夫した点
    地面の揺れ
[README_SAMPLE.md](https://github.com/user-attachments/files/28131518/README_SAMPLE.md)
# Unity Game A Week - Season1 Week1

# Rolling Goal

Unity 6 + URP + 新 Input System を使用した
3D物理アクションゲームのサンプルです。

専門学校向け Unityプログラム授業
「Unity Game A Week」Week1 の教材として制作しています。

---

# Screenshots

| Title   | Game    | Result  |
| ------- | ------- | ------- |
| （画像を貼る） | （画像を貼る） | （画像を貼る） |

---

# ゲーム概要

プレイヤーはボールを操作し、
制限時間内にゴールを目指します。

物理演算（Rigidbody）を利用した
Unity基礎向けの3Dアクションゲームです。

---

# 基本ルール

* WASD / 左スティックで移動
* Space / Aボタンでジャンプ
* ゴールに触れるとクリア
* ステージ外に落下するとゲームオーバー
* 制限時間が0になるとゲームオーバー
* Result画面からRetry可能

---

# 🛠 使用環境

* Unity 6
* URP
* 新 Input System
* TextMeshPro
* 3D Physics（Rigidbody）

---

# シーン構成

| Scene  | 内容     |
| ------ | ------ |
| Init   | 初期化    |
| Title  | タイトル画面 |
| Game   | ゲーム本編  |
| Result | 結果画面   |

---

# 主なスクリプト

| Script                  | 内容          |
| ----------------------- | ----------- |
| GameManager.cs          | シーン遷移管理     |
| GameFlowController.cs   | Gameシーン進行管理 |
| PlayerBallController.cs | ボール操作       |
| GoalTrigger.cs          | ゴール判定       |
| FallDetector.cs         | 落下判定        |
| GameTimer.cs            | 制限時間        |
| GameHUD.cs              | HUD表示       |
| ResultUI.cs             | Result画面UI  |
| SimpleMovingPlatform.cs | 動く床         |
| Checkpoint.cs           | チェックポイント    |

---

# プレイヤー仕様

## 移動

Rigidbody に AddForce を使用して移動。

```csharp
rb.AddForce(moveDirection * moveForce, ForceMode.Force);
```

---

## ジャンプ

接地判定後にImpulseでジャンプ。

```csharp
rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
```

---

## 接地判定

```csharp
Physics.CheckSphere()
```

を利用。

---

# ゴール判定

Goalオブジェクトに Trigger Collider を設定。

```text
Is Trigger : ON
```

Playerが接触すると：

```csharp
gameFlowController.ClearGame();
```

---
