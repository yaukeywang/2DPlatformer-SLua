![Screenshot of example](Screenshot.jpg)

## 简介
The SLua version of Unity showcase demo : [2D Platformer](https://www.assetstore.unity3d.com/cn/#!/content/11228).

SLua 版本的 Unity 官方 2D 演示 Demo : 2D Platformer

2D Platformer 原始版本下载地址：https://www.assetstore.unity3d.com/cn/#!/content/11228

SLua 地址：http://www.slua.net/, https://github.com/pangweiwei/slua

## 特点
主要逻辑代码全部在 SLua 框架下的 lua 代码中，采用类似于 Monobehavior 的面向对象方式，全面禁止直接定义全局变量，必须使用 YwDeclare 来显示定义全局变量。

不需要继承自 Monobehavior 的代码可以全部以面向对象的方式写在 lua 中，由挂接在 YwDispatcher 上的各种继承自 YwRegisterObject 的对象来驱动和展开逻辑。

## 使用
请打开项目后直接打开 Assets/Game/Scenes/LevelStart.unity 来开始游戏，不要运行 Level.unity。

## 注意
本项目需要运行在 Unity 5.x 的版本中。

本项目所采用的 SLua 版本为自行编译的版本，具体为：SLua 0.8.3 + lua 5.3.1 + lpeg 0.12.2 + sproto。

由于本项目代码基于 lua 5.3，如果需要将所有代码放置在默认的 SLua 框架中，需要手动修改使用了位移操作符(<<, >>)的地方。

## 版权和授权协议
本项目中 SLua 基础代码及框架版权归 SLua 原作者所有。

本项目中所有除代码外的资源（包括但不限于音效，贴图，动画，模型等）版权归 [Unity](http://www.unity3d.com/) 所有。

本项目中 Lpeg 版权归原作者所有

本项目中 sproto 版权归原作者所有

本项目中除以上几项以外的代码（包括部分基础代码和所有移植的游戏代码）版权归该项目实际贡献者所有，且基于 MIT 协议的方式授权，具体内容请参见文件：LICENSE。
