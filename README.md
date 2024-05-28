
# ゲームの概要  
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


## 構成するファイル/ディレクトリのリストと簡単な解説  
- Assets
	- DLAssets　ダウンロードした無料アセット
	- Editor
	- Fonts　使用したFont
	- Materials　ゲーム内で使用したマテリアル
	- Prefabs　作成したプレファブ
	- Scenes　ゲーム内のシーン
	- Script　ソースコード
		- BreakBlockScene　ブロック崩しを作るのに使用したソースコード
		- MenuScene　メニュー画面を作るのに使用したソースコード
## 良い実装ができた点
- 凍結処理
⽔元素のブロックに氷元素のボールをぶつけると当たった⽔元素ブロックと連結しているブロックが
すべて凍結する。 この処理を実装に深さ優先探索を使⽤した。 競技プログラミングで勉強したことが
ゲームで初めて活⽤できたので嬉しかった。
実装場所 Script->BreakBlockScene->Manager->ElemReacManager 141⾏⽬から
```
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
