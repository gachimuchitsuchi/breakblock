# breakblock
氏名　土田崚資  
学校　室蘭工業大学院　情報電子工学系専攻　システム情報学コース　1年  
住所　北海道室蘭市高砂町5-20-5コーポ東園506  
連絡先　090-5983-0715  

## 自分紹介
学習した技術と期間(現在時点)
- Unity　１年半程度
- c++    　半年
- UE5   　２か月

# 成果物の概要  
タイトル　Elemental Break Block  

作成環境 　Unity3D　Editor Version 2021.3.22f1
作成期間　2023年4月 ~ 2023年5月　(1か月)
作成人数 　1人

## ゲーム内容  
原神の期間限定で実装されたブロック崩しのミニゲームをまねして作成しました。
バーとボール、ブロックに元素が付与されており、ボールとブロックが異なる元素を持ってぶつかると元素反応が起きます。元素反応はブロックを壊すのに役立つ効果を持っています。この元素反応を駆使しながらすべてのブロックを破壊するとゲームクリアです。


![Page3Image1](https://github.com/gachimuchitsuchi/breakblock/assets/101007932/34967c52-dfa1-4a1c-87a9-a91b32d54d79)  

図１　雷元素ボール　×　水元素ブロック　の元素反応（感電）  
![Page3Image2](https://github.com/gachimuchitsuchi/breakblock/assets/101007932/6ad7ba5d-dfd9-4be7-9b4c-ba2a9f99e52d)  

図２　炎元素ボール　×　雷元素ブロック　の元素反応（過負荷）

## 良い実装ができたと思う点
凍結処理
	水元素のブロックに氷元素のボールをぶつけると当たった水元素ブロックと連結しているブロックがすべて凍結する。
	この処理を実装に深さ優先探索を使用した。
	競技プログラミングで勉強したことがゲームで初めて活用できたのでとても嬉しかった。

実装場所　Script->BreakBlockScene->Manager->ElemReacManager 141行目から
```
cpp
//凍結反応
    public void Frozen(GameObject block)
    {
        Block b = block.GetComponent<Block>();

        //2次元配列nowMapの配置位置を取得
        int x = b.row;
        int y = b.col;



        block.GetComponent<Renderer>().material = frozenMaterial;
        //ブロックの子のマテリアルをfrozenに変更
        ChangeChildrenColor(block);



        b.isFrozen = true;
        b.blockElement = Elements.None;



        //深さ優先探索
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                //x方向にdx,y方向にdy移動した場所を(nx,ny)とする
                int nx = x + dx;
                int ny = y + dy;

                //nx,nyがブロックマップの範囲内かどうか
                if (0 <= nx && nx < StageMapInfo.STAGE_ROW && 0 <= ny && ny < StageMapInfo.STAGE_COL)
                {
                    //nowMap[x][y]がnullでなく水ブロックかどうか
                    if (BlockMap.nowMap[nx, ny] != null && BlockMap.nowMap[nx, ny].GetComponent<Block>().blockElement == Elements.Hydro)
                    {
                        Frozen(BlockMap.nowMap[nx, ny]);
                    }
                }
            }
        }
    }



    public void BreakFrozen(GameObject block)
    {
        Block b = block.GetComponent<Block>();
        int x = b.row;
        int y = b.col;



        BlockMap.nowMap[x, y] = null;
        Destroy(block);



        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                //x方向にdx,y方向にdy移動した場所を(nx,ny)とする
                int nx = x + dx;
                int ny = y + dy;

                //nx,nyがブロックマップの範囲内かどうかとnowMap[x][y]が凍結ブロックかどうかを判定
                if (0 <= nx && nx < StageMapInfo.STAGE_ROW && 0 <= ny && ny < StageMapInfo.STAGE_COL)
                {
                    if (BlockMap.nowMap[nx, ny] != null && BlockMap.nowMap[nx, ny].GetComponent<Block>().isFrozen == true)
                    {
                        BreakFrozen(BlockMap.nowMap[nx, ny]);
                    }
                }
            }
        }
    }
```

## うまくいかなかった点  
反射・貫通処理  
元素反応の１つの超伝導反応(氷元素×雷元素)の効果がブロックとぶつかったとき反射せずに貫通し、ブロックを破壊する効果があります。

最初は物理マテリアルを用いた反射処理を実装しました。しかし、物理マテリアルだと貫通処理を適用する際にボールが反射してから貫通効果を得るため、意図した挙動にならなかったので使用を断念しました。

次にボールとブロックが重なったときに、超伝導の反応以外であれば反射するという方法を実装しました。
この方針だとボールの速度がある程度早くなると反射しないということが起こりました。
これ以上の方針は思いつかなかったので、この方針を取りました。

また、反射処理の実装でキューブの面の法線ベクトルの取得の仕方がわからなかったので、planeをキューブの子オブジェクトに置き、そのplaneから法線ベクトルを取得して反射処理を実装しました。
このやり方は非常によくないやり方だとわかっていましたが、当時はこれ以上の技術力はありませんでした。

このやり方のせいでボールがブロックの角に重なった際に、２回あたり判定がおき、変な方向にボールが跳ね返ってしまうことを確認しています。

実装場所　Script->BreakBlockScene->Ball->Ball　74行目から146行目
```
cpp

private void OnTriggerEnter(Collider other)
    {
        if (rb == null) return;
        //法線ベクトルの取得
        Vector3 inNormal = other.transform.up;




        //ブロックと当たったとき
        if (other.gameObject.CompareTag("BlockPlane"))
        {
            //親オブジェクトの取得
            GameObject parent = other.transform.parent.gameObject;

            if (parent.GetComponent<Block>().isFrozen)
            {
                ElemReacManager.instance.BreakFrozen(parent);
            }



            //超電導モードの時は破壊処理のみ
            if (isSuperMode)
            {
                Destroy(parent);
                return;
            }
            else
            {
                //ボールとブロックの元素反応を取得
                ElementalReactions reaction = blockElementalReaction[(int)ballElement, (int)parent.GetComponent<Block>().blockElement];
                //元素反応処理
                ElemReacManager.instance.ElementalReaction(this.gameObject, parent.gameObject, reaction);
                //超伝導反応(貫通処理)以外反射する
                if(reaction != ElementalReactions.Superconduct)
                {
                    BallReflector(inNormal);
                }
            }
        }



        //壁と当たったとき
        if (other.gameObject.CompareTag("WallPlane"))
        {
            if (isSuperMode)
            {
                isSuperMode = false;
                ChangeBallElement(Elements.Electro);
                ShowParticle();
            }
            BallReflector(inNormal);
        }




        //プレイヤーと当たったとき
        if (other.gameObject.CompareTag("PlayerPlane"))
        {
            if (isSuperMode)
            {
                isSuperMode = false;
            }

            if(Player.instance.GetPlayerElem() == Elements.Hydro && ballElement == Elements.Cryo)
            {
                GameManager.instance.CreateBall(Elements.Cryo);

                Destroy(this.gameObject);
            }



            ChangeBallElement(Player.instance.GetPlayerElem());
            ShowParticle();
            //Debug.Log(ballElement);
            BallReflector(inNormal);
        }
    }
```

## うまくいかなかった点について今後自身の課題    
面と球のあたり判定がどういう数学で成り立っているのか知識がなかった。
数学の知識が足りていないことを痛感したので数学の勉強をする。(3D数学)  

meshからキューブの法線ベクトルを取得することができることを知った。
もっとUnityのオブジェクトなどが当たり前に持っているコンポーネントの知識を見つける  

## 構成するファイル/ディレクトリのリストと簡単な解説  
-Assets
- DLAssets　ダウンロードした無料アセット
- Editor
- Fonts　使用したFont
- Materials　ゲーム内で使用したマテリアル
- Prefabs　ゲーム内で作成したプレファブ
- Scenes　ゲーム内のシーン
- Script　ソースコード
	- BreakBlockSene　ブロック崩しを作るのに使用したソースコード
	- MenuScene　メニュー画面を作るのに使用したソースコード

## 参考文献  
blockmap:
https://qiita.com/yaju/items/5b54016c6574bc84c41d

割れたブロック(スプライト、スプライトマスク):
//ピクセル説明
https://shibuya24.info/entry/unity-unit
//Cubeにひびが徐々に入っているように見せたい
https://teratail.com/questions/145642

//貫通処理
//反射ベクトルの基礎
https://qiita.com/edo_m18/items/b145f2f5d2d05f0f29c9
//unityで反射ベクトルを求める
https://nekojara.city/unity-object-direction

//Skill
https://3dunity.org/game-create-lesson/srpg-game/debuff-heal-magic/

//ゲームクリア画面実装方法
https://tech.pjin.jp/blog/2021/12/08/unity-rungame-16

//オブジェクトが消えたときのイベントを受け取る方法
https://watablog.tech/2020/09/01/post-1876/#i

//CanVas解説
https://qiita.com/4_mio_11/items/a65e929ae64a018a24b0
//配置に関する文献
https://qiita.com/akira581/items/585c917aa8650768888e

//画面サイズ対応
https://www.create-forever.games/unity-resolution-game/

//Font使用方法
使用Font M+
https://mychma.com/unity-textmeshpro-japanese/1239/#index_id6

//空のアセットの使い方
https://illust55.com/3233/#toc4

//カーソルターゲット　ボタンにカーソルが重なったときの処理
https://teratail.com/questions/360571

//戻るボタン
https://kotonohaworks.com/free-icons/modoru/

//外枠
https://konsuki.com/7033/#toc1

//パーティクル説明
https://styly.cc/ja/tips/unity-introduction-particle/#i
