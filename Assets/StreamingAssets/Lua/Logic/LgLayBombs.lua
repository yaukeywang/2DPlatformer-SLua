--
-- Lay bombs class.
--
-- @filename  LgLayBombs.lua
-- @copyright Copyright (c) 2015 Yaukey/yaukeywang/WangYaoqi (yaukeywang@gmail.com) all rights reserved.
-- @license   The MIT License (MIT)
-- @author    Yaukey
-- @date      2015-09-03
--

local YwDeclare = YwDeclare
local YwClass = YwClass

local DLog = YwDebug.Log
local DLogWarn = YwDebug.LogWarning
local DLogError = YwDebug.LogError

local Input = Input

-- Register new class LgLayBombs.
local strClassName = "LgLayBombs"
local LgLayBombs = YwDeclare(strClassName, YwClass(strClassName))

-- Member variables.

-- The c# class object.
LgLayBombs.this = false

-- The transform.
LgLayBombs.transform = false

-- The c# gameObject.
LgLayBombs.gameObject = false

-- Whether or not a bomb has currently been laid.
LgLayBombs.m_bBombLaid = false

-- Heads up display of whether the player has a bomb or not. (GUITexture)
LgLayBombs.m_cBombHUD = false

-- Awake method.
function LgLayBombs:Awake()
    --print("LgLayBombs:Awake")

    -- Check variable.
    if (not self.this) or (not self.transform) or (not self.gameObject) then
        DLogError("Init error in LgLayBombs!")
        return
    end

    -- Setting up the reference.
    self.m_cBombHUD = GameObject.Find("ui_bombHUD"):GetComponent(GUITexture)
end

-- Update method.
function LgLayBombs:Update()
    --print("LgLayBombs:Update")

    local this = self.this

    -- If the bomb laying button is pressed, the bomb hasn't been laid and there's a bomb to lay...
    if Input.GetButtonDown("Fire2") and (not self.m_bBombLaid) and (this.m_bombCount > 0) then
        -- Decrement the number of bombs.
        this.m_bombCount = this.m_bombCount - 1

        -- Set bomb laid to true.
        self.m_bBombLaid = true

        -- Play the bomb laying sound.
        AudioSource.PlayClipAtPoint(this.m_bombsAway, self.transform.position)

        -- Instantiate the bomb prefab.
        GameObject.Instantiate(this.m_bomb, self.transform.position, self.transform.rotation)
    end

    -- The bomb heads up display should be enabled if the player has bombs, other it should be disabled.
    self.m_cBombHUD.enabled = this.m_bombCount > 0
end

-- Return this class.
return LgLayBombs
