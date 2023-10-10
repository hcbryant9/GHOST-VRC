■概要
オブジェクトの中に入ったときにSkyboxを切り替えるシェーダーです。
Vケットなどで見かけた「ブースの中に入ると別空間に変わる」というものを自分なりに作ってテンプレート化したものです。
Stencilを利用することでサンプル動画のようにSkyboxだけではなくオブジェクトにも適用可能です。

本シェーダーはSkyboxを貼り付けた巨大なCubeを表示・非表示することで実装されています。
Cubeのサイズを大きくすると奥行き感は増しますが、他のオブジェクトに干渉しないように注意してください。

仕組みについては下記サイトを参照
凹みTips - HoloLens で向こう側が見える窓を動的に追加してみる
http://tips.hecomi.com/entry/2017/02/18/190949

■各シェーダーの説明
(1)StencilSkyB
　表示したいSkyboxを設定するシェーダー

　・Mask
　　　各シェーダーで共通の値を指定(初期値=1)
　・Comp
　　　Equalを指定
　　　NotEqualを指定するとSkyboxが表示されるようになるのでSkyboxのサイズを変更する際に使用する
　・Skybox Size
　　　Skyboxのサイズ
　・Cubemap
　　　表示したいSkybox(Cubemap)を指定

(2)StencilWindow
　このシェーダーを適用したオブジェクト内部にカメラが入ると(1)で指定したSkyboxが表示される

　・Mask
　　　各シェーダーで共通の値を指定(初期値=1)
　・Enabled Rendering Distance
　　　Rendering Distanceの有効・無効
　　　無効にした場合このシェーダーを適用したオブジェクトが常に表示されるようになり、
　　　このオブジェクトを通して別空間を覗くことができる窓シェーダーのような表現になる
  ・Rendering Distance
　　　このシェーダーを適用したオブジェクトとカメラの距離がRendering Distance以下の場合、Skyboxを切り替える
　　　オブジェクトの中心からの距離となっているため、例えばサイズが1x1x1のCubeにRendering Distance=0.5を指定すると
　　　Cube内部にカメラが入った時にSkyboxが切り替わるようになる

(3)StencilObject
　Skybox以外で表示を切り替えたいオブジェクトに適用
　※Stencil部分を移植すれば他のシェーダーにも適用可能
　
　・Mask
　　　各シェーダーで共通の値を指定(初期値=1)
　・Comp
　　　通常時に表示したいオブジェクトにはNotEqual、Skybox切り替えと共に表示したいものにはEqualを指定
　・Main Color
　　　オブジェクトの色