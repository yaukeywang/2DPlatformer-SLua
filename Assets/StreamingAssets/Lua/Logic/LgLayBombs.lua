--
-- Lay bombs class.
--
-- @filename  LgLayBombs.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-03
--

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

local Input = Input

-- Register new class LgLayBombs.
local strClassName = "LgLayBombs"
local LgLayBombs = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The parent class.
LgLayBombs.m_cParent = nil

-- The transform.
LgLayBombs.transform = false

-- The c# gameObject.
LgLayBombs.gameObject = false

-- Params from unity editor.
LgLayBombs.m_nBombCount = 0
LgLayBombs.m_cBombsAway = nil
LgLayBombs.m_cBomb = nil

-- Whether or not a bomb has currently been laid.
LgLayBombs.m_bBombLaid = false

-- Heads up display of whether the player has a bomb or not. (GUITexture)
LgLayBombs.m_cBombHUD = false

-- Constructor.
function LgLayBombs:ctor(cParent)
    --print("LgLayBombs:ctor")

    self.m_cParent = cParent
    self.gameObject = cParent.gameObject
    self.transform = cParent.gameObject.transform

    -- Setting up the reference.
    self.m_cBombHUD = GameObject.Find("ui_bombHUD"):GetComponent(GUITexture)
end

-- Destructor.
function LgLayBombs:dtor()
    --print("LgLayBombs:dtor")

    self.m_cParent = nil
    self.gameObject = nil
    self.transform = nil
    self.m_cBombHUD = nil
    self.m_cBombsAway = nil
    self.m_cBomb = nil
end

-- Update method.
function LgLayBombs:Update()
    --print("LgLayBombs:Update")

    -- If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
    if Input.GetButtonDown("Fire2") and (not self.m_bBombLaid) and (self.m_nBombCount > 0) then
        -- Decrement the number of bombs.
        self.m_nBombCount = self.m_nBombCount - 1

        -- Set bomb laid to true.
        self.m_bBombLaid = true

        -- Play the bomb laying sound.
        AudioSource.PlayClipAtPoint(self.m_cBombsAway, self.transform.position)

        -- Instantiate the bomb prefab.
        GameObject.Instantiate(self.m_cBomb, self.transform.position, self.transform.rotation)
    end

    -- The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
    self.m_cBombHUD.enabled = self.m_nBombCount > 0
end

-- On destroy method.
function LgLayBombs:OnDestroy()
    --print("LgLayBombs:OnDestroy")
    
    self.m_cParent = nil
    self.gameObject = nil
    self.transform = nil
    self.m_cBombHUD = nil
    self.m_cBombsAway = nil
    self.m_cBomb = nil
end

-- Return this class.
return LgLayBombs
