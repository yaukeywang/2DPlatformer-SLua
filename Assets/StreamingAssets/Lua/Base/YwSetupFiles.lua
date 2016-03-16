--
-- Lua setup file, require all need files.
--
-- @filename  SetupFiles.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-05-28
--

-- Import library to global space.
import "UnityEngine"

-- require base library.
require "Base/YwGlobal"
require "Base/YwVersion"
require "Base/YwClass"
require "Base/YwRegister"
require "Base/YwRegisterObject"
require "Base/YwMonoBehaviour"
require "Base/YwConfigMng"
require "Base/YwDispatcher"
require "Base/YwMonoBehaviourUpdater"

-- Require Game module.
--require "Base/YwBehaviourExample"
require "Logic/LgBackgroundParallax"
require "Logic/LgBackgroundPropSpawner"
require "Logic/LgBomb"
require "Logic/LgBombPickup"
require "Logic/LgCameraFollow"
require "Logic/LgDestroyer"
require "Logic/LgEnemy"
require "Logic/LgFollowPlayer"
require "Logic/LgGun"
require "Logic/LgHealthPickup"
require "Logic/LgLayBombs"
require "Logic/LgPauser"
require "Logic/LgPickupSpawner"
require "Logic/LgPlayerControl"
require "Logic/LgPlayerHealth"
require "Logic/LgRemover"
require "Logic/LgRocket"
require "Logic/LgScore"
require "Logic/LgScoreShadow"
require "Logic/LgSetParticleSortingLayer"
require "Logic/LgSpawner"
